using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping.Finance
{
    public class ServiceTypePointMap:ClassMap<Model.Finance.ServiceTypePoint>
    {
        public ServiceTypePointMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map (x => x.ServiceTypeId);
            Map(x => x.Point);
        }
    }
}
