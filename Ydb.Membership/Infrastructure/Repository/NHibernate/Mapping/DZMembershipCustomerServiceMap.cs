using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using FluentNHibernate.Mapping;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping
{
    public class DZMembershipCustomerServiceMap : SubclassMap<DZMembershipCustomerService>
    {
        public DZMembershipCustomerServiceMap()
        {
            HasMany<DZMembershipImage>(x => x.DZMembershipImages).Cascade.AllDeleteOrphan().Not.LazyLoad();
            Map(x => x.ApplyMemo);
            Map(x => x.ApplyTime);
            Map(x => x.VerifyTime);
            Map(x => x.RefuseReason);
            Map(x => x.IsAgentCustomerService);
            Map(x => x.IsVerified);
            Map(x => x.VerificationIsAgree);
        }
    }
}
