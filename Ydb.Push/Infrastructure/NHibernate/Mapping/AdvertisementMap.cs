using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Push.DomainModel;
namespace Ydb.Push.Infrastructure.NHibernate.Mapping
{
    public class AdvertisementMap : ClassMap<Advertisement>
    {
        public AdvertisementMap() {
            Id(x => x.Id);
            Map(x => x.ImgUrl);
            Map(x => x.Url);
            Map(x => x.Num).CustomType<int>();
            Map(x => x.SaveTime);
            Map(x => x.PushTarget);
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.IsUseful);
            Map(x => x.SaveControllerId);
            Map(x => x.LastUpdateTime);
            Map(x => x.UpdateControllerId);
            Map(x => x.PushType);
        }
    }
}
