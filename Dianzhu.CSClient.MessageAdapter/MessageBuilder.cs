using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agsXMPP.protocol.client;
using Dianzhu.Model;
using log4net;
using agsXMPP.Xml.Dom;
namespace Dianzhu.CSClient.MessageAdapter
{
   
    public  class MessageBuilder
    {
        
        ILog ilog = LogManager.GetLogger("Dianzhu.CSClient.Dianzhu.CSClient.MessageBuilder");

        Node extNode;//message 中的ext节点
        Message message = null;
        public Message Message { get { return message; } }

        public MessageBuilder( )
        {
             
            message = new Message();
        }
        public MessageBuilder BuildBase(string id, string toJid, string fromJid, string body,string orderId)
        {  
            message = new Message();
        
            message.SetAttribute("type", "chat");
            message.Id =string.IsNullOrEmpty(id)?Guid.NewGuid().ToString() : id;
            //     msg.From = new agsXMPP.Jid(chat.From.Id + "@" + server);
            //用户最新的客户端名称. 应该通过restfulapi获取, 弃用数据库数据.


            message.To = toJid;
            message.From = fromJid;

            message.Body = body;

            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            message.AddChild(nodeActive);

            extNode = new agsXMPP.Xml.Dom.Element("ext");
            extNode.Namespace = "ihelper:chat:text";
            var extOrderID = new agsXMPP.Xml.Dom.Element("orderID", orderId);
            extNode.AddChild(extOrderID);
            message.AddChild(extNode);
            return this;

        }
        public MessageBuilder BuildMedia(string mediaType, string mediaUrl)
        {
            extNode.Namespace = "ihelper:chat:media";

           
            var extMedia = new agsXMPP.Xml.Dom.Element("msgObj");
            extMedia.SetAttribute("url", mediaUrl);
            extMedia.SetAttribute("type", mediaType);
            extNode.AddChild(extMedia);
            return this;
        }
        public MessageBuilder BuildUserStatus(string userId, string status)
        {
            extNode.Namespace = "ihelper:chat:userstatus";

            
            var extStatus = new agsXMPP.Xml.Dom.Element("msgObj");
            extStatus.SetAttribute("userId", userId);
            extStatus.SetAttribute("status", status);
            extNode.AddChild(extStatus);
            return this;
        }
        public MessageBuilder BuildReAssign(string userId, string alias,string imgUrl)
        {
            extNode.Namespace = "ihelper:notice:cer:change";
            var cerObj = new agsXMPP.Xml.Dom.Element("cerObj");
            cerObj.SetAttribute("userID", userId);
            cerObj.SetAttribute("alias", alias);
            cerObj.SetAttribute("imgUrl", imgUrl);
            extNode.AddChild(cerObj);
            message.SetAttribute("type", "headline");
            return this;
        }
        public MessageBuilder BuildPushedService(string svcID, string svcName,string svcType,string startTime,string userId,string alias,string imgUrl,string customerPhone, string customerName, string customerAddress)
        {
            extNode.Namespace = "ihelper:chat:orderobj";
            var svcObj = new agsXMPP.Xml.Dom.Element("svcObj");
         
            svcObj.SetAttribute("svcID", svcID);
            svcObj.SetAttribute("name", svcName);
            svcObj.SetAttribute("type", svcType);
            svcObj.SetAttribute("startTime", startTime);
            extNode.AddChild(svcObj);

            /* "storeObj": {
             "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
             "alias": "望海国际",
             "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao_MD.png"
         }*/
            var storeObj = new agsXMPP.Xml.Dom.Element("storeObj");

            storeObj.SetAttribute("userID", userId);
            storeObj.SetAttribute("alias", alias);
            storeObj.SetAttribute("imgUrl", imgUrl);
            extNode.AddChild(storeObj);

            var customerObj = new agsXMPP.Xml.Dom.Element("customerObj");

            customerObj.SetAttribute("customerPhone", customerPhone);
            customerObj.SetAttribute("customerName", customerName);
            customerObj.SetAttribute("customerAddress", customerAddress);
            extNode.AddChild(customerObj);
            return this;
        }
        
        public MessageBuilder BuildNoticeOrder(string title, string status,string type)
        {
            extNode.Namespace = "ihelper:notice:order";


            var extMedia = new agsXMPP.Xml.Dom.Element("orderObj");
            extMedia.SetAttribute("status", status);
            extMedia.SetAttribute("type", type);
            extMedia.SetAttribute("title", title);
            extNode.AddChild(extMedia);
            return this;
        }
    }
}
