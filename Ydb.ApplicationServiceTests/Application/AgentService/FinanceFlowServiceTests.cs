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
using Ydb.ApplicationService.Application.AgentService.DataStatistics;
using Ydb.Finance.Application;
using Rhino.Mocks;
using Ydb.Membership.DomainModel;

namespace Ydb.ApplicationService.Application.AgentServiceTests
{
    [TestFixture()]
    public class FinanceFlowServiceTests
    {
        IDZMembershipService dzMembershipService;
        IDZMembershipDomainService dzMembershipDomainService;
        IStatisticsCount statisticsCount;
        IBalanceFlowService balanceFlowService;
        IFinanceFlowService financeFlowService;
        IBalanceTotalService balanceTotalService;
    [SetUp]
        public void Initialize()
        {
            dzMembershipService = MockRepository.Mock<IDZMembershipService>();
            dzMembershipDomainService = MockRepository.Mock<IDZMembershipDomainService>();
            statisticsCount = MockRepository.Mock<IStatisticsCount>();
            balanceFlowService = MockRepository.Mock<IBalanceFlowService>();
            balanceTotalService = MockRepository.Mock<IBalanceTotalService>();
            financeFlowService = new FinanceFlowService(dzMembershipService, statisticsCount, balanceFlowService, balanceTotalService);
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
            IList<BalanceFlowDto> balanceFlowDtoList = new List<BalanceFlowDto>();
            balanceFlowService.Stub(x => x.GetBalanceFlowByArea(UserIdList)).Return(balanceFlowDtoList);
            dzMembershipService.GetCountOfNewMembershipsYesterdayByArea(areaList, UserType.customer);
            IList<FinanceFlowDto> financeFlowDto = new List<FinanceFlowDto>();
            statisticsCount.Stub(x => x.StatisticsFinanceFlowList(balanceFlowDtoList, memberList)).Return(financeFlowDto);
            IList<FinanceFlowDto> financeFlowDto1 = financeFlowService.GetFinanceFlowList(areaList, memberAgent);
            Assert.IsNotNull(financeFlowDto1);
            Assert.AreEqual(financeFlowDto, financeFlowDto1);
        }

        [Test()]
        public void FinanceFlowService_GetFinanceTotalList_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto>();
            dzMembershipService.Stub(x => x.GetDZMembershipCustomerServiceByArea(areaList)).Return(memberList);
            IList<string> UserIdList = memberList.Select(x => x.Id.ToString()).ToList();
            IList<BalanceTotalDto> balanceTotalDtoList = new List<BalanceTotalDto>();
            balanceTotalService.Stub(x => x.GetBalanceTotalByArea(UserIdList)).Return(balanceTotalDtoList);
            IList<FinanceTotalDto> financeTotalDto = new List<FinanceTotalDto>();
            statisticsCount.Stub(x => x.StatisticsFinanceTotalList(balanceTotalDtoList, memberList)).Return(financeTotalDto);
            IList<FinanceTotalDto> financeTotalDto1 = financeFlowService.GetFinanceTotalList(areaList);
            Assert.IsNotNull(financeTotalDto1);
            Assert.AreEqual(financeTotalDto, financeTotalDto1);
        }
    }
}