using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using FluentNHibernate.Mapping;
using Ydb.Common;

namespace Dianzhu.DAL.Mapping
{
    public class ServiceOrderMap : ClassMap<ServiceOrder>
    {
        public ServiceOrderMap()
        {
            Id(x => x.Id);
            Map (x => x.CustomerId);
            Map(x => x.SerialNo);
            Map(x => x.OrderCreated);
            Map(x => x.OrderConfirmTime);
            Map(x => x.LatestOrderUpdated);
            Map(x => x.OrderFinished);
            Map(x => x.OrderServerStartTime);
            Map(x => x.OrderServerFinishedTime);
            Map(x => x.Memo);
            Map(x => x.OrderStatus).CustomType<enum_OrderStatus>();
            Map (x => x.CustomerServiceId);
            HasMany<ServiceOrderDetail>(x => x.Details).Cascade.All();
            Map(x => x.BusinessId);
            Map(x => x.NegotiateAmount);
            Map(x => x.NegotiateAmount_Modified);
            Map(x => x.DepositAmount);

            //指派的负责人
            Map(x => x.StaffId);
            Map(x => x.IsShared);
        }
    }
}
