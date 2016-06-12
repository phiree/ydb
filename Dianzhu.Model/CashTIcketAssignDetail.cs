using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 分配详情
    /// </summary>
   public class CashTicketAssignDetail:DDDCommon.Domain.Entity<Guid>
    {
       
       public virtual CashTicket CashTicket { get; set; }
       public virtual Business Business { get; set; }
       /// <summary>
       /// 分配给该商家的数量
       /// </summary>
       public virtual int Amount { get; set; }

       public CashTicketAssignRecord AssignRecord { get; set; }
    }
}
