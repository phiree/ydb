using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Ydb.Finance.DomainModel;

namespace Ydb.Finance.Application
{
    public interface IBalanceAccountService
    {
        /// <summary>
        /// 绑定提现账号
        /// </summary>
        /// <param name="balanceAccountDto" type="BalanceAccountDto">要绑定的账号信息</param>
        void BindingAccount(BalanceAccountDto balanceAccountDto);

        /// <summary>
        /// 修改提现账号
        /// </summary>
        /// <param name="balanceAccountDto" type="BalanceAccountDto">要修改的账号信息</param>
        void UpdateAccount(BalanceAccountDto balanceAccountDto);

        /// <summary>
        /// 根据用户获取该用户的提现账号
        /// </summary>
        /// <param name="userId" type="string"用户账号ID</param>
        /// <returns type="BalanceAccountDto">提现账号信息</returns>
        BalanceAccountDto GetAccount(string userId);
    }
}
