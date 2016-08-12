using NUnit.Framework;
using Dianzhu.CSClient.MessageAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient;
using Dianzhu.Model;
namespace Dianzhu.CSClient.MessageAdapter.Tests
{
    [TestFixture()]
    public class MessageAdapterTests
    {
        [Test()]
        public void RawXmlToChatTest()
        {
            string rawXml = "<message xmlns=\"jabber:client\" to=\"4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService\" id=\"eb6ecbc6-6979-42b8-b449-a1b7c7ae5ce0\" from=\"272be8b3-100c-423c-83e2-a63d012dd455@localhost/YDBan_DemoClient\"><body>97</body><active xmlns=\"http://jabber.org/protocol/chatstates\" /><ext xmlns=\"ihelper:chat:text\"><orderID>bed76b1b-fe4a-4a90-abbd-a65b0129b1d9</orderID></ext></message>";
            MessageAdapter ma = new MessageAdapter();

          ReceptionChat chat=  ma.RawXmlToChat(rawXml);
            Assert.AreEqual(chat.To, "4e2676e1-5561-11e6-b7f0-001a7dda7106");
        }
    }
}