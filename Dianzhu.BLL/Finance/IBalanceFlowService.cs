using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Dianzhu.BLL.Finance
{
    public interface IBalanceFlowService
    {
        void Save(Dianzhu.Model.Finance.BalanceFlow flow);
        IList<Dianzhu.Model.Finance.BalanceFlow> GetList();

        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        IList<Dianzhu.Model.Finance.BalanceFlow> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType);

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
        IList GetBillList(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string status, string billType, string orderId, string billServiceType, Model.Trait_Filtering filter);


    }
}
