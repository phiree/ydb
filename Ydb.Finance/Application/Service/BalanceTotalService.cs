using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Collections;
using Ydb.Finance.DomainModel;
using AutoMapper;
using Ydb.Common.Specification;

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

        /// <summary>
        /// 获取代理及其助理的所有账户余额信息
        /// </summary>
        /// <param name="UserList">代理及其助理的用户Id列表</param>
        /// <returns></returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList<BalanceTotalDto> GetBalanceTotalByArea(IList<string> UserIdList)
        {
            var where = PredicateBuilder.True<BalanceTotal>();
            if (UserIdList.Count > 0)
            {
                where = where.And(x => UserIdList.Contains(x.UserId));
            }
            return Mapper.Map<IList<BalanceTotal>, IList<BalanceTotalDto>>(repositoryBalanceTotal.Find(where));
        }
    }
}
