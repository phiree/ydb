using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Collections;
using Ydb.Finance.DomainModel;

namespace Ydb.Finance.Application
{
    public class BalanceFlowService : IBalanceFlowService
    {

        ISession session;
        IRepositoryBalanceFlow repositoryBalanceFlow;
        public BalanceFlowService(ISession session,
        IRepositoryBalanceFlow repositoryBalanceFlow)
        {
            this.repositoryBalanceFlow = repositoryBalanceFlow;
        }

        public IList<BalanceFlow> GetList()
        {
          return repositoryBalanceFlow.Find(x => true);
        }

        public void Save(BalanceFlow flow)
        {
            repositoryBalanceFlow.Add(flow);
        }

        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public IList<BalanceFlow> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType)
        {
            return repositoryBalanceFlow.GetBillSatistics(userID, startTime, endTime, serviceTypeLevel, dateType);
        }

        /// <summary>
        /// 根据用户ID获取用户的账单
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="status"></param>
        /// <param name="billType"></param>
        /// <param name="orderId"></param>
        /// <param name="billServiceType"></param>
        /// <param name="filter"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public IList GetBillList(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, 
            string status, string billType, string orderId, string billServiceType,string filter)

        {
            return repositoryBalanceFlow.GetBillList(userID, startTime, endTime, serviceTypeLevel, status, billType, orderId, billServiceType, filter);
        }


    }
}
