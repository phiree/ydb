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
            Id(x => x.Id);
            References<Model.ServiceType>(x => x.ServiceType);
            Map(x => x.Point);
        }
    }
}
