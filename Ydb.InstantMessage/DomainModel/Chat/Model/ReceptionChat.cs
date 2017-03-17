using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.Common.Domain;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /*
        与文档 Xmpp通信协议 定义的消息/通知格式一一对应.
        例外情况已在具体子类中说明.
    */


    /// <summary>
    ///文本消息
    /// </summary>
    public class ReceptionChat : Entity<Guid>
    {
        public ReceptionChat() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="messageBody"></param>
        /// <param name="sessionId"></param>
        /// <param name="resourceFrom"></param>
        /// <param name="resourceTo"></param>
        public ReceptionChat(Guid id, string from, string to, string messageBody, string sessionId,
             ChatType chatType, XmppResource resourceFrom, XmppResource resourceTo)
        {
            //默认值
            this.Id = id;
            SavedTime = DateTime.Now;
            SavedTimestamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            //构造传入.
            this.FromId = from;
            this.ToId = to;
            this.MessageBody = messageBody;
            this.FromResource = resourceFrom;
            this.ToResource = resourceTo;
            this.SessionId = sessionId;
            this.ChatType = chatType;
            this.IsReaded = true;



            if (FromResource == XmppResource.YDBan_CustomerService || ToResource == XmppResource.YDBan_CustomerService)
            {
                this.ChatTarget = ChatTarget.cer;
            }
            else if (FromResource == XmppResource.YDBan_Store || ToResource == XmppResource.YDBan_Store)
            {
                this.ChatTarget = ChatTarget.store;
            }
            else
            {
                this.ChatTarget = ChatTarget.all;
            }
        }

        /// <summary>
        /// 所属的回话ID, 目前等于 orderid;
        /// </summary>
        public virtual string SessionId { get; protected internal set; }
        //保存的时间, 作为排序依据.
        public virtual DateTime SavedTime { get; protected internal set; }
        public virtual double SavedTimestamp { get; protected internal set; }
        public virtual DateTime SendTime { get; protected internal set; }//发送时间
        public virtual DateTime ReceiveTime { get; protected internal set; }//接收时间

        public virtual ChatType ChatType { get; protected internal set; }

        //移除对dzmembership的依赖
        public virtual string FromId { get; protected internal set; }
        public virtual string ToId { get; protected internal set; }

        public virtual string MessageBody { get; protected internal set; }//消息的内容


        public virtual ChatTarget ChatTarget { get; protected internal set; } //聊天状态，接待方是平台客服还是商家客服
        public virtual XmppResource FromResource { get; protected internal set; }//from 的资源名
        public virtual string FromResourceName
        {
            get
            {
                string strName = "";
                switch (FromResource)
                {
                    case XmppResource.YDBan_CustomerService:
                        strName = "客服";
                        break;
                    case XmppResource.YDBan_User:
                        strName = "用户";
                        break;
                    case XmppResource.YDBan_Store:
                        strName = "商户";
                        break;
                    case XmppResource.YDBan_DianDian:
                        strName = "点点";
                        break;
                    case XmppResource.YDBan_Staff:
                        strName = "员工";
                        break;
                    case XmppResource.YDBan_IMServer:
                        strName = "推送";
                        break;
                }
                return strName;
            }
        }

        public virtual XmppResource ToResource { get; protected internal set; }//to 的资源名

        public virtual bool IsReaded { get; protected internal set; }
        public virtual bool IsfromCustomerService
        {
            get
            {

                return FromResource == XmppResource.YDBan_CustomerService;
            }
        }
        public virtual void SetReceiveTime(DateTime receiveTime)
        {
            this.ReceiveTime = receiveTime;
        }
        public virtual void SetUnread()
        {
            this.IsReaded = false;
        }
        public virtual void SetReaded()
        {
            this.IsReaded = true;
        }

        public virtual ReceptionChatDto ToDto()
        {
            ReceptionChatDto dto = new ReceptionChatDto();

            dto.Id = Id;
            dto.SessionId = SessionId;
            dto.SavedTime = SavedTime;
            dto.SavedTimestamp = SavedTimestamp;
            dto.SendTime = SendTime;
            dto.ReceiveTime = ReceiveTime;
            dto.ChatType = ChatType.ToString();
            dto.FromId = FromId;
            dto.ToId = ToId;
            dto.MessageBody = MessageBody;
            dto.ChatTarget = ChatTarget.ToString();
            dto.FromResource = FromResource.ToString();
            dto.FromResourceName = FromResourceName;
            dto.ToResource = ToResource.ToString();
            dto.IsReaded = IsReaded;
            dto.IsfromCustomerService = IsfromCustomerService;

            return dto;
        }


    }

    /// <summary>
    /// IM,通知,系统通知(系统公告?广播)
    /// </summary>
    public class ReceptionChatNoticeSys : ReceptionChat
    {
        public ReceptionChatNoticeSys() { }
        public ReceptionChatNoticeSys(Guid id,
           string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
           : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {

        }

    }

    /// <summary>
    /// IM,通知,促销通知.
    /// </summary>
    public class ReceptionChatNoticePromote : ReceptionChat
    {
        public ReceptionChatNoticePromote() { }
        public ReceptionChatNoticePromote(string promotionUrl,
          Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {
            this.PromotionUrl = promotionUrl;
        }
        /// <summary>
        /// 重新分配的客服
        /// </summary>
        public virtual string PromotionUrl { get; protected internal set; }
    }

    /// <summary>
    /// IM,通知, 用户状态改变. 文档内没有的.
    /// </summary>
    public class ReceptionChatUserStatus : ReceptionChat
    {
        public ReceptionChatUserStatus() { }
        public ReceptionChatUserStatus(string userId, string userStatus,
           Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {
            this.UserId = userId;
            this.Status = userStatus;
        }

        public virtual string UserId { get; protected internal set; }//状态发生变化的用户
        public virtual string Status { get; protected internal set; }//用户状态
    }

    /// <summary>
    /// IM ,通知,新的草稿单
    /// </summary>
    public class ReceptionChatNoticeNewOrder : ReceptionChat
    {
        public ReceptionChatNoticeNewOrder() { }
        public ReceptionChatNoticeNewOrder(
           Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {

        }
    }
}
