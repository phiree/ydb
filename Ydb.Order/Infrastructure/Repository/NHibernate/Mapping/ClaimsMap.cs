using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Order.DomainModel;
using Ydb.Common;
using Ydb.Common.Domain;

namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
    public class ClaimsMap : ClassMap<Claims>
    {
        public ClaimsMap()
        {
            Id(x => x.Id);
            References(x => x.Order);
            Map(x => x.Status).CustomType<enum_OrderStatus>();
            Map(x => x.CreatTime);
            Map(x => x.ApplicantId);
            Map(x => x.LastUpdateTime);
            HasMany(x => x.ClaimsDatailsList).Cascade.All();
        }
    }
}
