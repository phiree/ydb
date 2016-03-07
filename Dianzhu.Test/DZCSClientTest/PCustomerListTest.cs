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
        [Test]
        public void ReceiveMessageTest()
        {
            var viewCustomerList = MockRepository.GenerateStub<IViewCustomerList>();
            var iIM = MockRepository.GenerateStub<InstantMessage>();
            var viewChatList = MockRepository.GenerateStub<IViewChatList>();
            PCustomerList pCustomerList = new PCustomerList(viewCustomerList, iIM, viewChatList);

            var members = Builder<DZMembership>.CreateListOfSize(5).Build();



            ReceptionChat chat = Builder<ReceptionChat>
                .CreateNew()
                .With(x=>x.From=members[1])
                .With(x=>x.To=members[0])
                .Build();
            pCustomerList.IIm_IMReceivedMessage(chat);
            Assert.AreEqual(1, pCustomerList.CustomerList.Count);

           
            ReceptionChat chat2 = Builder<ReceptionChat>
                .CreateNew()
                .With(x => x.From = members[2])
                .With(x => x.To = members[0])
                .Build();

            pCustomerList.IIm_IMReceivedMessage(chat2);
            Assert.AreEqual(2, pCustomerList.CustomerList.Count);


            pCustomerList.IIm_IMReceivedMessage(chat);
            Assert.AreEqual(2, pCustomerList.CustomerList.Count);


        }
    }
}
