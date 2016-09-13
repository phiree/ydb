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
        Dianzhu.CSClient.LocalStorage.LocalUIDataManager luidm;
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
            luidm = MockRepository.GenerateStub<Dianzhu.CSClient.LocalStorage.LocalUIDataManager>();
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
