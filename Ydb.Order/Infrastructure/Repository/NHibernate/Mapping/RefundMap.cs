using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Order.DomainModel;
using Ydb.Common;

namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
    public class RefundMap : ClassMap<Refund>
    {
        public RefundMap()
        {
            Id(x => x.Id);
            References(x => x.Order);
            References(x => x.Payment);

            Map(x => x.TotalAmount);
            Map(x => x.RefundAmount);
            Map(x => x.RefundReason);
            Map(x => x.PlatformTradeNo);

            Map(x => x.CreatedTime);
            Map(x => x.LastUpdateTime);

            Map(x => x.RefundStatus).CustomType<enum_RefundStatus>();
            Map(x => x.Memo);
        }
    }
}
