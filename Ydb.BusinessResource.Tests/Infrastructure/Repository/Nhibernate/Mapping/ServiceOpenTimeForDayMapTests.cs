using NUnit.Framework;
using Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Ydb.BusinessResource.Application.Tests;
using FluentNHibernate.Testing;
using Ydb.Common;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Domain;

namespace Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping.Tests
{
    [TestFixture()]
    public class ServiceOpenTimeForDayMapTests
    {
        ISession session;
        [SetUp]
        public void Setup()
        {

            session = Bootstrap.dbConfigInstantMessage
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping.BusinessImageMap>())
                .BuildSessionFactory().OpenSession();

        }
        [Test()]
        public void ServiceOpenTimeForDayMapTest()
        {
            var aa = new PersistenceSpecification<ServiceOpenTimeForDay>(session);

            aa.CheckProperty(c => c.TimePeriod, new TimePeriod(new Time("12:00"),new Time("15:00")));

     aa  .VerifyTheMappings();
        }
    }
}