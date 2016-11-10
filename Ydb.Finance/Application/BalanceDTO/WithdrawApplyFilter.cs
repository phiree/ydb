using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
   public class WithdrawApplyFilter
    {
        /// <summary>
        /// 申请提现用户账户ID
        /// </summary>
        public string ApplyUserId { get; set; }

        /// <summary>
        /// 申请时间的开始时间
        /// </summary>
        public DateTime BeginApplyTime { get; set; }

        /// <summary>
        /// 申请时间的开始时间
        /// </summary>
        public DateTime EndApplyTime { get; set; }

        /// <summary>
        /// 收款账户类型
        /// </summary>
        public AccountTypeEnums AccountType { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public ApplyStatusEnums ApplyStatus { get; set; }

        /// <summary>
        /// 付款人
        /// </summary>
        public string PayUserId { get; set; }

        /// <summary>
        /// 付款时间的开始时间
        /// </summary>
        public DateTime BeginPayTime { get; set; }

        /// <summary>
        /// 付款时间的结束时间
        /// </summary>
        public DateTime EndPayTime { get; set; }

    }
}
