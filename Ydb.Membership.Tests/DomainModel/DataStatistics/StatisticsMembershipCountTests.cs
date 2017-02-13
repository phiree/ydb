using NUnit.Framework;
using Ydb.Membership.DomainModel.DataStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Membership.DomainModel.DataStatisticsTests
{
    [TestFixture()]
    public class StatisticsMembershipCountTests
    {
        [Test()]
        public void StatisticsMembershipCount_StatisticsLoginCountLastMonth_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf")}
            };
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog> {
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,""),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,""),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,""),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"")
            };
            long l = statisticsMembershipCount.StatisticsLoginCountLastMonth(memberList, loginList);
            Assert.AreEqual(2, l);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsNewMembershipsCountListByTime_ByDay_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),TimeCreated=DateTime.Parse("2017-01-02 10:10")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),TimeCreated=DateTime.Parse("2017-01-02 14:10")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-04 14:10")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),TimeCreated=DateTime.Parse("2017-01-04 15:14")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-04 07:01")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-06 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-02-01");
            bool IsHour = false;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsNewMembershipsCountListByTime(memberList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("新增用户", statisticsInfo.YName);
            Assert.AreEqual("日", statisticsInfo.XName);
            Assert.AreEqual(31, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-01")]);
            Assert.AreEqual(2, statisticsInfo.XYValue[DateTime.Parse("2017-01-02")]);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-03")]);
            Assert.AreEqual(3, statisticsInfo.XYValue[DateTime.Parse("2017-01-04")]);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-05")]);
            Assert.AreEqual(1, statisticsInfo.XYValue[DateTime.Parse("2017-01-06")]);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-07")]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsNewMembershipsCountListByTime_ByHour_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),TimeCreated=DateTime.Parse("2017-01-01 14:11")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),TimeCreated=DateTime.Parse("2017-01-01 14:12")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 14:13")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),TimeCreated=DateTime.Parse("2017-01-01 07:14")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 07:01")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-01-02");
            bool IsHour = true;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsNewMembershipsCountListByTime(memberList, beginTime, endTime, true);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("新增用户", statisticsInfo.YName);
            Assert.AreEqual("时", statisticsInfo.XName);
            Assert.AreEqual(24, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 00:00")]);
            Assert.AreEqual(2, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 07:00")]);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 09:00")]);
            Assert.AreEqual(3, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 14:00")]);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 18:00")]);
            Assert.AreEqual(1, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 20:00")]);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 21:00")]);
        }


        [Test()]
        public void StatisticsMembershipCount_StatisticsAllMembershipsCountListByTime_ByDay_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),TimeCreated=DateTime.Parse("2017-01-02 10:10")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),TimeCreated=DateTime.Parse("2017-01-02 14:10")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-04 14:10")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),TimeCreated=DateTime.Parse("2017-01-04 15:14")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-04 07:01")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-06 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-02-01");
            bool IsHour = false;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByTime(memberList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("累计用户", statisticsInfo.YName);
            Assert.AreEqual("日", statisticsInfo.XName);
            Assert.AreEqual(31, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-01")]);
            Assert.AreEqual(2, statisticsInfo.XYValue[DateTime.Parse("2017-01-02")]);
            Assert.AreEqual(2, statisticsInfo.XYValue[DateTime.Parse("2017-01-03")]);
            Assert.AreEqual(5, statisticsInfo.XYValue[DateTime.Parse("2017-01-04")]);
            Assert.AreEqual(5, statisticsInfo.XYValue[DateTime.Parse("2017-01-05")]);
            Assert.AreEqual(6, statisticsInfo.XYValue[DateTime.Parse("2017-01-06")]);
            Assert.AreEqual(6, statisticsInfo.XYValue[DateTime.Parse("2017-01-07")]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsAllMembershipsCountListByTime_ByHour_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),TimeCreated=DateTime.Parse("2017-01-01 14:11")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),TimeCreated=DateTime.Parse("2017-01-01 14:12")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 14:13")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),TimeCreated=DateTime.Parse("2017-01-01 07:14")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 07:01")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-01-02");
            bool IsHour = true;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByTime(memberList, beginTime, endTime, true);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("累计用户", statisticsInfo.YName);
            Assert.AreEqual("时", statisticsInfo.XName);
            Assert.AreEqual(24, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 00:00")]);
            Assert.AreEqual(2, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 07:00")]);
            Assert.AreEqual(2, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 09:00")]);
            Assert.AreEqual(5, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 14:00")]);
            Assert.AreEqual(5, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 18:00")]);
            Assert.AreEqual(6, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 20:00")]);
            Assert.AreEqual(6, statisticsInfo.XYValue[DateTime.Parse("2017-01-01 21:00")]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsLoginCountListByTime_ByDay_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf")},
                new DZMembership { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23")},
                new DZMembership { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25")}
            };
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog> {
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,""),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,""),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,""),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"")
            };
            DateTime dateBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime beginTime = dateBase.AddDays(-2);
            DateTime endTime = dateBase.AddDays(1);
            bool IsHour = false;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsLoginCountListByTime(memberList,loginList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户活跃度", statisticsInfo.YName);
            Assert.AreEqual("日", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddDays(-2)]);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddDays(-1)]);
            Assert.AreEqual(2, statisticsInfo.XYValue[dateBase]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsLoginCountListByTime_ByHour_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf")},
                new DZMembership { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23")},
                new DZMembership { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25")}
            };
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog> {
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,""),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,""),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,""),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"")
            };
            DateTime dateBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH")+":00");
            DateTime beginTime = dateBase.AddHours(-2);
            DateTime endTime = dateBase.AddHours(1);
            bool IsHour = true;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsLoginCountListByTime(memberList, loginList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户活跃度", statisticsInfo.YName);
            Assert.AreEqual("时", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddHours(-2)]);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddHours(-1)]);
            Assert.AreEqual(2, statisticsInfo.XYValue[dateBase]);
        }
    }
}