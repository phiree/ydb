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
        public void Business_AddTest()
        {
            IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
            string businessEmail = "test_businessemail_1";
            string businessName = "test_businessname_1";
            businessService.Add(businessName, "test_phone", businessEmail, Guid.NewGuid(),"test_latitude","test_longtitude","test_rawAddressFromMapApi","test_contact"
                ,23,23);

           var b= businessService.GetBusinessByEmail(businessEmail);

             Assert.AreEqual(businessName, b.Name);
        }
    }
}