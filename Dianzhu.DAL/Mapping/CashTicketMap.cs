using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    /// <summary>
    ///现金券map
    /// </summary>
   public class CashTicketMap:ClassMap<CashTicket>
    {
       public CashTicketMap()
       {
            Id(x => x.Id);
            References<CashTicketTemplate>(x => x.CashTicketTemplate);
            Map(x => x.TicketCode).Unique();
            Map(x => x.BusinessAssigned);
            
            References<CashTicketCreateRecord>(x => x.CashTicketCreateRecord);
            
            References<User>(x => x.UserAssigned);
            References<User>(x => x.UserAssignedTime);
            References<User>(x => x.UserUsedTime);
       }
    }
 
}
