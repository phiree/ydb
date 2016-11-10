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
    public interface IRepositoryBalanceTotal : IRepository<BalanceTotal, Guid>
    {
        /// <summary>
        /// 根据账户用户ID获取该账户信息
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <returns type="BalanceTotal">账户信息</returns>
        BalanceTotal GetOneByUserId(string userId);

        /// <summary>
        /// 获取所有的账户信息列表
        /// </summary>
        /// <returns type="IList<BalanceTotal>">账户信息列表</returns>
        IList<BalanceTotal> GetAll();

        /// <summary>
        /// 账户入账
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">入账金额</param>
        void InBalance(string userId, decimal amount);

        /// <summary>
        /// 账户出账
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">出账金额</param>
        void OutBalance(string userId, decimal amount);

        /// <summary>
        /// 账户冻结
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">冻结金额</param>
        void FrozenBalance(string userId, decimal amount);

        /// <summary>
        /// 账户解冻
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">解冻金额</param>
        void ReleaseBalance(string userId, decimal amount);

        /// <summary>
        /// 账户出账,从冻结的余额中出账
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">出账金额</param>
        void OutFrozenBalance(string userId, decimal amount);
    }
}
