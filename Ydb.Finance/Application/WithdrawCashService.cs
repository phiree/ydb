using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using Ydb.Finance.DomainModel.Enums;
using NHibernate;

namespace Ydb.Finance.Application
{
    public class WithdrawCashService: IWithdrawCashService
    {
      
        IRepositoryBalanceFlow repositoryBalanceFlow;
        IRepositoryBalanceTotal repositoryBalanceTotal;
        internal WithdrawCashService(IRepositoryBalanceFlow repositoryBalanceFlow, IRepositoryBalanceTotal repositoryBalanceTotal)
        {
         
            this.repositoryBalanceFlow = repositoryBalanceFlow;
            this.repositoryBalanceTotal = repositoryBalanceTotal;
        }

        /// <summary>
        /// 提现操作
        /// </summary>
        /// <param name="order" type="IList<WithdrawCashParam>">提现信息列表</param>
        [Ydb.Common.Repository.UnitOfWork]
        public void WithdrawCash(IList<WithdrawCashParam> cashList)
        {
            foreach (WithdrawCashParam cash in cashList)
            {
                BalanceFlow flow = new BalanceFlow
                {
                    AccountId = cash.AccountId,
                    Amount = cash.Amount,
                    RelatedObjectId = cash.RelatedObjectId,
                    OccurTime = DateTime.Now,
                    FlowType = FlowType.Withdrawals,
                    Income = false
                };
                repositoryBalanceFlow.Add(flow);
                repositoryBalanceTotal.OutBalance(cash.AccountId, cash.Amount);
            }
        }
    }
}
