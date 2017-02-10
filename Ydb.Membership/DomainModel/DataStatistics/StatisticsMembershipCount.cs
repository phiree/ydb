using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Common.Domain;

namespace Ydb.Membership.DomainModel.DataStatistics
{
    public class StatisticsMembershipCount: IStatisticsMembershipCount
    {
        public long StatisticsLoginCountLastMonth(IList<DZMembership> memberList, IList<MembershipLoginLog> loginList)
        {
            long l = 0;
            foreach (DZMembership member in memberList)
            {
                //IEnumerable<MembershipLoginLog> longin = loginList.Where(x => x.MemberId == member.Id.ToString());
                //if (longin.Count() > 0)
                //{
                //    l++;
                //}
                if (loginList.Count(x => x.MemberId == member.Id.ToString()) > 0)
                {
                    l++;
                }
            }
            return l;
        }

        public StatisticsInfo StatisticsNewMembershipCountListByTime(IList<DZMembership> memberList, DateTime beginTime,DateTime endTime,bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.XName = "新增用户";
            statisticsInfo.YName = IsHour ? "时" : "日";
            while (beginTime < endTime)
            {
                DateTime middleTime =IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                statisticsInfo.XYValue.Add(beginTime, 0);
                statisticsInfo.XYValue[beginTime]= memberList.Count(x => x.TimeCreated >= beginTime && x.TimeCreated < middleTime);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }

        public StatisticsInfo StatisticsAllMembershipCountListByTime(IList<DZMembership> memberList, DateTime beginTime, DateTime endTime, bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.XName = "累计用户";
            statisticsInfo.YName = IsHour ? "时" : "日";
            while (beginTime < endTime)
            {
                DateTime middleTime = IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                statisticsInfo.XYValue.Add(beginTime, 0);
                beginTime = middleTime;
                statisticsInfo.XYValue[beginTime] = memberList.Count(x => x.TimeCreated < middleTime);
            }
            return statisticsInfo;
        }

        public StatisticsInfo StatisticsLoginCountListByTime(IList<DZMembership> memberList, IList<MembershipLoginLog> loginList, DateTime beginTime, DateTime endTime, bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.XName = "用户活跃度";
            statisticsInfo.YName = IsHour ? "时" : "日";
            while (beginTime < endTime)
            {
                DateTime middleTime = IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                IList<MembershipLoginLog>  logins = loginList.Where(x => x.LogTime >= beginTime && x.LogTime < middleTime).ToList();
                statisticsInfo.XYValue.Add(beginTime, 0);
                statisticsInfo.XYValue[beginTime] = StatisticsLoginCountLastMonth(memberList,logins);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }


    }
}
