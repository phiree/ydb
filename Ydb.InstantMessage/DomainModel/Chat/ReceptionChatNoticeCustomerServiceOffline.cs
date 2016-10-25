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
    /// IM,通知,客服离线. 不需要指定离线的客服id，点点收到消息后会处理.
    /// </summary>
    public class ReceptionChatNoticeCustomerServiceOffline : ReceptionChat
    {
        public ReceptionChatNoticeCustomerServiceOffline() { }
        public ReceptionChatNoticeCustomerServiceOffline(
           Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {

        }
    }
}
