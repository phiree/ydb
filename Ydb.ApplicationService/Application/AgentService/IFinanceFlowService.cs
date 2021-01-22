using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.ApplicationService.ModelDto;
using Ydb.Membership.Application.Dto;


namespace Ydb.ApplicationService.Application.AgentService
{
    public interface IFinanceFlowService
    {
        IList<FinanceFlowDto> GetFinanceFlowList(IList<string> areaList,MemberDto memberAgent);

        IList<FinanceTotalDto> GetFinanceTotalList(IList<string> areaList);

        FinanceWithdrawTotalDto GetFinanceWithdrawList(IList<string> areaList, MemberDto memberAgent);
    }
}
