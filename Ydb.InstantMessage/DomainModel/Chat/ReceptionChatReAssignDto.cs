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
    public class ReceptionChatReAssignDto : ReceptionChatDto
    {
        /// <summary>
        /// 重新分配的客服
        /// </summary>
        public virtual string ReAssignedCustomerServiceId { get; protected internal set; }
        public virtual string CSAlias { get; protected internal set; }
        public virtual string CSAvatar { get; protected internal set; }
    }
}
