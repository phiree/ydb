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
    public class CashTicketCreateRecordMap : ClassMap<CashTicketCreateRecord>
    {
        public CashTicketCreateRecordMap()
        {
            Id(x => x.Id);
            Map(x => x.Amount);
            Map(x => x.Operator);
            Map(x => x.TimeCreated);
 
        }

    }

   
}
