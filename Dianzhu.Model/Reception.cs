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
        }
        public  static ReceptionChat Create(Enums.enum_ChatType chatType)
        {
            
            switch (chatType)
            {
                case Enums.enum_ChatType.PushedService:
                case Enums.enum_ChatType.ConfirmedService:
                    return new ReceptionChatService();
                case Enums.enum_ChatType.Order: return new ReceptionChatOrder();
                default:
                    return new ReceptionChat();
            }
        }
        public virtual Guid Id { get; set; }
        //保存的时间, 作为排序依据.
        public virtual DateTime SavedTime { get; set; }
        public virtual DateTime SendTime { get; set; }
        public virtual  DateTime ReceiveTime { get; set; }
        public virtual  DZMembership From { get; set; }
        public virtual  DZMembership To { get; set; }
        public virtual  string MessageBody { get; set; }
        public virtual Enums.enum_ChatType ChatType { get; set; }
        /// <summary>
        /// 消息中媒体文件的地址,多个媒体文件用分号风格.
        /// </summary>
        public virtual string MessageMediaUrl { get; set; }
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
            else {
                return BuildLine();
            }
        }
         

    }
    public class ReceptionChatService:ReceptionChat
    {
        
        public virtual DZService Service { get; set; }
        //如果是系统外的服务
        public virtual string ServiceName { get; set; }
        public virtual string ServiceDescription { get; set; }
        public virtual string ServiceBusinessName { get; set; }
        public virtual string ServiceUrl { get; set; }
        public virtual decimal UnitPrice { get; set; }
    }
    public class ReceptionChatOrder : ReceptionChat
    {
        
        public ServiceOrder ServiceOrder { get; set; }
    }
    
}
