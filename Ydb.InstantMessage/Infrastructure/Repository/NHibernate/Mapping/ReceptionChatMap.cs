using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
using FluentNHibernate.Mapping;

namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping
{
    public class ReceptionChatMap:ClassMap<ReceptionChat>
    {
        public ReceptionChatMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.ChatTarget).CustomType<Ydb.InstantMessage.DomainModel.Chat.Enums.ChatTarget>();
            Map(x => x.ChatType).CustomType<Ydb.InstantMessage.DomainModel.Chat.Enums.ChatType>();
            Map(x => x.FromId);
            Map(x => x.FromResource).CustomType<Ydb.InstantMessage.DomainModel.Enums.XmppResource>();
         
            Map(x => x.IsReaded);
            Map(x => x.MessageBody);
            Map(x => x.ReceiveTime);
            Map(x => x.SavedTime);
            Map(x => x.SavedTimestamp);
            Map(x => x.SendTime);
            Map(x => x.SessionId);
            Map(x => x.ToId);
            Map(x => x.ToResource).CustomType<Ydb.InstantMessage.DomainModel.Enums.XmppResource>();
        }

        public class ReceptionChatMediaMap : SubclassMap<ReceptionChatMedia>
        {
            public ReceptionChatMediaMap()
            {
                Map(x => x.MedialUrl);
                Map(x => x.MediaType);
            }
        }

        public class ReceptionChatPushServiceMap : SubclassMap<ReceptionChatPushService>
        {
            public ReceptionChatPushServiceMap()
            {
                HasMany<PushedServiceInfo>(x => x.ServiceInfos).Component(
                    x => {
                        x.Map(c => c.ServiceId);
                        x.Map(c => c.ServiceName);
                        x.Map(c => c.ServiceEndTime);
                        x.Map(c => c.ServiceStartTime);
                        x.Map(c => c.ServiceType);
                        x.Map(c => c.StoreAlias);
                        x.Map(c => c.StoreAvatar);
                        x.Map(c => c.StoreUserId);
                    }
                ).Not.LazyLoad();
            }
        }

        public class ReceptionChatReAssignMap : SubclassMap<ReceptionChatReAssign>
        {
            public ReceptionChatReAssignMap()
            {
                Map(x => x.ReAssignedCustomerServiceId);
                Map(x => x.CSAlias);
                Map(x => x.CSAvatar);
            }
        }

        public class ReceptionChatNoticeOrderMap : SubclassMap<ReceptionChatNoticeOrder>
        {
            public ReceptionChatNoticeOrderMap()
            {
                Map(x => x.OrderTitle);
                Map(x => x.CurrentStatus);
                Map(x => x.OrderType);
            }
        }

    }
}
