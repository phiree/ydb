using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.ApplicationService.ModelDto;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.DataStatistics;
using Ydb.Finance.Application;


namespace Ydb.ApplicationService.Application.AgentService
{
    public class FinanceFlowService:IFinanceFlowService
    {
        IDZMembershipService dzMembershipService;
        IStatisticsCount statisticsCount;
        IBalanceFlowService balanceFlowService;
        public FinanceFlowService(IDZMembershipService dzMembershipService,
            IStatisticsCount statisticsCount,IBalanceFlowService balanceFlowService)
        {
            this.dzMembershipService = dzMembershipService;
            this.statisticsCount = statisticsCount;
            this.balanceFlowService = balanceFlowService;
        }

        public IList<FinanceFlowDto> GetFinanceFlowList(IList<string> areaList, MemberDto memberAgent)
        {
            IList<MemberDto> memberList = dzMembershipService.GetMembershipsByArea(areaList, Membership.DomainModel.Enums.UserType.customerservice);
            memberList.Add(memberAgent);
            IList<string> UserIdList = memberList.Select(x => x.Id.ToString()).ToList();
            IList<BalanceFlowDto> balanceFlowDtoList = balanceFlowService.GetBalanceFlowByArea(UserIdList);
            IList<FinanceFlowDto> financeFlowDtoList = statisticsCount.StatisticsFinanceFlowList(balanceFlowDtoList,memberList);
            return financeFlowDtoList;
        }
    }
}
