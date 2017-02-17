using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.Application;
using Ydb.Membership.DomainModel;
using Ydb.ApplicationService.ModelDto;

namespace Ydb.ApplictaionService.DataStatistics
{
    public interface IStatisticsCount
    {
        IList<FinanceFlowDto> StatisticsFinanceFlowList(IList<BalanceFlowDto> balanceFlowDtoList, IList<DZMembership> memberList);
    }
}
