using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// im消息：文本
    /// </summary>
    public class ReceptionChatDto
    {
        public Guid Id { get;  set; }
        /// <summary>
        /// 所属的回话ID, 目前等于 orderid;
        /// </summary>
        public string SessionId { get;  set; }
        //保存的时间, 作为排序依据.
        public DateTime SavedTime { get;  set; }
        public double SavedTimestamp { get;  set; }
        public DateTime SendTime { get;  set; }//发送时间
        public DateTime ReceiveTime { get;  set; }//接收时间
        public string ChatType { get;  set; }
        public string FromId { get;  set; }
        public string ToId { get;  set; }
        public string MessageBody { get;  set; }//消息的内容
        public string ChatTarget { get;  set; } //聊天状态，接待方是平台客服还是商家客服
        public string FromResource { get;  set; }//from 的资源名
        public string FromResourceName { get; set; }//from 的资源名
        public string ToResource { get;  set; }//to 的资源名
        public bool IsReaded { get;  set; }
        public bool IsfromCustomerService { get;  set; }
        //原本的类名
        public string OriginalClassName { get; set; }
        public string CustomerChangedArea { get; set; }

        public override string ToString()
        {
            //todo:
            return base.ToString();
        }
    }
}
