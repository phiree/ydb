using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public class ServiceOrderMap:ClassMap<ServiceOrder>
    {
       public ServiceOrderMap()
       {
           Id(x => x.Id);
           References<DZMembership>(x => x.Customer);
           Map(x => x.OrderCreated);
           Map(x => x.OrderStatus);
           References<DZService>(x => x.Service);
           HasMany<Staff>(x => x.Staff);
           Map(x => x.TargetAddress);
           Map(x => x.ServiceUnitPrice);
           Map(x => x.UnitAmount);

           Map(x => x.ServiceURL);
           Map(x => x.ServiceName);
           Map(x => x.ServiceDescription);
           Map(x => x.ServiceBusinessName);
           Map(x => x.CustomerName);
           Map(x => x.CustomerEmail);
           Map(x => x.CustomerPhone);
            


           
       }
    }
}
