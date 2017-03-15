using NUnit.Framework;
using Ydb.ApplicationService.Application.AgentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Rhino.Mocks;
using Ydb.ApplicationService.Application.AgentService.DataStatistics;
using Ydb.ApplicationService.ModelDto;

namespace Ydb.ApplicationService.Application.AgentServiceTests
{
    [TestFixture()]
    public class OrdersServiceTests
    {
        IBusinessService businessService;
        IServiceOrderService serviceOrderService;
        IOrdersService ordersService;
        IStatisticsCount statisticsCount;
        IServiceTypeService serviceTypeService;
        [SetUp]
        public void Initialize()
        {
            Bootstrap.Boot();

            businessService = MockRepository.Mock<IBusinessService>();
            serviceOrderService = MockRepository.Mock<IServiceOrderService>();
            statisticsCount = MockRepository.Mock<IStatisticsCount>();
            serviceTypeService = MockRepository.Mock<IServiceTypeService>();
            ordersService = new OrdersService(businessService, serviceOrderService, statisticsCount, serviceTypeService);
        }

        [Test()]
        public void OrdersService_GetOrdersCountByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            IList<Business> businessList = new List<Business>();
            businessService.Stub(x => x.GetAllBusinessesByArea(areaList)).Return(businessList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            serviceOrderService.Stub(x => x.GetOrdersCountByBusinessList(businessIdList, true)).Return(100);
            serviceOrderService.Stub(x => x.GetOrdersCountByBusinessList(businessIdList, false)).Return(50);
            long l = ordersService.GetOrdersCountByArea(areaList, true);
            long l1 = ordersService.GetOrdersCountByArea(areaList, false);
            Assert.AreEqual(100, l);
            Assert.AreEqual(50, l1);
        }

        [Test()]
        public void OrdersService_GetOrdersListByAreaAndTime_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            IList<Business> businessList = new List<Business>();
            businessService.Stub(x => x.GetAllBusinessesByArea(areaList)).Return(businessList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            IList<ServiceOrder> serviceOrderList = new List<ServiceOrder>() { new ServiceOrder { Id = Guid.NewGuid() } };
            DateTime baseTime = DateTime.Now;
            serviceOrderService.Stub(x => x.GetOrdersByBusinessList(businessIdList, baseTime, baseTime.AddMonths(1), enum_IsDone.None.ToString())).Return(serviceOrderList);
            IList<ServiceOrderDto> serviceOrderDtoList = ordersService.GetOrdersListByAreaAndTime(areaList, baseTime, baseTime.AddMonths(1), enum_IsDone.None);
            Assert.AreEqual(1, serviceOrderDtoList.Count );
            Assert.AreEqual(serviceOrderList[0].Id.ToString(), serviceOrderDtoList[0].Id);
        }
    }
}