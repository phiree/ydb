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
        public void testabc()
        {
            IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            IBalanceFlowService balanceFlowService= Bootstrap.Container.Resolve<IBalanceFlowService>();

            Console.WriteLine(memberService.GetUserByName("18889387677").UserName);
            Console.WriteLine(balanceFlowService.GetAll().Count);

        }
        [Test]
        public void Testavc()
        {
            Assert.Fail();
        }
    }
}
