using System;
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
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DeepLevel);
            References<ServiceType>(x => x.Parent).Not.LazyLoad();
            

        }
    
    }

}
