using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 分配详情
    /// </summary>
   public class CashTIcketAssignDetail
    {
       public virtual Guid Id { get; set; }
       public virtual CashTicket CashTicket { get; set; }
       public virtual Business Business { get; set; }
       /// <summary>
       /// 分配给该商家的数量
       /// </summary>
       public virtual int Amount { get; set; }

       public CashTicketAssignRecord AssignRecord { get; set; }
    }
}
