using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.Common.Domain;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// IM,通知,客服更换
    /// </summary>
    public class ReceptionChatReAssign : ReceptionChat
    {
        public ReceptionChatReAssign() { }
        public ReceptionChatReAssign(string reAssignedCustomerServiceId, string csAlias, string csAvatar,
            Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
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

        public virtual new ReceptionChatReAssignDto ToDto()
        {
            ReceptionChatReAssignDto dto = new ReceptionChatReAssignDto();

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
            dto.ToResource = ToResource.ToString();
            dto.IsReaded = IsReaded;
            dto.IsfromCustomerService = IsfromCustomerService;

            dto.ReAssignedCustomerServiceId = this.ReAssignedCustomerServiceId;
            dto.CSAlias = this.CSAlias;
            dto.CSAvatar = CSAvatar;

            return dto;
        }
    }
}
