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
        public void ServiceTypePointService_Add_AddOneServiceTypePoint()
        {
            ServiceTypePointDto dPoint = serviceTypePointService.GetPointInfo("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
            if (dPoint == null)
            {
                serviceTypePointService.Add("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.08m);
            }
            serviceTypePointService.Add("004fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.08m);
            Console.WriteLine("ServiceTypePointService.Add:添加成功！");
        }

        /// <summary>
        /// 修改一条服务类型扣点比例
        /// </summary>
        [Test()]
        public void ServiceTypePointService_Update_UpdateOneServiceTypePoint()
        {
            ServiceTypePointDto dPoint = serviceTypePointService.GetPointInfo("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
            if (dPoint == null)
            {
                serviceTypePointService.Add("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.08m);
            }
            serviceTypePointService.Update("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.09m);
            Console.WriteLine("ServiceTypePointService.Update:修改成功！");
        }

        /// <summary>
        /// 根据服务类型ID获取该服务类型的扣点比例
        /// </summary>
        [Test()]
        public void ServiceTypePointService_GetPoint_ByServiceTypeID()
        {
            ServiceTypePointDto dPointDto = serviceTypePointService.GetPointInfo("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
            if (dPointDto == null)
            {
                serviceTypePointService.Add("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.08m);
            }
            decimal dPoint = serviceTypePointService.GetPoint("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
            Console.WriteLine("ServiceTypePointService.GetPoint:" + dPoint);
        }

        /// <summary>
        /// 根据用户类型获取该用户类型的分配比例信息
        /// </summary>
        [Test()]
        public void ServiceTypePointService_GetPointInfo_ByServiceTypeID()
        {
            ServiceTypePointDto dPoint = serviceTypePointService.GetPointInfo("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
            Console.WriteLine("UserTypeSharePointService.GetSharePointInfo:" + (dPoint == null).ToString());
        }

        /// <summary>
        /// 获取所有服务类型的扣点比例信息
        /// </summary>
        [Test()]
        public void ServiceTypePointService_GetAll()
        {
            var list = serviceTypePointService.GetAll();
            Console.WriteLine("ServiceTypePointService.GetAll:" + list.Count);
        }

        /// <summary>
        /// 获取所有服务类型的扣点比例信息
        /// </summary>
        [Test()]
        public void ServiceTypePointService_SaveList()
        {
            serviceTypePointService.SaveList(new List<ServiceTypePointDto> { new ServiceTypePointDto { ServiceTypeId = "004fa8eb-3649-4ddd-8c7d-9028c0f6a94f", Point = 0.08m },
                 new ServiceTypePointDto { ServiceTypeId = "003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", Point = 0.09m }
            });
            Console.WriteLine("ServiceTypePointService.SaveList:添加成功！");
        }
    }
}
