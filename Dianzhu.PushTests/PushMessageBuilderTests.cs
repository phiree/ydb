using NUnit.Framework;
using System;
using Ydb.InstantMessage.DomainModel.Enums;

using FizzWare.NBuilder;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Common;

namespace Dianzhu.Push.Tests
{
    [TestFixture()]
    public class PushMessageBuilderTests
    {
        BuildPushMessageBInputDto inputDto;
        [SetUp]
        public void Setup()
        {
              inputDto = Builder<BuildPushMessageBInputDto>.CreateNew().Build();
        }
       
        private PushMessage BuildPushMessage()
        {
            PushMessageBuilder messageBuilder = new PushMessageBuilder();
            PushMessage pushMessage = messageBuilder.BuildPushMessage(inputDto.chatMessage, inputDto.chatType, inputDto.fromResource, inputDto.fromUserName, inputDto.orderId, inputDto.orderBusinessName,
                 inputDto.serialNo, inputDto.orderStatus, inputDto.orderStatStr);
            return pushMessage;
        }
        [Test()]
        public void BuildPushMessageTest_ChatNoticeOrder_EndWarranty()
        {
            inputDto.chatType = typeof(ReceptionChatNoticeOrder);
            inputDto.orderStatus = enum_OrderStatus.EndWarranty;
            var pushMessage = BuildPushMessage();
            Assert.AreEqual(null, pushMessage); 

        }
        [Test()]
        public void BuildPushMessageTest_ChatNoticeOrder_EndCancel_EndRefund_EndIntervention()
        {
            inputDto.chatType = typeof(ReceptionChatNoticeOrder);
            inputDto.orderStatus = enum_OrderStatus.EndCancel;
            var pushMessage = BuildPushMessage();
            Assert.AreEqual(string.Format("<订单完成>{0}订单状态已变为{1},快来看看吧", pushMessage.OrderSerialNo,inputDto.orderStatus), pushMessage.DisplayContent);
        }
        [Test()]
        public void BuildPushMessageTest_ChatNoticeOrder_OtherStatus()
        {
            inputDto.chatType = typeof(ReceptionChatNoticeOrder);
            inputDto.orderStatus = enum_OrderStatus.Ended;
            var pushMessage = BuildPushMessage();
            Assert.AreEqual(string.Format("<订单更新>{0}订单状态已变为{1},快来看看吧", pushMessage.OrderSerialNo, inputDto.orderStatus), pushMessage.DisplayContent);
        }
        [Test()]
        public void BuildPushMessageTest_ChatNoticeSys()
        {
            inputDto.chatType = typeof(ReceptionChatNoticeSys);
            
            var pushMessage = BuildPushMessage();
            
            Assert.AreEqual( inputDto.chatMessage, pushMessage.DisplayContent);
        }
        [Test()]
        public void BuildPushMessageTest_ChatText_Media_FromCustomerService()
        {
            inputDto.chatType = typeof(ReceptionChat);
            inputDto.fromResource = XmppResource.YDBan_CustomerService;
            var pushMessage = BuildPushMessage();

            Assert.AreEqual("[小助理]"+inputDto.chatMessage, pushMessage.DisplayContent);
        }
        [Test()]
        public void BuildPushMessageTest_ChatText_Media_FromStore()
        {
            inputDto.chatType = typeof(ReceptionChat);
            inputDto.fromResource = XmppResource.YDBan_Store;
            var pushMessage = BuildPushMessage();

            Assert.AreEqual("["+inputDto.orderBusinessName+"]" + inputDto.chatMessage, pushMessage.DisplayContent);
        }
        [Test()]
        public void BuildPushMessageTest_ChatText_Media_FromCustomer()
        {
            inputDto.chatType = typeof(ReceptionChat);
            inputDto.fromResource = XmppResource.YDBan_User;
            var pushMessage = BuildPushMessage();

            Assert.AreEqual("[" + inputDto.fromUserName + "]" + inputDto.chatMessage, pushMessage.DisplayContent);
        }

        class BuildPushMessageBInputDto
        {
           public   string chatMessage { get; set; }
            public Type chatType { get; set; }
            public XmppResource fromResource { get; set; }
            public string fromUserName { get; set; }
            public string orderId  { get; set; }
            public string orderBusinessName { get; set; }
            public string serialNo { get; set; }
            public enum_OrderStatus orderStatus { get; set; }
            public string orderStatStr { get; set; }

        }
    }
}