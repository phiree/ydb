using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;

namespace Dianzhu.DAL.Mapping
{
    public class ServicePropertyValueMap : ClassMap<ServicePropertyValue>
    {
        public ServicePropertyValueMap()
        {
            Id(x => x.Id);

            Map(x => x.PropertyValue);
            References<ServiceProperty>(x => x.ServiceProperty).Not.LazyLoad();


        }

    }

}
