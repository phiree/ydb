using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.Model.Finance
{
    /// <summary>
    /// 资金流水线
    /// </summary>
   public  class BalanceFlow:DDDCommon.Domain.Entity<Guid>
    {
        
        public virtual Guid MemberId { get; set; }
       
        /// <summary>
        /// 本次发生金额
        /// </summary>
        public virtual decimal Amount { get; set; }
        public virtual DateTime OccurTime { get; set; }
        /// <summary>
        /// 关联对象的Id, 比如 订单, 提现单,充值单..等等.
        /// </summary>
        public virtual string RelatedObjectId { get; set; }
        public virtual enumFlowType FlowType { get; set; }

    }
    public enum enumFlowType
    {
        OrderShare,//订单分账
    }
}
