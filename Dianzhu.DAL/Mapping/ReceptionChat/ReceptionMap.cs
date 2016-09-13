using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    
    public class ReceptionChatMap : ClassMap<ReceptionChat>
    {
        public ReceptionChatMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SavedTime);
            Map(x => x.SavedTimestamp);
            Map(x => x.MessageBody).Length(1000);
            Map(x => x.ReceiveTime);
            Map(x => x.SendTime);
            Map(x => x.ToId);
            Map(x => x.FromId);
            Map(x => x.SessionId);
            Map(x => x.ChatType).CustomType<Model.Enums.enum_ChatType>();
            Map(x => x.ChatTarget).CustomType<Model.Enums.enum_ChatTarget>();
            Map(x => x.FromResource).CustomType<Model.Enums.enum_XmppResource>();
            Map(x => x.ToResource).CustomType<Model.Enums.enum_XmppResource>();
        }
    }
 
 
    public class ReceptionChatMediaMap : SubclassMap<ReceptionChatMedia>
    {
        public ReceptionChatMediaMap()
        {
            Map(x => x.MedialUrl);
            Map(x => x.MediaType);
        }
    }
    public class ReceptionChatReAssignMap : SubclassMap<ReceptionChatReAssign>
    {
        public ReceptionChatReAssignMap()
        {
            Map(x => x.ReAssignedCustomerServiceId);
        }
    }
    public class ReceptionChatNoticeSys : SubclassMap<ReceptionChatNoticeSys>
    {
         
    }
    public class ReceptionChatNoticeOrderMap : SubclassMap<ReceptionChatNoticeOrder>
    {
        public ReceptionChatNoticeOrderMap()
        {
            Map(x => x.OrderTitle);
            Map(x => x.OrderTitle);
            Map(x => x.CurrentStatus);
        }
    }
    public class ReceptionChatNoticePromoteMap : SubclassMap<ReceptionChatNoticePromote>
    {
        public ReceptionChatNoticePromoteMap()
        {

            Map(x => x.PromotionUrl);
        }
    }

    public class ReceptionChatNoticeCustomerServiceOfflineMap : SubclassMap<ReceptionChatNoticeCustomerServiceOffline>
    {
      
    }
    public class ReceptionChatNoticeCustomerServiceOnlineMap : SubclassMap<ReceptionChatNoticeCustomerServiceOnline>
    {

    }
    public class ReceptionChatUserStatusMap : SubclassMap<ReceptionChatUserStatus>
    {
        public ReceptionChatUserStatusMap()
        {
            Map(x => x.UserId);
            Map(x => x.Status).CustomType<Model.Enums.enum_UserStatus>();
        }
    }

    public class ReceptionChatNoticeNewOrderMap : SubclassMap<ReceptionChatNoticeNewOrder>
    {

    }
    public class ReceptionChatPushServiceMap : SubclassMap<ReceptionChatPushService>
    {
        public ReceptionChatPushServiceMap()
        {
            HasMany<PushedServiceInfo>(x => x.ServiceInfos);
        }
    }
    public class ServiceInfoMap : ComponentMap<PushedServiceInfo>
    {
        public ServiceInfoMap()
        {
            Map(x => x.ServiceId);
            Map(x => x.ServiceName);
            Map(x => x.ServiceEndTime);
            Map(x => x.ServiceStartTime);
            Map(x => x.ServiceType);
            Map(x => x.StoreAlias);
            Map(x => x.StoreAvatar);
            Map(x => x.StoreUserId);
        }
    }




}
