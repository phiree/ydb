using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class AdvertisementMap : ClassMap<Advertisement>
    {
        public AdvertisementMap() {
            Id(x => x.Id);
            Map(x => x.ImgUrl);
            Map(x => x.Url);
            Map(x => x.Num).CustomType<int>();
            Map(x => x.SaveTime);
            References<DZMembership>(x => x.SaveController);
            Map(x => x.LastUpdateTime);
            References<DZMembership>(x => x.UpdateController);
        }
    }
}
