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
    public class ReceptionChatPushService : ReceptionChat
    {
        public ReceptionChatPushService() { }
        public ReceptionChatPushService(IList<PushedServiceInfo> serviceInfos,
            Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Chat, resourceFrom, resourceTo)
        {
            this.ServiceInfos = serviceInfos;
        }

        public virtual IList<PushedServiceInfo> ServiceInfos { get; protected internal set; }

        public virtual new ReceptionChatPushServiceDto ToDto()
        {
            ReceptionChatPushServiceDto dto = new ReceptionChatPushServiceDto();

            dto.Id = Id;
            dto.SessionId = SessionId;
            dto.SavedTime = SavedTime;
            dto.SavedTimestamp = SavedTimestamp;
            dto.SendTime = SendTime;
            dto.ReceiveTime = ReceiveTime;
            dto.ChatType = ChatType.ToString();
            dto.FromId = FromId;
            dto.ToId = ToId;
            dto.MessageBody = MessageBody+SessionId;
            dto.ChatTarget = ChatTarget.ToString();
            dto.FromResource = FromResource.ToString();
            dto.FromResourceName = FromResourceName;
            dto.ToResource = ToResource.ToString();
            dto.IsReaded = IsReaded;
            dto.IsfromCustomerService = IsfromCustomerService;

            dto.ServiceInfos = ServiceInfos;
            //dto.ServiceInfos = new List<PushedServiceInfoDto>();
            //foreach (var item in ServiceInfos)
            //{
            //    dto.ServiceInfos.Add(item.ToDto());
            //}

            return dto;
        }
    }


    /// <summary>
    /// 消息推送的服务内容
    /// </summary>
    public class PushedServiceInfo
    {
        public PushedServiceInfo() { }
        public PushedServiceInfo(string serviceId, string servicename,
            string servicetype, string servicestarttime,
            string serviceendtime, string storeuserid,
            string storealias, string storeavatar
            )
        {
            this.ServiceId = serviceId;
            this.ServiceName = servicename;
            this.ServiceType = servicetype;
            this.ServiceStartTime = servicestarttime;
            this.ServiceEndTime = serviceendtime;
            this.StoreUserId = storeuserid;
            this.StoreAlias = storealias;
            this.StoreAvatar = storeavatar;
        }
        public virtual string ServiceId { get; protected internal set; }
        public virtual string ServiceName { get; protected internal set; }
        public virtual string ServiceType { get; protected internal set; }
        public virtual string ServiceStartTime { get; protected internal set; }
        public virtual string ServiceEndTime { get; protected internal set; }
        public virtual string StoreUserId { get; protected internal set; }
        public virtual string StoreAlias { get; protected internal set; }
        public virtual string StoreAvatar { get; protected internal set; }


        //public virtual PushedServiceInfoDto ToDto()
        //{
        //    PushedServiceInfoDto dto = new PushedServiceInfoDto();

        //    dto.ServiceId = ServiceId;
        //    dto.ServiceName = ServiceName;
        //    dto.ServiceType = ServiceType;
        //    dto.ServiceStartTime = ServiceStartTime;
        //    dto.ServiceEndTime = ServiceEndTime;
        //    dto.StoreUserId = StoreUserId;
        //    dto.StoreAlias = StoreAlias;
        //    dto.StoreAvatar = StoreAvatar;

        //    return dto;
        //}

        /*
         <svcObj svcID = "SBWA-DKJI-OFNS-SDLK" name="小飞侠" type="设计>平面设计" startTime="20151116120000" endTime="20151117120000"></svcObj>
        <storeObj userID = "SBWA-DKJI-OFNS-SDLK" alias="望海国际" imgUrl="http://i-guess.cn/ihelp/userimg/issumao_MD.png"></storeObj>
        */

    }
}
