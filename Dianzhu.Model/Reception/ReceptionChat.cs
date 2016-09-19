using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /*
        与文档 Xmpp通信协议 定义的消息/通知格式一一对应.
        例外情况已在具体子类中说明.
    */


    /// <summary>
    ///文本消息
    /// </summary>
    public class ReceptionChat : DDDCommon.Domain.Entity<Guid>
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
            enum_ChatType chatType, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
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


            if (FromResource == enum_XmppResource.YDBan_CustomerService || ToResource == enum_XmppResource.YDBan_CustomerService)
            {
                this.ChatTarget = enum_ChatTarget.cer;
            }
            else if (FromResource == enum_XmppResource.YDBan_Store || ToResource == enum_XmppResource.YDBan_Store)
            {
                this.ChatTarget = enum_ChatTarget.store;
            }
            else
            {
                this.ChatTarget = enum_ChatTarget.all;
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

        public virtual enum_ChatType ChatType { get; protected internal set; }

        //移除对dzmembership的依赖
        public virtual string FromId { get; protected internal set; }
        public virtual string ToId { get; protected internal set; }

        public virtual string MessageBody { get; protected internal set; }//消息的内容


        public virtual Enums.enum_ChatTarget ChatTarget { get; protected internal set; } //聊天状态，接待方是平台客服还是商家客服
        public virtual enum_XmppResource FromResource { get; protected internal set; }//from 的资源名
        public virtual enum_XmppResource ToResource { get; protected internal set; }//to 的资源名

        public virtual bool IsfromCustomerService
        {
            get
            {

                return FromResource == enum_XmppResource.YDBan_CustomerService;
            }
        }
        public virtual void SetReceiveTime(DateTime receiveTime)
        {
            this.ReceiveTime = receiveTime;
        }



    }

    /// <summary>
    /// IM,信息,多媒体
    /// </summary>
    public class ReceptionChatMedia : ReceptionChat
    {
        public ReceptionChatMedia() { }
        public ReceptionChatMedia(string mediaurl, string mediatype,
         Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Chat, resourceFrom, resourceTo)
        {
            this.MedialUrl = mediaurl;
            this.MediaType = mediatype;
        }

        public virtual void SetMediaUrl(string mediaUrl)
        {
            this.MedialUrl = mediaUrl;
        }

        public virtual string MedialUrl { get; protected internal set; }
        public virtual string MediaType { get; protected internal set; }
    }

    /// <summary>
    /// IM,通知,系统通知(系统公告?广播)
    /// </summary>
    public class ReceptionChatNoticeSys : ReceptionChat
    {
        public ReceptionChatNoticeSys() { }
        public ReceptionChatNoticeSys(Guid id,
           string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
           : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {

        }

    }


    /// <summary>
    /// IM,通知,订单状态变更通知
    /// ihelper:notice:order 订单状态通知.
    /// </summary>
    public class ReceptionChatNoticeOrder : ReceptionChat
    {
        public ReceptionChatNoticeOrder() { }
        public ReceptionChatNoticeOrder(string orderTilte, enum_OrderStatus orderStatus, string orderType,
             Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {
            this.OrderTitle = orderTilte;
            this.CurrentStatus = orderStatus;
            this.OrderType = orderType;
        }
        public virtual string OrderTitle { get; protected internal set; }
        public virtual enum_OrderStatus CurrentStatus { get; protected internal set; }
        public virtual string OrderType { get; protected internal set; }

    }

    /// <summary>
    /// IM,通知,客服更换
    /// </summary>
    public class ReceptionChatReAssign : ReceptionChat
    {
        public ReceptionChatReAssign() { }
        public ReceptionChatReAssign(string reAssignedCustomerServiceId, string csAlias, string csAvatar,
            Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {
            this.ReAssignedCustomerServiceId = reAssignedCustomerServiceId;
            this.CSAlias = csAlias;
            this.CSAvatar = csAvatar;
        }
        /// <summary>
        /// 重新分配的客服
        /// </summary>
        public virtual string ReAssignedCustomerServiceId { get; protected internal set; }
        public virtual string CSAlias { get; protected internal set; }
        public virtual string CSAvatar { get; protected internal set; }
    }


    /// <summary>
    /// IM,通知,促销通知.
    /// </summary>
    public class ReceptionChatNoticePromote : ReceptionChat
    {
        public ReceptionChatNoticePromote() { }
        public ReceptionChatNoticePromote(string promotionUrl,
          Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {
            this.PromotionUrl = promotionUrl;
        }
        /// <summary>
        /// 重新分配的客服
        /// </summary>
        public virtual string PromotionUrl { get; protected internal set; }
    }

    /// <summary>
    /// IM,通知,客服离线. 不需要指定离线的客服id. 客户端收到该消息后会再次申请客服.
    /// </summary>
    public class ReceptionChatNoticeCustomerServiceOffline : ReceptionChat
    {
        public ReceptionChatNoticeCustomerServiceOffline() { }
        public ReceptionChatNoticeCustomerServiceOffline(
           Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {

        }
    }

    /// <summary>
    /// IM,通知,客服上线. 不需要指定上线的客服id. 客户端收到该消息后会再次申请客服,避免客户端一直被未上线客服或者点点接待.
    /// </summary>
    public class ReceptionChatNoticeCustomerServiceOnline : ReceptionChat
    {
        public ReceptionChatNoticeCustomerServiceOnline() { }
        public ReceptionChatNoticeCustomerServiceOnline(
          Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {

        }
    }


    /// <summary>
    /// IM,通知, 用户状态改变. 文档内没有的.
    /// </summary>
    public class ReceptionChatUserStatus : ReceptionChat
    {
        public ReceptionChatUserStatus() { }
        public ReceptionChatUserStatus(string userId, enum_UserStatus userStatus,
           Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {
            this.UserId = userId;
            this.Status = userStatus;
        }

        public virtual string UserId { get; protected internal set; }//状态发生变化的用户
        public virtual enum_UserStatus Status { get; protected internal set; }//用户状态
    }


    /// <summary>
    /// 推送的服务消息,供用户选择
    /// </summary>
    public class ReceptionChatPushService : ReceptionChat
    {
        public ReceptionChatPushService() { }
        public ReceptionChatPushService(IList<PushedServiceInfo> serviceInfos,
            Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Chat, resourceFrom, resourceTo)
        {
            this.ServiceInfos = serviceInfos;
        }

        public virtual IList<PushedServiceInfo> ServiceInfos { get; protected internal set; }
    }

    /// <summary>
    /// IM ,通知,新的草稿单
    /// </summary>
    public class ReceptionChatNoticeNewOrder : ReceptionChat
    {
        public ReceptionChatNoticeNewOrder() { }
        public ReceptionChatNoticeNewOrder(
           Guid id, string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, enum_ChatType.Notice, resourceFrom, resourceTo)
        {

        }
    }



    /// <summary>
    /// 消息推送的服务内容
    /// </summary>
    public class PushedServiceInfo
    {
        public PushedServiceInfo() { }
        public PushedServiceInfo(string serviceId, string servicename,
            string servicetype, string servicestarttime,
            string serviceendtime, string storeuserid,
            string storealias, string storeavatar
            )
        {
            this.ServiceId = serviceId;
            this.ServiceName = servicename;
            this.ServiceType = servicetype;
            this.ServiceStartTime = servicestarttime;
            this.ServiceEndTime = serviceendtime;
            this.StoreUserId = storeuserid;
            this.StoreAlias = storealias;
            this.StoreAvatar = storeavatar;
        }
        public virtual string ServiceId { get; protected internal set; }
        public virtual string ServiceName { get; protected internal set; }
        public virtual string ServiceType { get; protected internal set; }
        public virtual string ServiceStartTime { get; protected internal set; }
        public virtual string ServiceEndTime { get; protected internal set; }
        public virtual string StoreUserId { get; protected internal set; }
        public virtual string StoreAlias { get; protected internal set; }
        public virtual string StoreAvatar { get; protected internal set; }


        /*
         <svcObj svcID = "SBWA-DKJI-OFNS-SDLK" name="小飞侠" type="设计>平面设计" startTime="20151116120000" endTime="20151117120000"></svcObj>
        <storeObj userID = "SBWA-DKJI-OFNS-SDLK" alias="望海国际" imgUrl="http://i-guess.cn/ihelp/userimg/issumao_MD.png"></storeObj>
        */

    }
}
