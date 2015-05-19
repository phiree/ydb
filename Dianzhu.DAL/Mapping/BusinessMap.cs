using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class Business_AbsMap : ClassMap<Business_Abs>
    {
        public Business_AbsMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
    public class BusinessMap : SubclassMap<Business>
    {
        public BusinessMap()
        {
            Map(x => x.Address);
           
            Map(x=>x.Description);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            Map(x => x.IsApplyApproved);
            Map(x => x.ApplyRejectMessage);
           
        }
    }

}
