using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using FluentNHibernate.Mapping;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping
{
    public class MembershipLoginLogMap : ClassMap<MembershipLoginLog>
    {
        public MembershipLoginLogMap()
        {
            Id(x => x.Id);
            Map(x => x.LogTime);
            Map(x => x.LogType).CustomType<enumLoginLogType>();
            Map(x => x.Memo);
            Map(x => x.MemberId);

        }
    }
}
