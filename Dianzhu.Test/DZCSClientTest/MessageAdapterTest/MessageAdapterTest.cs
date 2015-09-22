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
using agsXMPP;

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
        Guid userid =Guid .NewGuid ();
        Guid csid =Guid.NewGuid ();
        Guid serviceId = Guid.NewGuid();
        Guid orderId = Guid.NewGuid();
        string server = "server";
        [SetUp]
        public void setup()
        {
            var dalMember = MockRepository.GenerateStub<DAL.DALMembership>(string.Empty);
            var dalService = MockRepository.GenerateStub<DAL.DALDZService>(string.Empty);
            var dalOrder = MockRepository.GenerateStub<DAL.DALServiceOrder>(string.Empty);

              bllMember = MockRepository.GenerateStub<DZMembershipProvider>( dalMember );
            bllMember.Stub(x => x.GetUserById(userid)).Return(
                Builder<Model.DZMembership>.CreateNew().With(x => x.Id = userid).Build()
                  );
              bllMember.Stub(x => x.GetUserById(csid)).Return(
                  Builder<Model.DZMembership>.CreateNew().With(x => x.Id = csid).Build()
                  );

              bllService = MockRepository.GenerateStub<BLLDZService>(dalService);
              bllService.Stub(x => x.GetOne(serviceId)).Return(
                  Builder<Model.DZService>.CreateNew().With(x=>x.Id=serviceId).Build()
                  );


              bllOrder = MockRepository.GenerateStub<BLLServiceOrder>(dalOrder);
              bllOrder.Stub(x => x.GetOne(orderId)).Return(
                Builder<Model.ServiceOrder>.CreateNew().With(x => x.Id = orderId).Build()
                );

              adapter = new MessageAdapter(bllMember, bllService, bllOrder);
              message = Builder<Message>.CreateNew().Build();
        }
        [Test]
        public void AdaptTest_textChat()
        {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message
                (new Jid(userid.ToString()+"@"+ server), new Jid(csid.ToString() + "@" + server),
                agsXMPP.protocol.client.MessageType.chat, "body");

            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);

            var extnode = new agsXMPP.Xml.Dom.Element("ext", string.Empty, "ihelper:chat:text");
           
            var orderNode = new agsXMPP.Xml.Dom.Element("orderID", orderId.ToString());
            extnode.AddChild(orderNode);
            msg.AddChild(extnode);
            Model.ReceptionChat chat = adapter.MessageToChat(msg);
            Assert.AreEqual(orderId, chat.ServiceOrder.Id);

            Message msg2 = adapter.ChatToMessage(chat, server);

            Assert.AreEqual(orderId.ToString(),msg2.SelectSingleElement("ext").SelectSingleElement("orderID").Value);
        }

        [Test]
        public void AdaptTest_Media()
        {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message
                (new Jid(csid.ToString() + "@" + server), new Jid(userid.ToString() + "@" + server),
                agsXMPP.protocol.client.MessageType.chat, "body");

            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);

            var extnode = new agsXMPP.Xml.Dom.Element("ext", string.Empty, "ihelper:chat:media");

            var orderNode = new agsXMPP.Xml.Dom.Element("orderID", orderId.ToString());
            extnode.AddChild(orderNode);

            var mediaNode = new agsXMPP.Xml.Dom.Element("MsgObj");
            string mediaurl = "www.google.com/logo.png";
            mediaNode.SetAttribute("url", mediaurl);
            mediaNode.SetAttribute("type", "image");

            extnode.AddChild(mediaNode);

            msg.AddChild(extnode);
            Model.ReceptionChat chat = adapter.MessageToChat(msg);
            Assert.AreEqual(mediaurl,((Model.ReceptionChatMedia)chat).MedialUrl);
            Assert.AreEqual(userid.ToString(), chat.From.Id.ToString())
;
            Message msg2 = adapter.ChatToMessage(chat,server);
            Assert.AreEqual(userid.ToString(), msg2.From.User);
            Assert.AreEqual(csid.ToString(), msg2.To.User);
            Assert.AreEqual(mediaurl, msg2.SelectSingleElement("ext").SelectSingleElement("MsgObj").GetAttribute("url"));
        }
    }
}
