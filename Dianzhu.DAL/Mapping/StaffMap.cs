using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class StaffMap : ClassMap<Staff>
    {
        public StaffMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.LoginName);
            Map(x => x.UserID);
            Map(x => x.Age);
            Map(x => x.WorkingYears);
            References<Business>(x => x.Belongto);
            Map(x => x.Code);
            Map(x => x.Gender);
            Map(x => x.NickName);
            Map(x => x.Phone);
            Map(x => x.Email);
            Map(x => x.Address);
            Map(x => x.Photo);
            Map(x => x.Enable);           
            Map(x => x.IsAssigned);
            HasMany<BusinessImage>(x => x.StaffAvatar).Cascade.AllDeleteOrphan(); 
        }
    }
}
