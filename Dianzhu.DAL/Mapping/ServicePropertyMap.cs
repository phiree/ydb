using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class ServicePropertyMap : ClassMap<ServiceProperty>
    {
        public ServicePropertyMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References<Business_Abs>(x => x.BelongsTo);
            References<ServiceType>(x => x.ServiceType).Not.LazyLoad();
            HasMany<ServicePropertyValue>(x => x.Values).Cascade.All().Not.LazyLoad();
         
            

        }
    
    }

}
