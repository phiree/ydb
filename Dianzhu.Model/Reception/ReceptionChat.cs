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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="messageBody"></param>
        /// <param name="sessionId"></param>
        /// <param name="resourceFrom"></param>
        /// <param name="resourceTo"></param>
        public ReceptionChat(string from, string to, string messageBody, string sessionId,
            enum_ChatType chatType, 
            enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
        {
            //默认值
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
        public ReceptionChat(string from, string to, string messageBody, string sessionId,

           enum_XmppResource resourceFrom, enum_XmppResource resourceTo) : this(from, to, messageBody, sessionId, enum_ChatType.Chat,   resourceFrom, resourceTo)
        {
        }
        /// <summary>
        /// 所属的回话ID, 目前等于 orderid;
        /// </summary>
        public string SessionId { get; private set; }
        //保存的时间, 作为排序依据.
        public virtual DateTime SavedTime { get; private set; }
        public virtual double SavedTimestamp { get; private set; }
        public virtual DateTime SendTime { get; private set; }//发送时间
        public virtual DateTime ReceiveTime { get; private set; }//接收时间

        public virtual enum_ChatType ChatType { get; private set; }

        //移除对dzmembership的依赖
        public virtual string FromId { get; set; }
        public virtual string ToId { get; set; }

        public virtual string MessageBody { get; private set; }//消息的内容


        public virtual Enums.enum_ChatTarget ChatTarget { get; private set; } //聊天状态，接待方是平台客服还是商家客服
        public virtual enum_XmppResource FromResource { get; private set; }//from 的资源名
        public virtual enum_XmppResource ToResource { get; private set; }//to 的资源名

        public bool IsfromCustomerService
        {
            get {
                return FromResource == enum_XmppResource.YDBan_CustomerService;
            }
        }
        public void SetReceiveTime(DateTime receiveTime)
        {
            this.ReceiveTime = receiveTime;
        }



    }

    /// <summary>
    /// IM,信息,多媒体
    /// </summary>
    public class ReceptionChatMedia : ReceptionChat
    {
        public ReceptionChatMedia(string mediaurl, string mediatype,
          string from, string to, string messageBody, string sessionId, enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Chat,    resourceFrom, resourceTo)
        {
            this.MedialUrl = mediaurl;
            this.MediaType = mediatype;
        }

        public void SetMediaUrl(string mediaUrl)
        {
            this.MedialUrl = mediaUrl;
        }
       
        public virtual string MedialUrl { get; private set; }
        public virtual string MediaType { get; private set; }
    }

    /// <summary>
    /// IM,通知,系统通知(系统公告?广播)
    /// </summary>
    public class ReceptionChatNoticeSys : ReceptionChat
    {
        public ReceptionChatNoticeSys(
           string from, string to, string messageBody, string sessionId,  enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
           : base(from, to, messageBody, sessionId, enum_ChatType.Notice,   resourceFrom, resourceTo)
        {

        }

    }


    /// <summary>
    /// IM,通知,订单状态变更通知
    /// ihelper:notice:order 订单状态通知.
    /// </summary>
    public class ReceptionChatNoticeOrder : ReceptionChat
    {
        public ReceptionChatNoticeOrder(string orderTilte, enum_OrderStatus orderStatus, string orderType,
              string from, string to, string messageBody, string sessionId,  enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Notice,   resourceFrom, resourceTo)
        {
            this.OrderTitle = orderTilte;
            this.CurrentStatus = orderStatus;
            this.OrderType = orderType;
        }
        public string OrderTitle { get; private set; }
        public enum_OrderStatus CurrentStatus { get; private set; }
        public string OrderType { get; private set; }

    }

    /// <summary>
    /// IM,通知,客服更换
    /// </summary>
    public class ReceptionChatReAssign : ReceptionChat
    {
        public ReceptionChatReAssign(string reAssignedCustomerServiceId, string csAlias, string csAvatar,
            string from, string to, string messageBody, string sessionId,  enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Notice,  resourceFrom, resourceTo)
        {
            this.ReAssignedCustomerServiceId = reAssignedCustomerServiceId;
            this.CSAlias = csAlias;
            this.CSAvatar = csAvatar;
        }
        /// <summary>
        /// 重新分配的客服
        /// </summary>
        public virtual string ReAssignedCustomerServiceId { get; private set; }
        public virtual string CSAlias { get; private set; }
        public virtual string CSAvatar { get; private set; }
    }


    /// <summary>
    /// IM,通知,促销通知.
    /// </summary>
    public class ReceptionChatNoticePromote : ReceptionChat
    {
        public ReceptionChatNoticePromote(string promotionUrl,
            string from, string to, string messageBody, string sessionId,   enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Notice,   resourceFrom, resourceTo)
        {
            this.PromotionUrl = promotionUrl;
        }
        /// <summary>
        /// 重新分配的客服
        /// </summary>
        public virtual string PromotionUrl { get; private set; }
    }

    /// <summary>
    /// IM,通知,客服离线. 不需要指定离线的客服id. 客户端收到该消息后会再次申请客服.
    /// </summary>
    public class ReceptionChatNoticeCustomerServiceOffline : ReceptionChat
    {
        public ReceptionChatNoticeCustomerServiceOffline(
            string from, string to, string messageBody, string sessionId,   enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Notice,   resourceFrom, resourceTo)
        {

        }
    }

    /// <summary>
    /// IM,通知,客服上线. 不需要指定上线的客服id. 客户端收到该消息后会再次申请客服,避免客户端一直被未上线客服或者点点接待.
    /// </summary>
    public class ReceptionChatNoticeCustomerServiceOnline : ReceptionChat
    {
        public ReceptionChatNoticeCustomerServiceOnline(
            string from, string to, string messageBody, string sessionId,  enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Notice,  resourceFrom, resourceTo)
        {

        }
    }


    /// <summary>
    /// IM,通知, 用户状态改变. 文档内没有的.
    /// </summary>
    public class ReceptionChatUserStatus : ReceptionChat
    {
        public ReceptionChatUserStatus(string userId, enum_UserStatus userStatus,
              string from, string to, string messageBody, string sessionId,  enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Notice,  resourceFrom, resourceTo)
        {
            this.UserId = userId;
            this.Status = userStatus;
        }

        public virtual string UserId { get; set; }//状态发生变化的用户
        public virtual enum_UserStatus Status { get; set; }//用户状态
    }


    /// <summary>
    /// 推送的服务消息,供用户选择
    /// </summary>
    public class ReceptionChatPushService : ReceptionChat
    {
        public ReceptionChatPushService(IList<PushedServiceInfo> serviceInfos,
             string from, string to, string messageBody, string sessionId,  enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Chat,   resourceFrom, resourceTo)
        {
            this.ServiceInfos = serviceInfos;
        }

        public virtual IList<PushedServiceInfo> ServiceInfos { get; private set; }
    }

    /// <summary>
    /// IM ,通知,新的草稿单
    /// </summary>
    public class ReceptionChatNoticeNewOrder : ReceptionChat
    {
        public ReceptionChatNoticeNewOrder(
            string from, string to, string messageBody, string sessionId,  enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
            : base(from, to, messageBody, sessionId, enum_ChatType.Notice,  resourceFrom, resourceTo)
        {

        }
    }



    /// <summary>
    /// 消息推送的服务内容
    /// </summary>
    public class PushedServiceInfo
    {
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
        public string ServiceId { get; private set; }
        public string ServiceName { get; private set; }
        public string ServiceType { get; private set; }
        public string ServiceStartTime { get; private set; }
        public string ServiceEndTime { get; private set; }
        public string StoreUserId { get; private set; }
        public string StoreAlias { get; private set; }
        public string StoreAvatar { get; private set; }


        /*
         <svcObj svcID = "SBWA-DKJI-OFNS-SDLK" name="小飞侠" type="设计>平面设计" startTime="20151116120000" endTime="20151117120000"></svcObj>
        <storeObj userID = "SBWA-DKJI-OFNS-SDLK" alias="望海国际" imgUrl="http://i-guess.cn/ihelp/userimg/issumao_MD.png"></storeObj>
        */

    }
}
