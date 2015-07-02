using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
  
    /// <summary>
    /// 试卷类
    /// </summary>
    public class CashTicketAssignRecordMap : ClassMap<CashTicketAssignRecord>
    {
        public CashTicketAssignRecordMap()
        {
            Id(x => x.Id);
            Map(x => x.AmountBusiness);
            Map(x => x.AmountCashTicket);
            Map(x => x.TimeBegin);
            Map(x => x.TimeEnd);
            References<Area>(x => x.Area);
            Map(x => x.IsSuccess);
            
            
        }

    }

   
}
