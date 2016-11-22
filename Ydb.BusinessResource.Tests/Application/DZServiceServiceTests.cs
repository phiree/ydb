using NUnit.Framework;
using Ydb.BusinessResource.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.DomainModel;

using FizzWare.NBuilder;
using Ydb.Common.Application;

namespace Ydb.BusinessResource.Application.Tests
{
    [TestFixture()]
    public class DZServiceServiceTests
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }
        [Category("dddd")]
        [Test()]
        public void SaveServiceTest()
        {
            //add Service
            DZService service = Builder<DZService>.CreateNew().Build();
            IDZServiceService dzServie = Bootstrap.Container.Resolve<IDZServiceService>();
            dzServie.Save(service);

        }

        [Test()]
        public void GetOneServiceTest()
        {
            DZService service = Builder<DZService>.CreateNew().Build();
            IDZServiceService dzServie = Bootstrap.Container.Resolve<IDZServiceService>();
            dzServie.Save(service);

            var rs = dzServie.GetOne(service.Id);
            Assert.AreEqual(service.ServiceType, null);
        }

        [Test()]
        public void ModifyWorkTimeDayTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void AddWorkTimeTest()
        {
            DZService service = Builder<DZService>.CreateNew().Build();
            IDZServiceService dzServie = Bootstrap.Container.Resolve<IDZServiceService>();
            dzServie.Save(service);

           ActionResult<ServiceOpenTimeForDay> addedWorkTime= dzServie.AddWorkTime(string.Empty, service.Id.ToString(), DayOfWeek.Sunday, "01:01", "03:13", 33, "test_tag");

            

         
            Assert.AreEqual(13, addedWorkTime.ResultObject.TimePeriod. EndTime.Minute);
        }
    }
}