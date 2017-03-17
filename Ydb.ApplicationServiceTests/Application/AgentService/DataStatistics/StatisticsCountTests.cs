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
using Ydb.Order.DomainModel;
using Ydb.Common.Domain;

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
                new BalanceFlowDto {AccountId="935351d4-ba0a-51b2-bc5e-a6b400c11c26" },
                new BalanceFlowDto {AccountId="03b647cb-a449-4f93-abf3-a67c0098ecdf" },
                new BalanceFlowDto {AccountId="115351d4-ba0a-41b2-bc5e-a6b400c11c23" }

            };
            IList<MemberDto> memberList = new List<MemberDto> {
                new MemberDto { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName1",UserType="UserType1"},
                new MemberDto { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName2",UserType="UserType2"},
                new MemberDto { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),DisplayName="UserName3",UserType="UserType3"},
                new MemberDto { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),DisplayName="UserName4",UserType="UserType4"},
                new MemberDto { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName5",UserType="UserType5"},
                new MemberDto { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),DisplayName="UserName6",UserType="UserType6"},
                new MemberDto { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName7",UserType="UserType7"},
                new MemberDto { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23"),DisplayName="UserName8",UserType="UserType8"},
                new MemberDto { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25"),DisplayName="UserName9",UserType="UserType9"}
            };

            IList<FinanceFlowDto> financeFlowDtoList = statisticsCount.StatisticsFinanceFlowList(balanceFlowDtoList, memberList);

            Assert.AreEqual(4, financeFlowDtoList.Count);
            Assert.AreEqual("003c0c77-c2a0-4dba-930c-a6b000f80ceb", financeFlowDtoList[0].UserId);
            Assert.AreEqual("00c02067-4900-41b6-b3d4-a68100c47cb9", financeFlowDtoList[1].UserId);
            Assert.AreEqual("03b647cb-a449-4f93-abf3-a67c0098ecdf", financeFlowDtoList[2].UserId);
            Assert.AreEqual("115351d4-ba0a-41b2-bc5e-a6b400c11c23", financeFlowDtoList[3].UserId);
            Assert.AreEqual("UserName1", financeFlowDtoList[0].UserNickName);
            Assert.AreEqual("UserName3", financeFlowDtoList[1].UserNickName);
            Assert.AreEqual("UserName6", financeFlowDtoList[2].UserNickName);
            Assert.AreEqual("UserName8", financeFlowDtoList[3].UserNickName);
        }


        [Test()]
        public void StatisticsCount_StatisticsFinanceTotalList_Test()
        {

            StatisticsCount statisticsCount = new StatisticsCount();
            IList<BalanceTotalDto> balanceTotalDtoList = new List<BalanceTotalDto>
            {
                new BalanceTotalDto {UserId="003c0c77-c2a0-4dba-930c-a6b000f80ceb" },
                new BalanceTotalDto {UserId="00c02067-4900-41b6-b3d4-a68100c47cb9" },
                new BalanceTotalDto {UserId="935351d4-ba0a-51b2-bc5e-a6b400c11c26" },
                new BalanceTotalDto {UserId="03b647cb-a449-4f93-abf3-a67c0098ecdf" },
                new BalanceTotalDto {UserId="115351d4-ba0a-41b2-bc5e-a6b400c11c23" }

            };
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto> {
                new DZMembershipCustomerServiceDto { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName1",UserType="UserType1"},
                new DZMembershipCustomerServiceDto { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName2",UserType="UserType2"},
                new DZMembershipCustomerServiceDto { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),DisplayName="UserName3",UserType="UserType3"},
                new DZMembershipCustomerServiceDto { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),DisplayName="UserName4",UserType="UserType4"},
                new DZMembershipCustomerServiceDto { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName5",UserType="UserType5"},
                new DZMembershipCustomerServiceDto { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),DisplayName="UserName6",UserType="UserType6"},
                new DZMembershipCustomerServiceDto { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName7",UserType="UserType7"},
                new DZMembershipCustomerServiceDto { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23"),DisplayName="UserName8",UserType="UserType8"},
                new DZMembershipCustomerServiceDto { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25"),DisplayName="UserName9",UserType="UserType9"}
            };

            IList<FinanceTotalDto> financeTotalDtoList = statisticsCount.StatisticsFinanceTotalList(balanceTotalDtoList, memberList);

            Assert.AreEqual(4, financeTotalDtoList.Count);
            Assert.AreEqual("003c0c77-c2a0-4dba-930c-a6b000f80ceb", financeTotalDtoList[0].UserId);
            Assert.AreEqual("00c02067-4900-41b6-b3d4-a68100c47cb9", financeTotalDtoList[1].UserId);
            Assert.AreEqual("03b647cb-a449-4f93-abf3-a67c0098ecdf", financeTotalDtoList[2].UserId);
            Assert.AreEqual("115351d4-ba0a-41b2-bc5e-a6b400c11c23", financeTotalDtoList[3].UserId);
            Assert.AreEqual("UserName1", financeTotalDtoList[0].UserNickName);
            Assert.AreEqual("UserName3", financeTotalDtoList[1].UserNickName);
            Assert.AreEqual("UserName6", financeTotalDtoList[2].UserNickName);
            Assert.AreEqual("UserName8", financeTotalDtoList[3].UserNickName);
        }

        [Test()]
        public void StatisticsCount_StatisticsFinanceWithdrawList_Test()
        {

            StatisticsCount statisticsCount = new StatisticsCount();
            IList<WithdrawApplyDto> withdrawApplyDtoList = new List<WithdrawApplyDto>
            {
                new WithdrawApplyDto {ApplyUserId="003c0c77-c2a0-4dba-930c-a6b000f80ceb",ApplyStatus=ApplyStatusEnums.ApplyWithdraw ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="00c02067-4900-41b6-b3d4-a68100c47cb9",ApplyStatus=ApplyStatusEnums.ApplyWithdraw ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="935351d4-ba0a-51b2-bc5e-a6b400c11c26",ApplyStatus=ApplyStatusEnums.ApplyWithdraw ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="03b647cb-a449-4f93-abf3-a67c0098ecdf",ApplyStatus=ApplyStatusEnums.Payfail ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="115351d4-ba0a-41b2-bc5e-a6b400c11c23",ApplyStatus=ApplyStatusEnums.PaySeccuss,ApplyAmount=11 ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="115351d4-ba0a-41b2-bc5e-a6b400c11b34",ApplyStatus=ApplyStatusEnums.PaySeccuss,ApplyAmount=31 ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="218ff2ae-fd01-487a-b8eb-036320a1fd0f",ApplyStatus=ApplyStatusEnums.PaySeccuss,ApplyAmount=5 ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="218ff2ae-fd01-487a-b8eb-036320a1fd0f",ApplyStatus=ApplyStatusEnums.ApplyWithdraw,ApplyAmount=6 ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="218ff2ae-fd01-487a-b8eb-036320a1fd0f",ApplyStatus=ApplyStatusEnums.PaySeccuss,ApplyAmount=7 ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="218ff2ae-fd01-487a-b8eb-036320a1fd0f",ApplyStatus=ApplyStatusEnums.Payfail,ApplyAmount=8 ,ReceiveAccount=new BalanceAccountDto()},
                new WithdrawApplyDto {ApplyUserId="218ff2ae-fd01-487a-b8eb-036320a1fd0f",ApplyStatus=ApplyStatusEnums.ApplyWithdraw,ApplyAmount=9 ,ReceiveAccount=new BalanceAccountDto()}
            };
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto> {
                new DZMembershipCustomerServiceDto { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName1",UserType="UserType1"},
                new DZMembershipCustomerServiceDto { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName2",UserType="UserType2"},
                new DZMembershipCustomerServiceDto { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),DisplayName="UserName3",UserType="UserType3"},
                new DZMembershipCustomerServiceDto { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),DisplayName="UserName4",UserType="UserType4"},
                new DZMembershipCustomerServiceDto { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName5",UserType="UserType5"},
                new DZMembershipCustomerServiceDto { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),DisplayName="UserName6",UserType="UserType6"},
                new DZMembershipCustomerServiceDto { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb"),DisplayName="UserName7",UserType="UserType7"},
                new DZMembershipCustomerServiceDto { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23"),DisplayName="UserName8",UserType="UserType8"},
                new DZMembershipCustomerServiceDto { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25"),DisplayName="UserName9",UserType="UserType9"},
                new DZMembershipCustomerServiceDto { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11b34"),DisplayName="UserName10",UserType="UserType10"}
            };
            MemberDto memberAgent = new MemberDto { Id = new Guid("218ff2ae-fd01-487a-b8eb-036320a1fd0f") };
            IList<MemberDto> memberDtoList = new List<MemberDto>();
            memberDtoList.Add(memberAgent);
            foreach (DZMembershipCustomerServiceDto cs in memberList)
            {
                memberDtoList.Add(cs);
            }

            FinanceWithdrawTotalDto financeWithdrawTotalDto = statisticsCount.StatisticsFinanceWithdrawList(withdrawApplyDtoList, memberDtoList);

            Assert.AreEqual(4, financeWithdrawTotalDto.WithdrawNotDeal);
            Assert.AreEqual(54, financeWithdrawTotalDto.WithdrawTotal);
            Assert.AreEqual(10, financeWithdrawTotalDto.financeWithdrawDtoList.Count);
        }

    }
}