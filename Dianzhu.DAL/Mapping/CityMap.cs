using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class CityMap : ClassMap<City>
    {
        public CityMap() { 
            Id(x=>x.Id);
            Map(x => x.CityName);
            Map(x => x.CityCode);
            Map(x => x.CityPinyin);
        }
    }
}
