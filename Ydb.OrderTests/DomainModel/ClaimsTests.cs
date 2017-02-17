using NUnit.Framework;
using Ydb.Order.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Ydb.Common;
namespace Ydb.Order.DomainModel.Tests
{
    [TestFixture()]
    public class ClaimsTests
    {
        [Test()]
        public void AddDetailsFromClaimsTest()
        {
            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();
            Claims c = new Claims(order, enum_OrderStatus.Appraised, "applicationid");
            IList<string> resouceUrls = new List<string>();
            c.AddDetailsFromClaims("context", 1, resouceUrls, enum_ChatTarget.all, "memberid");
            Assert.AreEqual(1, c.ClaimsDatailsList.Count);
        }
    }
}