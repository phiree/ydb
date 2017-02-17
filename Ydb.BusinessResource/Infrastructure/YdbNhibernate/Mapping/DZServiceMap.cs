using FluentNHibernate.Mapping;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Mapping
{
    public class DZServiceMap : ClassMap<DZService>
    {
        public DZServiceMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References<Business>(x => x.Business).Not.LazyLoad();
               
            Map(x => x.Description);
            References<ServiceType>(x => x.ServiceType).Not.LazyLoad();
            //References<ServiceType>(x => x.ServiceType);
           
            Map(x => x.Scope).Length(2000);
            Map(x => x.ChargeUnit).CustomType<int>();
            Map(x => x.FixedPrice);
            Map(x => x.IsCertificated);
            Map(x => x.IsCompensationAdvance);
            Map(x => x.IsForBusiness);
            //Map(x => x.MaxOrdersPerDay);
            Map(x => x.MinPrice);
            Map(x => x.OrderDelay);
            Map(x => x.AllowedPayType).CustomType<int>();
            Map(x => x.ServiceMode).CustomType<int>();
            Map(x => x.UnitPrice);
            Map(x => x.Enabled);
            Map(x => x.IsDeleted);
            Map(x => x.CreatedTime);
            Map(x => x.LastModifiedTime);
            HasMany<ServiceOpenTime>(x => x.OpenTimes).Cascade.All().Not.LazyLoad();// SaveUpdate();
            Map(x => x.PayFirst);
             
            Map(x => x.OverTimeForCancel);
            Map(x => x.DepositAmount);
            Map(x => x.CancelCompensation);

        }
    }
  

}
