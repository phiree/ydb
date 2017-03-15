using NUnit.Framework;
using Ydb.Order.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
namespace Ydb.Order.Application.Tests
{
    [TestFixture()]
    public class PaymentServiceTests
    {
        [Test]
        public void ComponentRegisterTest()
        {
            var repoClaims =OrderTests. Bootstrap.Container.Resolve<Ydb.Order.DomainModel.Repository.IRepositoryClaims>();
            Assert.IsNotNull(repoClaims);
        }
        [Test()]
        public void ApplyPayTest()
        {
        //    Assert.Fail();
        }
    }
}