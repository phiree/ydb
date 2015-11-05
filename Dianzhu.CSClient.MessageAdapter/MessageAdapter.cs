using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using agsXMPP.protocol.client;
namespace Dianzhu.CSClient.MessageAdapter
{
    /// <summary>
    /// Convert between IM message and ReceptionChat
    /// todo:需要区分 聊天记录, 和 系统消息.
    /// </summary>
    public class MessageAdapter : IMessageAdapter.IAdapter
    {
        
        static DZMembershipProvider bllMember;
        DZMembershipProvider BllMember
        {
            get
            {
                if (bllMember == null) bllMember = new DZMembershipProvider();
                return bllMember;
            }

        }
        static BLLDZService bllDZService;
        BLLDZService BllDZService
        {
            get
            {
                if (bllDZService == null) bllDZService = new BLLDZService();
                return bllDZService;
            }

        }
        static BLLServiceOrder bllOrder;
        BLLServiceOrder BLLOrder
        {
            get
            {
                if (bllOrder == null) bllOrder = new BLLServiceOrder();
                return bllOrder;
            }

        }



        log4net.ILog ilog = log4net.LogManager.GetLogger("xmpp");
        public MessageAdapter()
        {
 
        }
        /// <summary>
        /// 将im的 message为 系统设计的格式(chat)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Model.ReceptionChat MessageToChat(Message message)
        {
            ilog.Debug("receive___" + message.ToString());
            //chat or headline
            string messageType = message.GetAttribute("type");
            bool isNotice = messageType == "headline";

            var ext_element = message.SelectSingleElement("ext", true);
            var extNamespace = ext_element.Namespace;
            enum_ChatType chatType;
            switch (extNamespace.ToLower())
            {
                case "ihelper:chat:text":
                    chatType = enum_ChatType.Text;
                    break;
                case "ihelper:chat:media":
                    chatType = enum_ChatType.Media;
                    break;
                case "ihelper:notce:system":
                     
                case "ihelper:notce:order":
                     
                case "ihelper:notce:promote":
                     
                case "ihelper:notce:cer:change":
                    chatType = enum_ChatType.Notice;
                    break;
                default:
                    throw new Exception("未知的命名空间");

            }
            ReceptionChat chat = ReceptionChat.Create(chatType);
            var chatFrom = BllMember.GetUserById(new Guid(message.From.User));
            if (!isNotice) { 
            var chatTo = BllMember.GetUserById(new Guid(message.To.User));
            chat.To = chatTo;
            }
            Guid messageId;
            if (Guid.TryParse(message.Id, out messageId))
            {
                //chat.Id = messageId;
            }
            chat.From = chatFrom;

            //这个逻辑放在orm002001接口里面处理.

            //如果是

            bool hasOrderId = ext_element.HasTag("orderID");
            
            
            if (hasOrderId)
            {
                Guid order_ID;
                var e_orderId = ext_element.SelectSingleElement("orderID").Value;
                bool isValidGuid = Guid.TryParse(e_orderId, out order_ID);

                if (isValidGuid)
                {
                    var existedServiceOrder = BLLOrder.GetOne(order_ID);
                    if (existedServiceOrder != null)
                    {
                        chat.ServiceOrder = existedServiceOrder;

                    }
                }
            }


            chat.MessageBody = message.Body;
            chat.SavedTime = DateTime.Now;
            if (chatType == enum_ChatType.Media)
            {
                var mediaNode = ext_element.SelectSingleElement("MsgObj");
                var mediaUrl = mediaNode.GetAttribute("url");
                var mediaType = mediaNode.GetAttribute("type");
                ((ReceptionChatMedia)chat).MedialUrl = mediaUrl;
                ((ReceptionChatMedia)chat).MediaType = mediaType;
            }
            return chat;
        }
        /// <summary>
        /// 将聊天对象转换为具体通讯协议规定的格式.
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public Message ChatToMessage(Model.ReceptionChat chat, string server)
        {

            Message msg = new Message();

            msg.SetAttribute("type", "chat");
            msg.Id = chat.Id.ToString();
            //     msg.From = new agsXMPP.Jid(chat.From.Id + "@" + server);
            msg.To = new agsXMPP.Jid(chat.To.Id + "@" + server);//发送对象
            msg.Body = chat.MessageBody;

            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);

            var extNode = new agsXMPP.Xml.Dom.Element("ext");
            var extOrderID = new agsXMPP.Xml.Dom.Element("orderID", chat.ServiceOrder == null ? string.Empty : chat.ServiceOrder.Id.ToString());
            extNode.AddChild(extOrderID);
            msg.AddChild(extNode);

            switch (chat.ChatType)
            {
                case enum_ChatType.Text:
                    extNode.Namespace = "ihelper:chat:text";

                    break;
                case enum_ChatType.Media:
                    extNode.Namespace = "ihelper:chat:media";

                    var mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                    var mediaType = ((ReceptionChatMedia)chat).MediaType;
                    var extMedia = new agsXMPP.Xml.Dom.Element("MsgObj");
                    extMedia.SetAttribute("url", mediaUrl);
                    extMedia.SetAttribute("type", mediaType);
                    extNode.AddChild(extMedia);
                    break;
                case enum_ChatType.ReAssign:
                    extNode.Namespace = "ihelper:cer:change";
                    var cerObj = new agsXMPP.Xml.Dom.Element("cerObj");
                    cerObj.SetAttribute("UserID", ((ReceptionChatReAssign)chat).ReAssignedCustomerService.Id.ToString());
                    cerObj.SetAttribute("alias", ((ReceptionChatReAssign)chat).ReAssignedCustomerService.DisplayName);
                    cerObj.SetAttribute("imgUrl", ((ReceptionChatReAssign)chat).ReAssignedCustomerService.AvatarUrl);
                    extNode.AddChild(cerObj);
                    msg.SetAttribute("type", "headline");
                    break;
                case enum_ChatType.Notice:
                    extNode.Namespace = "ihelper:cer:notce";
                    var UserObj = new agsXMPP.Xml.Dom.Element("UserObj");

                    UserObj.SetAttribute("UserID", ((ReceptionChatNotice)chat).UserObj.Id.ToString());
                    UserObj.SetAttribute("alias", ((ReceptionChatNotice)chat).UserObj.DisplayName);
                    UserObj.SetAttribute("imgUrl", ((ReceptionChatNotice)chat).UserObj.AvatarUrl);
                    msg.SetAttribute("type", "headline");
                    extNode.AddChild(UserObj);
                    break;
            }
            ilog.Debug("send___" + msg.ToString());
            return msg;



        }
      
    }
}
