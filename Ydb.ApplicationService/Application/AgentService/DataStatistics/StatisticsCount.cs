using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.ModelDto;

namespace Ydb.ApplicationService.Application.AgentService.DataStatistics
{
    public class StatisticsCount : IStatisticsCount
    {

        public IList<FinanceFlowDto> StatisticsFinanceFlowList(IList<BalanceFlowDto> balanceFlowDtoList,IList<MemberDto>memberList )
        {
            IList<FinanceFlowDto> financeFlowDtoList = new List<FinanceFlowDto>();
            foreach (MemberDto member in memberList)
            {
                IList<BalanceFlowDto> flowList = balanceFlowDtoList.Where(x => x.AccountId == member.Id.ToString()).ToList();
                foreach (BalanceFlowDto balanceFlow in flowList)
                {
                    FinanceFlowDto financeFlowDto = new FinanceFlowDto();
                    financeFlowDto.Id = balanceFlow.Id;
                    financeFlowDto.UserId = balanceFlow.AccountId;
                    financeFlowDto.UserNickName = member.DisplayName;
                    financeFlowDto.UserType = member.UserType;
                    financeFlowDto.Amount = balanceFlow.Amount;
                    financeFlowDto.AmountTotal = balanceFlow.AmountTotal;
                    financeFlowDto.AmountView = balanceFlow.AmountView;
                    financeFlowDto.FlowType = balanceFlow.FlowType;
                    financeFlowDto.Income = balanceFlow.Income;
                    financeFlowDto.OccurTime = balanceFlow.OccurTime;
                    financeFlowDto.Rate = balanceFlow.Rate;
                    financeFlowDto.RelatedObjectId = balanceFlow.RelatedObjectId;
                    financeFlowDto.SerialNo = balanceFlow.SerialNo;
                    financeFlowDtoList.Add(financeFlowDto);
                }
            }
            return financeFlowDtoList;
        }

        public IList<FinanceTotalDto> StatisticsFinanceTotalList(IList<BalanceTotalDto> balanceTotalDtoList, IList<DZMembershipCustomerServiceDto> memberList)
        {
            IList<FinanceTotalDto> financeTotalDtoList = new List<FinanceTotalDto>();
            foreach (DZMembershipCustomerServiceDto member in memberList)
            {
                IList<BalanceTotalDto> TotalList = balanceTotalDtoList.Where(x => x.UserId == member.Id.ToString()).ToList();
                foreach (BalanceTotalDto balanceTotal in TotalList)
                {
                    FinanceTotalDto financeTotalDto = new FinanceTotalDto();
                    financeTotalDto.Id = balanceTotal.Id;
                    financeTotalDto.UserId = balanceTotal.UserId;
                    financeTotalDto.UserNickName = member.DisplayName;
                    financeTotalDto.UserType = member.UserType;
                    financeTotalDto.Phone = member.Phone;
                    financeTotalDto.RealName = member.RealName;
                    financeTotalDto.IsAgentCustomerService = member.IsAgentCustomerService;
                    financeTotalDto.Total = balanceTotal.Total;
                    financeTotalDto.Frozen = balanceTotal.Frozen;
                    financeTotalDto.Account = balanceTotal.AccountDto.Account;
                    financeTotalDtoList.Add(financeTotalDto);
                }
            }
            return financeTotalDtoList;
        }


    }
}
