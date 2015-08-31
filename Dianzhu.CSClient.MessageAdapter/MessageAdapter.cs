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

            string messageType = message.GetAttribute("message_type");
            Model.Enums.enum_ChatType chatType;
            bool isChatType = Enum.TryParse(messageType, out  chatType);
            if (!isChatType)
            {
                chatType = enum_ChatType.Text;
            }
            string attributeErr;
            bool ensureAttributes = EnsureMessageAttribute(chatType, message, out attributeErr);
            if (!ensureAttributes)
            {
                throw new ArgumentException(attributeErr);
            }
            ReceptionChat chat = ReceptionChat.Create(chatType);
            DZMembership from = bllMember.GetUserByName(PHSuit.StringHelper.EnsureNormalUserName(message.From.User));
            DZMembership to = bllMember.GetUserByName(PHSuit.StringHelper.EnsureNormalUserName(message.To.User));
            chat.From = from;
            chat.To = to;
            chat.MessageBody = message.Body;
            chat.SavedTime = DateTime.Now;
            string chatText = message.Body;
            switch (chatType)
            {
                case  enum_ChatType.PushedService:
                case  enum_ChatType.ConfirmedService:
                    ReceptionChatService chatService = (ReceptionChatService)chat;
                    string strServiceId = string.Empty;
                    bool hasServiceId = message.HasAttribute("service_id");
                    if (hasServiceId)
                    {
                        strServiceId = message.GetAttribute("service_id");
                        chatService.Service = bllDZService.GetOne(new Guid(strServiceId));
                    }
                    else {
                        chatService.ServiceName = message.GetAttribute("ServiceName");
                        chatService.ServiceDescription = message.GetAttribute("ServiceDescription");
                        chatService.ServiceBusinessName = message.GetAttribute("ServiceBusinessName");
                        chatService.UnitPrice =Convert.ToDecimal( message.GetAttribute("UnitPrice"));
                        chatService.ServiceUrl =  message.GetAttribute("ServiceUrl");
  
                    }


                    break;
                case enum_ChatType.Order:
                    Guid orderId = new Guid(message.GetAttribute("order_id"));
                    ServiceOrder order = bllOrder.GetOne(orderId);
                    ReceptionChatOrder chatOrder = (ReceptionChatOrder)chat;
                    chatOrder.ServiceOrder = order;
                    break;
            }
            return chat;
        }
        /// <summary>
        /// 客服发送消息
        /// 
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public  Message ChatToMessage(Model.ReceptionChat chat)
        {
            string server=System.Configuration.ConfigurationManager.AppSettings["server"];
            Message msg = new Message();
            msg.SetAttribute("MessageType", chat.ChatType.ToString());
            msg.To = PHSuit.StringHelper.EnsureOpenfireUserName(chat.To.UserName) + "@" + server;
            msg.Body = chat.MessageBody;
            if (!string.IsNullOrEmpty(chat.MessageMediaUrl))
            {
                msg.SetAttribute("MediaUrl", chat.MessageMediaUrl);
            }
            if (chat is ReceptionChatService)
            {
               ReceptionChatService chatService = (ReceptionChatService)chat;
               if (chatService.Service != null)
               {
                   msg.SetAttribute("ServiceId", chatService.Service.Id.ToString());
                   msg.SetAttribute("ServiceName", chatService.Service.Name);
                   msg.SetAttribute("ServiceDescription", chatService.Service.Description);
                   msg.SetAttribute("ServiceBusinessName", chatService.Service.Business.Name);
                   msg.SetAttribute("ServiceUnitPrice", chatService.Service.UnitPrice.ToString());

               }
               else
               {
                   msg.SetAttribute("ServiceName",msg.GetAttribute("ServiceName"));
                   msg.SetAttribute("ServiceDescription", msg.GetAttribute("ServiceDescription"));
                   msg.SetAttribute("ServiceBusinessName", msg.GetAttribute("ServiceBusinessName"));
                   msg.SetAttribute("ServiceUnitPrice", msg.GetAttribute("ServiceUnitPrice"));

               }
               
            }
            if (chat is ReceptionChatOrder)
            {
                ReceptionChatOrder chatOrder = (ReceptionChatOrder)chat;
                msg.SetAttribute("OrderId", chatOrder.ServiceOrder.Id.ToString());
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
            switch (chatType)
            {
                case enum_ChatType.BeginPay:

                    if (!msg.HasAttribute("order_id"))
                    {
                        sb.Append("错误.缺少order_id");
                    }

                    break;
                case enum_ChatType.ConfirmedService:
                case enum_ChatType.PushedService:
                    if (!msg.HasAttribute("service_id"))
                    {
                        if (!msg.HasAttribute("ServiceName"))
                        {
                            hasAttributes = false;
                            sb.Append("错误.缺少ServiceName");
                        }
                        
                        if (!msg.HasAttribute("ServiceDescription"))
                        {
                            hasAttributes = false;
                            sb.Append("错误.缺少ServiceDescription");
                        }
                        if (!msg.HasAttribute("ServiceBusinessName"))
                        {
                            hasAttributes = false;
                            sb.Append("错误.缺少ServiceBusinessName");
                        }
                        
                        if (!msg.HasAttribute("UnitPrice"))
                        {
                            hasAttributes = false;
                            sb.Append("错误.缺少UnitPrice");
                        }
                        if (!msg.HasAttribute("ServiceUrl"))
                        {
                            hasAttributes = false;
                            sb.Append("错误.缺少ServiceUrl");
                        }
                         
                    }

                    break;
               
                default: break;
            }

            errMsg = sb.ToString();
            return hasAttributes;
        }
    }
}
