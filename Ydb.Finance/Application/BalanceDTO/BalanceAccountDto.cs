using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class BalanceAccountDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户账户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户绑定的提现账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户绑定的提现账户的真实姓名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 用户绑定支付账户类型
        /// </summary>
        public AccountTypeEnums AccountType { get; set; }

        /// <summary>
        /// 预留手机号码，作短信验证用
        /// </summary>
        public string AccountPhone { get; set; }

        /// <summary>
        /// 省份证号码，用于修改账户验证用，输错身份证就不能修改提现账户
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        /// 有效状态,1为有效，0为无效
        /// </summary>
        public int flag { get; set; }
    }
}
