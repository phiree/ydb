using NUnit.Framework;
using Ydb.BusinessResource.DomainModel.Service.DataStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.BusinessResource.DomainModel.Service.DataStatisticsTests
{
    [TestFixture()]
    public class StatisticsBusinessCountTests
    {
        [Test()]
        public void StatisticsBusinessCount_StatisticsNewBusinessesCountListByTime_ByDay_Test()
        {
            StatisticsBusinessCount statisticsBusinessCount = new StatisticsBusinessCount();
            IList<Business> businessList = new List<Business> {
                new Business { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),CreatedTime=DateTime.Parse("2017-01-02 10:10")},
                new Business { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),CreatedTime=DateTime.Parse("2017-01-02 14:10")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-04 14:10")},
                new Business { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),CreatedTime=DateTime.Parse("2017-01-04 15:14")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-04 07:01")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-06 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-02-01");
            bool IsHour = false;
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsNewBusinessesCountListByTime(businessList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("新增店铺", statisticsInfo.YName);
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
        public void StatisticsBusinessCount_StatisticsNewBusinessesCountListByTime_ByHour_Test()
        {
            StatisticsBusinessCount statisticsBusinessCount = new StatisticsBusinessCount();
            IList<Business> businessList = new List<Business> {
                new Business { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),CreatedTime=DateTime.Parse("2017-01-01 14:11")},
                new Business { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),CreatedTime=DateTime.Parse("2017-01-01 14:12")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-01 14:13")},
                new Business { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),CreatedTime=DateTime.Parse("2017-01-01 07:14")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-01 07:01")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-01 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-01-02");
            bool IsHour = true;
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsNewBusinessesCountListByTime(businessList, beginTime, endTime, true);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("新增店铺", statisticsInfo.YName);
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
        public void StatisticsBusinessCount_StatisticsAllBusinessesCountListByTime_ByDay_Test()
        {
            StatisticsBusinessCount statisticsBusinessCount = new StatisticsBusinessCount();
            IList<Business> businessList = new List<Business> {
                new Business { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),CreatedTime=DateTime.Parse("2017-01-02 10:10")},
                new Business { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),CreatedTime=DateTime.Parse("2017-01-02 14:10")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-04 14:10")},
                new Business { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),CreatedTime=DateTime.Parse("2017-01-04 15:14")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-04 07:01")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-06 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-02-01");
            bool IsHour = false;
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountListByTime(businessList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("累计店铺", statisticsInfo.YName);
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
        public void StatisticsBusinessCount_StatisticsAllBusinessesCountListByTime_ByHour_Test()
        {
            StatisticsBusinessCount statisticsBusinessCount = new StatisticsBusinessCount();
            IList<Business> businessList = new List<Business> {
                new Business { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),CreatedTime=DateTime.Parse("2017-01-01 14:11")},
                new Business { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),CreatedTime=DateTime.Parse("2017-01-01 14:12")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-01 14:13")},
                new Business { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),CreatedTime=DateTime.Parse("2017-01-01 07:14")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-01 07:01")},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),CreatedTime=DateTime.Parse("2017-01-01 20:20")}
            };
            DateTime beginTime = DateTime.Parse("2017-01-01");
            DateTime endTime = DateTime.Parse("2017-01-02");
            bool IsHour = true;
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountListByTime(businessList, beginTime, endTime, IsHour);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("累计店铺", statisticsInfo.YName);
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
        public void StatisticsBusinessCount_StatisticsAllBusinessesCountListByLife_Test()
        {
            StatisticsBusinessCount statisticsBusinessCount = new StatisticsBusinessCount();
            IList<Business> businessList = new List<Business> {
                new Business { WorkingYears=1},
                new Business { WorkingYears=2},
                new Business { WorkingYears=2},
                new Business { WorkingYears=1},
                new Business { WorkingYears=3},
                new Business { WorkingYears=1},
                new Business { WorkingYears=1}
            };
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountListByLife(businessList);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("店铺数量", statisticsInfo.YName);
            Assert.AreEqual("店铺年限", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(4, statisticsInfo.XYValue["1"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["2"]);
            Assert.AreEqual(1, statisticsInfo.XYValue["3"]);
        }

        [Test()]
        public void StatisticsBusinessCount_StatisticsAllBusinessesCountGroupByStaff_Test()
        {
            StatisticsBusinessCount statisticsBusinessCount = new StatisticsBusinessCount();
            IList<Business> businessList = new List<Business> {
                new Business { StaffAmount=1},
                new Business { StaffAmount=2},
                new Business { StaffAmount=2},
                new Business { StaffAmount=1},
                new Business { StaffAmount=3},
                new Business { StaffAmount=1},
                new Business { StaffAmount=1}
            };
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountListByStaff(businessList);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("店铺数量", statisticsInfo.YName);
            Assert.AreEqual("员工总数", statisticsInfo.XName);
            Assert.AreEqual(3, statisticsInfo.XYValue.Count);
            Assert.AreEqual(4, statisticsInfo.XYValue["1"]);
            Assert.AreEqual(2, statisticsInfo.XYValue["2"]);
            Assert.AreEqual(1, statisticsInfo.XYValue["3"]);
        }

        [Test()]
        public void StatisticsBusinessCount_StatisticsAllBusinessesCountGroupByArea_Test()
        {
            StatisticsBusinessCount statisticsBusinessCount = new StatisticsBusinessCount();
            IList<Business> businessList = new List<Business> {
                new Business { Id=new Guid("003c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaBelongTo="1"},
                new Business { Id=new Guid("002c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaBelongTo="1"},
                new Business { Id=new Guid("00c02067-4900-41b6-b3d4-a68100c47cb9"),AreaBelongTo="2"},
                new Business { Id=new Guid("015351d4-ba0a-41b2-bc5e-a6b400c11c26"),AreaBelongTo="1"},
                new Business { Id=new Guid("005c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaBelongTo="2"},
                new Business { Id=new Guid("03b647cb-a449-4f93-abf3-a67c0098ecdf"),AreaBelongTo="1"},
                new Business { Id=new Guid("004c0c77-c2a0-4dba-930c-a6b000f80ceb"),AreaBelongTo="3"},
                new Business { Id=new Guid("115351d4-ba0a-41b2-bc5e-a6b400c11c23"),AreaBelongTo="1"},
                new Business { Id=new Guid("215351d4-ba0a-41b2-bc5e-a6b400c11c25"),AreaBelongTo="1"}
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
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountGroupByArea(businessList, areaList);
            Assert.IsNotNull(statisticsInfo);
            Assert.AreEqual("店铺数量", statisticsInfo.YName);
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
    }
}