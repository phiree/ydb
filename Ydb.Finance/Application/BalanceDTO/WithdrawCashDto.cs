using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class WithdrawCashDto
    {
        /// <summary>
        /// 提现用户
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 收款账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 收款姓名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 用户绑定支付账户类型
        /// </summary>
        public  AccountTypeEnums AccountType { get; set; }

        /// <summary>
        /// 申请说明
        /// </summary>
        public string Remark { get; set; }
    }
}
