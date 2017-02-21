using NUnit.Framework;
using Ydb.ApplicationService.Application.AgentService.DataStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.ModelDto;

namespace Ydb.ApplicationService.Application.AgentService.DataStatisticsTests
{
    [TestFixture()]
    public class StatisticsCountTests
    {
        //IDZMembershipService dzMembershipService;
        //IStatisticsCount statisticsCount;
        //IBalanceFlowService balanceFlowService;
        //IFinanceFlowService financeFlowService;
        //[SetUp]
        //public void Initialize()
        //{
        //    dzMembershipService = MockRepository.Mock<IDZMembershipService>();
        //    statisticsCount = MockRepository.Mock<IStatisticsCount>();
        //    balanceFlowService = MockRepository.Mock<IBalanceFlowService>();
        //    financeFlowService = new FinanceFlowService(dzMembershipService, statisticsCount, balanceFlowService);
        //}

        [Test()]
        public void StatisticsCount_StatisticsFinanceFlowList_Test()
        {
            StatisticsCount statisticsCount = new StatisticsCount();
            IList<BalanceFlowDto> balanceFlowDtoList = new List<BalanceFlowDto>
            {
                new BalanceFlowDto {AccountId="003c0c77-c2a0-4dba-930c-a6b000f80ceb" },
                new BalanceFlowDto {AccountId="00c02067-4900-41b6-b3d4-a68100c47cb9" },
                new BalanceFlowDto {AccountId="015351d4-ba0a-41b2-bc5e-a6b400c11c26" },
                new BalanceFlowDto {AccountId="03b647cb-a449-4f93-abf3-a67c0098ecdf" },
                new BalanceFlowDto {AccountId="115351d4-ba0a-41b2-bc5e-a6b400c11c23" },

            };
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26")},
                new DZMembership { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf")},
                new DZMembership { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23")},
                new DZMembership { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25")}
            };
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog> {
                new MembershipLoginLog ("215351d4-ba0a-41b2-bc5e-a6b400c11c25",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer),
                new MembershipLoginLog ("00c02067-4900-41b6-b3d4-a68100c47cb9",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("03b647cb-a449-4f93-abf3-a67c0098ecdf",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("115351d4-ba0a-41b2-bc5e-a6b400c11c23",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer)
            };
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByAppName(memberList, loginList);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户数量", statisticsInfo.YName);
            Assert.AreEqual("手机系统", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(2, statisticsInfo.XYValue["Android_Customer"]);
            Assert.AreEqual(4, statisticsInfo.XYValue["IOS_Customer"]);
            Assert.AreEqual(3, statisticsInfo.XYValue["other"]);
        }
    }
}