using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.ViewModel
{
    /// <summary>
    /// Base
    /// </summary>
    public class VMChat
    {
        public VMChat(string chatId, string fromId, string fromName, DateTime savedTime,double savedTimestamp, string csAvatar, string customerAvatar, string chatBackground, bool isFromCs)
        {
            this.ChatId = chatId;
            this.FromId = fromId;
            this.FromName = fromName;
            this.SavedTime = savedTime;
            this.SavedTimestamp = savedTimestamp;
            this.CSAvatar = csAvatar;
            this.CustomerAvatar = customerAvatar;
            this.ChatBackground = chatBackground;
            this.IsFromCS = isFromCs;
        }

        public string ChatId { get; protected internal set; }
        /// <summary>
        /// 发送方Id
        /// </summary>
        public string FromId { get; protected internal set; }
        /// <summary>
        /// 发送方名称
        /// </summary>
        public string FromName { get; protected internal set; }
        /// <summary>
        /// Chat保存时间
        /// </summary>
        public DateTime SavedTime { get; protected internal set; }
        /// <summary>
        ///  chat保存的时间戳
        /// </summary>
        public double SavedTimestamp { get; protected internal set; }
        /// <summary>
        /// 客服头像uri
        /// </summary>
        public string CSAvatar { get; protected internal set; }
        /// <summary>
        /// 用户头像uri
        /// </summary>
        public string CustomerAvatar { get; protected internal set; }
        /// <summary>
        /// 文本框背景色
        /// </summary>
        public string ChatBackground { get; protected internal set; }
        /// <summary>
        /// 消息是否来自客服
        /// </summary>
        public bool IsFromCS { get; protected internal set; }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    public class VMChatText : VMChat
    {
        public string MessageBody { get; protected internal set; }
        public VMChatText(string messageBody,
            string chatId,string fromId,string fromName,DateTime savedTime,double savedTimestamp, string csAvatar,string customerAvatar,string chatBackground,bool isFromCs) 
            : base(chatId, fromId, fromName, savedTime, savedTimestamp, csAvatar, customerAvatar, chatBackground, isFromCs)
        {
            this.MessageBody = messageBody;
        }
    }

    /// <summary>
    /// 多媒体消息
    /// </summary>
    public class VMChatMedia : VMChat
    {
        public string MediaType { get; set; }
        public string MedialUrl { get; set; }
        public VMChatMedia(string mediaType,string mediaUrl,
            string chatId, string fromId, string fromName, DateTime savedTime,double savedTimestamp, string csAvatar, string customerAvatar, string chatBackground, bool isFromCs)
            : base(chatId, fromId, fromName, savedTime, savedTimestamp, csAvatar, customerAvatar, chatBackground, isFromCs)
        {
            this.MediaType = mediaType;
            this.MedialUrl = mediaUrl;
        }
    }

    /// <summary>
    /// 推送单消息
    /// </summary>
    public class VMChatPushServie : VMChat
    {
        public VMChatPushServie(string servieName,bool isVerify,string imageUrl,int creditPoint, decimal unitPrice,decimal depositAmount,string serviceMemo, 
            string chatId, string fromId, string fromName, DateTime savedTime,double savedTimestamp, string csAvatar, string customerAvatar, string chatBackground, bool isFromCs)
            :base(chatId, fromId, fromName, savedTime, savedTimestamp, csAvatar, customerAvatar, chatBackground, isFromCs)
        {
            this.ServiceName = servieName;
            this.IsVerify = isVerify;
            this.ImageUrl = imageUrl;
            this.CreditPoint = creditPoint;
            this.UnitPrice = unitPrice;
            this.DepositAmount = depositAmount;
            this.ServiceMemo = serviceMemo;
        }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; protected internal set; }
        /// <summary>
        /// 是否验证
        /// </summary>
        public bool IsVerify { get; protected internal set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; protected internal set; }
        /// <summary>
        /// 信用评价
        /// </summary>
        public int CreditPoint { get; protected internal set; }
        /// <summary>
        /// 参考价格
        /// </summary>
        public decimal UnitPrice { get; protected internal set; }
        /// <summary>
        /// 预付金额
        /// </summary>
        public decimal DepositAmount { get; protected internal set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceMemo { get; protected internal set; }        
    }
    
}
