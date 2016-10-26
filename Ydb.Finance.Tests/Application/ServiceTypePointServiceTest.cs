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
    public class ServiceTypePointServiceTest
    {
        IServiceTypePointService serviceTypePointService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            serviceTypePointService = Bootstrap.Container.Resolve<IServiceTypePointService>();
        }

        [Test()]
        public void AddTest()
        {
            try
            {
                serviceTypePointService.Add("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.08m);
                Console.WriteLine("ServiceTypePointServiceTest.AddTest:添加成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ServiceTypePointServiceTest.AddTest:" + ex.ToString());
            }
        }

        [Test()]
        public void GetPointTest()
        {
            try
            {
                decimal dPoint = serviceTypePointService.GetPoint("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
                Console.WriteLine("ServiceTypePointServiceTest.GetPointTest:" + dPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ServiceTypePointServiceTest.GetPointTest:" + ex.ToString());
            }
        }

        [Test()]
        public void GetAllTest()
        {
            var list = serviceTypePointService.GetAll();
            Console.WriteLine("ServiceTypePointServiceTest.GetAllTest:" + list.Count);
        }
    }
}
