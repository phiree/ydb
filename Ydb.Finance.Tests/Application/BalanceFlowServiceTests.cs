using NUnit.Framework;
using Ydb.Finance.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Specification;
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
        public void BalanceFlowService_GetAll()
        {
            var list = balanceFlowService.GetAll();
            Console.WriteLine("BalanceFlowService.GetAll:"+list.Count);
        }

        /// <summary>
        /// 根据条件获取流水列表
        /// </summary>
        [Test()]
        public void BalanceFlowService_GetBalanceFlowList()
        {
            TraitFilter traitFilter = new TraitFilter();
            BalanceFlowFilter balanceFlowFilter = new BalanceFlowFilter();
            IList<BalanceFlowDto> balanceFlowDtoList = balanceFlowService.GetBalanceFlowList(traitFilter, balanceFlowFilter);
            Console.WriteLine("BalanceFlowService.GetBalanceFlowList:" + balanceFlowDtoList.Count);
        }

        /// <summary>
        /// 保存一条流水信息
        /// </summary>
        [Test()]
        public void BalanceFlowService_Save_SaveOneBalanceFlow()
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
            Console.WriteLine("BalanceFlowService.Save:保存成功！");
        }

        /// <summary>
        /// 根据时间或服务类型统计某个用户的收支账单
        /// </summary>
        [Test()]
        public void BalanceFlowService_GetBillSatistics_GetOneUser_SelectByTimeOrServiceType()
        {
            var list=  balanceFlowService.GetBillSatistics("09ccc183-ed87-462a-8d11-a66600fbbd24", DateTime.MinValue, DateTime.MaxValue, "0", " %Y-%m-%d");
            Console.WriteLine("BalanceFlowService.GetBillSatistics:" + list.Count);
        }

        /// <summary>
        /// 根据时间或服务类型统计某个用户的收支流水
        /// </summary>
        [Test()]
        public void BalanceFlowService_GetBillList_GetOneUser_SelectByTimeOrServiceType()
        {
            var list = balanceFlowService.GetBillList("09ccc183-ed87-462a-8d11-a66600fbbd24", DateTime.MinValue, DateTime.MaxValue, "0", "","OrderShare","","",new Common.Specification.TraitFilter());
            Console.WriteLine("BalanceFlowService.GetBillList:" + list.Count);
        }
    }
}