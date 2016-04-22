using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
namespace Finance.Infrastructure.Nhibernate.Mapping
{
   public class SalePointMapping:ClassMap<DomainModel.SalePoint>
    {
        public SalePointMapping()
        {
            Id(x => x.Id);
            Map(x => x.TypeId);
            Map(x => x.Point);
        }
    }
}
