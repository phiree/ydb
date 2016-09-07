using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.Presenter;
using FizzWare.NBuilder;
using Dianzhu.Model;
namespace Dianzhu.Test.DZCSClientTest
{
   
    [TestFixture]
    public class PCustomerListTest
    {
        IViewIdentityList viewCustomerList;
        InstantMessage iIM;
        IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        IViewOrderHistory viewOrderHistory;
        IViewSearchResult viewSearchResult;
        IDAL.IDALReceptionChat dalReceptionChat;
        IDAL.IDALReceptionStatus dalReceptionStaus;
        IDAL.IDALReceptionStatusArchieve dalReceptionStatusArchieve;
        BLL.IBLLServiceOrder bllServiceOrder;
        Dianzhu.CSClient.LocalStorage.LocalChatManager lcm;
        Dianzhu.CSClient.LocalStorage.LocalHistoryOrderManager lhom;
        [SetUp]
        public void setup()
        {
            viewCustomerList = MockRepository.GenerateStub<IViewIdentityList>();
            iIM = MockRepository.GenerateStub<InstantMessage>();
            viewChatList = MockRepository.GenerateStub<IViewChatList>();
            viewChatSend = MockRepository.GenerateStub<IViewChatSend>();
            viewOrderHistory = MockRepository.GenerateStub<IViewOrderHistory>();
            viewSearchResult = MockRepository.GenerateStub<IViewSearchResult>();
            dalReceptionChat = MockRepository.GenerateStub<IDAL.IDALReceptionChat>();
            dalReceptionStatusArchieve = MockRepository.GenerateStub<IDAL.IDALReceptionStatusArchieve>();
            dalReceptionStaus = MockRepository.GenerateStub<IDAL.IDALReceptionStatus>(); 
             bllServiceOrder = MockRepository.GenerateStub<BLL.IBLLServiceOrder>();
            lcm = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalChatManager>();
            lhom = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalHistoryOrderManager>();
        }
        [Test]
        public void ReceiveMessageTest()
        {
            IList<DZMembership> customers = Builder<DZMembership>.CreateListOfSize(10).Build();
            string[] idList = { "17b2007f-0267-4224-8d5a-cbaafa7ed1fc", "153ef5fa-600a-4a32-aefb-27c5e5fa5a50", "4a3727f6-ec21-42a8-ba84-70ba9db06354",
                                "20364ea5-c19c-409d-8b61-cb1905fc68d8", "684db3a7-6c2c-44bf-a50a-5ceb1b904a26", "8f506545-de72-4fdb-bbaa-85a8709ae63f",
                                "eb524d39-6800-4477-9b03-6c1347f766b2", "b8d66f6e-45c7-444a-a055-96429049eac0", "e858b491-d295-4978-9b83-4571cc05c030",
                                "96edcb84-c49e-43ea-ade0-f78d071bf1cd"};

            int i = 0;
            foreach (DZMembership item in customers)
            {
                item.Id = Guid.Parse(idList[i]);
                i++;
            }

            //一号用户的两个订单
            ServiceOrder order11 = ServiceOrderFactory.CreateDraft(customers[0], customers[1],string.Empty);
            order11.Id = Guid.Parse("91280460-80b4-4509-84f5-e323fceff9c8");
            ServiceOrder order12 = ServiceOrderFactory.CreateDraft(customers[0], customers[1],string.Empty);
            order12.Id = Guid.Parse("6ecc4ed8-f53d-4159-9a67-820f15d1420f");

            //二号用户的一个订单
            ServiceOrder order21 = ServiceOrderFactory.CreateDraft(customers[0], customers[2],string.Empty);
            order21.Id = Guid.Parse("e36d19a0-6c69-4810-8507-829c8f670863");

            //三号用户的一个订单
            ServiceOrder order31 = ServiceOrderFactory.CreateDraft(customers[0], customers[3],string.Empty);
            order31.Id = Guid.Parse("f38348af-48db-4f93-bc92-1a610207b245");

            IList<ReceptionChat> chats = Builder<ReceptionChat>.CreateListOfSize(6)
                .TheFirst(1).With(x=>x.From=order11.Customer). And(x=>x.ServiceOrder= order11)//一号用户订单
                .TheNext(1).With(x => x.From = order11.Customer).And(x => x.ServiceOrder = order11)//一号用户同一订单
                .TheNext(1).With(x => x.From = order12.Customer).And(x => x.ServiceOrder = order12)//一号用户新订单
                .TheNext(1).With(x => x.From = order21.Customer).And(x => x.ServiceOrder =order21)//二号用户的新订单
                .TheNext(1).With(x => x.From = order11.Customer).And(x => x.ServiceOrder = order11)//一号用户的已有订单
                .TheNext(1).With(x => x.From = order31.Customer).And(x => x.ServiceOrder = order31)//三号用户的新订单
                .Build();
            PIdentityList pCustomerList = new PIdentityList(viewCustomerList, viewChatList,iIM, dalReceptionChat, viewChatSend, bllServiceOrder, viewOrderHistory, dalReceptionStaus, viewSearchResult, dalReceptionStatusArchieve,lcm,lhom);
            IdentityTypeOfOrder identityTypeOfOrder;


            //发送第一条消息
            IdentityManager.UpdateIdentityList(chats[0].ServiceOrder, out identityTypeOfOrder);//一号用户订单
            pCustomerList.ReceivedMessage(chats[0], identityTypeOfOrder);
            pCustomerList.IView_IdentityClick(chats[0].ServiceOrder);
            Assert.AreEqual(order11.Id, IdentityManager.CurrentIdentity.Id);
            Console.WriteLine("order11.Id:" + order11.Id);
            Console.WriteLine("value:" + IdentityManager.CurrentIdentityList.Keys.Select(x=>x.Id).Contains(order11.Id));
            //Assert.AreEqual(1, IdentityManager.CurrentIdentityList.Count);
            // Assert.AreEqual(order11, IdentityManager.CurrentIdentityList.Keys.);


            IdentityManager.UpdateIdentityList(chats[1].ServiceOrder, out identityTypeOfOrder);//一号用户同一订单
            pCustomerList.ReceivedMessage(chats[1], identityTypeOfOrder);
            pCustomerList.IView_IdentityClick(chats[1].ServiceOrder);
            Assert.AreEqual(order11.Id, IdentityManager.CurrentIdentity.Id);
            Console.WriteLine("order11.Id:" + order11.Id);
            Console.WriteLine("value:" + IdentityManager.CurrentIdentityList.Keys.Select(x => x.Id).Contains(order11.Id));
            //Assert.AreEqual(1, IdentityManager.CurrentIdentityList.Count);
            //Assert.AreEqual(order12, IdentityManager.CurrentIdentityList[0]);

            IdentityManager.UpdateIdentityList(chats[2].ServiceOrder, out identityTypeOfOrder);//一号用户新订单
            pCustomerList.ReceivedMessage(chats[2], identityTypeOfOrder);
            pCustomerList.IView_IdentityClick(chats[2].ServiceOrder);
            Assert.AreEqual(order12.Id, IdentityManager.CurrentIdentity.Id);
            Console.WriteLine("order12.Id:" + order12.Id);
            Console.WriteLine("value:" + IdentityManager.CurrentIdentityList.Keys.Select(x => x.Id).Contains(order12.Id));
            //Assert.AreEqual(1, IdentityManager.CurrentIdentityList.Count);
            //Assert.AreEqual(order12.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(0).Id);


            IdentityManager.UpdateIdentityList(chats[3].ServiceOrder, out identityTypeOfOrder);//二号用户的新订单
            pCustomerList.ReceivedMessage(chats[3],identityTypeOfOrder);
            pCustomerList.IView_IdentityClick(chats[3].ServiceOrder);
            Assert.AreEqual(order21.Id, IdentityManager.CurrentIdentity.Id);
            Console.WriteLine("order21.Id:" + order21.Id);
            Console.WriteLine("value:" + IdentityManager.CurrentIdentityList.Keys.Select(x => x.Id).Contains(order21.Id));
            //Assert.AreEqual(2, IdentityManager.CurrentIdentityList.Count);
            //Assert.AreEqual(order21.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(0).Id);
            //Assert.AreEqual(order12.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(1).Id);

            IdentityManager.UpdateIdentityList(chats[4].ServiceOrder, out identityTypeOfOrder);//一号用户的已有订单          
            pCustomerList.ReceivedMessage(chats[4], identityTypeOfOrder);
            pCustomerList.IView_IdentityClick(chats[4].ServiceOrder);
            Assert.AreEqual(order11.Id, IdentityManager.CurrentIdentity.Id);
            Console.WriteLine("order11.Id:" + order11.Id);
            Console.WriteLine("value:" + IdentityManager.CurrentIdentityList.Keys.Select(x => x.Id).Contains(order11.Id));
            //Assert.AreEqual(2, IdentityManager.CurrentIdentityList.Count);
            //Assert.AreEqual(order21.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(0).Id);
            //Assert.AreEqual(order11.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(1).Id);

            IdentityManager.UpdateIdentityList(chats[5].ServiceOrder, out identityTypeOfOrder);//三号用户的新订单
            pCustomerList.ReceivedMessage(chats[5], identityTypeOfOrder);
            pCustomerList.IView_IdentityClick(chats[5].ServiceOrder);
            Assert.AreEqual(order31.Id, IdentityManager.CurrentIdentity.Id);
            Console.WriteLine("order31.Id:" + order31.Id);
            Console.WriteLine("value:" + IdentityManager.CurrentIdentityList.Keys.Select(x => x.Id).Contains(order31.Id));
            //Assert.AreEqual(3, IdentityManager.CurrentIdentityList.Count);
            //Assert.AreEqual(order31.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(0).Id);
            //Assert.AreEqual(order21.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(1).Id);
            //Assert.AreEqual(order11.Id, IdentityManager.CurrentIdentityList.Keys.ElementAt(2).Id);

            //Assert.AreEqual(null, pCustomerList.CurrentCustomer);
            //var members = Builder<DZMembership>.CreateListOfSize(5).Build();
            //ReceptionChat chat = Builder<ReceptionChat>
            //    .CreateNew()
            //    .With(x=>x.From=members[1])
            //    .With(x=>x.To=members[0])
            //    .Build();
            //pCustomerList.IIm_IMReceivedMessage(chat);
            //Assert.AreEqual(1, pCustomerList.CustomerList.Count);

            //Assert.AreEqual(members[1], pCustomerList.CurrentCustomer);


            //ReceptionChat chat2 = Builder<ReceptionChat>
            //    .CreateNew()
            //    .With(x => x.From = members[2])
            //    .With(x => x.To = members[0])
            //    .Build();

            //pCustomerList.IIm_IMReceivedMessage(chat2);
            //Assert.AreEqual(2, pCustomerList.CustomerList.Count);


            //pCustomerList.IIm_IMReceivedMessage(chat);
            //Assert.AreEqual(2, pCustomerList.CustomerList.Count);
            //Assert.AreEqual(members[1], pCustomerList.CurrentCustomer);



        }

        [Test]
        public void IView_CustomerClickTest()
        {
            //DAL.DALReception dalReception = MockRepository.GenerateStub<DAL.DALReception>(string.Empty);
            //int rowCount;
            //IList<ReceptionChat> chatList = Builder<ReceptionChat>.CreateListOfSize(10).Build();
            ////dalReception.Stub(x => x.GetReceptionChatList(new DZMembership(), new DZMembership(), Guid.Empty, DateTime.MaxValue, DateTime.MaxValue, 0, 0, Model.Enums.enum_ChatTarget.all, out rowCount))
            ////    .Return(chatList).WhenCalled;
            ////Rhino.Mocks.Expect.Call(dalReception.GetReceptionChatList(new DZMembership(), new DZMembership(), Guid.Empty, DateTime.MaxValue, DateTime.MaxValue, 0, 0, Model.Enums.enum_ChatTarget.all, out rowCount))
            ////    .Return(chatList);
            //DZMembership m = Builder<DZMembership>.CreateNew().Build();
            //dalReception.Stub(x =>
            //                     x.GetReceptionChatList(
            //                                       m, null, new Guid(), DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20,Dianzhu.Model.Enums. enum_ChatTarget.all, out rowCount
            //                                    )
            //             ).IgnoreArguments().Return(chatList);
            //PIdentityList pCustomerList = new PIdentityList(viewCustomerList, iIM, viewChatList,dalReception);
            //viewCustomerList.Raise(x=>x.IdentityClick+=null, m);
            //Assert.AreEqual(m, pCustomerList.CurrentCustomer);
            //Assert.AreEqual(10, viewChatList.ChatList.Count);
        }

    } 
}
