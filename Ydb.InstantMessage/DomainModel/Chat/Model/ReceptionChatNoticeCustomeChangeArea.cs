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
    public class ReceptionChatNoticeCustomerChangeArea : ReceptionChat
    {
        public ReceptionChatNoticeCustomerChangeArea() { }
        public ReceptionChatNoticeCustomerChangeArea(
           Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo,string areaCode,string customerId)
            : base(id, from, to, messageBody, sessionId, ChatType.Notice, resourceFrom, resourceTo)
        {
            this.NewAreaCode = areaCode;
            this.CustomerId = customerId;
        }
        public string NewAreaCode
        {
            get;internal set;
        }
        public string CustomerId
        {
            get; internal set;
        }
        public override ReceptionChatDto ToDto()
        {
           var b=   base.ToDto();
            b.CustomerChangedArea = CustomerId;
            return b;
        }
    }
}
