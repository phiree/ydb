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
    public class PGlobalTest
    {
        IViewIdentityList viewCustomerList;
        InstantMessage iIM;
        IViewChatList viewChatList;
        PIdentityList pi;
        PChatList pc;
        [SetUp]
        public void setup()
        {
            viewCustomerList = MockRepository.GenerateStub<IViewIdentityList>();
            iIM = MockRepository.GenerateStub<InstantMessage>();
            viewChatList = MockRepository.GenerateStub<IViewChatList>();
             pi = MockRepository.GenerateStub<PIdentityList>();
          pc=  MockRepository.GenerateStub<PChatList>();

        }

        [Test]
        public void UpdateIdentityListTest()
        {
            IList<DZMembership> customers = Builder<DZMembership>.CreateListOfSize(10).Build();

            //同一个用户的两个订单
            ServiceOrder order11 = ServiceOrderFactory.CreateDraft(customers[0], customers[1]);
            ServiceOrder order12 = ServiceOrderFactory.CreateDraft(customers[0], customers[1]);

            //另一个用户的一个订单
            ServiceOrder order21 = ServiceOrderFactory.CreateDraft(customers[0], customers[2]);

            IList<ReceptionChat> chats = Builder<ReceptionChat>.CreateListOfSize(5)
                .TheFirst(1).With(x => x.From = order11.Customer).And(x => x.ServiceOrder = order11)//新用户订单
                .TheNext(1).With(x => x.From = order11.Customer).And(x => x.ServiceOrder = order11)//同一订单
                .TheNext(1).With(x => x.From = order12.Customer).And(x => x.ServiceOrder = order12)//同一用户，新订单
                .TheNext(1).With(x => x.From = order21.Customer).And(x => x.ServiceOrder = order21)//不同用户，新订单
                .TheNext(1).With(x => x.From = order11.Customer).And(x => x.ServiceOrder = order11)//已有用户的订单        
                .Build();

            //发送第一条消息
            PGlobal pg = new PGlobal(iIM, pi, pc);
            IdentityTypeOfOrder type;
            pg.UpdateIdentityList(order11,out type);
            Assert.AreEqual(IdentityTypeOfOrder.NewIdentity, type);
            Assert.AreEqual(null, PGlobal.CurrentIdentity);
            Assert.AreEqual(1, PGlobal.CurrentIdentityList.Count);
            Assert.AreEqual(false, PGlobal.CurrentIdentityList[order11]);


            pg.UpdateIdentityList(order12, out type);
            Assert.AreEqual(IdentityTypeOfOrder.InList, type);
            Assert.AreEqual(null, PGlobal.CurrentIdentity);
            Assert.AreEqual(1, PGlobal.CurrentIdentityList.Count);
            Assert.AreEqual(false, PGlobal.CurrentIdentityList[order12]);

            pg.UpdateIdentityList(order21, out type);
            Assert.AreEqual(IdentityTypeOfOrder.NewIdentity, type);
            Assert.AreEqual(null, PGlobal.CurrentIdentity);
            Assert.AreEqual(2, PGlobal.CurrentIdentityList.Count);
            Assert.AreEqual(false, PGlobal.CurrentIdentityList[order21]);
            Assert.AreEqual(false, PGlobal.CurrentIdentityList[order12]);
        }
    }
}
