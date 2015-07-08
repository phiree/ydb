﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
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
           Map(x => x.OrderStatus).CustomType<int>();
           References<DZService>(x => x.Service);
           HasMany<Staff>(x => x.Staff);
           
       }
    }
}
