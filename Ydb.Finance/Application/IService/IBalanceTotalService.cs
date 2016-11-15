using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public interface IBalanceTotalService
    {
        /// <summary>
        /// 根据账户用户ID获取该账户信息
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <returns type="BalanceTotalDto">账户信息</returns>
        BalanceTotalDto GetOneByUserId(string userId);
    }
}
