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
using Ydb.Common.Domain;

namespace Ydb.BusinessResource.ApplicationTests
{
    [TestFixture()]
    public class DZServiceServiceTests
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }

        [Test()]
        public void DZServiceService_GetServicesByArea_Test()
        {
            Business business1= Builder<Business>.CreateNew().With(x=>x.AreaBelongTo="1").Build();
            IRepositoryBusiness repositoryBusiness= Bootstrap.Container.Resolve<IRepositoryBusiness>();
            repositoryBusiness.Add(business1);
            DZService service1 = Builder<DZService>.CreateNew().With(x=>x.Business=business1).Build();
            IDZServiceService dzServie = Bootstrap.Container.Resolve<IDZServiceService>();
            dzServie.Save(service1);

            Business business2 = Builder<Business>.CreateNew().With(x => x.AreaBelongTo = "2").Build();
            repositoryBusiness.Add(business2);
            DZService service2 = Builder<DZService>.CreateNew().With(x => x.Business = business2).Build();
            dzServie.Save(service2);
            Business business3 = Builder<Business>.CreateNew().With(x => x.AreaBelongTo = "3").Build();
            repositoryBusiness.Add(business3);
            DZService service3 = Builder<DZService>.CreateNew().With(x => x.Business = business3).Build();
            dzServie.Save(service3);
            Business business4 = Builder<Business>.CreateNew().With(x => x.AreaBelongTo = "4").Build();
            repositoryBusiness.Add(business4);
            DZService service4 = Builder<DZService>.CreateNew().With(x => x.Business = business4).Build();
            dzServie.Save(service4);
            
            DZService service5 = Builder<DZService>.CreateNew().With(x => x.Business = business3).Build();
            dzServie.Save(service5);
            DZService service6 = Builder<DZService>.CreateNew().With(x => x.Business = business4).Build();
            dzServie.Save(service6);

            IList<string> areaIdLit = new List<string> { "2", "3" };
            IList<ServiceDto> serviceList = dzServie.GetServicesByArea(areaIdLit);
            Assert.AreEqual(3, serviceList.Count);
        }
    }
}

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
         

        [Test()]
        public void GetOneServiceTest()
        {
            Business b = Builder<Business>.CreateNew().Build();
            IBusinessService bs = Bootstrap.Container.Resolve<IBusinessService>();
          ActionResult<Business> resultAddBusiness=  bs.Add(b.Name,b.Phone,b.Email,b.OwnerId,b.Latitude.ToString(),b.Longitude.ToString(),b.RawAddressFromMapAPI,b.Contact,b.WorkingYears,b.StaffAmount);
            DZService service = Builder<DZService>.CreateNew().
               With(x=>x.Business=resultAddBusiness.ResultObject)
                .Build();
            IDZServiceService dzServie = Bootstrap.Container.Resolve<IDZServiceService>();
            dzServie.Save(service);

            var rs = dzServie.GetOne(service.Id);
            Assert.AreEqual(service.ServiceType, null);
        }

       

        [Test()]
        public void AddWorkTimeTest()
        {
           Business business= Builder<Business>.CreateNew().Build();
            IRepositoryBusiness businessRepo = Bootstrap.Container.Resolve<IRepositoryBusiness>();
            businessRepo.Add(business);
            DZService service = Builder<DZService>.CreateNew()
                .With(x=>x.Business=business)
                .Build();
            IDZServiceService dzServie = Bootstrap.Container.Resolve<IDZServiceService>();
            dzServie.Save(service);

           ActionResult<ServiceOpenTimeForDay> addedWorkTime= dzServie.AddWorkTime(business.Id.ToString(), service.Id.ToString(), DayOfWeek.Sunday, "01:01", "03:13", 33, "test_tag");

            

         
            Assert.AreEqual(13, addedWorkTime.ResultObject.TimePeriod. EndTime.Minute);
        }
    }
}