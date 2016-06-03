using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
  
    /// <summary>
    /// 生成的现金券
    /// </summary>
    public class CashTicket :DDDCommon.Domain.Entity<Guid>{
        /// <summary>
        /// 生成操作的记录。
        /// </summary>
        public virtual CashTicketCreateRecord CashTicketCreateRecord { get; set; }
        /// <summary>
        /// 分派记录
        /// </summary>
        public virtual CashTicketAssignRecord CashTicketAssigneRecord { get; set; }
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public virtual CashTicketTemplate CashTicketTemplate { get; set; }
        /// <summary>
        /// 唯一代号，二维码图片名称
        /// </summary>
        public virtual string TicketCode { get; set; }
        /// <summary>
        /// 分配的商家
        /// </summary>
        public virtual Business BusinessAssigned { get; set; }
        /// <summary>
        /// 领取的用户
        /// </summary>
        public virtual DZMembership UserAssigned { get; set; }
        /// <summary>
        /// 用户获券时间
        /// </summary>
        public virtual DateTime UserAssignedTime { get; set; }
        /// <summary>
        /// 用户兑用时间
        /// </summary>
        public virtual DateTime UserUsedTime { get; set; }

        

    }
  

}
