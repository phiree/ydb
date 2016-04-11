using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public class DZRoleMap:ClassMap<Model.DZRole>
    {
        public DZRoleMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

        }
    }
}
