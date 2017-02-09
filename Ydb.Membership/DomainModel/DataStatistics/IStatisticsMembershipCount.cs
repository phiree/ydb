using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel.DataStatistics
{
    public interface IStatisticsMembershipCount
    {
        long StatisticsLoginCountLastMonth(IList<DZMembership> memberList, IList<MembershipLoginLog> loginList);
    }
}
