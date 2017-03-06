using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.ModelDto;
using Ydb.Order.DomainModel;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.ApplicationService.Application.AgentService.DataStatistics
{
    public class StatisticsCount : IStatisticsCount
    {

        public IList<FinanceFlowDto> StatisticsFinanceFlowList(IList<BalanceFlowDto> balanceFlowDtoList,IList<MemberDto>memberList )
        {
            IList<FinanceFlowDto> financeFlowDtoList = new List<FinanceFlowDto>();
            foreach (MemberDto member in memberList)
            {
                IList<BalanceFlowDto> flowList = balanceFlowDtoList.Where(x => x.AccountId == member.Id.ToString()).ToList();
                foreach (BalanceFlowDto balanceFlow in flowList)
                {
                    FinanceFlowDto financeFlowDto = new FinanceFlowDto();
                    financeFlowDto.Id = balanceFlow.Id;
                    financeFlowDto.UserId = balanceFlow.AccountId;
                    financeFlowDto.UserNickName = member.DisplayName;
                    financeFlowDto.UserType = member.UserType;
                    financeFlowDto.Amount = balanceFlow.Amount;
                    financeFlowDto.AmountTotal = balanceFlow.AmountTotal;
                    financeFlowDto.AmountView = balanceFlow.AmountView;
                    financeFlowDto.FlowType = balanceFlow.FlowType;
                    financeFlowDto.Income = balanceFlow.Income;
                    financeFlowDto.OccurTime = balanceFlow.OccurTime;
                    financeFlowDto.Rate = balanceFlow.Rate;
                    financeFlowDto.RelatedObjectId = balanceFlow.RelatedObjectId;
                    financeFlowDto.SerialNo = balanceFlow.SerialNo;
                    financeFlowDtoList.Add(financeFlowDto);
                }
            }
            return financeFlowDtoList;
        }

        public IList<FinanceTotalDto> StatisticsFinanceTotalList(IList<BalanceTotalDto> balanceTotalDtoList, IList<DZMembershipCustomerServiceDto> memberList)
        {
            IList<FinanceTotalDto> financeTotalDtoList = new List<FinanceTotalDto>();
            foreach (DZMembershipCustomerServiceDto member in memberList)
            {
                IList<BalanceTotalDto> TotalList = balanceTotalDtoList.Where(x => x.UserId == member.Id.ToString()).ToList();
                foreach (BalanceTotalDto balanceTotal in TotalList)
                {
                    FinanceTotalDto financeTotalDto = new FinanceTotalDto();
                    financeTotalDto.Id = balanceTotal.Id;
                    financeTotalDto.UserId = balanceTotal.UserId;
                    financeTotalDto.UserNickName = member.DisplayName;
                    financeTotalDto.UserType = member.UserType;
                    financeTotalDto.Phone = member.Phone;
                    financeTotalDto.RealName = member.RealName;
                    financeTotalDto.IsAgentCustomerService = member.IsAgentCustomerService;
                    financeTotalDto.Total = balanceTotal.Total;
                    financeTotalDto.Frozen = balanceTotal.Frozen;
                    financeTotalDto.Account = balanceTotal.AccountDto==null?"": balanceTotal.AccountDto.Account;
                    financeTotalDtoList.Add(financeTotalDto);
                }
            }
            return financeTotalDtoList;
        }


        public FinanceWithdrawTotalDto StatisticsFinanceWithdrawList(IList<WithdrawApplyDto> withdrawApplyDtoList, IList<MemberDto> memberList)
        {
            FinanceWithdrawTotalDto financeWithdrawTotalDto = new FinanceWithdrawTotalDto();
            financeWithdrawTotalDto.financeWithdrawDtoList = new List<FinanceWithdrawDto>();
            foreach (MemberDto member in memberList)
            {
                IList<WithdrawApplyDto> applyList = withdrawApplyDtoList.Where(x => x.ApplyUserId == member.Id.ToString()).ToList();
                foreach (WithdrawApplyDto withdrawApplyDto in applyList)
                {
                    FinanceWithdrawDto financeWithdrawDto = new FinanceWithdrawDto();
                    financeWithdrawDto.Id = withdrawApplyDto.Id;
                    financeWithdrawDto.ApplyUserId = withdrawApplyDto.ApplyUserId;
                    financeWithdrawDto.UserNickName = member.DisplayName;
                    financeWithdrawDto.ApplyAmount = withdrawApplyDto.ApplyAmount;
                    financeWithdrawDto.ApplyTime = withdrawApplyDto.ApplyTime;
                    financeWithdrawDto.ReceiveAccount = withdrawApplyDto.ReceiveAccount.Account;
                    financeWithdrawDto.TransferAmount = withdrawApplyDto.TransferAmount;
                    financeWithdrawDto.ServiceFee = withdrawApplyDto.ServiceFee;
                    financeWithdrawDto.ApplyStatus = withdrawApplyDto.ApplyStatus.ToString();
                    financeWithdrawDto.ApplyRemark = withdrawApplyDto.ApplyRemark;
                    financeWithdrawDto.Rate = withdrawApplyDto.Rate;
                    financeWithdrawDto.PayUserId = withdrawApplyDto.PayUserId;
                    financeWithdrawDto.PayTime = withdrawApplyDto.PayTime;
                    financeWithdrawDto.PayStatus = withdrawApplyDto.PayStatus;
                    financeWithdrawDto.PayRemark = withdrawApplyDto.PayRemark;
                    financeWithdrawDto.CreateTime = withdrawApplyDto.CreateTime;
                    financeWithdrawDto.UpdateTime = withdrawApplyDto.UpdateTime;
                    financeWithdrawDto.ApplySerialNo = withdrawApplyDto.ApplySerialNo;
                    financeWithdrawDto.PaySerialNo = withdrawApplyDto.PaySerialNo;
                    financeWithdrawDto.D3SerialNo = withdrawApplyDto.D3SerialNo;
                    financeWithdrawDto.D3Time = withdrawApplyDto.D3Time;
                    financeWithdrawTotalDto.financeWithdrawDtoList.Add(financeWithdrawDto);
                }
            }
            financeWithdrawTotalDto.WithdrawNotDeal = financeWithdrawTotalDto.financeWithdrawDtoList.Count(x => x.ApplyStatus =="ApplyWithdraw");
            financeWithdrawTotalDto.WithdrawTotal = financeWithdrawTotalDto.financeWithdrawDtoList.Where(x => x.ApplyStatus == "PaySeccuss").Sum(x => x.ApplyAmount);
            return financeWithdrawTotalDto;
        }



        public StatisticsInfo StatisticsNewOrdersCountListByTime(IList<ServiceOrder> orderList, DateTime beginTime, DateTime endTime, bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "新增订单";
            statisticsInfo.XName = IsHour ? "时" : "日";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            while (beginTime < endTime)
            {
                DateTime middleTime = IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                string strKey = IsHour ? beginTime.Hour.ToString() : beginTime.ToString("yyyyMMdd");
                statisticsInfo.XYValue.Add(strKey, 0);
                statisticsInfo.XYValue[strKey] = orderList.Count(x => x.OrderConfirmTime >= beginTime && x.OrderConfirmTime < middleTime);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }

        public StatisticsInfo StatisticsAllOrdersCountListByTime(IList<ServiceOrder> orderList, DateTime beginTime, DateTime endTime, bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "累计订单";
            statisticsInfo.XName = IsHour ? "时" : "日";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            while (beginTime < endTime)
            {
                DateTime middleTime = IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                string strKey = IsHour ? beginTime.Hour.ToString() : beginTime.ToString("yyyyMMdd");
                statisticsInfo.XYValue.Add(strKey, 0);
                statisticsInfo.XYValue[strKey] = orderList.Count(x => x.OrderConfirmTime < middleTime);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }


        public StatisticsInfo StatisticsAllOrdersCountGroupByArea(IList<ServiceOrder> orderList, IList<Business> businessList, IList<Area> areaList)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "订单数量";
            statisticsInfo.XName = "区域";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            foreach (Area area in areaList)
            {
                IList<Business> blist = businessList.Where(x => x.AreaBelongTo == area.Id.ToString()).ToList();
                IList<string> bIdList = blist.Select(x => x.Id.ToString()).ToList();
                long bc = orderList.Count(x => bIdList.Contains(x.Id.ToString()));
                statisticsInfo.XYValue.Add(area.Name, bc);
            }
            return statisticsInfo;
        }

    }
}
