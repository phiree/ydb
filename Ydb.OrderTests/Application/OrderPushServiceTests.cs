using NUnit.Framework;
using Ydb.Order.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application.Tests;
using Ydb.OrderTests;
using Ydb.Order.DomainModel;
using FizzWare.NBuilder;
namespace Ydb.Order.Application.Tests
{
    [TestFixture()]
    public class OrderPushServiceTests
    {
        [Test()]
        public void PushTest()
        {
            IOrderPushService orderPushService = Bootstrap.Container.Resolve<IOrderPushService>();
            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();
            IList<ServiceOrderPushedService> pushServices = Builder<ServiceOrderPushedService>.CreateListOfSize(1).Build();
            orderPushService.Push(order.Id, pushServices, "TargetAddress", DateTime.Now);
        }
    }
}