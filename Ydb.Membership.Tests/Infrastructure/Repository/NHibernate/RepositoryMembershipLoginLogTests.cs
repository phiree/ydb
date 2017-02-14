using NUnit.Framework;
using Ydb.Membership.Infrastructure.Repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.Tests;
using Ydb.Membership.DomainModel;

namespace Ydb.Membership.Infrastructure.Repository.NHibernateTests
{
    [TestFixture()]
    public class RepositoryMembershipLoginLogTests
    {
        IRepositoryMembershipLoginLog repositoryMembershipLoginLog;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            repositoryMembershipLoginLog = Bootstrap.Container.Resolve<IRepositoryMembershipLoginLog>();
        }


        [Test()]
        public void RepositoryMembershipLoginLog_GetMembershipLastLoginLog_Test()
        {
            IList<MembershipLoginLog> membershipLoginLogList = repositoryMembershipLoginLog.GetMembershipLastLoginLog();
        }
    }
}