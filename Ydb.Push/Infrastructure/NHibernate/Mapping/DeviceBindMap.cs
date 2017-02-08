using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Push.DomainModel;

namespace Ydb.Push.Infrastructure.NHibernate.Mapping
{
    public class DeviceBindMap : ClassMap<DeviceBind>
    {
        public DeviceBindMap()
        {
            Id(x => x.Id);
            Map(x => x.AppName);
            Map(x => x.AppToken);
            Map(x => x.BindChangedTime);
            Map(x => x.DZMembershipId);
            Map(x =>x.IsBinding);
            Map(x => x.AppUUID);
            Map(x => x.SaveTime);
            Map(x => x.PushAmount);
        }
    }
     
}
