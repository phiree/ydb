using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Collections;
using Ydb.Finance.DomainModel;
using AutoMapper;

namespace Ydb.Finance.Application
{
    public class BalanceTotalService: IBalanceTotalService
    {
        IRepositoryBalanceTotal repositoryBalanceTotal;
        public BalanceTotalService(IRepositoryBalanceTotal repositoryBalanceTotal)
        {
            this.repositoryBalanceTotal = repositoryBalanceTotal;
        }

        /// <summary>
        /// 根据账户用户ID获取该账户信息
        /// </summary>
        /// <param name="userId" type="string">账户用户ID</param>
        /// <returns type="BalanceTotalDto">账户信息</returns>
        public BalanceTotalDto GetOneByUserId(string userId)
        {
            return Mapper.Map<BalanceTotal, BalanceTotalDto>(repositoryBalanceTotal.GetOneByUserId(userId));
        }
    }
}
