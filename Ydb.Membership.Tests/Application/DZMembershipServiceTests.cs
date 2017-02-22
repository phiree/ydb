using NUnit.Framework;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Tests;
using System.Transactions;
using Ydb.Membership.DomainModel;
using Ydb.Common.Infrastructure;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.DomainModel.DataStatistics;
using Ydb.Membership.DomainModel.Service;
using Rhino.Mocks;
using Ydb.Common.Domain;
using Ydb.Membership.DomainModel.Enums;

namespace Ydb.Membership.ApplicationTests
{
    [TestFixture()]
    public class DZMembershipServiceTests
    {
        IDZMembershipDomainService dzmembershipDomainService;
        IEmailService emailService;
        IRepositoryDZMembership repositoryMembership;
        IRepositoryUserToken repositoryUserToken;
        ILogin3rd login3rdService;
        IEncryptService encryptService;
        IRepositoryMembershipLoginLog repositoryMembershipLoginLog;
        IStatisticsMembershipCount statisticsMembershipCount;

        IDZMembershipService dzMembershipService;

        IDZMembershipService dzMembershipService1;
        [SetUp]
        public void Initialize()
        {
            dzmembershipDomainService = MockRepository.Mock<IDZMembershipDomainService>();
            emailService = MockRepository.Mock<IEmailService>();
            repositoryMembership = MockRepository.Mock<IRepositoryDZMembership>();
            repositoryUserToken = MockRepository.Mock<IRepositoryUserToken>();
            login3rdService = MockRepository.Mock<ILogin3rd>();
            encryptService = MockRepository.Mock<IEncryptService>();
            repositoryMembershipLoginLog = MockRepository.Mock<IRepositoryMembershipLoginLog>();
            statisticsMembershipCount = MockRepository.Mock<IStatisticsMembershipCount>();
            dzMembershipService = new DZMembershipService(dzmembershipDomainService,
            repositoryMembership, emailService, encryptService, login3rdService, repositoryUserToken,
            repositoryMembershipLoginLog, statisticsMembershipCount);

            Bootstrap.Boot();
            AutoMapper.Mapper.Initialize(x =>
            {
                AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
            });
            dzMembershipService1 = Bootstrap.Container.Resolve<IDZMembershipService>();
        }

        [Test()]
        public void DZMembershipService_GetCountOfNewMembershipsYesterdayByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            DateTime beginTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            repositoryMembership.Stub(x => x.GetUsersCountByArea(areaList, beginTime, beginTime.AddDays(1), UserType.customer)).Return(100);
            long l = dzMembershipService.GetCountOfNewMembershipsYesterdayByArea(areaList, UserType.customer);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void DZMembershipService_GetCountOfAllMembershipsByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            repositoryMembership.Stub(x => x.GetUsersCountByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(100);
            long l = dzMembershipService.GetCountOfAllMembershipsByArea(areaList, UserType.customer);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void DZMembershipService_GetCountOfLoginMembershipsLastMonthByArea_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            IList<DZMembership> memberList = new List<DZMembership>();
            DateTime baseTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            repositoryMembershipLoginLog.Stub(x => x.GetMembershipLoginLogListByTime(baseTime.AddMonths(-1), baseTime)).Return(loginList);
            statisticsMembershipCount.Stub(x => x.StatisticsLoginCountLastMonth(memberList, loginList)).Return(100);
            long l = dzMembershipService.GetCountOfLoginMembershipsLastMonthByArea(areaList, UserType.customer);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsNewMembershipsCountListByTime_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = new List<DZMembership>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaList, BeginTime, EndTime, UserType.customer)).Return(memberList);
            statisticsMembershipCount.Stub(x => x.StatisticsNewMembershipsCountListByTime(memberList, BeginTime, EndTime, strBeginTime == strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsNewMembershipsCountListByTime(areaList, strBeginTime, strEndTime, UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsAllMembershipsCountListByTime_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = new List<DZMembership>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            statisticsMembershipCount.Stub(x => x.StatisticsAllMembershipsCountListByTime(memberList, BeginTime, EndTime, strBeginTime == strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsAllMembershipsCountListByTime(areaList, strBeginTime, strEndTime, UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsLoginCountListByTime_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = new List<DZMembership>();
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            repositoryMembershipLoginLog.Stub(x => x.GetMembershipLoginLogListByTime(BeginTime, EndTime)).Return(loginList);
            statisticsMembershipCount.Stub(x => x.StatisticsLoginCountListByTime(memberList, loginList, BeginTime, EndTime, strBeginTime == strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsLoginCountListByTime(areaList, strBeginTime, strEndTime, UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsAllMembershipsCountListByAppName_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            IList<DZMembership> memberList = new List<DZMembership>();
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            repositoryMembershipLoginLog.Stub(x => x.GetMembershipLastLoginLog()).Return(loginList);
            statisticsMembershipCount.Stub(x => x.StatisticsAllMembershipsCountListByAppName(memberList, loginList)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsAllMembershipsCountListByAppName(areaList, UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsAllMembershipsCountListBySex_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            IList<DZMembership> memberList = new List<DZMembership>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            statisticsMembershipCount.Stub(x => x.StatisticsAllMembershipsCountListBySex(memberList)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsAllMembershipsCountListBySex(areaList, UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsAllMembershipsCountListByArea_Test()
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
            IList<DZMembership> memberList = new List<DZMembership>();
            repositoryMembership.Stub(x => x.GetUsersByArea(AreaIdList, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            statisticsMembershipCount.Stub(x => x.StatisticsAllMembershipsCountGroupByArea(memberList, areaList)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsAllMembershipsCountListByArea(areaList, UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_CompleteDZMembership_Test()
        {
            dzMembershipService1.RegisterCustomerService("customerService1", "123456", "123456", "");
            DZMembershipCustomerServiceDto dzMembershipCustomerServiceDto = (DZMembershipCustomerServiceDto)dzMembershipService1.GetDZMembershipCustomerServiceByName("customerService1");
            dzMembershipCustomerServiceDto.QQNumber = "4548973";
            dzMembershipService1.CompleteDZMembership(dzMembershipCustomerServiceDto);
            DZMembershipCustomerServiceDto dzMembershipCustomerServiceDto1 = (DZMembershipCustomerServiceDto)dzMembershipService1.GetDZMembershipCustomerServiceByName("customerService1");
            Assert.AreEqual(dzMembershipCustomerServiceDto.Id, dzMembershipCustomerServiceDto1.Id);
            Assert.AreEqual(dzMembershipCustomerServiceDto.QQNumber, dzMembershipCustomerServiceDto1.QQNumber);
        }

        [Test()]
        public void DZMembershipService_RegisterCustomerService_RightPassword_Test()
        {
            string errMsg = "";
            string userName = "userName1";
            string password = "123456";
            DZMembership dzMembership = new DZMembershipCustomerService { UserName = userName, Password = "123456" };
            dzmembershipDomainService.Stub(x => x.CreateCustomerService(userName, password,out errMsg)).Return(dzMembership).OutRef(errMsg);
            RegisterResult registerResult = dzMembershipService.RegisterCustomerService(userName, password, password, "");
            Assert.IsTrue(registerResult.RegisterSuccess);
            Assert.IsTrue(registerResult.SendEmailSuccess);
        }

        [Test()]
        public void DZMembershipService_RegisterCustomerService_ErrorPassword_Test()
        {
            string errMsg = "";
            string userName = "userName1";
            string password = "123456";
            DZMembership dzMembership = new DZMembershipCustomerService { UserName = userName, Password = "123456" };
            dzmembershipDomainService.Stub(x => x.CreateCustomerService(userName, password, out errMsg)).Return(dzMembership).OutRef(errMsg);
            RegisterResult registerResult = dzMembershipService.RegisterCustomerService(userName, password, "1234567", "");
            Assert.IsFalse(registerResult.RegisterSuccess);
            Assert.AreEqual("密码不匹配", registerResult.RegisterErrMsg);
            Assert.IsTrue(registerResult.SendEmailSuccess);
        }
    }
}

namespace Ydb.Membership.Application.Tests
{
    [TestFixture()]
    public class DZMembershipServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            AutoMapper.Mapper.Initialize(x => {
                AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
            });
            dzmembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        }
        IDZMembershipService dzmembershipService;
        string userName = "test_user"+Guid.NewGuid();
        [Test()]

        public void RegisterBusinessUserTest()
        {
           Dto.RegisterResult registerResult= dzmembershipService.RegisterBusinessUser(userName,"123456", "123456","http://localhost/");
            Assert.AreEqual(true,string.IsNullOrEmpty( registerResult.RegisterErrMsg));
            Assert.AreEqual(true, registerResult.RegisterSuccess);  // your test code here
            Dto.MemberDto memberDto = dzmembershipService.GetUserByName(userName);
            Assert.AreEqual(userName, memberDto.UserName);


        }
       
         
    }
}