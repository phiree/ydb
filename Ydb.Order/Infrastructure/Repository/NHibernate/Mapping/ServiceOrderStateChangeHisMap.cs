﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
using Ydb.Common;
using FluentNHibernate.Mapping;
namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
   public class ServiceOrderStateChangeHisMap : ClassMap<ServiceOrderStateChangeHis>
    {
       public ServiceOrderStateChangeHisMap()
       {
            Id(x => x.Id);
            References<ServiceOrder>(x => x.Order).Not.LazyLoad();
            Map(x => x.OldStatus).CustomType< enum_OrderStatus>();
            Map(x => x.NewStatus).CustomType< enum_OrderStatus>();
            Map(x => x.OrderAmount);
            Map(x => x.DepositAmount);
            Map(x => x.NegotiateAmount);
            Map(x => x.Remark);
            Map(x => x.CreatTime);
            Map(x => x.ControllerId);
            Map(x => x.Number);
        }
    }
}
