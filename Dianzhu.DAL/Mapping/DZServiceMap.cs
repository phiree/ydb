using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class DZServiceMap : ClassMap<DZService>
    {
        public DZServiceMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References<Business>(x => x.Business);
               
            Map(x => x.Description);
            References<ServiceType>(x => x.ServiceType).Not.LazyLoad();
            //References<ServiceType>(x => x.ServiceType);
            HasMany<ServicePropertyValue>(x => x.PropertyValues);
            Map(x => x.BusinessAreaCode).Length(2000);
            Map(x => x.ChargeUnit).CustomType<int>();
            Map(x => x.FixedPrice);
            Map(x => x.IsCertificated);
            Map(x => x.IsCompensationAdvance);
            Map(x => x.IsForBusiness);
            Map(x => x.MaxOrdersPerDay);
            Map(x => x.MaxOrdersPerHour);
            Map(x => x.MinPrice);
            Map(x => x.OrderDelay);
            Map(x => x.PayType).CustomType<int>();
            Map(x => x.ServiceMode).CustomType<int>();
            Map(x => x.ServiceTimeBegin);
            Map(x => x.ServiceTimeEnd);
            Map(x => x.UnitPrice);
            Map(x => x.Enabled);
            Map(x => x.CreatedTime);
            Map(x => x.LastModifiedTime);
            HasMany<ServiceOpenTime>(x => x.OpenTimes).Cascade.SaveUpdate();
            Map(x => x.PayFirst);

        }
    }
  

}
