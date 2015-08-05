using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class DZTagMap:ClassMap<DZTag>
    {
        public DZTagMap()
        { 
            Id(x=>x.Id);
            Map(x=>x.Text);
            Map(x => x.ForPK);
            Map(x => x.ForPK2);
           
            Map(x => x.ForPK3);
            Map(x => x.CreateDate);
            Map(x => x.OriginalText);
          
        }
    }
}
