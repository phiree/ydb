using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
using Ydb.Common;

namespace Dianzhu.DAL.Mapping
{
    public class ComplaintMap : ClassMap<Complaint>
    {
        public ComplaintMap() {
            Id(x => x.Id);
            References<ServiceOrder>(x => x.Order);
            Map(x => x.Target).CustomType<enum_ChatTarget>();
            Map(x => x.Content);

            //20160614_longphui_modify
            //Map(x => x.ResourcesUrl);//.Length(1000);
            HasMany(x => x.ComplaitResourcesUrl)
            .Cascade.AllDeleteOrphan().KeyColumn("ResourcesUrlId").EntityName("ComplaitResourcesUrl").Element("ResourcesUrl");
            //HasMany<string>(x => x.ResourcesUrl).Cascade.AllDeleteOrphan();
            Map(x => x.Status).CustomType<enum_ComplaintStatus>();

            Map(x => x.CreatTime);
            Map(x => x.Result);
            Map(x => x.OperatorId);
            Map(x => x.LastUpdateTime);
        }
    }
}
