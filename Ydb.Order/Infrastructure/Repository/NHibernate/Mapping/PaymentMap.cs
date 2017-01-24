using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Order.DomainModel;
using Ydb.Common;

namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
    public class PaymentMap : ClassMap<Payment>
    {
        public PaymentMap()
        {
            Id(x => x.Id);
            Map(x => x.Amount);
            Map(x => x.CreatedTime);
            Map(x => x.LastUpdateTime);
            References<ServiceOrder>(x => x.Order);
            Map(x => x.PayTarget).CustomType<enum_PayTarget>();
            Map(x => x.Status).CustomType<enum_PaymentStatus>();
            Map(x => x.PayApi).CustomType<enum_PayAPI>();
            Map(x => x.PlatformTradeNo);
            Map(x => x.Memo);
            Map(x => x.PayType).CustomType<enum_PayType>();
        }
    }
  

}
