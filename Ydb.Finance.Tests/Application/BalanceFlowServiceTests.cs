using NUnit.Framework;
using Ydb.Finance.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.Tests;
namespace Ydb.Finance.Application.Tests
{
    [TestFixture()]
    public class BalanceFlowServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
        }
        [Test()]
        public void GetBillSatisticsTest()
        {
            IBalanceFlowService balanceFlowService = Bootstrap.Container.Resolve<IBalanceFlowService>();

            var list=  balanceFlowService.GetBillSatistics("09ccc183-ed87-462a-8d11-a66600fbbd24", DateTime.MinValue, DateTime.MaxValue, "0", " %Y-%m-%d");
            Console.WriteLine(list.Count);
        }
    }
}