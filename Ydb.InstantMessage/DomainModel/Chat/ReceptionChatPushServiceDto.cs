using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// im消息：推送服务消息，供用户选择
    /// </summary>
    public class ReceptionChatPushServiceDto : ReceptionChatDto
    {
        public IList<PushedServiceInfo> ServiceInfos { get; set; }
    }

    //public class PushedServiceInfoDto
    //{
    //    public string ServiceId { get; set; }
    //    public string ServiceName { get; set; }
    //    public string ServiceType { get; set; }
    //    public string ServiceStartTime { get; set; }
    //    public string ServiceEndTime { get; set; }
    //    public string StoreUserId { get; set; }
    //    public string StoreAlias { get; set; }
    //    public string StoreAvatar { get; set; }
    //}
}
