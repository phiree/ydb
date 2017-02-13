using NUnit.Framework;
using Ydb.Membership.Application;
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
        }

        [Test()]
        public void DZMembershipService_GetCountOfNewMembershipsYesterdayByArea_Test()
        {
            string areaId = "areaId";
            DateTime beginTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            repositoryMembership.Stub(x => x.GetUsersCountByArea(areaId, beginTime, beginTime.AddDays(1), UserType.customer)).Return(100);
            long l = dzMembershipService.GetCountOfNewMembershipsYesterdayByArea(areaId,UserType.customer);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void DZMembershipService_GetCountOfAllMembershipsByArea_Test()
        {
            string areaId = "areaId";
            repositoryMembership.Stub(x => x.GetUsersCountByArea(areaId, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(100);
            long l = dzMembershipService.GetCountOfAllMembershipsByArea(areaId,UserType.customer);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void DZMembershipService_GetCountOfLoginMembershipsLastMonthByArea_Test()
        {
            string areaId = "areaId";
            IList<DZMembership> memberList = new List<DZMembership>();
            DateTime baseTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaId, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            repositoryMembershipLoginLog.Stub(x => x.GetMembershipLoginLogListByTime(baseTime.AddMonths(-1), baseTime)).Return(loginList);
            statisticsMembershipCount.Stub(x => x.StatisticsLoginCountLastMonth(memberList, loginList)).Return(100);
            long l = dzMembershipService.GetCountOfLoginMembershipsLastMonthByArea(areaId, UserType.customer);
            Assert.AreEqual(100, l);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsNewMembershipsCountListByTime_Test()
        {
            string areaId = "areaId";
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = new List<DZMembership>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaId, BeginTime, EndTime, UserType.customer)).Return(memberList);
            statisticsMembershipCount.Stub(x => x.StatisticsNewMembershipsCountListByTime(memberList,BeginTime,EndTime,strBeginTime==strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsNewMembershipsCountListByTime(areaId,strBeginTime,strEndTime,UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsAllMembershipsCountListByTime_Test()
        {
            string areaId = "areaId";
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = new List<DZMembership>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaId, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            statisticsMembershipCount.Stub(x => x.StatisticsAllMembershipsCountListByTime(memberList, BeginTime, EndTime, strBeginTime == strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsAllMembershipsCountListByTime(areaId, strBeginTime, strEndTime,UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
        }

        [Test()]
        public void DZMembershipService_GetStatisticsLoginCountListByTime_Test()
        {
            string areaId = "areaId";
            string strBeginTime = "2017-01-01";
            string strEndTime = "2017-01-31";
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = new List<DZMembership>();
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog>();
            repositoryMembership.Stub(x => x.GetUsersByArea(areaId, DateTime.MinValue, DateTime.MinValue, UserType.customer)).Return(memberList);
            repositoryMembershipLoginLog.Stub(x => x.GetMembershipLoginLogListByTime(BeginTime,EndTime)).Return(loginList);
            statisticsMembershipCount.Stub(x => x.StatisticsLoginCountListByTime(memberList,loginList, BeginTime, EndTime, strBeginTime == strEndTime)).Return(statisticsInfo);
            StatisticsInfo statisticsInfo1 = dzMembershipService.GetStatisticsLoginCountListByTime(areaId, strBeginTime, strEndTime,UserType.customer);
            Assert.AreEqual(statisticsInfo, statisticsInfo1);
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