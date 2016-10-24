using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Ydb.Finance.DomainModel;
namespace Ydb.Finance.Application
{
    public interface IBalanceFlowService
    {
        /// <summary>
        /// 保存一条流水
        /// </summary>
        /// <param name="flow"></param>
        void Save( BalanceFlow flow);
        /// <summary>
        /// 获取所有的财务流水
        /// </summary>
        /// <returns></returns>
        IList< BalanceFlow> GetAll();

        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        IList< BalanceFlow> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, 
                        string serviceTypeLevel, string dateType);

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
        IList GetBillList(string userID, DateTime startTime, DateTime endTime,
            string serviceTypeLevel, string status, string billType, string orderId, string billServiceType,string filter);


    }
}
