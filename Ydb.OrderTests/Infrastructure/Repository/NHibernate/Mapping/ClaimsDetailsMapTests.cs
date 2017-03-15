using NUnit.Framework;
using Ydb.Order.Infrastructure.Repository.NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Testing;
using Ydb.Order.DomainModel;
using NHibernate;
using Ydb.OrderTests;
using Ydb.Common;
using System.Collections;

namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping.Tests
{
    [TestFixture()]
    public class OrderMapTests
    {
        private ISession session;
        [SetUp]
        public void setup()
        {
            session = Bootstrap.dbConfigOrder
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Order.Infrastructure.Repository.NHibernate.Mapping.ClaimsDetailsMap>())
               .BuildSessionFactory().OpenSession();
        }

       
        [Test()]
        public void OrderAssignmentMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t = new DateTime(2011, 1, 1);
            new PersistenceSpecification<OrderAssignment>(session)
     //    .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.AssignedStaffId, "assignedstaffid")
         .CheckProperty(c => c.BusinessId,"businessid")
          .CheckProperty(c => c.Enabled,true)
           .CheckProperty(c => c.CreateTime, t)
            .CheckProperty(c => c.DeAssignedTime, t)
           
            .VerifyTheMappings();
        }

        [Test()]
        public void ClaimsDetailsMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t =new DateTime(2014,1,1);
            new PersistenceSpecification<ClaimsDetails>(session)
         .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.Amount, 1m)
         .CheckProperty(c => c.ClaimsDetailsResourcesUrl.Count, 0)
          .CheckProperty(c => c.Context, "context")
           .CheckProperty(c => c.CreatTime, t)
            .CheckProperty(c => c.LastUpdateTime, t)
            .CheckProperty(c => c.MemberId, "memberid")
            .CheckProperty(c => c.Target, enum_ChatTarget.user)

            .VerifyTheMappings();
        }
       

      
    }
}