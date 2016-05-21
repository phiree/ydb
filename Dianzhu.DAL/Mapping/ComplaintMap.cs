using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class ComplaintMap : ClassMap<Complaint>
    {
        public ComplaintMap() {
            Id(x => x.Id);
            References<ServiceOrder>(x => x.Order);
            Map(x => x.Target).CustomType<Model.Enums.enum_ChatTarget>();
            Map(x => x.Context);
            Map(x => x.ResourcesUrl);
            Map(x => x.CreatTime);
            Map(x => x.Result);
            References<DZMembership>(x => x.Operator);
            Map(x => x.LastUpdateTime);
        }
    }
}
