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
            Map(x => x.OrderFinished);
            Map(x => x.OrderServerStartTime);
            Map(x => x.OrderServerFinishedTime);
            Map(x => x.Memo);
            Map(x => x.OrderStatus);
            Map(x => x.TargetAddress);
            Map(x => x.TargetTime);
            HasMany<Staff>(x => x.Staff);
            Map(x => x.UnitAmount);
            Map(x => x.OrderAmount);
           References<DZService>(x => x.Service);
            Map(x => x.ServiceURL);
            Map(x => x.ServiceName);
            Map(x => x.ServiceDescription);
            Map(x => x.ServiceBusinessName);

            Map(x => x.ServiceUnitPrice);
           
           
         
           Map(x => x.CustomerName);
           Map(x => x.CustomerEmail);
           Map(x => x.CustomerPhone);
            References<DZMembership>(x => x.CustomerService);
            Map(x => x.TradeNo);
            


           
       }
    }
}
