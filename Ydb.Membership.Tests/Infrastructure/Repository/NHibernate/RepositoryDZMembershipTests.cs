using NUnit.Framework;
using Ydb.Membership.Infrastructure.Repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Tests;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;

namespace Ydb.Membership.Infrastructure.Repository.NHibernateTests
{
    [TestFixture()]
    public class RepositoryDZMembershipTests
    {
       IRepositoryDZMembership repositoryDZMembership;
       [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            repositoryDZMembership = Bootstrap.Container.Resolve<IRepositoryDZMembership>();
        }

        [Test()]
        public void RepositoryDZMembership_GetUsersCountByArea_Test()
        {
            IList<string> areaList = new List<string> { "1", "2" };
            long c = repositoryDZMembership.GetUsersCountByArea(areaList,DateTime.MinValue,DateTime.MinValue,UserType.customer);
        }
    }
}