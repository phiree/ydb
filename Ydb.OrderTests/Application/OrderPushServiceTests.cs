using NUnit.Framework;
using Ydb.Order.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
using Ydb.Order.Application.Tests;
using Ydb.OrderTests;
namespace Ydb.Order.Application.Tests
{
    [TestFixture()]
    public class OrderPushServiceTests
    {
        [Test()]
        public void PushTest()
        {
            IOrderPushService pushService = Bootstrap.Container.Resolve<IOrderPushService>();

            Assert.IsNotNull(pushService);
        }
    }
}