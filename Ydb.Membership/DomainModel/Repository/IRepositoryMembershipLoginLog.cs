using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
using Ydb.Common.Specification;

namespace Ydb.Membership.DomainModel.Repository
{
    public interface IRepositoryMembershipLoginLog : IRepository<MembershipLoginLog, Guid>
    {
        IList<MembershipLoginLog> GetMembershipLoginLogListByTime(DateTime beginTime, DateTime endTime);
    }
}
