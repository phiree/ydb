using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public class RoleMemberMap:ClassMap<Model.RoleMember>
    {
        public  RoleMemberMap()
        {
            Id(x => x.Id);
            References<Model.DZRole>(x => x.Role);
            References<Model.RoleMember>(x => x.Member);
        }
    }
}
