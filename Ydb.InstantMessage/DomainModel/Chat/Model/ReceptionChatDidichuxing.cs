using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.InstantMessage.DomainModel.Enums;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// IM,信息,多媒体
    /// </summary>
    public class ReceptionChatDidichuxing : ReceptionChat
    {
        public ReceptionChatDidichuxing() { }
        public ReceptionChatDidichuxing(string fromlat, string fromlng,string fromaddr,string fromname,string tolat,string tolng,string toaddr,string toname,string phone,
         Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Chat, resourceFrom, resourceTo)
        {
            this.Fromlat = fromlat;
            this.Fromlng = fromlng;
            this.Fromaddr = fromaddr;
            this.Fromname = fromname;
            this.Tolat = tolat;
            this.Tolng = tolng;
            this.Toaddr = toaddr;
            this.Toname = toname;
            this.Phone = phone;
        }

        /// <summary>
        /// 出发地纬度
        /// </summary>
        public virtual string Fromlat { get; protected internal set; }
        /// <summary>
        /// 出发地经度
        /// </summary>
        public virtual string Fromlng { get; protected internal set; }
        /// <summary>
        /// 出发地地址
        /// </summary>
        public virtual string Fromaddr { get; protected internal set; }
        /// <summary>
        /// 出发地名称
        /// </summary>
        public virtual string Fromname { get; protected internal set; }
        /// <summary>
        /// 目的地纬度
        /// </summary>
        public virtual string Tolat { get; protected internal set; }
        /// <summary>
        /// 目的地经度
        /// </summary>
        public virtual string Tolng { get; protected internal set; }
        /// <summary>
        /// 目的地地址
        /// </summary>
        public virtual string Toaddr { get; protected internal set; }
        /// <summary>
        /// 目的地名称
        /// </summary>
        public virtual string Toname { get; protected internal set; }
        /// <summary>
        /// 乘客手机号
        /// </summary>
        public virtual string Phone { get; protected internal set; }

        public virtual new ReceptionChatDidichuxingDto ToDto()
        {
            ReceptionChatDidichuxingDto dto = new ReceptionChatDidichuxingDto();

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

            dto.Fromlat = Fromlat;
            dto.Fromlng = Fromlng;
            dto.Fromaddr = Fromaddr;
            dto.Fromname = Fromaddr;
            dto.Tolat = Tolat;
            dto.Tolng = Tolng;
            dto.Toaddr = Toaddr;
            dto.Toname = Toname;
            dto.Phone = Phone;

            return dto;
        }
    }
}
