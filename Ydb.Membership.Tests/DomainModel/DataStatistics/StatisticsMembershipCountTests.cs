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
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer)
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
            Assert.AreEqual(0, statisticsInfo.XYValue["20170101"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["20170102"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["20170103"]);
            Assert.AreEqual(3, statisticsInfo.XYValue["20170104"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["20170105"]);
            Assert.AreEqual(1, statisticsInfo.XYValue["20170106"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["20170107"]);
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
            Assert.AreEqual(0, statisticsInfo.XYValue["0"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["7"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["9"]);
            Assert.AreEqual(3, statisticsInfo.XYValue["14"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["18"]);
            Assert.AreEqual(1, statisticsInfo.XYValue["20"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["21"]);
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
            Assert.AreEqual(0, statisticsInfo.XYValue["20170101"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["20170102"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["20170103"]);
            Assert.AreEqual(5, statisticsInfo.XYValue["20170104"]);
            Assert.AreEqual(5, statisticsInfo.XYValue["20170105"]);
            Assert.AreEqual(6, statisticsInfo.XYValue["20170106"]);
            Assert.AreEqual(6, statisticsInfo.XYValue["20170107"]);
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
            Assert.AreEqual(0, statisticsInfo.XYValue["0"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["7"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["9"]);
            Assert.AreEqual(5, statisticsInfo.XYValue["14"]);
            Assert.AreEqual(5, statisticsInfo.XYValue["18"]);
            Assert.AreEqual(6, statisticsInfo.XYValue["20"]);
            Assert.AreEqual(6, statisticsInfo.XYValue["21"]);
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
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer)
            };
            DateTime dateBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime beginTime = dateBase.AddDays(-2);
            DateTime endTime = dateBase.AddDays(1);
            bool IsHour = false;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsLoginCountListByTime(memberList, loginList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户活跃度", statisticsInfo.YName);
            Assert.AreEqual("日", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddDays(-2).ToString("yyyyMMdd")]);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddDays(-1).ToString("yyyyMMdd")]);
            Assert.AreEqual(2, statisticsInfo.XYValue[dateBase.ToString("yyyyMMdd")]);
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
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer)
            };
            DateTime dateBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH") + ":00");
            DateTime beginTime = dateBase.AddHours(-2);
            DateTime endTime = dateBase.AddHours(1);
            bool IsHour = true;
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsLoginCountListByTime(memberList, loginList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户活跃度", statisticsInfo.YName);
            Assert.AreEqual("时", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddHours(-2).Hour.ToString()]);
            Assert.AreEqual(0, statisticsInfo.XYValue[dateBase.AddHours(-1).Hour.ToString()]);
            Assert.AreEqual(2, statisticsInfo.XYValue[dateBase.Hour.ToString()]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsAllMembershipsCountListBySex_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),Sex=true},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),Sex=false},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),Sex=true},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),Sex=true},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),Sex=false},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 20:20")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),TimeCreated=DateTime.Parse("2017-01-01 20:20")}
            };
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListBySex(memberList);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户数量", statisticsInfo.YName);
            Assert.AreEqual("性别", statisticsInfo.XName);
            Assert.AreEqual(2, statisticsInfo.XYValue.Count);
            Assert.AreEqual(3, statisticsInfo.XYValue["女"]);
            Assert.AreEqual(4, statisticsInfo.XYValue["男"]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsAllMembershipsCountListByAppName_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9")},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26")},
                new DZMembership { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf")},
                new DZMembership { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb")},
                new DZMembership { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23")},
                new DZMembership { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25")}
            };
            IList<MembershipLoginLog> loginList = new List<MembershipLoginLog> {
                new MembershipLoginLog ("215351d4-ba0a-41b2-bc5e-a6b400c11c25",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer),
                new MembershipLoginLog ("00c02067-4900-41b6-b3d4-a68100c47cb9",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer),
                new MembershipLoginLog ("002c0c77-c2a0-4dba-930c-a6b000f80ceb",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("03b647cb-a449-4f93-abf3-a67c0098ecdf",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer),
                new MembershipLoginLog ("015351d4-ba0a-41b2-bc5e-a6b400c11c26",enumLoginLogType.Login,"",Common.enum_appName.Android_Customer),
                new MembershipLoginLog ("115351d4-ba0a-41b2-bc5e-a6b400c11c23",enumLoginLogType.Login,"",Common.enum_appName.IOS_Customer)
            };
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByAppName(memberList, loginList);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户数量", statisticsInfo.YName);
            Assert.AreEqual("手机系统", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(2, statisticsInfo.XYValue["Android_Customer"]);
            Assert.AreEqual(4, statisticsInfo.XYValue["IOS_Customer"]);
            Assert.AreEqual(3, statisticsInfo.XYValue["other"]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsAllMembershipsCountGroupByArea_Test()
        {
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="1"},
                new DZMembership { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="1"},
                new DZMembership { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),AreaId="2"},
                new DZMembership { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),AreaId="1"},
                new DZMembership { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="2"},
                new DZMembership { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),AreaId="1"},
                new DZMembership { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="3"},
                new DZMembership { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23"),AreaId="1"},
                new DZMembership { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25"),AreaId="1"}
            };
            IList<Area> areaList = new List<Area> {
                new Area {Id=1,Name="北京市" },
                new Area {Id=2,Name="北京市市辖区" },
                new Area {Id=3,Name="北京市东城区" },
                new Area {Id=4,Name="北京市西城区" },
                new Area {Id=5,Name="北京市崇文区" },
                new Area {Id=6,Name="北京市宣武区" },
                new Area {Id=7,Name="北京市朝阳区" },
                new Area {Id=8,Name="北京市丰台区" },
                new Area {Id=9,Name="北京市石景山区" }
            };
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountGroupByArea(memberList, areaList);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("用户数量", statisticsInfo.YName);
            Assert.AreEqual("区域", statisticsInfo.XName);
            Assert.AreEqual(9, statisticsInfo.XYValue.Count);
            Assert.AreEqual(6, statisticsInfo.XYValue["北京市"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["北京市市辖区"]);
            Assert.AreEqual(1, statisticsInfo.XYValue["北京市东城区"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["北京市西城区"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["北京市崇文区"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["北京市宣武区"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["北京市朝阳区"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["北京市丰台区"]);
            Assert.AreEqual(0, statisticsInfo.XYValue["北京市石景山区"]);
        }

        [Test()]
        public void StatisticsMembershipCount_StatisticsVerifiedCustomerServiceByArea_Test()
        {
            IList<string> memberKey = new List<string> { "NotVerifiedCustomerService", "AgreeVerifiedCustomerService", "RefuseVerifiedCustomerService", "MyCustomerService" };
            StatisticsMembershipCount statisticsMembershipCount = new StatisticsMembershipCount();
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="1"},
                new DZMembershipCustomerService { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="1"},
                new DZMembershipCustomerService { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),AreaId="2",IsVerified=true,IsAgentCustomerService=true,VerificationIsAgree=true},
                new DZMembershipCustomerService { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),AreaId="1",IsVerified=true,VerificationIsAgree=true},
                new DZMembershipCustomerService { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="2"},
                new DZMembershipCustomerService { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),AreaId="1",IsVerified=true,VerificationIsAgree=true},
                new DZMembershipCustomerService { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaId="3",IsVerified=true,VerificationIsAgree=false},
                new DZMembershipCustomerService { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23"),AreaId="1",IsVerified=true,VerificationIsAgree=true},
                new DZMembership { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25"),AreaId="1"},
                new DZMembershipCustomerService { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80bce"),AreaId="1"},
                new DZMembershipCustomerService { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c479cb"),AreaId="2",IsVerified=true,IsAgentCustomerService=true,VerificationIsAgree=true},
                new DZMembershipCustomerService { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c116c2"),AreaId="1",IsVerified=true,IsAgentCustomerService=true,VerificationIsAgree=true},
                new DZMembershipCustomerService { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80bce"),AreaId="2",IsVerified=true,VerificationIsAgree=false},
                new DZMembershipCustomerService { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098efcd"),AreaId="1"},
                new DZMembershipCustomerService { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80bce"),AreaId="3",IsVerified=true,VerificationIsAgree=false},
                new DZMembershipCustomerService { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c113c2"),AreaId="1",IsVerified=true,IsAgentCustomerService=true,VerificationIsAgree=true},
            };
            IList<Area> areaList = new List<Area> {
                new Area {Id=1,Name="北京市" },
                new Area {Id=2,Name="北京市市辖区" },
                new Area {Id=3,Name="北京市东城区" },
                new Area {Id=4,Name="北京市西城区" },
                new Area {Id=5,Name="北京市崇文区" },
                new Area {Id=6,Name="北京市宣武区" },
                new Area {Id=7,Name="北京市朝阳区" },
                new Area {Id=8,Name="北京市丰台区" },
                new Area {Id=9,Name="北京市石景山区" }
            };
            IDictionary<string, IList<DZMembershipCustomerService>> dic = statisticsMembershipCount.StatisticsVerifiedCustomerServiceByArea(memberList, areaList,memberKey);
            Assert.AreEqual(4, dic.Count);
            Assert.AreEqual(4, dic["NotVerifiedCustomerService"].Count);
            Assert.AreEqual(7, dic["AgreeVerifiedCustomerService"].Count);
            Assert.AreEqual(3, dic["RefuseVerifiedCustomerService"].Count);
            Assert.AreEqual(4, dic["MyCustomerService"].Count);
        }
    }
}