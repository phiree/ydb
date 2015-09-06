using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.CSClient;
using NUnit.Framework;
using Rhino.Mocks;
using FizzWare.NBuilder;
using Dianzhu.BLL;
using agsXMPP.protocol.client;
using Dianzhu.CSClient.MessageAdapter;
namespace Dianzhu.Test.DZCSClientTest.MessageAdapterTest
{
    [TestFixture]
    public class MessageAdapterTest
    {
        DZMembershipProvider bllMember;
        BLLDZService bllService;
        BLLServiceOrder bllOrder;
        MessageAdapter adapter;
        Message message;
        string user = "user1";
        string cs = "cs1";
        Guid serviceId = Guid.NewGuid();
        [SetUp]
        public void setup()
        {
            var dalMember = MockRepository.GenerateStub<DAL.DALMembership>(string.Empty);
            var dalService = MockRepository.GenerateStub<DAL.DALDZService>(string.Empty);
            var dalOrder = MockRepository.GenerateStub<DAL.DALServiceOrder>(string.Empty);

              bllMember = MockRepository.GenerateStub<DZMembershipProvider>( dalMember );
              bllMember.Stub(x => x.GetUserByName(user)).Return(new Model.DZMembership());
              bllMember.Stub(x => x.GetUserByName(cs)).Return(new Model.DZMembership());

              bllService = MockRepository.GenerateStub<BLLDZService>(dalService);
              bllService.Stub(x => x.GetOne(serviceId)).Return(
                  Builder<Model.DZService>.CreateNew().With(x=>x.Id=serviceId).Build()
                  );


              bllOrder = MockRepository.GenerateStub<BLLServiceOrder>(dalOrder);

              adapter = new MessageAdapter(bllMember, bllService, bllOrder);
              message = Builder<Message>.CreateNew().Build();
        }
        [Test]
        public void AdaptTest_ServiceChat_WithServiceId()
        {
            message.To = "user1";
            message.From = "cs1";
            message.SetAttribute("service_id", serviceId.ToString());
            message.SetAttribute("message_type", "PushedService");
            Model.ReceptionChatServicePushed chat = (Model.ReceptionChatServicePushed)adapter.MessageToChat(message);
            Assert.AreEqual(serviceId,chat.Service.Id);
        }

        [Test]
        public void AdaptTest_ServiceChat_WithoutServiceId()
        {
            message.To = "user1";
            message.From = "cs1";
            message.SetAttribute("message_type", "PushedService");
            message.SetAttribute("ServiceName", "name1");
            message.SetAttribute("ServiceDescription", "ServiceDescription1");
            message.SetAttribute("ServiceBusinessName", "business1");
            message.SetAttribute("UnitPrice", 13);
            message.SetAttribute("ServiceUrl", "taopbao.com");
            Model.ReceptionChatServicePushed chat = (Model.ReceptionChatServicePushed)adapter.MessageToChat(message);
            Assert.AreEqual("name1",chat.ServiceName);
            Assert.AreEqual(13, chat.UnitPrice);
        }
    }
}
