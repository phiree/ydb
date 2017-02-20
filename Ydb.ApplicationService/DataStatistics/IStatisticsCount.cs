using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.ModelDto;

namespace Ydb.ApplicationService.DataStatistics
{
    public interface IStatisticsCount
    {
        IList<FinanceFlowDto> StatisticsFinanceFlowList(IList<BalanceFlowDto> balanceFlowDtoList, IList<MemberDto> memberList);
    }
}
