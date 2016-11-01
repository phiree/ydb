using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class WithdrawCashParam
    {
        /// <summary>
        /// 提现申请单号
        /// </summary>
        public virtual string RelatedObjectId { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// 提现用户
        /// </summary>
        public string AccountId { get; set; }
    }
}
