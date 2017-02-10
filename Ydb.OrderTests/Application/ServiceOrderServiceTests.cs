using NUnit.Framework;
using Ydb.Order.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using FizzWare.NBuilder;
namespace Ydb.Order.Application.Tests
{
    [TestFixture()]
    public class ServiceOrderServiceTests
    {
        [Test()]
        public void OrderFlow_ConfirmOrderTest()
        {
            IServiceOrderService orderService = Ydb.OrderTests.Bootstrap.Container.Resolve<IServiceOrderService>();
            Order.DomainModel.ServiceOrder order = FizzWare.NBuilder.Builder<ServiceOrder>.CreateNew().Build();
            orderService.OrderFlow_ConfirmOrder(order, "serviceid");
        }
    }
}