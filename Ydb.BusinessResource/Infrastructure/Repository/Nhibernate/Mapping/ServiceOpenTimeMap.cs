using FluentNHibernate.Mapping;
using Ydb.BusinessResource.DomainModel;
namespace Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping
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

            //20160617_longphui_add
            Map(x => x.Tag);

            Component(c => c.TimePeriod, m => {
                
               m.Component(t => t.StartTime, ms => { ms.Map(x => x.Hour).Column("StartTimeHour"); ms.Map(x => x.Minute).Column("StartTimeMinute"); });
                m.Component(t => t.EndTime, ms => { ms.Map(x => x.Hour).Column("EndTimeHour"); ms.Map(x => x.Minute).Column("EndTimeMinute"); });
            });

            Map(x => x.MaxOrderForOpenTime);
            Map(x => x.Enabled);
            References<ServiceOpenTime>(x => x.ServiceOpenTime).Column("ServiceOpenTime_id").Not.LazyLoad();

        }

    }

}
