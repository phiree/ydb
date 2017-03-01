using NUnit.Framework;
using Ydb.Finance.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Tests.Application
{
   public class BalanceTotalServiceTests
    {

        IBalanceTotalService balanceTotalService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            balanceTotalService = Bootstrap.Container.Resolve<IBalanceTotalService>();
        }

        /// <summary>
        /// 获取所有流水列表
        /// </summary>
        [Test()]
        public void BalanceTotalService_GetOneByUserId()
        {
            var list = balanceTotalService.GetOneByUserId("09ccc183-ed87-462a-8d11-a66600fbbd24");
            Console.WriteLine("BalanceTotalService.GetOneByUserId:" + (list==null).ToString());
        }

        /// <summary>
        /// 获取代理及其助理的所有账户余额信息
        /// </summary>
        [Test()]
        public void BalanceTotalService_GetBalanceTotalByArea_Test()
        {
            var list = balanceTotalService.GetBalanceTotalByArea(new List<string> { "09ccc183-ed87-462a-8d11-a66600fbbd24" });
            Console.WriteLine("BalanceTotalService.GetOneByUserId:" + (list == null).ToString());
        }
    }
}
