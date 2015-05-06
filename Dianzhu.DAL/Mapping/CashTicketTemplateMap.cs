using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class CashTicketTemplateMap : ClassMap<CashTicketTemplate>
    {
        public CashTicketTemplateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Owner);
            Map(x => x.Amount);
            Map(x => x.Conditions);
            Map(x => x.ExpiredDate);
            Map(x => x.ValidDate);
            Map(x => x.Coverage);
         
        }
    }

}
