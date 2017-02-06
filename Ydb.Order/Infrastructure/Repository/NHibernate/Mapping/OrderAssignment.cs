using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
    public class OrderAssignmentMap : ClassMap<OrderAssignment>
    {
        public OrderAssignmentMap()
        {
            Id(x => x.Id);
            Map(x => x.AssignedStaffId);
            References<ServiceOrder>(x => x.OrderId);
            Map(x => x.AssignedTime);
            Map(x => x.DeAssignedTime);
            Map(x => x.Enabled);
            Map(x => x.CreateTime);
            Map(x => x.IsHeader);
            Map(x => x.BusinessId);


        }
    }
  

}
