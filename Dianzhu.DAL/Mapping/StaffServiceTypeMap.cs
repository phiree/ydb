using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class StaffServiceTypeMap : ClassMap<StaffServiceType>
    {
        public StaffServiceTypeMap()
        {
            Id(x => x.Id);
            References<Staff>(x => x.Staff);
            References<ServiceType>(x => x.ServiceType);
        }
    }
}
