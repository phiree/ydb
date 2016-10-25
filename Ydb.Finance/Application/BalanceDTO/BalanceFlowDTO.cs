using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.Application
{
    public class BalanceFlowDto : Entity<Guid>
    {
        public virtual string AccountId { get; set; }
        /// <summary>
        /// 本次发生金额
        /// </summary>
        public virtual decimal Amount { get; set; }
        public virtual DateTime OccurTime { get; set; }
        /// <summary>
        /// 关联对象的Id, 比如 订单, 提现单,充值单..等等.
        /// </summary>
        public virtual string RelatedObjectId { get; set; }
        public virtual string FlowType { get; set; }
    }
}
