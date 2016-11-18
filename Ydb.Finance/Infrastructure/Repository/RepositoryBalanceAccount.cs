using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
using NHibernate.Transform;

namespace Ydb.Finance.Infrastructure.Repository
{
    public class RepositoryBalanceAccount : NHRepositoryBase<BalanceAccount, Guid>, IRepositoryBalanceAccount
    {
        /// <summary>
        /// 根据提现账号及提现账号类型获取绑定的提现账号信息
        /// </summary>
        /// <param name="account" type="string">提现账号</param>
        /// <param name="accountType" type="string">提现账号类型</param>
        /// <param name="userId" type="string">用户账号ID</param>
        /// <returns type="BalanceAccount">提现账号信息</returns>
        public BalanceAccount GetOneByAccount(string account, string accountType,string userId)
        {
            var result = FindOne(x => x.Account == account && x.AccountType==accountType && x.UserId== userId);
            return result;
        }

        /// <summary>
        /// 根据用户账号ID获取该账户的有效提现账户
        /// </summary>
        /// <param name="userId" type="string">用户账号ID</param>
        /// <returns type="BalanceAccount">提现账号信息</returns>
        public BalanceAccount GetOneByUserId(string userId)
        {
            var result = FindOne(x => x.UserId == userId && x.flag == 1);
            return result;
        }

    }
}
