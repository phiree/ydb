using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class ReceptionStatusMap:ClassMap<ReceptionStatus>
    {
        public ReceptionStatusMap()
        { 
            Id(x=>x.Id);
            References<DZMembership>(x => x.CustomerService);
            References<DZMembership>(x => x.Customer);
          
        }
    }
}
