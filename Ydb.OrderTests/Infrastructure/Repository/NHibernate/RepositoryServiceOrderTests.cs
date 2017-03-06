using NUnit.Framework;
using Ydb.Order.Infrastructure.Repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel.Repository;
using Ydb.OrderTests;
using FizzWare.NBuilder;
using Ydb.Order.DomainModel;
using Ydb.Common;

namespace Ydb.Order.Infrastructure.Repository.NHibernateTests
{
    [TestFixture()]
    public class RepositoryServiceOrderTests
    {
        IRepositoryServiceOrder repositoryServiceOrder;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            repositoryServiceOrder = Bootstrap.Container.Resolve<IRepositoryServiceOrder>();
        }

        [Test()]
        public void RepositoryServiceOrder_GetOrdersCountByBusinessList_ByShared_Test()
        {
            DateTime dt = DateTime.Now.AddDays(1);
            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();
            repositoryServiceOrder.Add(order);
            ServiceOrder order1 = Builder<ServiceOrder>.CreateNew()
               .With(x => x.IsShared = true).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot1 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId1").Build();
            WorkTimeSnapshot OpenTimeSnapShot1 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order1.AddDetailFromIntelService("serviceId1", serviceSnapShot1, OpenTimeSnapShot1, 1,
                "targetCustomerName1", "targetCustomerPhone1", "targetAddress1", dt, "");
            repositoryServiceOrder.Add(order1);
            ServiceOrder order2 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.IsShared = true).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot2 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId2").Build();
            WorkTimeSnapshot OpenTimeSnapShot2 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order2.AddDetailFromIntelService("serviceId2", serviceSnapShot2, OpenTimeSnapShot2, 2,
                "targetCustomerName2", "targetCustomerPhone2", "targetAddress2", dt, "");
            repositoryServiceOrder.Add(order2);
            ServiceOrder order3 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.IsShared = false).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot3 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId3").Build();
            WorkTimeSnapshot OpenTimeSnapShot3 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order3.AddDetailFromIntelService("serviceId3", serviceSnapShot3, OpenTimeSnapShot3, 3,
                "targetCustomerName3", "targetCustomerPhone3", "targetAddress3", dt, "");
            repositoryServiceOrder.Add(order3);
            ServiceOrder order4 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.IsShared = true).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot4 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId4").Build();
            WorkTimeSnapshot OpenTimeSnapShot4 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order4.AddDetailFromIntelService("serviceId4", serviceSnapShot4, OpenTimeSnapShot4, 4,
                "targetCustomerName4", "targetCustomerPhone4", "targetAddress4", dt, "");
            repositoryServiceOrder.Add(order4);
            IList<string> businessIdList = new List<string> { "BusinessId1", "BusinessId3", "BusinessId4" };
            long l = repositoryServiceOrder.GetOrdersCountByBusinessList(businessIdList, true);
            Assert.AreEqual(2, l);
            long l1 = repositoryServiceOrder.GetOrdersCountByBusinessList(businessIdList, false);
            Assert.AreEqual(1, l1);
        }

        [Test()]
        public void RepositoryServiceOrder_GetOrdersByBusinessList_ByShared_Test()
        {
            DateTime dt = DateTime.Now.AddDays(1);
            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();
            repositoryServiceOrder.Add(order);
            ServiceOrder order1 = Builder<ServiceOrder>.CreateNew()
               .With(x => x.IsShared = true).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot1 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId1").Build();
            WorkTimeSnapshot OpenTimeSnapShot1 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order1.AddDetailFromIntelService("serviceId1", serviceSnapShot1, OpenTimeSnapShot1, 1,
                "targetCustomerName1", "targetCustomerPhone1", "targetAddress1", dt, "");
            repositoryServiceOrder.Add(order1);
            ServiceOrder order2 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.IsShared = true).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot2 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId2").Build();
            WorkTimeSnapshot OpenTimeSnapShot2 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order2.AddDetailFromIntelService("serviceId2", serviceSnapShot2, OpenTimeSnapShot2, 2,
                "targetCustomerName2", "targetCustomerPhone2", "targetAddress2", dt, "");
            repositoryServiceOrder.Add(order2);
            ServiceOrder order3 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.IsShared = false).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot3 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId3").Build();
            WorkTimeSnapshot OpenTimeSnapShot3 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order3.AddDetailFromIntelService("serviceId3", serviceSnapShot3, OpenTimeSnapShot3, 3,
                "targetCustomerName3", "targetCustomerPhone3", "targetAddress3", dt, "");
            repositoryServiceOrder.Add(order3);
            ServiceOrder order4 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.IsShared = true).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot4 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId4").Build();
            WorkTimeSnapshot OpenTimeSnapShot4 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order4.AddDetailFromIntelService("serviceId4", serviceSnapShot4, OpenTimeSnapShot4, 4,
                "targetCustomerName4", "targetCustomerPhone4", "targetAddress4", dt, "");
            repositoryServiceOrder.Add(order4);
            IList<string> businessIdList = new List<string> { "BusinessId1", "BusinessId3", "BusinessId4" };
            IList<ServiceOrder> orderList = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList, true);
            Assert.AreEqual(2, orderList.Count);
            Assert.AreEqual(order1.Id, orderList[0].Id);
            Assert.AreEqual(order4.Id, orderList[1].Id);
            IList<ServiceOrder> orderList1 = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList, false);
            Assert.AreEqual(1, orderList1.Count);
            Assert.AreEqual(order3.Id, orderList1[0].Id);
        }

        [Test()]
        public void RepositoryServiceOrder_GetOrdersByBusinessList_ByTime_Test()
        {
            DateTime dt = DateTime.Now.AddDays(1);
            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();
            repositoryServiceOrder.Add(order);
            ServiceOrder order1 = Builder<ServiceOrder>.CreateNew()
               .With(x => x.OrderConfirmTime = DateTime.Now.AddDays(-1)).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot1 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId1").Build();
            WorkTimeSnapshot OpenTimeSnapShot1 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order1.AddDetailFromIntelService("serviceId1", serviceSnapShot1, OpenTimeSnapShot1, 1,
                "targetCustomerName1", "targetCustomerPhone1", "targetAddress1", dt, "");
            repositoryServiceOrder.Add(order1);
            ServiceOrder order2 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.OrderConfirmTime = DateTime.Now).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot2 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId2").Build();
            WorkTimeSnapshot OpenTimeSnapShot2 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order2.AddDetailFromIntelService("serviceId2", serviceSnapShot2, OpenTimeSnapShot2, 2,
                "targetCustomerName2", "targetCustomerPhone2", "targetAddress2", dt, "");
            repositoryServiceOrder.Add(order2);
            ServiceOrder order3 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.OrderConfirmTime = DateTime.Now).With(x => x.OrderStatus = enum_OrderStatus.Finished).Build();
            ServiceSnapShot serviceSnapShot3 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId3").Build();
            WorkTimeSnapshot OpenTimeSnapShot3 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order3.AddDetailFromIntelService("serviceId3", serviceSnapShot3, OpenTimeSnapShot3, 3,
                "targetCustomerName3", "targetCustomerPhone3", "targetAddress3", dt, "");
            repositoryServiceOrder.Add(order3);
            ServiceOrder order4 = Builder<ServiceOrder>.CreateNew()
              .With(x => x.OrderConfirmTime = DateTime.Now).With(x => x.OrderStatus = enum_OrderStatus.Payed).Build();
            ServiceSnapShot serviceSnapShot4 = Builder<ServiceSnapShot>.CreateNew()
               .With(x => x.BusinessId = "BusinessId4").Build();
            WorkTimeSnapshot OpenTimeSnapShot4 = Builder<WorkTimeSnapshot>.CreateNew().Build();
            order4.AddDetailFromIntelService("serviceId4", serviceSnapShot4, OpenTimeSnapShot4, 4,
                "targetCustomerName4", "targetCustomerPhone4", "targetAddress4", dt, "");
            repositoryServiceOrder.Add(order4);
            IList<string> businessIdList = new List<string> { "BusinessId1", "BusinessId3", "BusinessId4" };
            IList<ServiceOrder> orderList = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList,DateTime.MinValue, DateTime.MinValue,"");
            Assert.AreEqual(3, orderList.Count);
            Assert.AreEqual(order1.Id, orderList[0].Id);
            Assert.AreEqual(order3.Id, orderList[1].Id);
            Assert.AreEqual(order4.Id, orderList[2].Id);
            IList<ServiceOrder> orderList1 = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList,DateTime.Now.AddHours(-23), DateTime.Now.AddDays(1), "");
            Assert.AreEqual(2, orderList1.Count);
            Assert.AreEqual(order3.Id, orderList1[0].Id);
            Assert.AreEqual(order4.Id, orderList1[1].Id);
            IList<ServiceOrder> orderList2 = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), "");
            Assert.AreEqual(3, orderList2.Count);
            Assert.AreEqual(order1.Id, orderList2[0].Id);
            Assert.AreEqual(order3.Id, orderList2[1].Id);
            Assert.AreEqual(order4.Id, orderList2[2].Id);
            IList<ServiceOrder> orderList3 = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "");
            Assert.AreEqual(0, orderList3.Count);
            IList<ServiceOrder> orderList4 = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "OrderIsDone");
            Assert.AreEqual(1, orderList4.Count);
            Assert.AreEqual(order3.Id, orderList2[0].Id);
            IList<ServiceOrder> orderList5 = repositoryServiceOrder.GetOrdersByBusinessList(businessIdList, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "OrderNotDone");
            Assert.AreEqual(2, orderList5.Count);
            Assert.AreEqual(order1.Id, orderList2[0].Id);
            Assert.AreEqual(order4.Id, orderList2[1].Id);
        }
    }
}