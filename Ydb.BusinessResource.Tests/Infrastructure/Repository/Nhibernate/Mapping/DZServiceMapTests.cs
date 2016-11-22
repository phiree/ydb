using NUnit.Framework;
using Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Testing;
using Ydb.BusinessResource.DomainModel;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Ydb.BusinessResource.Application.Tests;
using Ydb.Common;
namespace Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping.Tests
{
    [TestFixture()]
    public class DZServiceMapTests
    {
        ISession session;
        [SetUp]
        public void Setup()
        {

            session = Bootstrap.dbConfigInstantMessage
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping.AreaMap>())
                .BuildSessionFactory().OpenSession();

        }
        [Test()]
        public void DZServiceMapTest()
        {
             new PersistenceSpecification<DZService>(session)
        
        .CheckProperty(c => c.AllowedPayType,enum_PayType.Offline )
      
        .VerifyTheMappings();
        }
    }
}