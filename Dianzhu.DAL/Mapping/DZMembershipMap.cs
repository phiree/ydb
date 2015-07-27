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
            Map(x => x.UserName).Unique();
            Map(x => x.Password);
            Map(x => x.TimeCreated);
            Map(x => x.LastLoginTime);
            Map(x=>x.NickName);
            Map(x => x.Address);
            Map(x => x.Email).Unique();
            Map(x => x.Phone).Unique();
            Map(x => x.IsRegisterValidated);
            Map(x => x.RegisterValidateCode);
            
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
