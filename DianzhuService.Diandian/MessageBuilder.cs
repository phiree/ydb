using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
namespace DianzhuService.Diandian
{
   public  class MessageBuilder
    {
        Message msg;
        public MessageBuilder Create(string from, string to, string body, string orderId)
        {
            msg = new Message(new Jid(to + "@" + GlobalViables.DomainName + "/" + Dianzhu.enum_XmppResource.YDBan_DianDian)
              , new Jid(from + "@" + GlobalViables.DomainName + "/" + Dianzhu.enum_XmppResource.YDBan_DianDian), body);
            msg.SetAttribute("type", "chat");
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
    }
}
