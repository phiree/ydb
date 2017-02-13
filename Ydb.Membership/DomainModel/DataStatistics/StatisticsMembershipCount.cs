﻿using System;
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

        public StatisticsInfo StatisticsNewMembershipsCountListByTime(IList<DZMembership> memberList, DateTime beginTime,DateTime endTime,bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "新增用户";
            statisticsInfo.XName = IsHour ? "时" : "日";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            while (beginTime < endTime)
            {
                DateTime middleTime =IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                string strKey= IsHour ? beginTime.Hour .ToString () : beginTime.ToString("yyyyMMdd");
                statisticsInfo.XYValue.Add(strKey, 0);
                statisticsInfo.XYValue[strKey] = memberList.Count(x => x.TimeCreated >= beginTime && x.TimeCreated < middleTime);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }

        public StatisticsInfo StatisticsAllMembershipsCountListByTime(IList<DZMembership> memberList, DateTime beginTime, DateTime endTime, bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "累计用户";
            statisticsInfo.XName = IsHour ? "时" : "日";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            while (beginTime < endTime)
            {
                DateTime middleTime = IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                string strKey = IsHour ? beginTime.Hour.ToString() : beginTime.ToString("yyyyMMdd");
                statisticsInfo.XYValue.Add(strKey, 0);
                statisticsInfo.XYValue[strKey] = memberList.Count(x => x.TimeCreated < middleTime);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }

        public StatisticsInfo StatisticsLoginCountListByTime(IList<DZMembership> memberList, IList<MembershipLoginLog> loginList, DateTime beginTime, DateTime endTime, bool IsHour)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "用户活跃度";
            statisticsInfo.XName = IsHour ? "时" : "日";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            while (beginTime < endTime)
            {
                DateTime middleTime = IsHour ? beginTime.AddHours(1) : beginTime.AddDays(1);
                IList<MembershipLoginLog>  logins = loginList.Where(x => x.LogTime >= beginTime && x.LogTime < middleTime).ToList();
                string strKey = IsHour ? beginTime.Hour.ToString() : beginTime.ToString("yyyyMMdd");
                statisticsInfo.XYValue.Add(strKey, 0);
                statisticsInfo.XYValue[strKey] = StatisticsLoginCountLastMonth(memberList,logins);
                beginTime = middleTime;
            }
            return statisticsInfo;
        }


        public StatisticsInfo StatisticsAllMembershipsCountListBySex(IList<DZMembership> memberList)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "用户数量";
            statisticsInfo.XName = "性别";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            long l = memberList.Count(x => x.Sex);
            statisticsInfo.XYValue.Add("男", memberList.Count-l);
            statisticsInfo.XYValue.Add("女", l);
            return statisticsInfo;
        }


    }
}