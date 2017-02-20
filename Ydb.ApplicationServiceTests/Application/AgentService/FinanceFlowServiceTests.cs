using NUnit.Framework;
using Ydb.ApplicationService.Application.AgentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.ApplicationService.ModelDto;
using Ydb.Membership.Application;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.DataStatistics;
using Ydb.Finance.Application;
using Rhino.Mocks;

namespace Ydb.ApplicationService.ApplicationTests.AgentServiceTests
{
    [TestFixture()]
    public class FinanceFlowServiceTests
    {
        IDZMembershipService dzMembershipService;
        IStatisticsCount statisticsCount;
        IBalanceFlowService balanceFlowService;
        IFinanceFlowService financeFlowService;
        [SetUp]
        public void Initialize()
        {
            dzMembershipService = MockRepository.Mock<IDZMembershipService>();
            statisticsCount = MockRepository.Mock<IStatisticsCount>();
            balanceFlowService = MockRepository.Mock<IBalanceFlowService>();
            financeFlowService = new FinanceFlowService(dzMembershipService, statisticsCount, balanceFlowService);
        }

        [Test()]
        public void FinanceFlowService_GetFinanceFlowList_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            MemberDto memberAgent = new MemberDto();
            IList<MemberDto> memberList = new List<MemberDto>();
            dzMembershipService.Stub(x => x.GetMembershipsByArea(areaList, UserType.customerservice)).Return(memberList);
            IList<string> UserIdList = memberList.Select(x => x.Id.ToString()).ToList();
            UserIdList.Add(memberAgent.Id.ToString());
            IList<BalanceFlowDto> balanceFlowDtoList =new List<BalanceFlowDto>();
            balanceFlowService.Stub(x => x.GetBalanceFlowByArea(UserIdList)).Return(balanceFlowDtoList);
            dzMembershipService.GetCountOfNewMembershipsYesterdayByArea(areaList, UserType.customer);
            IList<FinanceFlowDto> financeFlowDto = new List<FinanceFlowDto>();
            statisticsCount.Stub(x => x.StatisticsFinanceFlowList(balanceFlowDtoList, memberList)).Return(financeFlowDto);
            IList<FinanceFlowDto> financeFlowDto1 = financeFlowService.GetFinanceFlowList(areaList, memberAgent);
            Assert.IsNotNull(financeFlowDto1);
            Assert.AreEqual(financeFlowDto,financeFlowDto1);
        }
    }
}