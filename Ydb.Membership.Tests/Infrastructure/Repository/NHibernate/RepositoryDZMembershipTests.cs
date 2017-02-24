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
            DZMembership member = new DZMembership() { AreaId = "1", UserType = UserType.customer };
            repositoryDZMembership.Add(member);
            DZMembership member1 = new DZMembership() { AreaId = "1", UserType = UserType.customerservice };
            repositoryDZMembership.Add(member1);
            DZMembership member2 = new DZMembership() { AreaId = "1", UserType = UserType.customer, TimeCreated = DateTime.Now.AddDays(-1) };
            repositoryDZMembership.Add(member2);
            IList<string> areaList = new List<string> { "1", "2" };
            long c = repositoryDZMembership.GetUsersCountByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer);
            Assert.AreEqual(2, c);
            c = repositoryDZMembership.GetUsersCountByArea(areaList, DateTime.Now.AddHours(-23), DateTime.Now.AddDays(1), UserType.customer);
            Assert.AreEqual(1, c);
            c = repositoryDZMembership.GetUsersCountByArea(areaList, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), UserType.customer);
            Assert.AreEqual(2, c);
            c = repositoryDZMembership.GetUsersCountByArea(areaList, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), UserType.customer);
            Assert.AreEqual(0, c);
        }

        [Test()]
        public void RepositoryDZMembership_GetUsersByArea_Test()
        {
            DZMembership member = new DZMembership() { AreaId = "1", UserType = UserType.customer };
            repositoryDZMembership.Add(member);
            DZMembership member1 = new DZMembership() { AreaId = "1", UserType = UserType.customerservice };
            repositoryDZMembership.Add(member1);
            DZMembership member2 = new DZMembership() { AreaId = "1", UserType = UserType.customer, TimeCreated = DateTime.Now.AddDays(-1) };
            repositoryDZMembership.Add(member2);
            IList<string> areaList = new List<string> { "1", "2" };
            IList<DZMembership> memberList = repositoryDZMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer);
            Assert.AreEqual(2, memberList.Count);
            Assert.AreEqual("1", memberList[0].AreaId);
            Assert.AreEqual("1", memberList[1].AreaId);
            Assert.AreEqual(UserType.customer, memberList[0].UserType);
            Assert.AreEqual(UserType.customer, memberList[1].UserType);
            memberList = repositoryDZMembership.GetUsersByArea(areaList, DateTime.Now.AddHours(-23), DateTime.Now.AddDays(1), UserType.customer);
            Assert.AreEqual(1, memberList.Count);
            Assert.AreEqual("1", memberList[0].AreaId);
            Assert.AreEqual(UserType.customer, memberList[0].UserType);
            Assert.AreEqual(member.Id, memberList[0].Id);
            memberList = repositoryDZMembership.GetUsersByArea(areaList, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), UserType.customer);
            Assert.AreEqual(2, memberList.Count);
            Assert.AreEqual("1", memberList[0].AreaId);
            Assert.AreEqual("1", memberList[1].AreaId);
            Assert.AreEqual(UserType.customer, memberList[0].UserType);
            Assert.AreEqual(UserType.customer, memberList[1].UserType);
            memberList = repositoryDZMembership.GetUsersByArea(areaList, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), UserType.customer);
            Assert.AreEqual(0, memberList.Count);
        }

        [Test()]
        public void RepositoryDZMembership_GetOneNotVerifiedDZMembershipCustomerServiceByArea_Test()
        {
            //Assert.Fail();
        }
    }
}