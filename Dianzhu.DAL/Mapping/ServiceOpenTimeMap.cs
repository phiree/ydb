using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class ServiceOpenTimeMap : ClassMap<ServiceOpenTime>
    {
        public ServiceOpenTimeMap()
        {
            Id(x => x.Id);
            Map(x => x.Enabled);
            Map(x => x.DayOfWeek).CustomType<int>();
            Map(x => x.MaxOrderForDay);
            HasMany<ServiceOpenTimeForDay>(x => x.OpenTimeForDay).Cascade.All().Not.LazyLoad();
            

        }
    
    }
    public class ServiceOpenTimeForDayMap : ClassMap< ServiceOpenTimeForDay>
    {
        public ServiceOpenTimeForDayMap()
        {
            Id(x => x.Id);
            
           
            Map(x => x.TimeEnd);
            Map(x => x.TimeStart);
            Map(x => x.PeriodEnd);
            Map(x => x.PeriodStart);
            Map(x => x.MaxOrderForOpenTime);
            Map(x => x.Enabled);
            References<ServiceOpenTime>(x => x.ServiceOpenTime).Column("ServiceOpenTime_id");

        }

    }

}
