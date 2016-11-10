using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.Presenter;
using FizzWare.NBuilder;
using Dianzhu.Model;
using Dianzhu.CSClient.Presenter.VMAdapter;
using Dianzhu.CSClient.ViewModel;
using Ydb.InstantMessage.Application;
using Ydb.Membership.Application;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;

namespace Dianzhu.Test.DZCSClientTest
{
    [TestFixture]
    public class PIdentityListTest
    {
        IViewIdentityList viewIdentityList;
        IInstantMessage iIM;
        IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        BLL.BLLServiceOrder bllServiceOrder;
        IViewOrderHistory viewOrderHistory;
        IViewSearchResult viewSearchResult;
        IVMChatAdapter vmChatAdapter;
        IVMIdentityAdapter vmIdentityAdapter;
        IReceptionService receptionService;
        IDZMembershipService memberService;

        PIdentityList pIdentityList;
        IList<DZMembership> customerList;
        IList<DZMembership> csList;
        IList<ServiceOrder> orderList;
        Dianzhu.CSClient.LocalStorage.LocalChatManager lcm;
        Dianzhu.CSClient.LocalStorage.LocalHistoryOrderManager lhom;
        Dianzhu.CSClient.LocalStorage.LocalUIDataManager luidm;
       IChatService chatServive;
        [SetUp]
        public void setup()
        {
            chatServive = MockRepository.GenerateStub<IChatService>();
            viewIdentityList = MockRepository.GenerateStub<IViewIdentityList>();
            iIM = MockRepository.GenerateStub<IInstantMessage>();
            viewChatList = MockRepository.GenerateStub<IViewChatList>();
            viewChatSend = MockRepository.GenerateStub<IViewChatSend>();
          
            bllServiceOrder = MockRepository.GenerateStub<BLL.BLLServiceOrder>();
            viewOrderHistory = MockRepository.GenerateStub<IViewOrderHistory>();
            viewSearchResult = MockRepository.GenerateStub<IViewSearchResult>();
            lcm = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalChatManager>();
            lhom = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalHistoryOrderManager>();
            luidm = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalUIDataManager>();
            vmChatAdapter = MockRepository.GenerateStub<IVMChatAdapter>();
            vmIdentityAdapter = MockRepository.GenerateStub<IVMIdentityAdapter>();
            receptionService = MockRepository.GenerateStub<IReceptionService>();
            memberService = MockRepository.GenerateStub<IDZMembershipService>();

            pIdentityList = new PIdentityList(viewIdentityList, viewChatList, iIM,
                viewChatSend, bllServiceOrder, viewOrderHistory, viewSearchResult,
                lcm,lhom,luidm, vmChatAdapter, vmIdentityAdapter, receptionService, memberService);

            string[] customerIdList = { "17b2007f-0267-4224-8d5a-cbaafa7ed1fc", "153ef5fa-600a-4a32-aefb-27c5e5fa5a50", "4a3727f6-ec21-42a8-ba84-70ba9db06354" };
            string[] csIdList = { "20364ea5-c19c-409d-8b61-cb1905fc68d8", "684db3a7-6c2c-44bf-a50a-5ceb1b904a26", "8f506545-de72-4fdb-bbaa-85a8709ae63f" };
            string[] orderIdList = { "eb524d39-6800-4477-9b03-6c1347f766b2", "b8d66f6e-45c7-444a-a055-96429049eac0", "e858b491-d295-4978-9b83-4571cc05c030" };

            customerList = Builder<DZMembership>.CreateListOfSize(3)
                    .TheFirst(1)
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = UserType.customer)
                    .TheNext(1)
                                .With(x => x.UserName = "user2")
                                .With(x => x.UserType = UserType.customer)
                    .TheLast(1)
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = UserType.customer)
                    .Build();

            csList = Builder<DZMembership>.CreateListOfSize(3)
                    .TheFirst(1)
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = UserType.customer)
                    .TheNext(1)
                                .With(x => x.UserName = "user2")
                                .With(x => x.UserType = UserType.customer)
                    .TheLast(1)
                                .With(x => x.UserName = "user1")
                                .With(x => x.UserType = UserType.customer)
                    .Build();

            //orderList = Builder<ServiceOrder>.CreateListOfSize(3)
            //        .TheFirst(1)
            //                    .With(x => x.CustomerId = customerList[0].Id.ToString())
            //                    .With(x => x.CustomerServiceId = csList[0].Id.ToString())
            //        .TheNext(1).With(x => x.Id = new Guid(orderIdList[1]))
            //                    .With(x => x.CustomerId = customerList[1].Id.ToString())
            //                    .With(x => x.CustomerServiceId = csList[1].Id.ToString())
            //        .TheLast(1).With(x => x.Id = new Guid( orderIdList[2] ))
            //                    .With(x => x.CustomerId = customerList[2].Id.ToString())
            //                    .With(x => x.CustomerServiceId = csList[2].Id.ToString())
            //        .Build();
        }

        [Test]
        public void IdentityClick_Test()
        {
            viewChatList.ChatList = Builder<VMChat>.CreateListOfSize(1).Build();

            PChatList pChatList = new PChatList(viewChatList, viewChatSend, viewIdentityList, iIM,lcm, vmChatAdapter, chatServive);

            IList<DZMembership> members = Builder<DZMembership>.CreateListOfSize(3)
                 //.TheFirst(1).With(x => x.Id = new Guid("f197a81d-c984-4894-b21c-a5f00106e08b"))
                 //            .With(x => x.UserName = "user1")
                 //            .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                 // .TheNext(1).With(x => x.Id = new Guid("a8a2fe97-33cc-4602-85ed-a5f001197c72"))
                 //            .With(x => x.UserName = "user2")
                 //            .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                 //.TheLast(1).With(x => x.Id = new Guid("6ba73c46-83ea-450d-90b2-a5f00101da01"))
                 //            .With(x => x.UserName = "cs001")
                 //            .With(x => x.UserType = Model.Enums.enum_UserType.customerservice)
                 .Build();

            ServiceOrder order_user1 = Builder<ServiceOrder>.CreateNew()
                //.With(x => x.CustomerId = members[0].Id.ToString())
                //.With(x => x.CustomerServiceId = members[2].Id.ToString())
                .With(x=>x.Id=new Guid("d9f216b5-92e4-4f7a-87a0-a5f00107b6bc"))
                .Build();

            pChatList.ViewIdentityList_IdentityClick(null);

            ServiceOrder order_user2 = Builder<ServiceOrder>.CreateNew()
               //.With(x => x.CustomerId = members[1].Id.ToString())
               //.With(x => x.CustomerServiceId = members[2].Id.ToString())
               .With(x => x.Id = new Guid("39a6f7ed-f9d8-4782-b75f-a5f001198475"))
               .Build();
            pChatList.ViewIdentityList_IdentityClick(null);
        }

        [Test]
        public void IMReceivedMessage_Test()
        {
             


        }
    }
}
