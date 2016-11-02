using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.DomainModel
{
    internal class BalanceAccount : Entity<Guid>
    {
        /// <summary>
        /// 用户账户ID
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 用户绑定的提现账户
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 用户绑定支付账户类型
        /// </summary>
        public virtual string AccountType { get; set; }

        /// <summary>
        /// 预留手机号码，作短信验证用
        /// </summary>
        public virtual string AccountPhone { get; set; }

        /// <summary>
        /// 省份证号码，用于修改账户验证用，输错身份证就不能修改提现账户
        /// </summary>
        public virtual string AccountCode { get; set; }

        /// <summary>
        /// 有效状态,1为有效，0为无效
        /// </summary>
        public virtual int flag { get; set; }
    }
}
