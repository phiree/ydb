using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class BalanceUserParam
    {
        /// <summary>
        /// 分账用户ID
        /// </summary>
        public virtual string AccountId { get; set; }
        /// <summary>
        /// 分账用户类型
        /// </summary>
        public virtual string UserType { get; set; }
    }
}
