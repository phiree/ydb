using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.Application;
using Ydb.Membership.Application.Dto;
using Ydb.ApplicationService.ModelDto;

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
                    financeFlowDto.UserNickName = member.NickName;
                    financeFlowDto.UserType = member.UserType.ToString();
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

        //public StatisticsInfo StatisticsAllBusinessesCountListByTime(IList<Business> businessList, DateTime beginTime, DateTime endTime, bool IsHour)
        //{
        //    StatisticsInfo statisticsInfo = new StatisticsInfo();
        //    statisticsInfo.YName = "累计店铺";
        //    statisticsInfo.XName = IsHour ? "时" : "日";
        //    statisticsInfo.XYValue = new Dictionary<string, long>();
        //    while (beginTime < endTime)
        //    {
        //        DateTime middleTime = IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
        //        string strKey = IsHour ? beginTime.Hour.ToString() : beginTime.ToString("yyyyMMdd");
        //        statisticsInfo.XYValue.Add(strKey, 0);
        //        statisticsInfo.XYValue[strKey] = businessList.Count(x => x.CreatedTime < middleTime);
        //        beginTime = middleTime;
        //    }
        //    return statisticsInfo;
        //}

        //public StatisticsInfo StatisticsAllBusinessesCountListByLife(IList<Business> businessList)
        //{
        //    StatisticsInfo statisticsInfo = new StatisticsInfo();
        //    statisticsInfo.YName = "店铺数量";
        //    statisticsInfo.XName = "店铺年限";
        //    statisticsInfo.XYValue = new Dictionary<string, long>();
        //    foreach (Business business in businessList)
        //    {
        //        string strWorkingYears = business.WorkingYears.ToString();
        //        if (statisticsInfo.XYValue.Keys.Contains(strWorkingYears))
        //        {
        //            statisticsInfo.XYValue[strWorkingYears]++;
        //        }
        //        else
        //        {
        //            statisticsInfo.XYValue.Add(strWorkingYears, 1);
        //        }
        //    }
        //    return statisticsInfo;
        //}

        //public StatisticsInfo StatisticsAllBusinessesCountListByStaff(IList<Business> businessList)
        //{
        //    StatisticsInfo statisticsInfo = new StatisticsInfo();
        //    statisticsInfo.YName = "店铺数量";
        //    statisticsInfo.XName = "员工总数";
        //    statisticsInfo.XYValue = new Dictionary<string, long>();
        //    foreach (Business business in businessList)
        //    {
        //        string strStaffAmount = business.StaffAmount.ToString();
        //        if (statisticsInfo.XYValue.Keys.Contains(strStaffAmount))
        //        {
        //            statisticsInfo.XYValue[strStaffAmount]++;
        //        }
        //        else
        //        {
        //            statisticsInfo.XYValue.Add(strStaffAmount, 1);
        //        }
        //    }
        //    return statisticsInfo;
        //}

        //public StatisticsInfo StatisticsAllBusinessesCountGroupByArea(IList<Business> businessList, IList<Area> areaList)
        //{
        //    StatisticsInfo statisticsInfo = new StatisticsInfo();
        //    statisticsInfo.YName = "店铺数量";
        //    statisticsInfo.XName = "区域";
        //    statisticsInfo.XYValue = new Dictionary<string, long>();
        //    foreach (Area area in areaList)
        //    {
        //        long bc = businessList.Count(x => x.AreaBelongTo == area.Id.ToString());
        //        statisticsInfo.XYValue.Add(area.Name, bc);
        //    }
        //    return statisticsInfo;
        //}


    }
}
