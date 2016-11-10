using NUnit.Framework;
using Ydb.BusinessResource.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
namespace Ydb.BusinessResource.Application.Tests
{
    [TestFixture()]
    public class BusinessServiceTests
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }
        [Test()]
        public void AddTest()
        {
            IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
            string businessEmail = "test_businessemail_1";
            string businessName = "test_businessname_1";
            businessService.Add(Builder<DomainModel.Business>.CreateNew().With(x=>x.Email= businessEmail).With(x=>x.Name=businessName) .Build());

           var b= businessService.GetBusinessByEmail(businessEmail);

             Assert.AreEqual(businessName, b.Name);
        }
    }
}