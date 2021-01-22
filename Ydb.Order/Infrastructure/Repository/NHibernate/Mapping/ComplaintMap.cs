using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Order.DomainModel;
using FluentNHibernate.Mapping;
using Ydb.Common;

namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
    public class ComplaintMap : ClassMap<Complaint>
    {
        public ComplaintMap()
        {
            Id(x => x.Id);
            Map(x => x.OrderId);
            Map(x => x.CustomerServiceId);
            Map(x => x.BusinessId);
            Map(x => x.Target).CustomType<enum_ChatTarget>();
            Map(x => x.Content);

            //20160614_longphui_modify
            //Map(x => x.ResourcesUrl);//.Length(1000);
            HasMany(x => x.ComplaitResourcesUrl)
            .Cascade.AllDeleteOrphan().KeyColumn("ResourcesUrlId").EntityName("ComplaitResourcesUrl").Element("ResourcesUrl").Not.LazyLoad();
            //HasMany<string>(x => x.ResourcesUrl).Cascade.AllDeleteOrphan();
            Map(x => x.Status).CustomType<enum_ComplaintStatus>();

            Map(x => x.CreatTime);
            Map(x => x.Result);
            Map(x => x.OperatorId);
            Map(x => x.LastUpdateTime);
        }
    }
}
