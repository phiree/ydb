using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Finance.Application;

namespace Ydb.Finance.Tests.Application
{
    [TestFixture()]
    public class WithdrawCashServiceTests
    {
        IWithdrawCashService withdrawCashService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            withdrawCashService = Bootstrap.Container.Resolve<IWithdrawCashService>();
        }

        /// <summary>
        /// 提现操作
        /// </summary>
        [Test()]
        public void WithdrawCashServiceTests_WithdrawCashTest()
        {
            IList<WithdrawCashParam> flow = new List<WithdrawCashParam> {
                new WithdrawCashParam{
                    AccountId = "c64d9dda-4f6e-437b-89d2-a591012d8c65",
                    Amount = 0.1m,
                    RelatedObjectId = "0763ec35-e349-425f-8217-a69b0114bb1e",
                }
            };
            withdrawCashService.WithdrawCash(flow);
            Console.WriteLine("BalanceFlowServiceTests.WithdrawCashTest:提现成功！");
        }
    }
}
