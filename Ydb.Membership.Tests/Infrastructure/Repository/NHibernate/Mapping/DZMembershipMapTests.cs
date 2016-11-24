using NUnit.Framework;
using Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Testing;
using NHibernate;
using Ydb.Common;
using Ydb.Membership.DomainModel;
using Ydb.Membership.Tests;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping.Tests
{
    [TestFixture()]
    public class DZMembershipMapTests
    {
        private ISession session;
        [SetUp]
        public void setup()
        {
            session = Bootstrap.dbConfigMembership
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping.DZMembershipMap>())
               .BuildSessionFactory().OpenSession();
        }

        [Test()]
        public void DZMembershipMapTest()
        {
            new PersistenceSpecification<DZMembership>(session)


       .VerifyTheMappings();
        }
    }
}