using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel.Enums;
using Ydb.Common.Domain;
namespace Ydb.Finance.DomainModel
{
    public class BalanceFlow : Entity<Guid>
    {
        /// <summary>
        /// 用户账户ID
        /// </summary>
        public virtual string AccountId { get; set; }

        /// <summary>
        /// 本次发生金额
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public virtual DateTime OccurTime { get; set; }

        /// <summary>
        /// 关联对象的Id, 比如 订单, 提现单,充值单..等等.
        /// </summary>
        public virtual string RelatedObjectId { get; set; }

        /// <summary>
        /// 发生类型
        /// </summary>
        public virtual FlowType FlowType { get; set; }

        /// <summary>
        /// true为收入，false为支出
        /// </summary>
        public virtual bool Income { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public virtual string AmountTotal { get; set; }

        /// <summary>
        /// 比率
        /// </summary>
        public virtual string Rate { get; set; }

        /// <summary>
        /// 本次发生金额显示字符串
        /// </summary>
        public virtual string AmountView { get; set; }
    }
  
}
