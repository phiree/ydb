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

        [Test()]
        public void GetAllTest()
        {
            var list = balanceFlowService.GetAll();
            Console.WriteLine("BalanceFlowServiceTests.GetAllTest:"+list.Count);
        }

        [Test()]
        public void SaveTest()
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
            try
            {
                balanceFlowService.Save(flow);
                Console.WriteLine("BalanceFlowServiceTests.SaveTest:保存成功！");
            }
            catch(Exception ex)
            {
                Console.WriteLine("BalanceFlowServiceTests.SaveTest:"+ex.ToString());
            }
        }

        [Test()]
        public void GetBillSatisticsTest()
        {
            var list=  balanceFlowService.GetBillSatistics("09ccc183-ed87-462a-8d11-a66600fbbd24", DateTime.MinValue, DateTime.MaxValue, "0", " %Y-%m-%d");
            Console.WriteLine("BalanceFlowServiceTests.GetBillSatisticsTest:" + list.Count);
        }
        
        [Test()]
        public void GetBillListTest()
        {
            var list = balanceFlowService.GetBillList("09ccc183-ed87-462a-8d11-a66600fbbd24", DateTime.MinValue, DateTime.MaxValue, "0", "","OrderShare","",""," %Y-%m-%d");
            Console.WriteLine("BalanceFlowServiceTests.GetBillListTest:" + list.Count);
        }
    }
}