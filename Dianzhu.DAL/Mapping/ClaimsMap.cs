using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class ClaimsMap : ClassMap<Claims>
    {
        public ClaimsMap() {
            Id(x => x.Id);
            References(x => x.Order);
            Map(x => x.Status).CustomType<Model.Enums.enum_OrderStatus>();
            Map(x => x.CreatTime);
            Map(x => x.ApplicantId);
            Map(x => x.LastUpdateTime);
            HasMany(x => x.ClaimsDatailsList).Cascade.All();
        }
    }
}
