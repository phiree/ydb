using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping.Finance
{
    public class ServiceTypePointMapping:ClassMap<Model.Finance.ServiceTypePoint>
    {
        public ServiceTypePointMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            References<Model.ServiceType>(x => x.ServiceType).Update();
            Map(x => x.Point);
        }
    }
}
