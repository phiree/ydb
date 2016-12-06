using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.DomainModel;

using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.DomainModel;
namespace Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping
{
    public class ServiceTypeMap : ClassMap<ServiceType>
    {
        public ServiceTypeMap()
        {
            Id(x => x.Id).CustomType<Guid>().GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.DeepLevel);
            Map(x => x.Code);
            Map(x => x.OrderNumber);
            References<ServiceType>(x => x.Parent).Not.LazyLoad();
            HasMany<ServiceType>(x => x.Children).Cascade.All().Inverse().Not.LazyLoad();
           // HasMany<ServiceProperty>(x => x.Properties);
            

        }
    
    }

}
