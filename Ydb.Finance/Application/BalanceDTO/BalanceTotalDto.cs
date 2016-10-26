using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.Application
{
    public class BalanceTotalDto : Entity<Guid>
    {
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
