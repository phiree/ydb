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
        IWithdrawApplyService withdrawApplyService;
        [SetUp]
        public void Initialize()
        {
            dzMembershipService = MockRepository.Mock<IDZMembershipService>();
            dzMembershipDomainService = MockRepository.Mock<IDZMembershipDomainService>();
            statisticsCount = MockRepository.Mock<IStatisticsCount>();
            balanceFlowService = MockRepository.Mock<IBalanceFlowService>();
            balanceTotalService = MockRepository.Mock<IBalanceTotalService>();
            withdrawApplyService = MockRepository.Mock<IWithdrawApplyService>();
            financeFlowService = new FinanceFlowService(dzMembershipService, statisticsCount, balanceFlowService, balanceTotalService, withdrawApplyService);
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

        [Test()]
        public void FinanceFlowService_GetFinanceWithdrawList_Test()
        {
            IList<string> areaList = new List<string> { "areaId" };
            MemberDto memberAgent = new MemberDto() { Id=Guid.NewGuid()};
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto>();
            dzMembershipService.Stub(x => x.GetDZMembershipCustomerServiceByArea(areaList)).Return(memberList);
            IList<string> UserIdList = memberList.Select(x => x.Id.ToString()).ToList();
            UserIdList.Add(memberAgent.Id.ToString());
            IList<MemberDto> memberDtoList = new List<MemberDto>();
            memberDtoList.Add(memberAgent);
            foreach (DZMembershipCustomerServiceDto cs in memberList)
            {
                memberDtoList.Add(cs);
            }
            IList<WithdrawApplyDto> withdrawApplyDtoList = new List<WithdrawApplyDto>();
            withdrawApplyService.Stub(x => x.GetWithdrawApplyListByArea(UserIdList)).Return(withdrawApplyDtoList);
            FinanceWithdrawTotalDto financeWithdrawTotalDto = new FinanceWithdrawTotalDto();
            statisticsCount.Stub(x => x.StatisticsFinanceWithdrawList(withdrawApplyDtoList, memberDtoList)).Return(financeWithdrawTotalDto);
            FinanceWithdrawTotalDto financeWithdrawTotalDto1 = financeFlowService.GetFinanceWithdrawList(areaList, memberAgent);
            Assert.IsNotNull(financeWithdrawTotalDto1);
            Assert.AreEqual(financeWithdrawTotalDto, financeWithdrawTotalDto1);
        }
    }
}