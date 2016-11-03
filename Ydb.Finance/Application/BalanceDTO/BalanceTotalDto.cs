using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class BalanceTotalDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户账户ID
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 用户账户余额
        /// </summary>
        public virtual decimal Total { get; set; }
    }
}
