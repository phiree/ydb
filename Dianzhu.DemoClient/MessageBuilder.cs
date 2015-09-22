using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
namespace Dianzhu.DemoClient
{
   public  class MessageBuilder
    {
        Message msg;
        public MessageBuilder Create(string from,string to,string body,string orderId)
        {
              msg = new Message(new Jid(to+"@"+GlobalViables.ServerName)
                ,new Jid(from+"@" +GlobalViables.ServerName), body);
            var nodeExt = new agsXMPP.Xml.Dom.Element("ext");
            var nodeOrder = new agsXMPP.Xml.Dom.Element("orderID",orderId);
            nodeExt.AddChild(nodeOrder);
            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);
            msg.AddChild(nodeExt);
            return this;
        }
        public Message BuildText()
        {
            msg.SelectSingleElement("ext").Namespace = "ihelper:chat:text";
            return msg;
        }
        
        public Message BuildMedia(string mediaType,string mediaUrl)
        {
            var nodeMedia= new agsXMPP.Xml.Dom.Element("MsgObj");
            nodeMedia.SetAttribute("url", mediaUrl);

            nodeMedia.SetAttribute("type", mediaType);
            msg.SelectSingleElement("ext").AddChild(nodeMedia);
            msg.SelectSingleElement("ext").Namespace = "ihelper:chat:media";
            return msg;
        }
    }
}
