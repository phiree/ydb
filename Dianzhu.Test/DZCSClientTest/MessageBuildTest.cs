using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;
namespace Dianzhu.Test.DZCSClientTest
{
    [TestFixture]
    public class MessageBuildTest
    {
        [Test]
        public void BuildWithOrderNode()
        {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message(new Jid("to"),new Jid("from"), agsXMPP.protocol.client.MessageType.chat, "body");

            var nodeActive =new  agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);

            var node= new  agsXMPP.Xml.Dom.Element("ext",string.Empty,"ihelper:chat:text");
            var childNode=new  agsXMPP.Xml.Dom.Element("orderID","this_is_an_order_id");
            node.AddChild(childNode);
            msg.AddChild(node);
            /*
             <body xmlns="jabber:client">body</body><subject xmlns="jabber:client">suject</subject><ext xmlns="ihelper:chat:text"><orderID>this_is_an_order_id</orderID></ext>
             */
            Console.WriteLine(msg.InnerXml);
            Console.WriteLine(msg.ToString());
            /*
             <message xmlns="jabber:client" to="to">
             * <body>body</body>
             * <subject>suject</subject>
             * <ext xmlns="ihelper:chat:text">
             * <orderID>this_is_an_order_id</orderID>
             * </ext>
             * </message>

             */
            Node extNode = msg.ChildNodes.Item(3);
           
            Console.WriteLine("ext node:"+extNode.ToString());
            Console.WriteLine("ext namespace:" + extNode.Namespace);
        }

    }
}
