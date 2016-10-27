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
    /// IM,通知,订单状态变更通知
    /// ihelper:notice:order 订单状态通知.
    /// </summary>
    public class ReceptionChatNoticeOrder : ReceptionChat
    {
        public ReceptionChatNoticeOrder() { }
        public ReceptionChatNoticeOrder(string orderTilte, string orderStatus, string orderType,
             Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {
            this.OrderTitle = orderTilte;
            this.CurrentStatus = orderStatus;
            this.OrderType = orderType;
        }
        public virtual string OrderTitle { get; protected internal set; }
        public virtual string CurrentStatus { get; protected internal set; }
        public virtual string OrderType { get; protected internal set; }

    }
}
