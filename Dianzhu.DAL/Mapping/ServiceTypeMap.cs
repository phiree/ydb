﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class ServiceTypeMap : ClassMap<ServiceType>
    {
        public ServiceTypeMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.DeepLevel);
            Map(x => x.Code);
            Map(x => x.OrderNumber);
            References<ServiceType>(x => x.Parent);
            HasMany<ServiceType>(x => x.Children).Cascade.All().Inverse();
           // HasMany<ServiceProperty>(x => x.Properties);
            

        }
    
    }

}
