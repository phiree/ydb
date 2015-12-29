using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class DeviceBindMap : ClassMap<DeviceBind>
    {
        public DeviceBindMap()
        {
            Id(x => x.Id);
            Map(x => x.AppName);
            Map(x => x.AppToken);
            Map(x => x.BindChangedTime);
            References<DZMembership>(x => x.DZMembership);
            Map(x =>x.IsBinding);
            Map(x => x.AppUUID);
            Map(x => x.SaveTime);
            Map(x => x.PushAmount);
        }
    }
     
}
