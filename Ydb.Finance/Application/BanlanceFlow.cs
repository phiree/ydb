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
    public class BalanceFlowService : IBalanceFlowService
    {

        ISession session;
        IRepositoryBalanceFlow repositoryBalanceFlow;
        internal BalanceFlowService(ISession session,
        IRepositoryBalanceFlow repositoryBalanceFlow)
        {
            this.repositoryBalanceFlow = repositoryBalanceFlow;
        }

        /// <summary>
        /// 获取所有账户流水信息
        /// </summary>
        /// <returns type="IList<BalanceFlowDto>">账户流水信息列表</returns>
        public IList<BalanceFlowDto> GetAll()
        {
            return Mapper.Map<IList<BalanceFlow>, IList<BalanceFlowDto>>(repositoryBalanceFlow.Find(x => true)); 
        }

        /// <summary>
        /// 保存一条账户流水信息
        /// </summary>
        /// <param name="flow" type="BalanceFlowDto">账户流水信息</param>
        public void Save(BalanceFlowDto flow)
        {
            repositoryBalanceFlow.Add(Mapper.Map<BalanceFlowDto, BalanceFlow>(flow));
        }

        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID" type="string">用户信息ID</param>
        /// <param name="startTime" type="DateTime">查询的开始时间</param>
        /// <param name="endTime" type="DateTime">查询的结束时间</param>
        /// <param name="serviceTypeLevel" type="string">服务类型级别</param>
        /// <param name="dateType" type="string">时间类型</param>
        /// <returns type="IList< BalanceFlowDto>">账户统计信息列表</returns>
        public IList<BalanceFlowDto> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType)
        {
            return Mapper.Map<IList<BalanceFlow>, IList<BalanceFlowDto>>(repositoryBalanceFlow.GetBillSatistics(userID, startTime, endTime, serviceTypeLevel, dateType));
        }

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
        /// <param name="filter" type="string">筛选器</param>
        /// <returns type="IList">统计结果列表</returns>
        public IList GetBillList(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, 
            string status, string billType, string orderId, string billServiceType,string filter)
        {
            return repositoryBalanceFlow.GetBillList(userID, startTime, endTime, serviceTypeLevel, status, billType, orderId, billServiceType, filter);
        }


    }
}
