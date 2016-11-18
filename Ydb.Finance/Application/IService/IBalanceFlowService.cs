using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Ydb.Finance.DomainModel;
using Ydb.Common.Specification;
namespace Ydb.Finance.Application
{
    public interface IBalanceFlowService
    {
        /// <summary>
        /// 保存一条账户流水信息
        /// </summary>
        /// <param name="flow" type="BalanceFlowDto">账户流水信息</param>
        void Save( BalanceFlowDto flow);

        /// <summary>
        /// 获取所有账户流水信息
        /// </summary>
        /// <returns type="IList<BalanceFlowDto>">账户流水信息列表</returns>
        IList< BalanceFlowDto> GetAll();

        /// <summary>
        /// 根据条件获取账户流水信息
        /// </summary>
        /// <param name="traitFilter" type="Ydb.Common.Specification.TraitFilter">通用筛选器分页、排序等</param>
        /// <param name="withdrawApplyFilter" type="Ydb.Finance.Application.BalanceFlowFilter">账户流水的查询筛选条件</param>
        /// <returns type="IList<Ydb.Finance.Application.BalanceFlowDto>">账户流水信息列表</returns>
        IList<BalanceFlowDto> GetBalanceFlowList(TraitFilter traitFilter, BalanceFlowFilter balanceFlowFilter);


        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID" type="string">用户信息ID</param>
        /// <param name="startTime" type="DateTime">查询的开始时间</param>
        /// <param name="endTime" type="DateTime">查询的结束时间</param>
        /// <param name="serviceTypeLevel" type="string">服务类型级别</param>
        /// <param name="dateType" type="string">时间类型</param>
        /// <returns type="IList< BalanceFlowDto>">账户统计信息列表</returns>
        IList< BalanceFlowDto> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, 
                        string serviceTypeLevel, string dateType);

        /// <summary>
        /// 根据用户ID获取用户的账单
        /// </summary>
        /// <param name="userID" type="string">用户信息ID</param>
        /// <param name="startTime" type="DateTime">查询的开始时间</param>
        /// <param name="endTime" type="DateTime">查询的结束时间</param>
        /// <param name="serviceTypeLevel" type="string">服务类型级别</param>
        /// <param name="status" type="string">收支状态</param>
        /// <param name="billType" type="string">流水记录类型</param>
        /// <param name="orderId" type="string">订单ID</param>
        /// <param name="billServiceType" type="string">服务类型</param>
        /// <param name="filter" type="Ydb.Common.Specification.TraitFilter">筛选器</param>
        /// <returns type="IList">统计结果列表</returns>
        IList GetBillList(string userID, DateTime startTime, DateTime endTime,
            string serviceTypeLevel, string status, string billType, string orderId, string billServiceType, Ydb.Common.Specification.TraitFilter filter);


    }
}
