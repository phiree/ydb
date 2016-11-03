using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Membership.Application;
using Ydb.Finance.Application;


namespace Ydb.Test.Integration
{
    [TestFixture]
    public class FinanceAndMembershipTest
    {

        [SetUp]
        public void setup()
        {
            Bootstrap.Boot();
        }
        [Test]
        public void GetUserNameAndGetAllFlowService()
        {
            IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            IServiceTypePointService serviceTypePointService= Bootstrap.Container.Resolve<IServiceTypePointService>();

            string username = "18889387677";
            memberService.RegisterMember(username, "123456","123456","business","localhost");
            Assert.AreEqual(username, memberService.GetUserByName(username).UserName);


            serviceTypePointService.Add("type1", 0.01m);
            Assert.AreEqual(0.01m, serviceTypePointService.GetPoint("type1"));

        }
         
    }
}
