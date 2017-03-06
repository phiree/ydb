using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.ModelDto;
using Ydb.Order.DomainModel;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.ApplicationService.Application.AgentService.DataStatistics
{
    public interface IStatisticsCount
    {
        IList<FinanceFlowDto> StatisticsFinanceFlowList(IList<BalanceFlowDto> balanceFlowDtoList, IList<MemberDto> memberList);

        IList<FinanceTotalDto> StatisticsFinanceTotalList(IList<BalanceTotalDto> balanceTotalDtoList, IList<DZMembershipCustomerServiceDto> memberList);

        FinanceWithdrawTotalDto StatisticsFinanceWithdrawList(IList<WithdrawApplyDto> withdrawApplyDtoList, IList<MemberDto> memberList);

        StatisticsInfo StatisticsNewOrdersCountListByTime(IList<ServiceOrder> orderList, DateTime beginTime, DateTime endTime, bool IsHour);
        StatisticsInfo StatisticsAllOrdersCountListByTime(IList<ServiceOrder> orderList, DateTime beginTime, DateTime endTime, bool IsHour);
        StatisticsInfo StatisticsAllOrdersCountGroupByArea(IList<ServiceOrder> orderList, IList<Business> businessList, IList<Area> areaList);
    }
}
