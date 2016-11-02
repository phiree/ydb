using NUnit.Framework;
using Ydb.Finance.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ydb.Finance.Tests.Application
{
    [TestFixture()]
    public class BalanceFlowServiceTests
    {
        IBalanceFlowService balanceFlowService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            balanceFlowService = Bootstrap.Container.Resolve<IBalanceFlowService>();
        }

        /// <summary>
        /// 获取所有流水列表
        /// </summary>
        [Test()]
        public void BalanceFlowServiceTests_GetAllTest()
        {
            var list = balanceFlowService.GetAll();
            Console.WriteLine("BalanceFlowServiceTests.GetAllTest:"+list.Count);
        }

        /// <summary>
        /// 保存一条流水信息
        /// </summary>
        [Test()]
        public void BalanceFlowServiceTests_SaveTest_SaveOneBalanceFlow()
        {
            BalanceFlowDto flow = new BalanceFlowDto
            {
                AccountId = "c64d9dda-4f6e-437b-89d2-a591012d8c65",
                Amount = 0.1m,
                RelatedObjectId = "0763ec35-e349-425f-8217-a69b0114bb1e",
                OccurTime = DateTime.Now,
                FlowType = "OrderShare",
                Income = true
            };
            balanceFlowService.Save(flow);
            Console.WriteLine("BalanceFlowServiceTests.SaveTest:保存成功！");
        }

        /// <summary>
        /// 根据时间或服务类型统计某个用户的收支账单
        /// </summary>
        [Test()]
        public void BalanceFlowServiceTests_GetBillSatisticsTest_GetOneUser_SelectByTimeOrServiceType()
        {
            var list=  balanceFlowService.GetBillSatistics("09ccc183-ed87-462a-8d11-a66600fbbd24", DateTime.MinValue, DateTime.MaxValue, "0", " %Y-%m-%d");
            Console.WriteLine("BalanceFlowServiceTests.GetBillSatisticsTest:" + list.Count);
        }

        /// <summary>
        /// 根据时间或服务类型统计某个用户的收支流水
        /// </summary>
        [Test()]
        public void BalanceFlowServiceTests_GetBillListTest_GetOneUser_SelectByTimeOrServiceType()
        {
            var list = balanceFlowService.GetBillList("09ccc183-ed87-462a-8d11-a66600fbbd24", DateTime.MinValue, DateTime.MaxValue, "0", "","OrderShare","",""," %Y-%m-%d");
            Console.WriteLine("BalanceFlowServiceTests.GetBillListTest:" + list.Count);
        }
    }
}