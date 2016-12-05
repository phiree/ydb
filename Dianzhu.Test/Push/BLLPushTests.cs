using NUnit.Framework;
using Dianzhu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Rhino.Mocks;
using FizzWare.NBuilder;
using Dianzhu.Model;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Membership.Application;

namespace Dianzhu.BLL.Tests
  
{
    [TestFixture()]
    public class PushMessageBuilderTests
    {
        IDALDeviceBind dalDeviceBind;
        IBLLServiceOrder bllServiceOrder;
        IDZMembershipService dalMembership;
        Dianzhu. Push.IPushMessageBiulder pushMessageBuilder;

        Guid   fromMemberId=Guid.NewGuid();
        Guid   toMemberId = Guid.NewGuid();
        Guid orderId = Guid.NewGuid();
        ServiceOrder order;
        [SetUp]
        public void SetUp()
        {
            //Bootstrap.Boot();
        
            bllServiceOrder = MockRepository.GenerateStub<IBLLServiceOrder>();
           
            dalMembership = MockRepository.GenerateStub<IDZMembershipService>();


            pushMessageBuilder = new Dianzhu. Push.PushMessageBuilder( );

            //dalMembership.Stub(x => x.GetMemberById(fromMemberId)).Return(new DZMembership());
            //dalMembership.Stub(x => x.GetMemberById(toMemberId)).Return(new DZMembership());

            order = new ServiceOrder();
            order.SerialNo = "FW001001";
            order.OrderStatusStr = "已支付";
            bllServiceOrder.Stub(x => x.GetOne(orderId)).Return(order);
        
          

        }
        [Test()]
        public void PushTextTest()
        {

           // ReceptionChatFactory chatFactory = new ReceptionChatFactory(Guid.NewGuid(),fromMemberId.ToString(),toMemberId.ToString(),
           //     "这是文本消息", "sessionId", enum_XmppResource.YDBan_CustomerService, enum_XmppResource.YDBan_User);

           // ReceptionChat chat = chatFactory.CreateChatText();
           // bool needPush;
           //string pushMessage=  pushMessageBuilder.BuildPushMessage(chat, out needPush);

           // Assert.AreEqual("[小助理]这是文本消息", pushMessage);

        }
        [Test]
        public void PushOrderTest()
        {

            //ReceptionChatFactory chatFactory = new ReceptionChatFactory(Guid.NewGuid(), fromMemberId.ToString(), toMemberId.ToString(),
            //    "这是文本消息", orderId.ToString(), enum_XmppResource.YDBan_CustomerService, enum_XmppResource.YDBan_User);

            //ReceptionChat chat = chatFactory.CreateNoticeOrder("ordertitle", enum_OrderStatus.Payed,"ordertype");
            //bool needPush;
            //string pushMessage = pushMessageBuilder.BuildPushMessage(chat, out needPush);

            //Assert.AreEqual("[小助理]这是文本消息", pushMessage);

        }


        [TearDown]
        public void TearDown()
        {
             
        }

       
    }
}