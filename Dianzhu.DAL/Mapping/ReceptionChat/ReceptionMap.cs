﻿using System;
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
            References<DZMembership>(x => x.To);
            References<DZMembership>(x => x.From);
            References<ServiceOrder>(x => x.ServiceOrder);
            Map(x => x.ChatType).CustomType<Model.Enums.enum_ChatType>();
            Map(x => x.Version);
              Map(x => x.ChatTarget).CustomType<Model.Enums.enum_ChatTarget>();
            Map(x => x.FromResource).CustomType<Model.Enums.enum_XmppResource>();
            Map(x => x.ToResource).CustomType<Model.Enums.enum_XmppResource>();
        }
    }

    public class ReceptionChatDDMap : ClassMap<ReceptionChatDD>
    {
        public ReceptionChatDDMap()
        {
            Id(x => x.Id).UnsavedValue(Guid.Empty);
            Map(x => x.SavedTime);
            Map(x => x.MessageBody).Length(1000);
            Map(x => x.ReceiveTime);
            Map(x => x.SendTime);
            References<DZMembership>(x => x.To);
            References<DZMembership>(x => x.From);
            References<ServiceOrder>(x => x.ServiceOrder);
            Map(x => x.ChatType).CustomType<int>();
            Map(x => x.Version);
             Map(x => x.IsCopy);
            Map(x => x.MedialUrl);
            Map(x => x.MediaType);
            Map(x => x.ChatTarget).CustomType<Model.Enums.enum_ChatTarget>();
            Map(x => x.FromResource).CustomType<Model.Enums.enum_XmppResource>();
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
            References<DZMembership>(x => x.ReAssignedCustomerService);
        }
    }
    public class ReceptionChatNoticeMap : SubclassMap<ReceptionChatNotice>
    {
        public ReceptionChatNoticeMap()
        {
            References<DZMembership>(x => x.UserObj);
        }
    }
    public class ReceptionChatUserStatusMap : SubclassMap<ReceptionChatUserStatus>
    {
        public ReceptionChatUserStatusMap()
        {
            References<DZMembership>(x => x.User);
            Map(x => x.Status).CustomType<Model.Enums.enum_ChatTarget>();
        }
    }

    public class ReceptionChatPushServiceMap : SubclassMap<ReceptionChatPushService>
    {
        public ReceptionChatPushServiceMap()
        {
            HasMany(x => x.PushedServices).Cascade.All();
        }
    }




}
