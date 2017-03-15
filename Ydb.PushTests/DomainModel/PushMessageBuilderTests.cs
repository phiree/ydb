using NUnit.Framework;
using System;

using Ydb.Push.DomainModel;
using Ydb.Push.Application;
using Ydb.Push;
using FizzWare.NBuilder;

namespace Ydb.Push.DomainModel.Tests
{
    [TestFixture()]
    public class PushMessageBuilderTests
    {
        private BuildPushMessageBInputDto inputDto;

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
            inputDto.chatType = "ReceptionChatNoticeOrder";
            inputDto.orderStatus = "EndWarranty";
            var pushMessage = BuildPushMessage();
            Assert.AreEqual(null, pushMessage);
        }

        [Test()]
        public void BuildPushMessageTest_ChatNoticeOrder_EndCancel_EndRefund_EndIntervention()
        {
            inputDto.chatType = "ReceptionChatNoticeOrder";
            inputDto.orderStatus = "EndCancel";
            inputDto.orderStatStr = "已取消";
            var pushMessage = BuildPushMessage();
            Assert.AreEqual(string.Format("<订单完成>{0}订单状态已变为{1},快来看看吧", pushMessage.OrderSerialNo, inputDto.orderStatStr), pushMessage.DisplayContent);
        }

        [Test()]
        public void BuildPushMessageTest_ChatNoticeOrder_OtherStatus()
        {
            inputDto.chatType = "ReceptionChatNoticeOrder";
            inputDto.orderStatus = "Ended";
            inputDto.orderStatStr = "已完成";
            var pushMessage = BuildPushMessage();
            Assert.AreEqual(string.Format("<订单更新>{0}订单状态已变为{1},快来看看吧", pushMessage.OrderSerialNo, inputDto.orderStatStr), pushMessage.DisplayContent);
        }

        [Test()]
        public void BuildPushMessageTest_ChatNoticeSys()
        {
            inputDto.chatType = "ReceptionChatNoticeSys";

            var pushMessage = BuildPushMessage();

            Assert.AreEqual(inputDto.chatMessage, pushMessage.DisplayContent);
        }

        [Test()]
        public void BuildPushMessageTest_ChatText_Media_FromCustomerService()
        {
            inputDto.chatType = "ReceptionChat";
            inputDto.fromResource = "YDBan_CustomerService";
            var pushMessage = BuildPushMessage();

            Assert.AreEqual("[小助理]" + inputDto.chatMessage, pushMessage.DisplayContent);
        }

        [Test()]
        public void BuildPushMessageTest_ChatText_Media_FromStore()
        {
            inputDto.chatType = "ReceptionChat";
            inputDto.fromResource = "YDBan_Store";
            var pushMessage = BuildPushMessage();

            Assert.AreEqual("[" + inputDto.orderBusinessName + "]" + inputDto.chatMessage, pushMessage.DisplayContent);
        }

        [Test()]
        public void BuildPushMessageTest_ChatText_Media_FromCustomer()
        {
            inputDto.chatType = "ReceptionChat";
            inputDto.fromResource = "YDBan_User";
            var pushMessage = BuildPushMessage();

            Assert.AreEqual("[" + inputDto.fromUserName + "]" + inputDto.chatMessage, pushMessage.DisplayContent);
        }

        [Test()]
        public void BuildPushMessageTest_PushedService()
        {
            inputDto.chatType = "ReceptionChatPushService";
            inputDto.fromResource = "YDBan_CustomerService";
            var pushMessage = BuildPushMessage();

            Assert.AreEqual("[小助理]" + inputDto.chatMessage, pushMessage.DisplayContent);
        }

        private class BuildPushMessageBInputDto
        {
            public string chatMessage { get; set; }
            public string chatType { get; set; }
            public string fromResource { get; set; }
            public string fromUserName { get; set; }
            public string orderId { get; set; }
            public string orderBusinessName { get; set; }
            public string serialNo { get; set; }
            public string orderStatus { get; set; }
            public string orderStatStr { get; set; }
        }
    }
}