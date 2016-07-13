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
        IViewOrder viewOrder;
        IViewSearchResult viewSearchResult;
        IDAL.IDALReceptionChat dalReceptionChat;
        IDAL.IDALReceptionStatus dalReceptionStaus;
        BLL.IBLLServiceOrder bllServiceOrder;
        [SetUp]
        public void setup()
        {
            viewCustomerList = MockRepository.GenerateStub<IViewIdentityList>();
            iIM = MockRepository.GenerateStub<InstantMessage>();
            viewChatList = MockRepository.GenerateStub<IViewChatList>();
            viewChatSend = MockRepository.GenerateStub<IViewChatSend>();
            viewOrderHistory = MockRepository.GenerateStub<IViewOrderHistory>();
            viewOrder =MockRepository.GenerateStub<IViewOrder>();
            viewSearchResult = MockRepository.GenerateStub<IViewSearchResult>();
            dalReceptionChat = MockRepository.GenerateStub<IDAL.IDALReceptionChat>();
            dalReceptionStaus = MockRepository.GenerateStub<IDAL.IDALReceptionStatus>();
            bllServiceOrder = MockRepository.GenerateStub<BLL.IBLLServiceOrder>();
        }
        [Test]
        public void ReceiveMessageTest()
        {
            IList<DZMembership> customers = Builder<DZMembership>.CreateListOfSize(10).Build();

            //同一个用户的两个订单
            ServiceOrder order11 = ServiceOrderFactory.CreateDraft(customers[0], customers[1]);
            ServiceOrder order12 = ServiceOrderFactory.CreateDraft(customers[0], customers[1]);

            //另一个用户的一个订单
            ServiceOrder order21 = ServiceOrderFactory.CreateDraft(customers[0], customers[2]);
           
            IList<ReceptionChat> chats = Builder<ReceptionChat>.CreateListOfSize(5)
                .TheFirst(1).With(x=>x.From=order11.Customer). And(x=>x.ServiceOrder= order11)//新用户订单
                .TheNext(1).With(x => x.From = order11.Customer).And(x => x.ServiceOrder = order11)//同一订单
                .TheNext(1).With(x => x.From = order12.Customer).And(x => x.ServiceOrder = order12)//同一用户，新订单
                .TheNext(1).With(x => x.From = order21.Customer).And(x => x.ServiceOrder =order21)//不同用户，新订单
                .TheNext(1).With(x => x.From = order11.Customer).And(x => x.ServiceOrder = order11)//已有用户的订单        
                .Build();
            PIdentityList pCustomerList = new PIdentityList(viewCustomerList, viewChatList,viewOrder,iIM, dalReceptionChat, viewChatSend, bllServiceOrder, viewOrderHistory, dalReceptionStaus, viewSearchResult);
            IdentityTypeOfOrder identityTypeOfOrder;
            IdentityManager.UpdateIdentityList(order11, out identityTypeOfOrder);

            //发送第一条消息
            pCustomerList.ReceivedMessage(chats[0], identityTypeOfOrder);
            Assert.AreEqual(order11, IdentityManager.CurrentIdentity);
            Assert.AreEqual(1, IdentityManager.CurrentIdentityList.Count);
            // Assert.AreEqual(order11, IdentityManager.CurrentIdentityList.Keys.);


            IdentityManager.UpdateIdentityList(order12, out identityTypeOfOrder);

            pCustomerList.ReceivedMessage(chats[2], identityTypeOfOrder);
            Assert.AreEqual(order12, IdentityManager.CurrentIdentity);
            Assert.AreEqual(1, IdentityManager.CurrentIdentityList.Count);
            //Assert.AreEqual(order12, IdentityManager.CurrentIdentityList[0]);

            pCustomerList.ReceivedMessage(chats[3], identityTypeOfOrder);
            Assert.AreEqual(order12, IdentityManager.CurrentIdentity);
            Assert.AreEqual(2, IdentityManager.CurrentIdentityList.Count);
            Assert.AreEqual(order12, IdentityManager.CurrentIdentityList.Keys.ElementAt(0));
            Assert.AreEqual(order21, IdentityManager.CurrentIdentityList.Keys.ElementAt(1));


            IdentityManager.UpdateIdentityList(order21, out identityTypeOfOrder);


            pCustomerList.ReceivedMessage(chats[4],identityTypeOfOrder);
            Assert.AreEqual(order11, IdentityManager.CurrentIdentity);
            Assert.AreEqual(2, IdentityManager.CurrentIdentityList.Count);
            Assert.AreEqual(order21, IdentityManager.CurrentIdentityList.Keys.ElementAt(0));
            Assert.AreEqual(order11, IdentityManager.CurrentIdentityList.Keys.ElementAt(1));

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
