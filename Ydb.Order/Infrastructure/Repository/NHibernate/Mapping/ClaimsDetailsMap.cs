﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Order.DomainModel;
using Ydb.Common;

namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
    public class ClaimsDetailsMap : ClassMap<ClaimsDetails>
    {
        public ClaimsDetailsMap() {
            Id(x => x.Id).GeneratedBy.Assigned();
            References(x => x.Claims);
            Map(x => x.Target).CustomType<enum_ChatTarget>();
            Map(x => x.Context);
            Map(x => x.Amount);

            //20160622_longphui_modify
            //Map(x => x.ResourcesUrl).Length(1000);
            HasMany(x => x.ClaimsDetailsResourcesUrl)
            .Cascade.AllDeleteOrphan().KeyColumn("ResourcesUrlId").EntityName("ClaimsDetailsResourcesUrl").Element("ResourcesUrl").Not.LazyLoad();

            Map(x => x.CreatTime);
            Map(x => x.MemberId);
            Map(x => x.LastUpdateTime);
        }
    }
}
