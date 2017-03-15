using NUnit.Framework;
using Ydb.Order.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using FizzWare.NBuilder;

namespace Ydb.Order.ApplicationTests
{
    [TestFixture()]
    public class ServiceOrderServiceTests
    {
      
    }
}

namespace Ydb.Order.Application.Tests
{
    [TestFixture()]
    public class ServiceOrderServiceTests
    {
        IServiceOrderService orderService = Ydb.OrderTests.Bootstrap.Container.Resolve<IServiceOrderService>();
        [Test()]
        public void OrderFlow_BusinessConfirmOrderTest()
        {
            Order.DomainModel.ServiceOrder order = FizzWare.NBuilder.Builder<ServiceOrder>.CreateNew()
               .With(x => x.OrderStatus = Common.enum_OrderStatus.Payed).Build();
            orderService.Save(order);
          
            orderService.OrderFlow_BusinessConfirm(order.Id);
           order=  orderService.GetOne(order.Id);
            Assert.AreEqual(Common.enum_OrderStatus.Negotiate, order.OrderStatus);
        }

        [Test()]
        public void OrderFlow_BusinessNegotiateTest()
        {
            ServiceOrder order = Builder<ServiceOrder>.CreateNew()
                 .With(x => x.OrderStatus = Common.enum_OrderStatus.Negotiate).Build();
            orderService.Save(order);
            orderService.OrderFlow_BusinessNegotiate(order.Id, 12m);
            order = orderService.GetOne(order.Id);
            Assert.AreEqual(12m, order.NegotiateAmount);
            Assert.AreEqual(Common.enum_OrderStatus.isNegotiate, order.OrderStatus);
        }

        [Test()]
        public void OrderFlow_ConfirmDepositTest()
        {
            Order.DomainModel.ServiceOrder order = FizzWare.NBuilder.Builder<ServiceOrder>.CreateNew()
             .With(x => x.OrderStatus = Common.enum_OrderStatus.Created).Build();
            orderService.Save(order);
           
            orderService.OrderFlow_ConfirmDeposit(order.Id);
            order = orderService.GetOne(order.Id);
            Assert.AreEqual(Common.enum_OrderStatus.Payed, order.OrderStatus);
        }
    }
}