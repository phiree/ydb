using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.DomainModel;

using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.DomainModel;
namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Mapping
{
    public class StaffMap:ClassMap<Staff>
    {
        public StaffMap() { 
            Id(x=>x.Id);
            
            Map(x => x.Code);
            Map(x => x.Age);
            Map(x => x.Address);
            HasMany<BusinessImage>(x => x.StaffAvatar).Cascade.AllDeleteOrphan(). Not.LazyLoad();
            References<Business>(x => x.Belongto);
         
            Map(x => x.Email);
         
            Map(x => x.Enable);
            Map(x => x.Gender);
            Map(x => x.IsAssigned);
            Map(x => x.LoginName);
            Map(x => x.Name);
            Map(x => x.NickName);
            Map(x => x.Phone);
            Map(x => x.Photo);
            Map(x => x.WorkingYears);
            Map(x => x.UserID);
           

        }
    }
}
