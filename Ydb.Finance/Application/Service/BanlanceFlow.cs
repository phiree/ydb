using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Collections;
using Ydb.Finance.DomainModel.Enums;
using Ydb.Finance.DomainModel;
using AutoMapper;
using Ydb.Common.Specification;

namespace Ydb.Finance.Application
{
    public class BalanceFlowService : IBalanceFlowService
    {
        IRepositoryBalanceFlow repositoryBalanceFlow;
        public BalanceFlowService(IRepositoryBalanceFlow repositoryBalanceFlow)
        {
            this.repositoryBalanceFlow= repositoryBalanceFlow;
        }

        /// <summary>
        /// 获取所有账户流水信息
        /// </summary>
        /// <returns type="IList<BalanceFlowDto>">账户流水信息列表</returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList<BalanceFlowDto> GetAll()
        {
            return Mapper.Map<IList<BalanceFlow>, IList<BalanceFlowDto>>(repositoryBalanceFlow.Find(x => true)); 
        }

        /// <summary>
        /// 根据条件获取账户流水信息
        /// </summary>
        /// <param name="traitFilter" type="Ydb.Common.Specification.TraitFilter">通用筛选器分页、排序等</param>
        /// <param name="withdrawApplyFilter" type="Ydb.Finance.Application.BalanceFlowFilter">账户流水的查询筛选条件</param>
        /// <returns type="IList<Ydb.Finance.Application.BalanceFlowDto>">账户流水信息列表</returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList<BalanceFlowDto> GetBalanceFlowList(TraitFilter traitFilter, BalanceFlowFilter balanceFlowFilter)
        {
            var where = PredicateBuilder.True<BalanceFlow>();
            if (!string.IsNullOrEmpty(balanceFlowFilter.AccountId))
            {
                where = where.And(x => x.AccountId == balanceFlowFilter.AccountId);
            }
            if (!string.IsNullOrEmpty(balanceFlowFilter.RelatedObjectId))
            {
                where = where.And(x => x.RelatedObjectId == balanceFlowFilter.RelatedObjectId);
            }
            if (!string.IsNullOrEmpty(balanceFlowFilter.SerialNo))
            {
                where = where.And(x => x.SerialNo == balanceFlowFilter.SerialNo);
            }
            if (!string.IsNullOrEmpty(balanceFlowFilter.Income))
            {
                where = where.And(x => x.Income == bool.Parse(balanceFlowFilter.Income));
            }
            if (!string.IsNullOrEmpty(balanceFlowFilter.FlowType))
            {
                FlowType enumFlowType;
                bool isFlowType = Enum.TryParse<FlowType>(balanceFlowFilter.FlowType, out enumFlowType);
                if (isFlowType)
                    where = where.And(x => x.FlowType == enumFlowType);
            }
            if (balanceFlowFilter.BeginTime != DateTime.MinValue)
            {
                where = where.And(x => x.OccurTime >= balanceFlowFilter.BeginTime);
            }
            if (balanceFlowFilter.EndTime != DateTime.MinValue)
            {
                where = where.And(x => x.OccurTime <= balanceFlowFilter.EndTime);
            }
            BalanceFlow baseone = null;
            if (!string.IsNullOrEmpty(traitFilter.baseID))
            {
                try
                {
                    baseone = repositoryBalanceFlow.FindByBaseId(new Guid(traitFilter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long totalRecord;
            var list = traitFilter.pageSize == 0 ? repositoryBalanceFlow.Find(where, traitFilter.sortby, traitFilter.ascending, traitFilter.offset, baseone).ToList() : repositoryBalanceFlow.Find(where, traitFilter.pageNum, traitFilter.pageSize, out totalRecord, traitFilter.sortby, traitFilter.ascending, traitFilter.offset, baseone).ToList();

            return Mapper.Map<IList<BalanceFlow>, IList<BalanceFlowDto>>(list);
        }

        /// <summary>
        /// 保存一条账户流水信息
        /// </summary>
        /// <param name="flow" type="BalanceFlowDto">账户流水信息</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void Save(BalanceFlowDto flow)
        {
            FlowType enumFlowType;
            bool isFlowType = Enum.TryParse<FlowType>(flow.FlowType, out enumFlowType);
            if (!isFlowType)
            {
                throw new ArgumentException("传入的流水类型(FlowType)不是有效值！");
            }
            if (flow.Amount < 0)
            {
                throw new ArgumentException("传入的流水金额不能为负值！");
            }
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
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList<BalanceFlowDto> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType)
        {
            //由于重构数据库分隔，多表联查需要重新设计
            //return Mapper.Map<IList<BalanceFlow>, IList<BalanceFlowDto>>(repositoryBalanceFlow.GetBillSatistics(userID, startTime, endTime, serviceTypeLevel, dateType));
            return new List<BalanceFlowDto>();
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
        /// <param name="filter" type="Ydb.Common.Specification.TraitFilter">筛选器</param>
        /// <returns type="IList">统计结果列表</returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList GetBillList(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, 
            string status, string billType, string orderId, string billServiceType, Ydb.Common.Specification.TraitFilter filter)
        {
            //由于重构数据库分隔，多表联查需要重新设计
            return new ArrayList(); 
            //return repositoryBalanceFlow.GetBillList(userID, startTime, endTime, serviceTypeLevel, status, billType, orderId, billServiceType, filter);
        }


    }
}
