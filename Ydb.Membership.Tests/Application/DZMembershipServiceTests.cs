using NUnit.Framework;
using Ydb.Membership.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Tests;
using System.Transactions;

namespace Ydb.Membership.Application.Tests
{
    [TestFixture()]
    public class DZMembershipServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            dzmembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();

        }
        IDZMembershipService dzmembershipService;
        string userName = "132909201331";
        [Test()]

        public void RegisterBusinessUserTest()
        {

            string errMsg;
           
            bool success = dzmembershipService.RegisterBusinessUser(userName, "123456", out errMsg);
            Console.WriteLine(errMsg);
            Assert.AreEqual(true, success);  // your test code here
            DomainModel.DZMembership membership = dzmembershipService.GetUserByName(userName);
            Assert.AreEqual(userName, membership.UserName);


        }
       
        [Test()]
        public void zGetUserByNameTest()
        {
            DomainModel.DZMembership membership = dzmembershipService.GetUserByName(userName);
            Assert.AreEqual(userName, membership.UserName);
        }
    }
}