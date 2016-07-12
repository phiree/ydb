using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class OrderAssignmentMap : ClassMap<OrderAssignment>
    {
        public OrderAssignmentMap()
        {
            Id(x => x.Id);
            References<Staff>(x => x.AssignedStaff);
            References<ServiceOrder>(x => x.Order);
            Map(x => x.AssignedTime);
            Map(x => x.DeAssignedTime);
            Map(x => x.Enabled);
            Map(x => x.CreateTime);
            Map(x => x.IsHeader);


        }
    }
  

}
