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
            statisticsInfo.XYValue.Add("men", memberList.Count-l);
            statisticsInfo.XYValue.Add("women", l);
            return statisticsInfo;
        }

        public StatisticsInfo StatisticsAllMembershipsCountListByAppName(IList<DZMembership> memberList, IList<MembershipLoginLog> loginList)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "用户数量";
            statisticsInfo.XName = "手机系统";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            statisticsInfo.XYValue.Add("other", 0);
            foreach (DZMembership member in memberList)
            {
                MembershipLoginLog membershipLoginLog = loginList.FirstOrDefault(x => x.MemberId == member.Id.ToString());
                if (membershipLoginLog==null)
                {
                    statisticsInfo.XYValue["other"]++;
                }
                else
                {
                    string appName = membershipLoginLog.AppName.ToString();
                    if (statisticsInfo.XYValue.Keys.Contains(appName))
                    {
                        statisticsInfo.XYValue[appName]++;
                    }
                    else
                    {
                        statisticsInfo.XYValue.Add(appName, 1);
                    }
                }
            }
            return statisticsInfo;
        }

        public StatisticsInfo StatisticsAllMembershipsCountGroupByArea(IList<DZMembership> memberList, IList<Area> areaList)
        {
            StatisticsInfo statisticsInfo = new StatisticsInfo();
            statisticsInfo.YName = "用户数量";
            statisticsInfo.XName = "区域";
            statisticsInfo.XYValue = new Dictionary<string, long>();
            foreach (Area area in areaList)
            {
                long mc = memberList.Count(x => x.AreaId == area.Id.ToString());
                statisticsInfo.XYValue.Add(area.Name, mc);
            }
            return statisticsInfo;
        }


        public IDictionary<string,IList<DZMembershipCustomerService>> StatisticsVerifiedCustomerServiceByArea(IList<DZMembership> memberList, IList<Area> areaList,IList<string> memberKey)
        {
            IDictionary<string, IList<DZMembershipCustomerService>> dic = new Dictionary<string, IList<DZMembershipCustomerService>>();
            foreach (string strKey in memberKey)
            {
                dic.Add(strKey, new List<DZMembershipCustomerService>());
            }
            foreach (DZMembership member in memberList)
            {
                if (member.GetType() == typeof(DZMembershipCustomerService))
                {
                    DZMembershipCustomerService membershipCustomerService = (DZMembershipCustomerService)member;
                    Area area = areaList.FirstOrDefault(x => x.Id == int.Parse(membershipCustomerService.AreaId));
                    membershipCustomerService.UserCity = area.Name;
                    foreach (string strKey in memberKey)
                    {
                        if (CheckCustomerServiceByArea(membershipCustomerService, strKey))
                        {
                            dic[strKey].Add(membershipCustomerService);
                        }
                    }
                }
            }
            return dic;
        }

        public bool CheckCustomerServiceByArea(DZMembershipCustomerService member, string strKey)
        {
            switch (strKey)
            {
                case "NotVerifiedCustomerService":
                    return !member.IsVerified;
                case "AgreeVerifiedCustomerService":
                    return member.VerificationIsAgree;
                case "RefuseVerifiedCustomerService":
                    return member.IsVerified && !member.VerificationIsAgree;
                case "MyCustomerService":
                    return member.IsAgentCustomerService;
                case "UnLockedCustomerService":
                    return member.IsVerified && member.VerificationIsAgree && !member.IsLocked;
                case "LockedCustomerService":
                    return member.IsVerified && member.VerificationIsAgree && member.IsLocked;
                default:return false;
            }
        }

    }
}
