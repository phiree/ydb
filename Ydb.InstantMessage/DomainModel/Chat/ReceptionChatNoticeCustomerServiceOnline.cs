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
    /// IM,通知,客服上线. 不需要指定上线的客服id. 点点收到该消息会处理.
    /// </summary>
    public class ReceptionChatNoticeCustomerServiceOnline : ReceptionChat
    {
        public ReceptionChatNoticeCustomerServiceOnline() { }
        public ReceptionChatNoticeCustomerServiceOnline(
          Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {

        }
    }
}
