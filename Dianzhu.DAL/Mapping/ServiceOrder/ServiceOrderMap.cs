using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
    public class ServiceOrderMap : ClassMap<ServiceOrder>
    {
        public ServiceOrderMap()
        {
            Id(x => x.Id);
            References<DZMembership>(x => x.Customer);
            Map(x => x.SerialNo);
            Map(x => x.OrderCreated);
            Map(x => x.OrderConfirmTime);
            Map(x => x.LatestOrderUpdated);
            Map(x => x.OrderFinished);
            Map(x => x.OrderServerStartTime);
            Map(x => x.OrderServerFinishedTime);
            Map(x => x.Memo);
            Map(x => x.OrderStatus).CustomType<Model.Enums.enum_OrderStatus>();
            References<DZMembership>(x => x.CustomerService);
            HasMany<ServiceOrderDetail>(x => x.Details).Cascade.All();
            References<Business>(x => x.Business);
            Map(x => x.NegotiateAmount);

            Map(x => x.DepositAmount);

            //指派的负责人
            References<Staff>(x => x.Staff);
        }
    }
}
