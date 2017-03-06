using NUnit.Framework;
using Ydb.ApplicationService.Application.AgentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Rhino.Mocks;
using Ydb.ApplicationService.Application.AgentService.DataStatistics;

namespace Ydb.ApplicationService.Application.AgentServiceTests
{
    [TestFixture()]
    public class OrdersServiceTests
    {
        IBusinessService businessService;
        IServiceOrderService serviceOrderService;
        IOrdersService ordersService;
        IStatisticsCount statisticsCount;
        [SetUp]
        public void Initialize()
        {
            businessService = MockRepository.Mock<IBusinessService>();
            serviceOrderService = MockRepository.Mock<IServiceOrderService>();
            ordersService = new OrdersService(businessService, serviceOrderService, statisticsCount);
        }

        [Test()]
        public void OrdersService_GetOrdersCountByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            IList<Business> businessList = new List<Business>();
            businessService.Stub(x => x.GetAllBusinessesByArea(areaList)).Return(businessList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            serviceOrderService.Stub(x => x.GetOrdersCountByBusinessList(businessIdList,true)).Return(100);
            serviceOrderService.Stub(x => x.GetOrdersCountByBusinessList(businessIdList, false)).Return(50);
            long l = ordersService.GetOrdersCountByArea(areaList,true);
            long l1 = ordersService.GetOrdersCountByArea(areaList, false);
            Assert.AreEqual(100, l);
            Assert.AreEqual(50, l1);
        }
    }
}