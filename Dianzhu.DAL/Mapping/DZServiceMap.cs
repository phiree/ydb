using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class DZServiceMap : ClassMap<DZService>
    {
        public DZServiceMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
              References<Business>(x => x.Business);
            Map(x => x.Description);
            References<ServiceType>(x => x.ServiceType).Not.LazyLoad();
            HasMany<ServicePropertyValue>(x => x.PropertyValues);
            

        }
    }
  

}
