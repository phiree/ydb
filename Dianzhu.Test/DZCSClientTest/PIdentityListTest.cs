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
    public class PIdentityListTest
    {
        IViewIdentityList viewIdentityList;
        InstantMessage iIM;
        IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        IDAL.IDALReceptionChat dalReceptionChat;
        BLL.BLLServiceOrder bllServiceOrder;
        IViewOrderHistory viewOrderHistory;
        IDAL.IDALReceptionStatus dalReceptionStatus;
        IViewSearchResult viewSearchResult;
        IDAL.IDALReceptionStatusArchieve dalReceptionStatusArchieve;

        PIdentityList pIdentityList;
        IList<DZMembership> customerList;
        IList<DZMembership> csList;
        IList<ServiceOrder> orderList;
        Dianzhu.CSClient.LocalStorage.LocalChatManager lcm;
        Dianzhu.CSClient.LocalStorage.LocalHistoryOrderManager lhom;
        Dianzhu.CSClient.LocalStorage.LocalUIDataManager luid;

        [SetUp]
        public void setup()
        {
            viewIdentityList = MockRepository.GenerateStub<IViewIdentityList>();
            iIM = MockRepository.GenerateStub<InstantMessage>();
            viewChatList = MockRepository.GenerateStub<IViewChatList>();
            viewChatSend = MockRepository.GenerateStub<IViewChatSend>();
            dalReceptionChat = MockRepository.GenerateStub<IDAL.IDALReceptionChat>();
            bllServiceOrder = MockRepository.GenerateStub<BLL.BLLServiceOrder>();
            viewOrderHistory = MockRepository.GenerateStub<IViewOrderHistory>();
            dalReceptionStatus = MockRepository.GenerateStub<IDAL.IDALReceptionStatus>();
            viewSearchResult = MockRepository.GenerateStub<IViewSearchResult>();
            dalReceptionStatusArchieve = MockRepository.GenerateStub<IDAL.IDALReceptionStatusArchieve>();
            lcm = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalChatManager>();
            lhom = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalHistoryOrderManager>();
            luid = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalUIDataManager>();

            pIdentityList = new PIdentityList(viewIdentityList, viewChatList, iIM, dalReceptionChat, viewChatSend, bllServiceOrder, viewOrderHistory, dalReceptionStatus, viewSearchResult, dalReceptionStatusArchieve,lcm,lhom, luid);

            string[] customerIdList = { "17b2007f-0267-4224-8d5a-cbaafa7ed1fc", "153ef5fa-600a-4a32-aefb-27c5e5fa5a50", "4a3727f6-ec21-42a8-ba84-70ba9db06354" };
            string[] csIdList = { "20364ea5-c19c-409d-8b61-cb1905fc68d8", "684db3a7-6c2c-44bf-a50a-5ceb1b904a26", "8f506545-de72-4fdb-bbaa-85a8709ae63f" };
            string[] orderIdList = { "eb524d39-6800-4477-9b03-6c1347f766b2", "b8d66f6e-45c7-444a-a055-96429049eac0", "e858b491-d295-4978-9b83-4571cc05c030" };

            customerList = Builder<DZMembership>.CreateListOfSize(3)
                    .TheFirst(1).With(x => x.Id = new Guid(customerIdList[0]))
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                    .TheNext(1).With(x => x.Id = new Guid(customerIdList[1]))
                                .With(x => x.UserName = "user2")
                                .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                    .TheLast(1).With(x => x.Id = new Guid(customerIdList[2]))
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                    .Build();

            csList = Builder<DZMembership>.CreateListOfSize(3)
                    .TheFirst(1).With(x => x.Id = new Guid(csIdList[0]))
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                    .TheNext(1).With(x => x.Id = new Guid(csIdList[1]))
                                .With(x => x.UserName = "user2")
                                .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                    .TheLast(1).With(x => x.Id = new Guid(csIdList[2]))
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                    .Build();

            orderList = Builder<ServiceOrder>.CreateListOfSize(3)
                    .TheFirst(1).With(x => x.Id = new Guid(orderIdList[0]))
                                .With(x => x.Customer = customerList[0])
                                .With(x => x.CustomerService = csList[0])
                    .TheNext(1).With(x => x.Id = new Guid(orderIdList[1]))
                                .With(x => x.Customer = customerList[1])
                                .With(x => x.CustomerService = csList[1])
                    .TheLast(1).With(x => x.Id = new Guid(orderIdList[2]))
                                .With(x => x.Customer = customerList[2])
                                .With(x => x.CustomerService = csList[2])
                    .Build();
        }

        [Test]
        public void IdentityClick_Test()
        {
            viewChatList.ChatList = Builder<ReceptionChat>.CreateListOfSize(1).Build();

            PChatList pChatList = new PChatList(viewChatList, viewChatSend, viewIdentityList, dalReceptionChat, iIM,lcm);

            IList<DZMembership> members = Builder<DZMembership>.CreateListOfSize(3)
                 .TheFirst(1).With(x => x.Id = new Guid("f197a81d-c984-4894-b21c-a5f00106e08b"))
                             .With(x => x.UserName = "user1")
                             .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                  .TheNext(1).With(x => x.Id = new Guid("a8a2fe97-33cc-4602-85ed-a5f001197c72"))
                             .With(x => x.UserName = "user2")
                             .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                 .TheLast(1).With(x => x.Id = new Guid("6ba73c46-83ea-450d-90b2-a5f00101da01"))
                             .With(x => x.UserName = "cs001")
                             .With(x => x.UserType = Model.Enums.enum_UserType.customerservice)
                 .Build();

            ServiceOrder order_user1 = Builder<ServiceOrder>.CreateNew()
                .With(x => x.Customer = members[0])
                .With(x => x.CustomerService = members[2])
                .With(x=>x.Id=new Guid("d9f216b5-92e4-4f7a-87a0-a5f00107b6bc"))
                .Build();

            pChatList.ViewIdentityList_IdentityClick(order_user1);

            ServiceOrder order_user2 = Builder<ServiceOrder>.CreateNew()
               .With(x => x.Customer = members[1])
               .With(x => x.CustomerService = members[2])
               .With(x => x.Id = new Guid("39a6f7ed-f9d8-4782-b75f-a5f001198475"))
               .Build();
            pChatList.ViewIdentityList_IdentityClick(order_user2);
        }

        [Test]
        public void IMReceivedMessage_Test()
        {
            ReceptionChat chat = Builder<ReceptionChat>.CreateNew()
                                .With(x => x.ChatTarget = Model.Enums.enum_ChatTarget.cer)
                                .With(x => x.ChatType = Model.Enums.enum_ChatType.Text)
                                .With(x => x.From = customerList[0])
                                .With(x => x.To = csList[0])
                                .With(x => x.MessageBody = "哈哈")
                                .Build();

            pIdentityList.IIM_IMReceivedMessage(chat);


        }
    }
}
