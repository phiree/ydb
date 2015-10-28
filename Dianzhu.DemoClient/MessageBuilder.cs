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

        public IQ BuildIq()
        {
            /*<iq from='bard@shakespeare.lit/globe'
  id='get-online-users-list-1'
  to='shakespeare.lit'
  type='set'
  xml:lang='en'>
<command xmlns='http://jabber.org/protocol/commands' 
         action='execute'
         node='http://jabber.org/protocol/admin#get-online-users-list'/>
</iq>*/

            IQ iq = new IQ(IqType.set);
            iq.From = GlobalViables.XMPPConnection.Username + "@" + GlobalViables.ServerName;
            iq.To = GlobalViables.ServerName;
            var nodeCommand = new agsXMPP.Xml.Dom.Element("command", string.Empty, "http://jabber.org/protocol/commands");
            nodeCommand.SetAttribute("action", "execute");
            nodeCommand.SetAttribute("node", "http://jabber.org/protocol/admin#get-online-users-list");
            iq.AddChild(nodeCommand);
            return iq;
        }
    }
}
