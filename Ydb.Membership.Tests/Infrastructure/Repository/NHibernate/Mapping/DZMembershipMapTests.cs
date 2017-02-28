using System;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using Ydb.Membership.DomainModel;
using Ydb.Membership.Tests;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping.Tests
{
    [TestFixture]
    public class DZMembershipMapTests
    {
        [SetUp]
        public void setup()
        {
            session = Bootstrap.dbConfigMembership
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DZMembershipMap>())
                .BuildSessionFactory().OpenSession();
        }

        private ISession session;

        [Test]
        public void DZMembershipMapTest()
        {
            new PersistenceSpecification<DZMembership>(session).CheckProperty(x => x.Id, Guid.NewGuid())
                .VerifyTheMappings();
        }
    }
}