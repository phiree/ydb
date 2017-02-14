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
using Ydb.Common.Domain;

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
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping.BusinessImageMap>())
                .BuildSessionFactory().OpenSession();

        }
        /// <summary>
        /// 确认是否做mapping
        /// </summary>
        [Test()]
        public void AllMappingsBasicTest()
        {
            new PersistenceSpecification<Business>(session).VerifyTheMappings();
            new PersistenceSpecification<DZService>(session).VerifyTheMappings();
            new PersistenceSpecification<ServiceOpenTime>(session).VerifyTheMappings();
            new PersistenceSpecification<ServiceOpenTimeForDay>(session).VerifyTheMappings();
            new PersistenceSpecification<ServiceType>(session).VerifyTheMappings();
            new PersistenceSpecification<Staff>(session).VerifyTheMappings();
            new PersistenceSpecification<Area>(session).VerifyTheMappings();
            new PersistenceSpecification<DZTag>(session).VerifyTheMappings();
        }
        /// <summary>
        /// mapping测试
        /// </summary>
        [Test()]
        public void DZServiceMapTest()
        {
        new PersistenceSpecification<DZService>(session)
        
        .CheckProperty(c => c.AllowedPayType,enum_PayType.Offline )
      
        .VerifyTheMappings();
        }
        [Test()]
        public void BusinessMapTest()
        {


            new PersistenceSpecification<Business>(session)

       .CheckProperty(c =>c.StaffAmount, 1)

       .VerifyTheMappings();
        }

        
    }
}