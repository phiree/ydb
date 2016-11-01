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
    public class ServiceTypePointServiceTests
    {
        IServiceTypePointService serviceTypePointService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            serviceTypePointService = Bootstrap.Container.Resolve<IServiceTypePointService>();
        }

        /// <summary>
        /// 添加一条服务类型扣点比例
        /// </summary>
        [Test()]
        public void ServiceTypePointServiceTests_AddTest_AddOneServiceTypePoint()
        {
            serviceTypePointService.Add("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.08m);
            Console.WriteLine("ServiceTypePointServiceTest.AddTest:添加成功！");
        }

        /// <summary>
        /// 根据服务类型ID获取该服务类型的扣点比例
        /// </summary>
        [Test()]
        public void ServiceTypePointServiceTests_GetPointTest_ByServiceTypeID()
        {
            decimal dPoint = serviceTypePointService.GetPoint("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
            Console.WriteLine("ServiceTypePointServiceTest.GetPointTest:" + dPoint);
        }

        /// <summary>
        /// 获取所有服务类型的扣点比例信息
        /// </summary>
        [Test()]
        public void ServiceTypePointServiceTests_GetAllTest()
        {
            var list = serviceTypePointService.GetAll();
            Console.WriteLine("ServiceTypePointServiceTest.GetAllTest:" + list.Count);
        }
    }
}
