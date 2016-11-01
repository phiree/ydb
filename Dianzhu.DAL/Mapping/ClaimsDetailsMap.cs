using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class ClaimsDetailsMap : ClassMap<ClaimsDetails>
    {
        public ClaimsDetailsMap() {
            Id(x => x.Id);
            References(x => x.Claims);
            Map(x => x.Target).CustomType<Model.Enums.enum_ChatTarget>();
            Map(x => x.Context);
            Map(x => x.Amount);

            //20160622_longphui_modify
            //Map(x => x.ResourcesUrl).Length(1000);
            HasMany(x => x.ClaimsDetailsResourcesUrl)
            .Cascade.AllDeleteOrphan().KeyColumn("ResourcesUrlId").EntityName("ClaimsDetailsResourcesUrl").Elemen‌​t("ResourcesUrl");

            Map(x => x.CreatTime);
            Map(x => x.MemberId);
            Map(x => x.LastUpdateTime);
        }
    }
}
