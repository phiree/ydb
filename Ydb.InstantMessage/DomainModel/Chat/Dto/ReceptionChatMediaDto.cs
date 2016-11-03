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
    public class ReceptionChatMediaDto : ReceptionChatDto
    {
        public virtual string MedialUrl { get; protected internal set; }
        public virtual string MediaType { get; protected internal set; }
    }
}
