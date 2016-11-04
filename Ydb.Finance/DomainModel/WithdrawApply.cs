﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.DomainModel
{
    public class WithdrawApply : Entity<Guid>
    {
        /// <summary>
        /// 用户账户ID
        /// </summary>
        public virtual string ApplyUserId { get; set; }

        /// <summary>
        /// 本次发生金额
        /// </summary>
        public virtual decimal ApplyAmount { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public virtual DateTime ApplyTime { get; set; }

        /// <summary>
        /// 关联对象的Id, 比如 订单, 提现单,充值单..等等.
        /// </summary>
        public virtual BalanceAccount RelatedObjectId { get; set; }

        /// <summary>
        /// 发生类型
        /// </summary>
        public virtual Enums.FlowType FlowType { get; set; }

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
    }
}