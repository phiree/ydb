﻿using System;
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
    /// ddd:DomainService
    /// </summary>
    public class MessageAdapter : IMessageAdapter.IAdapter
    {
        
        IDAL.IDALServiceOrder dalOrder;
        IDAL.IDALMembership dalMembership;
        public MessageAdapter(IDAL.IDALServiceOrder dalOrder,IDAL.IDALMembership dalMembership)
        {
           // this.bllOrder = bllOrder;
            this.dalOrder = dalOrder;
            this.dalMembership = dalMembership;
        }
 
        static BLLIMUserStatus bllIMUserStatus;
        BLLIMUserStatus BLLIMUserStatus
        {
            get
            {
                if (bllIMUserStatus == null) bllIMUserStatus = new BLLIMUserStatus();
                return bllIMUserStatus;
            }

        }



        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.CSClient.MessageAdapter");
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
            //收到的消息可能是 openfire服务器 标准的协议，

            //chat or headline
            string messageType = message.GetAttribute("type");
            bool isNotice = messageType == "headline";
            enum_ChatType chatType = enum_ChatType.Text;
            //收到的消息可能是 openfire服务器 标准的协议，不存在ext节点。

            var ext_element = message.SelectSingleElement("ext", true);
            bool has_ext = ext_element != null;
            if (!has_ext)
            {
                ilog.Warn("收到标准协议的消息，不存在ext节点");
            }
            else {
                var extNamespace = ext_element.Namespace;

                switch (extNamespace.ToLower())
                {
                    case "ihelper:chat:text":
                        chatType = enum_ChatType.Text;
                        break;
                    case "ihelper:chat:media":
                        chatType = enum_ChatType.Media;
                        break;
                    case "ihelper:notice:system":

                    case "ihelper:notice:order":

                    case "ihelper:notice:promote":

                    case "ihelper:notice:cer:change":
                        chatType = enum_ChatType.Notice;
                        break;
                    case "ihelper:chat:userstatus":
                        chatType = enum_ChatType.UserStatus;
                        break;
                    default:
                        throw new Exception("未知的命名空间");

                }
            }
            ReceptionChat chat = ReceptionChat.Create(chatType);
            Guid fromUser;
            bool isFromUser = Guid.TryParse(message.From.User, out fromUser);
            if (!isFromUser)
            {
                ilog.Error("发送用户的id有误，发送用户id为：" + message.From.User + "发送用户资源名为：" + message.From.Resource);
                throw new Exception("发送用户的id有误");
            }
            var chatFrom = dalMembership.FindById(fromUser);
            chat.From = chatFrom;
            chat.FromResource = enum_XmppResource.Unknow;
            try
            {
                chat.FromResource = (enum_XmppResource)Enum.Parse(typeof(enum_XmppResource), message.From.Resource);
            }
            catch (Exception e)
            {
                ilog.Error("未知的资源名称：" + message.From.Resource);
            }
            if (!isNotice)
            {
                var chatTo = dalMembership.FindById(new Guid(message.To.User));
                chat.To = chatTo;
                if (message.To.Resource != null)
                {
                    chat.ToResource = (enum_XmppResource)Enum.Parse(typeof(enum_XmppResource), message.To.Resource);
                }
                else
                {
                    ilog.Error("message中to的资源名为空！");
                }
            }
            Guid messageId;
            if (Guid.TryParse(message.Id, out messageId))
            {
                chat.Id = messageId;
            }


            //这个逻辑放在orm002001接口里面处理.

            //如果是
            if (has_ext)
            {
                bool hasOrderId = ext_element.HasTag("orderID");


                if (hasOrderId)
                {
                    Guid order_ID;
                    var e_orderId = ext_element.SelectSingleElement("orderID").Value;
                    bool isValidGuid = Guid.TryParse(e_orderId, out order_ID);

                    if (isValidGuid)
                    {
                        var existedServiceOrder = dalOrder.FindById(order_ID);
                        if (existedServiceOrder != null)
                        {
                            chat.ServiceOrder = existedServiceOrder;

                        }
                    }
                }
            }


            chat.MessageBody = message.Body;
            chat.SavedTime = DateTime.Now;
            try
            {
                if (chatType == enum_ChatType.Media)
                {
                    var mediaNode = ext_element.SelectSingleElement("msgObj");
                    var mediaUrl = mediaNode.GetAttribute("url");
                    var mediaType = mediaNode.GetAttribute("type");
                    ((ReceptionChatMedia)chat).MedialUrl = mediaUrl;
                    ((ReceptionChatMedia)chat).MediaType = mediaType;
                }
                else if (chatType == enum_ChatType.UserStatus)
                {
                    var userStatusNode = ext_element.SelectSingleElement("msgObj");
                    var userId = userStatusNode.GetAttribute("userId");
                    var status = userStatusNode.GetAttribute("status");
                    ((ReceptionChatUserStatus)chat).User = dalMembership.FindById(new Guid(userId));
                    ((ReceptionChatUserStatus)chat).Status = (enum_UserStatus)Enum.Parse(typeof(enum_UserStatus), status, true); ;
                }
            }
            catch (Exception e)
            {
                ilog.Error(e.Message);
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
            msg.Id = chat.Id != Guid.Empty ? chat.Id.ToString() : Guid.NewGuid().ToString();
            //     msg.From = new agsXMPP.Jid(chat.From.Id + "@" + server);

            try
            {
                IMUserStatus toUserStatus = BLLIMUserStatus.GetIMUSByUserId(chat.To.Id);
                msg.To = new agsXMPP.Jid(chat.To.Id + "@" + server + "/" + toUserStatus.ClientName);//发送对象
            }
            catch (Exception e)
            {
                ilog.Error(e.Message);
            }

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
                    var extMedia = new agsXMPP.Xml.Dom.Element("msgObj");
                    extMedia.SetAttribute("url", mediaUrl);
                    extMedia.SetAttribute("type", mediaType);
                    extNode.AddChild(extMedia);
                    break;
                case enum_ChatType.UserStatus:
                    extNode.Namespace = "ihelper:chat:userstatus";

                    var user = ((ReceptionChatUserStatus)chat).User;
                    var status = ((ReceptionChatUserStatus)chat).Status;
                    var extStatus = new agsXMPP.Xml.Dom.Element("msgObj");
                    extStatus.SetAttribute("userId", user.Id.ToString());
                    extStatus.SetAttribute("status", status.ToString());
                    extNode.AddChild(extStatus);
                    break;
                case enum_ChatType.ReAssign:
                    extNode.Namespace = "ihelper:notice:cer:change";
                    var cerObj = new agsXMPP.Xml.Dom.Element("cerObj");
                    cerObj.SetAttribute("userID", ((ReceptionChatReAssign)chat).ReAssignedCustomerService.Id.ToString());
                    cerObj.SetAttribute("alias", ((ReceptionChatReAssign)chat).ReAssignedCustomerService.DisplayName);
                    cerObj.SetAttribute("imgUrl", ((ReceptionChatReAssign)chat).ReAssignedCustomerService.AvatarUrl);
                    extNode.AddChild(cerObj);
                    msg.SetAttribute("type", "headline");
                    break;
                case enum_ChatType.Notice:
                    ilog.Error("没有处理该消息." + msg.ToString());
                    break;
                case enum_ChatType.PushedService:
                    extNode.Namespace = "ihelper:chat:orderobj";
                    var svcObj = new agsXMPP.Xml.Dom.Element("svcObj");
                    ReceptionChatPushService chatPushService = (ReceptionChatPushService)chat;
                    ServiceOrderPushedService service = chatPushService.PushedServices[0];
                    svcObj.SetAttribute("svcID", service.OriginalService.Id.ToString());
                    svcObj.SetAttribute("name", service.OriginalService.Name);
                    svcObj.SetAttribute("type", service.OriginalService.ServiceType.ToString());
                    svcObj.SetAttribute("startTime", service.TargetTime.ToString("yyyyMMddHHmmss"));
                    extNode.AddChild(svcObj);

                    /* "storeObj": {
                     "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                     "alias": "望海国际",
                     "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao_MD.png"
                 }*/
                    var storeObj = new agsXMPP.Xml.Dom.Element("storeObj");

                    storeObj.SetAttribute("userID", service.OriginalService.Business.Id.ToString());
                    storeObj.SetAttribute("alias", service.OriginalService.Business.Name);
                    storeObj.SetAttribute("imgUrl", service.OriginalService.Business.BusinessAvatar.ImageName);
                    extNode.AddChild(storeObj);
                    break;
            }

            return msg;



        }

    }
}
