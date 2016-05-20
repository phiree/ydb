using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
    public class ServiceOrderAppraiseMap: ClassMap<ServiceOrderAppraise>
    {
        public ServiceOrderAppraiseMap()
        {
            Id(x => x.Id);
            Map(x => x.CreateTime);
            References<DZMembership>(x => x.Order);
            Map(x => x.Tatget).CustomType<Dianzhu.Model.Enums.enum_ChatTarget>();
            Map(x => x.Value);
            Map(x => x.Content);
        }
    }
}
