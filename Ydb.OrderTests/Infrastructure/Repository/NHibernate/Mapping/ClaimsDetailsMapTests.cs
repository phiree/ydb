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
        public void ClaimsDetailsMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t =DateTime.Now;
            DateTime t2 = DateTime.Now;
            new PersistenceSpecification<ClaimsDetails>(session)
         .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.Amount, 1m)
         .CheckProperty(c => c.ClaimsDetailsResourcesUrl.Count, 0)
          .CheckProperty(c => c.Context,"context")
           .CheckProperty(c => c.CreatTime, t,new DateTimeEqualityComparer(new TimeSpan(0,0,1)))
            .CheckProperty(c => c.LastUpdateTime,t2, new DateTimeEqualityComparer(new TimeSpan(0, 0, 1)))
            .CheckProperty(c => c.MemberId, "memberid")
            .CheckProperty(c=> c.Target,enum_ChatTarget.all)
            
            .VerifyTheMappings();
        }
        class DateTimeEqualityComparer : IEqualityComparer
        {
            private TimeSpan maxDifference;

            public DateTimeEqualityComparer(TimeSpan maxDifference)
            {
                this.maxDifference = maxDifference;
            }

            public new bool Equals(object x, object y)
            {
                if (x == null || y == null)
                {
                    return false;
                }
                else if (x is DateTime && y is DateTime)
                {
                    var dt1 = (DateTime)x;
                    var dt2 = (DateTime)y;
                    var duration = (dt1 - dt2).Duration();
                    return duration < maxDifference;
                }
                return x.Equals(y);
            }

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }

        [Test()]
        public void ClaimsMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t = DateTime.Now;
            new PersistenceSpecification<Claims>(session)
       //  .CheckProperty(c => c.Id.GetType(), typeof(Guid))
         .CheckProperty(c => c.ApplicantId,"applicationId")
         .CheckProperty(c => c.ClaimsDatailsList.Count, 0)
          .CheckProperty(c => c.CreatTime, t, new DateTimeEqualityComparer(new TimeSpan(0, 0, 1)))
           .CheckProperty(c => c.LastUpdateTime, t, new DateTimeEqualityComparer(new TimeSpan(0, 0, 1)))
            .CheckProperty(c => c.Status,Ydb.Common.enum_OrderStatus.Appraised)
           
            .VerifyTheMappings();
        }

        [Test()]
        public void OrderAssignmentMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t = DateTime.Now;
            new PersistenceSpecification<OrderAssignment>(session)
     //    .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.AssignedStaffId, "assignedstaffid")
         .CheckProperty(c => c.BusinessId,"businessid")
          .CheckProperty(c => c.Enabled,true)
           .CheckProperty(c => c.CreateTime, t, new DateTimeEqualityComparer(new TimeSpan(0, 0, 1)))
            .CheckProperty(c => c.DeAssignedTime, t, new DateTimeEqualityComparer(new TimeSpan(0, 0, 1)))
           
            .VerifyTheMappings();
        }

        [Test()]
        public void aaClaimsDetailsMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t = DateTime.Now;
            new PersistenceSpecification<ClaimsDetails>(session)
         .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.Amount, 1)
         .CheckProperty(c => c.ClaimsDetailsResourcesUrl.Count, 0)
          .CheckProperty(c => c.Context, "context")
           .CheckProperty(c => c.CreatTime, t)
            .CheckProperty(c => c.LastUpdateTime, t)
            .CheckProperty(c => c.MemberId, "memberid")
            .CheckProperty(c => c.Target, "target")

            .VerifyTheMappings();
        }

        [Test()]
        public void bbClaimsDetailsMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t = DateTime.Now;
            new PersistenceSpecification<ClaimsDetails>(session)
         .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.Amount, 1)
         .CheckProperty(c => c.ClaimsDetailsResourcesUrl.Count, 0)
          .CheckProperty(c => c.Context, "context")
           .CheckProperty(c => c.CreatTime, t)
            .CheckProperty(c => c.LastUpdateTime, t)
            .CheckProperty(c => c.MemberId, "memberid")
            .CheckProperty(c => c.Target, "target")

            .VerifyTheMappings();
        }

        [Test()]
        public void ccClaimsDetailsMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t = DateTime.Now;
            new PersistenceSpecification<ClaimsDetails>(session)
         .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.Amount, 1)
         .CheckProperty(c => c.ClaimsDetailsResourcesUrl.Count, 0)
          .CheckProperty(c => c.Context, "context")
           .CheckProperty(c => c.CreatTime, t)
            .CheckProperty(c => c.LastUpdateTime, t)
            .CheckProperty(c => c.MemberId, "memberid")
            .CheckProperty(c => c.Target, "target")

            .VerifyTheMappings();
        }

        [Test()]
        public void ddClaimsDetailsMapTest()
        {
            Guid id = Guid.NewGuid();
            DateTime t = DateTime.Now;
            new PersistenceSpecification<ClaimsDetails>(session)
         .CheckProperty(c => c.Id, id)
         .CheckProperty(c => c.Amount, 1)
         .CheckProperty(c => c.ClaimsDetailsResourcesUrl.Count, 0)
          .CheckProperty(c => c.Context, "context")
           .CheckProperty(c => c.CreatTime, t)
            .CheckProperty(c => c.LastUpdateTime, t)
            .CheckProperty(c => c.MemberId, "memberid")
            .CheckProperty(c => c.Target, "target")

            .VerifyTheMappings();
        }
    }
}