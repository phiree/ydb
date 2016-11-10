using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using NHibernate;
using Ydb.Common.Repository;

namespace Ydb.Finance.Infrastructure.Repository
{
    public class RepositoryBalanceTotal : NHRepositoryBase<BalanceTotal, Guid>, IRepositoryBalanceTotal
    {
        

        /// <summary>
        /// 根据账户用户ID获取该账户信息
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <returns type="BalanceTotal">账户信息</returns>
        public BalanceTotal GetOneByUserId(string userId)
        {
            var result = FindOne(x => x.UserId == userId);
            return result;
        }

        /// <summary>
        /// 根据账户用户ID获取该账户信息,先判断该账户是否存在
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <returns type="BalanceTotal">账户信息</returns>
        public BalanceTotal CheckByUserId(string userId)
        {
            var result = FindOne(x => x.UserId == userId);
            if (result == null)
            {
                throw new Exception("该账户不存在！");
            }
            return result;
        }

        /// <summary>
        /// 获取所有的账户信息列表
        /// </summary>
        /// <returns type="IList<BalanceTotal>">账户信息列表</returns>
        public IList<BalanceTotal> GetAll()
        {
            return Find(x => true);
        }

        /// <summary>
        /// 账户入账
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">入账金额</param>
        public void InBalance(string userId,decimal amount )
        {
            BalanceTotal bt = GetOneByUserId(userId);
            if (bt == null)
            {
                bt = new BalanceTotal { UserId = userId, Total = amount };
                Add(bt);
            }
            else
            {
                bt.Total = bt.Total + amount;
                Update(bt);
            }
        }

        /// <summary>
        /// 账户出账,直接从余额中出账
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">出账金额</param>
        public void OutBalance(string userId, decimal amount)
        {
            BalanceTotal bt = CheckByUserId(userId);
            if (bt.Total < amount)
            {
                throw new Exception("账户余额不足！");
            }
            bt.Total = bt.Total - amount;
            Update(bt);
        }

        /// <summary>
        /// 账户冻结
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">冻结金额</param>
        public void FrozenBalance(string userId, decimal amount)
        {
            BalanceTotal bt = CheckByUserId(userId);
            if (bt.Total < amount)
            {
                throw new Exception("账户余额不足！");
            }
            bt.Total = bt.Total - amount;
            bt.Frozen = bt.Frozen + amount;
            Update(bt);
        }

        /// <summary>
        /// 账户解冻
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">解冻金额</param>
        public void ReleaseBalance(string userId, decimal amount)
        {
            BalanceTotal bt = CheckByUserId(userId);
            if (bt.Frozen < amount)
            {
                throw new Exception("冻结余额额度不足！");
            }
            bt.Total = bt.Total + amount;
            bt.Frozen = bt.Frozen - amount;
            Update(bt);
        }

        /// <summary>
        /// 账户出账,从冻结的余额中出账
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <param name="amount" type="decimal">出账金额</param>
        public void OutFrozenBalance(string userId, decimal amount)
        {
            BalanceTotal bt = CheckByUserId(userId);
            if (bt.Frozen < amount)
            {
                throw new Exception("账户的冻结余额不足！");
            }
            bt.Frozen = bt.Frozen - amount;
            Update(bt);
        }
        

    }
}
