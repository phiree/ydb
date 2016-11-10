using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.DomainModel
{
    public class WithdrawApply : Entity<Guid>
    {
        public WithdrawApply()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 申请提现用户账户ID
        /// </summary>
        public virtual string ApplyUserId { get; set; }

        /// <summary>
        /// 申请提现金额
        /// </summary>
        public virtual decimal ApplyAmount { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public virtual DateTime ApplyTime { get; set; }

        /// <summary>
        /// 收款账户
        /// </summary>
        public virtual BalanceAccount ReceiveAccount { get; set; }

        /// <summary>
        /// 实际转账金额
        /// </summary>
        public virtual decimal TransferAmount { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public virtual decimal ServiceFee { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public virtual string ApplyStatus { get; set; }

        /// <summary>
        /// 申请备注
        /// </summary>
        public virtual string ApplyRemark { get; set; }

        /// <summary>
        /// 手续费率
        /// </summary>
        public virtual string Rate { get; set; }

        /// <summary>
        /// 付款人
        /// </summary>
        public virtual string PayUserId { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public virtual DateTime PayTime { get; set; }

        /// <summary>
        /// 付款状态
        /// </summary>
        public virtual string PayStatus { get; set; }

        /// <summary>
        /// 付款备注
        /// </summary>
        public virtual string PayRemark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }

        /// <summary>
        /// 申请单流水编号
        /// </summary>
        public virtual string SerialNo { get; set; }

    }
}
