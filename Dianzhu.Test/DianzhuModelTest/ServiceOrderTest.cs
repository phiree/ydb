using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FizzWare.NBuilder;
using Dianzhu.Model;
namespace Dianzhu.Test.DianzhuModelTest
{
    [TestFixture]
public    class ServiceOrderTest
    {
        [Test]
        public void AddDetailFromIntelServiceTest()
        {
            ServiceOrder order = ServiceOrderFactory.CreateDraft(null, null,string.Empty);
            DZService service = new DZService();
            DZService service2 = new DZService();

            order.AddDetailFromIntelService(service, 1,string.Empty,string.Empty, string.Empty, DateTime.Now);
            Assert.AreEqual(1, order.Details.Count);
            order.AddDetailFromIntelService(service, 2,string.Empty,string.Empty, string.Empty, DateTime.Now);
            Assert.AreEqual(1, order.Details.Count);
            Assert.AreEqual(3, order.Details[0].UnitAmount);



        }
    }
}
