using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.ApplicationService.ModelDto;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.Application.AgentService.DataStatistics;
using Ydb.Finance.Application;


namespace Ydb.ApplicationService.Application.AgentService
{
    public class FinanceFlowService:IFinanceFlowService
    {
        IDZMembershipService dzMembershipService;
        IStatisticsCount statisticsCount;
        IBalanceFlowService balanceFlowService;
        IBalanceTotalService balanceTotalService;
        IWithdrawApplyService withdrawApplyService;
        public FinanceFlowService(IDZMembershipService dzMembershipService,
            IStatisticsCount statisticsCount,IBalanceFlowService balanceFlowService,
            IBalanceTotalService balanceTotalService, IWithdrawApplyService withdrawApplyService)
        {
            this.dzMembershipService = dzMembershipService;
            this.statisticsCount = statisticsCount;
            this.balanceFlowService = balanceFlowService;
            this.balanceTotalService = balanceTotalService;
            this.withdrawApplyService = withdrawApplyService;
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

        public IList<FinanceTotalDto> GetFinanceTotalList(IList<string> areaList)
        {
            IList<DZMembershipCustomerServiceDto> memberList = dzMembershipService.GetDZMembershipCustomerServiceByArea(areaList);
            IList<string> UserIdList = memberList.Select(x => x.Id.ToString()).ToList();
            IList<BalanceTotalDto> balanceTotalDtoList = balanceTotalService.GetBalanceTotalByArea(UserIdList);
            IList<FinanceTotalDto> financeTotalDtoList = statisticsCount.StatisticsFinanceTotalList(balanceTotalDtoList, memberList);
            return financeTotalDtoList;
        }

        public FinanceWithdrawTotalDto GetFinanceWithdrawList(IList<string> areaList,MemberDto memberAgent)
        {
            IList<DZMembershipCustomerServiceDto> memberList = dzMembershipService.GetDZMembershipCustomerServiceByArea(areaList).Where (x=>x.IsAgentCustomerService).ToList();
            IList<string> UserIdList = memberList.Select(x => x.Id.ToString()).ToList();
            UserIdList.Add(memberAgent.Id.ToString());
            IList<WithdrawApplyDto> withdrawApplyDtoList = withdrawApplyService.GetWithdrawApplyListByArea(UserIdList);
            IList<MemberDto> memberDtoList = new List<MemberDto>();
            memberDtoList.Add(memberAgent);
            foreach (DZMembershipCustomerServiceDto cs in memberList)
            {
                memberDtoList.Add(cs);
            }
            FinanceWithdrawTotalDto financeWithdrawTotalDto = statisticsCount.StatisticsFinanceWithdrawList(withdrawApplyDtoList, memberDtoList);
            return financeWithdrawTotalDto;
        }
    }
}
