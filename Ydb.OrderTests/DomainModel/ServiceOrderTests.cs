using NUnit.Framework;
using Ydb.Order.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
namespace Ydb.Order.DomainModel.Tests
{
    [TestFixture()]
    public class ServiceOrderTests
    {
        [Test()]
        public void AddDetailFromIntelServiceTest()
        {
            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();

            order.AddDetailFromIntelService("serviceid",
                Builder<ServiceSnapShot>.CreateNew().Build(),
                Builder<WorkTimeSnapshot>.CreateNew().Build(),
                1, "targetCustomerName", "targetCustomerPhone", "targetAddress", DateTime.Now, "memo"
                );
            Assert.AreEqual(1, order.Details.Count);

        }
    }
}