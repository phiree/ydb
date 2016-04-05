using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 接待记录基类, 记录一次接待情况
    /// </summary>
    public class ReceptionBase
    {
        public ReceptionBase()
        {
            ChatHistory = new List<ReceptionChat>();
            IsComplete = false;
            TimeBegin = DateTime.Now;
        }
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 接待发起方(
        /// 客户-客服时, 客户是发起方,客服是接收方
        /// 客户-商家时, 客户是发起方,商家是接收方
        /// 客服-商家是, 客服是发起方,商家是接收方
        /// </summary>
        public virtual DZMembership Sender { get; set; }
        /// <summary>
        /// 接收方
        /// </summary>
        public virtual DZMembership Receiver { get; set; }
        public virtual DateTime TimeBegin { get; set; }
        public virtual DateTime TimeEnd { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public virtual int SatisfactionRate { get; set; }
        /// <summary>
        /// 链条记录
        /// </summary>
        public virtual IList<ReceptionChat> ChatHistory { get; set; }

        public virtual bool IsComplete { get; set; }
        

    }
    public class ReceptionCustomer : ReceptionBase
    {
        public ReceptionCustomer()
        {
            PushedServices = new List<DZService>();

        }
        /// <summary>
        /// 推送的服务
        /// </summary>
        public virtual IList<DZService> PushedServices { get; set; }
        public virtual DZMembership TransferFrom { get; set; }
        /// <summary>
        /// 确认的订单
        /// </summary>
        public virtual IList<ServiceOrder> Orders { get; set; }

    }

    public class ReceptionBusiness : ReceptionBase
    {
        /// <summary>
        /// 商家和 客户的聊天记录.
        /// 针对哪个订单的接待.
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
    }

    /// <summary>
    /// 接待中的聊天记录.
    /// </summary>
    public class ReceptionChat
    {
        public ReceptionChat()
        {
            ChatType = Enums.enum_ChatType.Text;
            SavedTime = DateTime.Now;
        }
        public static ReceptionChat Create(Enums.enum_ChatType chatType)
        {
            ReceptionChat chat;
            switch (chatType)
            {
               
                case Enums.enum_ChatType.Media:
                    chat = new ReceptionChatMedia();
                    break;
                case Enums.enum_ChatType.Notice:
                    chat = new ReceptionChatNotice();
                    break;
                case Enums.enum_ChatType.UserStatus:
                    chat = new ReceptionChatUserStatus();
                    break;
                default:
                    chat= new ReceptionChat();
                    break;
            }
            chat.ChatType = chatType;
            return chat;
        }
        public virtual Guid Id { get; set; }
        public virtual ReceptionBase Reception { get; set; }//接待记录，多个聊天的集合
        //保存的时间, 作为排序依据.
        public virtual DateTime SavedTime { get; set; }
        public virtual DateTime SendTime { get; set; }//发送时间
        public virtual DateTime ReceiveTime { get; set; }//接收时间
        public virtual DZMembership From { get; set; }//发送方
        public virtual DZMembership To { get; set; }//接收方
        public virtual string MessageBody { get; set; }//消息的内容
        public virtual Enums.enum_ChatType ChatType { get; set; }
        public virtual ServiceOrder ServiceOrder { get; set;}
        public virtual int Version { get; set; }//版本号
        public virtual Enums.enum_ChatTarget ChatTarget { get; set; } //聊天状态，接待方是平台客服还是商家客服
        public virtual enum_XmppResource FromResource { get; set; }//from 的资源名
        public virtual enum_XmppResource ToResource { get; set; }//to 的资源名
        /// <summary>
        /// 消息中媒体文件的地址,多个媒体文件用分号风格.
        /// </summary>

        /// <summary>
        /// 消息中的服务信息
        /// </summary>
        /// <returns></returns>
        public virtual string BuildLine()
        {
            return SavedTime.ToShortTimeString() + " " + From.UserName + ":    " + MessageBody;
        }
        public virtual string BuildLine(DZMembership from)
        {
            if (from == this.From)
                return MessageBody + "   " + From.UserName + " " + SendTime.ToString("yyyy-MM-dd HH:mm:ss");
            else
            {
                return BuildLine();
            }
        }


    }

    /// <summary>
    /// 记录点点的接待记录
    /// </summary>
    public class ReceptionChatDD
    {
        public virtual Guid Id { get; set; }
        public virtual ReceptionBase Reception { get; set; }//接待记录，多个聊天的集合
        public virtual DateTime SavedTime { get; set; }//保存的时间, 作为排序依据.
        public virtual DateTime SendTime { get; set; }//发送时间
        public virtual DateTime ReceiveTime { get; set; }//接收时间
        public virtual DZMembership From { get; set; }//发送方
        public virtual DZMembership To { get; set; }//接收方
        public virtual string MessageBody { get; set; }//消息的内容
        public virtual Enums.enum_ChatType ChatType { get; set; }
        public virtual ServiceOrder ServiceOrder { get; set; }
        public virtual int Version { get; set; }//版本号
        public virtual bool IsCopy { get; set; }//是否复制过该聊天记录
        public virtual string MedialUrl { get; set; }
        public virtual string MediaType { get; set; }
        public virtual Enums.enum_ChatTarget ChatTarget { get; set; }//聊天状态，接待方是平台客服还是商家客服
        public virtual enum_XmppResource FromResource { get; set; }//from 的资源名,to 都是发给点点的，忽略不存
    }

    /// <summary>
    /// 多媒体消息,
    ///
    public class ReceptionChatMedia : ReceptionChat
    {
       
        public virtual string MedialUrl { get; set; }
        public virtual string MediaType { get; set; }
    }
    /// <summary>
    /// 重新分配客服的消息
    /// </summary>
    public class ReceptionChatReAssign : ReceptionChat
    {
        /// <summary>
        /// 重新分配的客服
        /// </summary>
       public virtual DZMembership ReAssignedCustomerService { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ReceptionChatNotice : ReceptionChat
    {
        public virtual DZMembership UserObj { get; set; }
    }
    /// <summary>
    /// 用户状态改变的消息
    /// </summary>
    public class ReceptionChatUserStatus : ReceptionChat
    {
        public virtual DZMembership User { get; set; }//状态发生变化的用户
        public virtual enum_UserStatus Status { get; set; }//用户状态
    }

    /// <summary>
    /// 推送的服务消息,供用户选择
    /// </summary>
    public class ReceptionChatPushService:ReceptionChat
    {
        public ReceptionChatPushService()
        {
            this.ChatType = enum_ChatType.PushedService;
        }
        public virtual IList<ServiceOrderPushedService> PushedServices { get; set; }
    }
}
