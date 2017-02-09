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

        public StatisticsInfo StatisticsNewMembershipCountListByDay(IList<DZMembership> memberList, DateTime beginTime,DateTime endTime)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.XName = "新增用户";
            statisticsInfo.YName = "日期";
            while (beginTime < endTime)
            {
                DateTime middleTime = beginTime.AddDays(1);
                statisticsInfo.XYValue.Add(beginTime, 0);
                //IList<DZMembership> members = memberList.Where(x => x.TimeCreated >= beginTime && x.TimeCreated < middleTime).ToList();
                statisticsInfo.XYValue[beginTime]= memberList.Count(x => x.TimeCreated >= beginTime && x.TimeCreated < middleTime);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }

    }
}
