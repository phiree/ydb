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
           
       }
    }
}
