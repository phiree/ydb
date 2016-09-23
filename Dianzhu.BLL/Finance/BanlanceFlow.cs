using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model.Finance;

namespace Dianzhu.BLL.Finance
{
    public class BalanceFlowService : IBalanceFlowService
    {
        IDAL.Finance.IDALBalanceFlow dalBalanceFlow;

        public BalanceFlowService(IDAL.Finance.IDALBalanceFlow dalBalanceFlow)
        {
            this.dalBalanceFlow = dalBalanceFlow;
        }

        public IList<BalanceFlow> GetList()
        {
          return  dalBalanceFlow.Find(x => true);
        }

        public void Save(BalanceFlow flow)
        {
            dalBalanceFlow.Add(flow);
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
            return dalBalanceFlow.GetBillSatistics(userID, startTime, endTime, serviceTypeLevel, dateType);
        }
    }
}
