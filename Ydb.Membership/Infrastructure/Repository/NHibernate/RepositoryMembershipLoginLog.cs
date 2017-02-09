using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Specification;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Common.Domain;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate
{
    public class RepositoryMembershipLoginLog : NHRepositoryBase<MembershipLoginLog, Guid>, IRepositoryMembershipLoginLog
    {
        public IList<MembershipLoginLog> GetMembershipLoginLogListByTime( DateTime beginTime, DateTime endTime)
        {
            var where = Ydb.Common.Specification.PredicateBuilder.True<MembershipLoginLog>();
            if (beginTime != DateTime.MinValue)
            {
                where = where.And(x => x.LogTime >= beginTime);
            }
            if (endTime != DateTime.MinValue)
            {
                where = where.And(x => x.LogTime < endTime);
            }
            return Find(where).ToList();
        }
    }
}
