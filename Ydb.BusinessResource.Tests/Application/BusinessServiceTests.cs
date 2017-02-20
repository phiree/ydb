using NUnit.Framework;
using Ydb.BusinessResource.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.DomainModel.Service.DataStatistics;
using Rhino.Mocks;
using Ydb.Common.Domain;

namespace Ydb.BusinessResource.ApplicationTests
{
    [TestFixture()]
    public class BusinessServiceTests
    {
        IRepositoryBusiness repositoryBusiness;
        IStatisticsBusinessCount statisticsBusinessCount;
        IBusinessService businessService;
        [SetUp]
        public void Initialize()
        {
            repositoryBusiness = MockRepository.GenerateMock<IRepositoryBusiness>();
            statisticsBusinessCount = MockRepository.GenerateMock<IStatisticsBusinessCount>();
            businessService = new BusinessService(repositoryBusiness, statisticsBusinessCount);
        }

        [Test()]
        public void BusinessService_GetCountOfNewBusinessesYesterdayByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            DateTime beginTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, beginTime, beginTime.AddDays(1))).Return(100);
            long l = businessService.GetCountOfNewBusinessesYesterdayByArea(areaList);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void BusinessService_GetCountOfAllBusinessesByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, DateTime.MinValue, DateTime.MinValue)).Return(100);
            long l = businessService.GetCountOfAllBusinessesByArea(areaList);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void BusinessService_GetStatisticsNewBusinessesCountListByTime_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<Business> businessList = new List<Business>();
            repositoryBusiness.Stub(x => x.GetBusinessesByArea(areaList, BeginTime, EndTime)).Return(businessList);
            statisticsBusinessCount.Stub(x => x.StatisticsNewBusinessesCountListByTime(businessList, BeginTime, EndTime, strBeginTime == strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = businessService.GetStatisticsNewBusinessesCountListByTime(areaList, strBeginTime, strEndTime);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void BusinessService_GetStatisticsAllBusinessesCountListByTime_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<Business> businessList = new List<Business>();
            repositoryBusiness.Stub(x => x.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue)).Return(businessList);
            statisticsBusinessCount.Stub(x => x.StatisticsAllBusinessesCountListByTime(businessList, BeginTime, EndTime, strBeginTime == strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = businessService.GetStatisticsAllBusinessesCountListByTime(areaList, strBeginTime, strEndTime);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void BusinessService_GetStatisticsAllBusinessesCountListByLife_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            IList<Business> businessList = new List<Business>();
            repositoryBusiness.Stub(x => x.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue)).Return(businessList);
            statisticsBusinessCount.Stub(x => x.StatisticsAllBusinessesCountListByLife(businessList)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = businessService.GetStatisticsAllBusinessesCountListByLife(areaList);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void BusinessService_GetStatisticsAllBusinessesCountListByStaff_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            IList<Business> businessList = new List<Business>();
            repositoryBusiness.Stub(x => x.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue)).Return(businessList);
            statisticsBusinessCount.Stub(x => x.StatisticsAllBusinessesCountListByStaff(businessList)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = businessService.GetStatisticsAllBusinessesCountListByStaff(areaList);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void BusinessService_GetStatisticsAllBusinessesCountListByArea_Test()
        {
            IList<Area> areaList = new List<Area> { new Area {Id=1,Name="北京市" },
                new Area {Id=2,Name="北京市市辖区" },
                new Area {Id=3,Name="北京市东城区" },
                new Area {Id=4,Name="北京市西城区" },
                new Area {Id=5,Name="北京市崇文区" },
                new Area {Id=6,Name="北京市宣武区" },
                new Area {Id=7,Name="北京市朝阳区" },
                new Area {Id=8,Name="北京市丰台区" },
                new Area {Id=9,Name="北京市石景山区" }};
            IList<string> AreaIdList = areaList.Select(x => x.Id.ToString()).ToList();
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            IList<Business> businessList = new List<Business>();
            repositoryBusiness.Stub(x => x.GetBusinessesByArea(AreaIdList, DateTime.MinValue, DateTime.MinValue)).Return(businessList);
            statisticsBusinessCount.Stub(x => x.StatisticsAllBusinessesCountGroupByArea(businessList, areaList)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = businessService.GetStatisticsAllBusinessesCountListByArea(areaList);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void BusinessService_GetStatisticsRatioYearOnYear_LastNotZero_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1), dtBase)).Return(100);
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1).AddYears(-1), dtBase.AddYears(-1))).Return(80);
            string strRatio = businessService.GetStatisticsRatioYearOnYear(areaList);
            Assert.AreEqual("125.00%", strRatio);
        }

        [Test()]
        public void BusinessService_GetStatisticsRatioYearOnYear_LastIsZero_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1), dtBase)).Return(100);
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1).AddYears(-1), dtBase.AddYears(-1))).Return(0);
            string strRatio = businessService.GetStatisticsRatioYearOnYear(areaList);
            Assert.AreEqual("0", strRatio);
        }

        [Test()]
        public void BusinessService_GetStatisticsRatioMonthOnMonth_LastNotZero_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1), dtBase)).Return(100);
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-2), dtBase.AddMonths(-1))).Return(80);
            string strRatio = businessService.GetStatisticsRatioMonthOnMonth(areaList);
            Assert.AreEqual("125.00%", strRatio);
        }

        [Test()]
        public void BusinessService_GetStatisticsRatioMonthOnMonth_LastIsZero_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1), dtBase)).Return(100);
            repositoryBusiness.Stub(x => x.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-2), dtBase.AddMonths(-1))).Return(0);
            string strRatio = businessService.GetStatisticsRatioMonthOnMonth(areaList);
            Assert.AreEqual("0", strRatio);
        }

        [Test()]
        public void BusinessService_GetAllBusinessesByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            IList<Business> businessList = new List<Business>();
            repositoryBusiness.Stub(x => x.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue)).Return(businessList);
            IList<Business> businessList1 = businessService.GetAllBusinessesByArea(areaList);
            Assert.AreEqual(businessList, businessList1);
        }
    }
}

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