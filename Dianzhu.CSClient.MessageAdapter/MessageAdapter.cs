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
    public class MessageAdapter : IMessageAdapter.IAdapter
    {
        DZMembershipProvider bllMember;
        BLLDZService bllDZService;
        BLLServiceOrder bllOrder;
        string IMServer;//服务器地址
        public MessageAdapter(DZMembershipProvider bllMembership,
            BLLDZService bllService,
            BLLServiceOrder bllOrder)
        {
            this.bllMember = bllMembership;
            this.bllDZService = bllService;
            this.bllOrder = bllOrder;
        }
        
          public Model.ReceptionChat MessageToChat( Message message)
        {
            
            var ext_element = message.SelectSingleElement("ext", true);
            var orderID = ext_element.SelectSingleElement("orderID").Value;
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
                default:
                    throw new Exception("未知的命名空间");
                     
            }
            ReceptionChat chat = ReceptionChat.Create(chatType);
            chat.From = bllMember.GetUserById(new Guid(message.From.User));
            chat.To = bllMember.GetUserById(new Guid(message.To.User));
            //这个逻辑放在orm002001接口里面处理.
            Guid order_ID;
            bool isValidGuid = Guid.TryParse(orderID, out order_ID);
            bool hasOrder = false;
            if (isValidGuid)
            {
                var existedServiceOrder = bllOrder.GetOne(order_ID);
                if (existedServiceOrder != null)
                {

                
                    chat.ServiceOrder = existedServiceOrder;
                    hasOrder = true;
                }
            }
            
            if (!hasOrder)
            {
               
           
                /* string serviceName,string serviceBusinessName,string serviceDescription,decimal serviceUnitPrice,string serviceUrl,
           DZMembership member,
           string targetAddress, int unitAmount, decimal orderAmount*/
                ServiceOrder newOrder =ServiceOrder.Create(
                     enum_ServiceScopeType.OSIM
                   ,string.Empty //serviceName
                   , string.Empty//serviceBusinessName
                   , string.Empty//serviceDescription
                   , 0//serviceUnitPrice
                   , string.Empty//serviceUrl
                   , chat.From //member
                   , string.Empty
                   , 0
                   ,0);
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
        /// 客服发送消息
        /// 
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public  Message ChatToMessage(Model.ReceptionChat chat,string server)
        {
            
            Message msg = new Message();
            
            msg.From = new agsXMPP.Jid(chat.From.Id + "@" + server);
            msg.To = new agsXMPP.Jid(chat.To.Id + "@" + server);
            msg.Body = chat.MessageBody;

            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);

            var extNode = new agsXMPP.Xml.Dom.Element("ext");
            var extOrderID = new agsXMPP.Xml.Dom.Element("orderID",chat.ServiceOrder==null?string.Empty:chat.ServiceOrder.Id.ToString());
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

                    break;
            }
            return msg;

        }
        /// <summary>
        /// 验证message是否包含需要的属性
        /// </summary>
        /// <param name="chatType">聊天类型</param>
        /// <param name="msg">im message</param>
        /// <param name="errMsg">错误消息</param>
        /// <returns></returns>
        private bool EnsureMessageAttribute(enum_ChatType chatType,
            agsXMPP.protocol.client.Message msg,
            out string errMsg)
        {
            bool hasAttributes = true;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("错误, 消息缺少以下属性  ");
            switch (chatType)
            {
                case enum_ChatType.BeginPay:

                    if (!msg.HasAttribute("OrderId"))
                    {
                        sb.Append(" OrderId");
                    }

                    break;
                case enum_ChatType.ConfirmedService:
                    //预订的服务数量. N小时,N天?
                    if (!msg.HasAttribute("ServiceUnitAmount"))
                    {
                        hasAttributes = false;
                        sb.Append(" ServiceUnitAmount");
                    }
                    EnsureServiceAttribute(msg, sb);
                    break;
                case enum_ChatType.PushedService:
                    EnsureServiceAttribute(msg, sb);

                    break;
               
                default: break;
            }

            errMsg = sb.ToString();
            return hasAttributes;
        }

        private bool EnsureServiceAttribute(Message msg,StringBuilder sb)
        {
            bool hasAttributes=true;
            if (!msg.HasAttribute("ServiceId"))
            {
                hasAttributes = false;
                sb.Append(" ServiceId");
            }

            if (!msg.HasAttribute("ServiceName"))
            {
                hasAttributes = false;
                sb.Append(" ServiceName");
            }

            if (!msg.HasAttribute("ServiceDescription"))
            {
                hasAttributes = false;
                sb.Append(" ServiceDescription");
            }
            if (!msg.HasAttribute("ServiceBusinessName"))
            {
                hasAttributes = false;
                sb.Append(" ServiceBusinessName");
            }

            if (!msg.HasAttribute("UnitPrice"))
            {
                hasAttributes = false;
                sb.Append(" UnitPrice");
            }
            if (!msg.HasAttribute("ServiceUrl"))
            {
                hasAttributes = false;
                sb.Append(" ServiceUrl");
            }
            return hasAttributes;
        }
    }
}
