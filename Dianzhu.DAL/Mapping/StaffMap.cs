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
             
            References<Business>(x => x.Belongto);
            Map(x => x.Code);
            Map(x => x.Gender);
            Map(x => x.NickName);
            Map(x => x.Phone);
            Map(x => x.Photo);
            Map(x => x.IsAssigned);
            HasMany<ServiceType>(x => x.ServiceTypes).Not.LazyLoad();
            HasMany<BusinessImage>(x => x.StaffAvatar);
            
        }
    }
  

}
