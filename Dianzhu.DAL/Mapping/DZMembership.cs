using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class DZMembershipMap:ClassMap<DZMembership>
    {
        public DZMembershipMap()
        {
            Id(x => x.Id);
            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.TimeCreated);
        }
               
    }
    public class BusinessUserMap : SubclassMap<BusinessUser>
    {
        public BusinessUserMap()
        {
            References<Business>(x => x.BelongTo);
        }
    }
}
