using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    /// <summary>
    /// 当返回只有一条数据时处理方式
    /// </summary>
    public class OnlineUserSessionResult_OnlyOne
    {
        public OnlineUserSession session { get; set; }
    }
    public class OnlineUserSessionResult
    {
        public IList<OnlineUserSession> session { get; set; }

    }
    /// <summary>
    /// 即时通讯服务器返回的 用户在线列表
    /// </summary>
    public class OnlineUserSession
    {
        public DateTime creationDate { get; set; }
        public string hostAddress { get; set; }
        public string hostName { get; set; }
        public string lastActionDate { get; set; }
        public string presenceStatus { get; set; }
        public string ressource { get; set; }
        public string username { get; set; }
        public string sessionId { get; set; }
    }
 
}
