using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;

namespace Ydb.Finance.DomainModel
{
    public interface IRepositoryBalanceAccount : IRepository<BalanceAccount, Guid>
    {
        /// <summary>
        /// 根据提现账号及提现账号类型获取绑定的提现账号信息
        /// </summary>
        /// <param name="account" type="string">提现账号</param>
        /// <param name="accountType" type="string">提现账号类型</param>
        /// <returns type="BalanceAccount">提现账号信息</returns>
        BalanceAccount GetOneByAccount(string account, string accountType);

        /// <summary>
        /// 根据用户账号ID获取该账户的有效提现账户
        /// </summary>
        /// <param name="userId" type="string">用户账号ID</param>
        /// <returns type="BalanceAccount">提现账号信息</returns>
        BalanceAccount GetOneByUserId(string userId);
    }
}
