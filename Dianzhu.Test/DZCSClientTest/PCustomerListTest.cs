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
        [SetUp]
        public void setup()
        {
              viewCustomerList = MockRepository.GenerateStub<IViewIdentityList>();
              iIM = MockRepository.GenerateStub<InstantMessage>();
              viewChatList = MockRepository.GenerateStub<IViewChatList>();
            
        }
        [Test]
        public void IIm_IMReceivedMessageTest()
        {
            //PIdentityList pCustomerList = new PIdentityList(viewCustomerList, iIM, viewChatList);
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
