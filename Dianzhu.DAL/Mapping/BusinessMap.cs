using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class BusinessMap : ClassMap<Business>
    {
        public BusinessMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.AddressOffice);
            Map(x => x.AddressShop);
            Map(x=>x.Description);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
           
        }
    }

}
