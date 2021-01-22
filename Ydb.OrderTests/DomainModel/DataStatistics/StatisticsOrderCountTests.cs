using NUnit.Framework;
using Ydb.Order.DomainModel.DataStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Order.DomainModel.DataStatisticsTests
{
    [TestFixture()]
    public class StatisticsOrderCountTests
    {
        [Test()]
        public void StatisticsOrderCount_GetOrderStateTimeLine_Test()
        {
            ServiceOrder order = new ServiceOrder();
            IList<ServiceOrderStateChangeHis> orderStatusList = new List<ServiceOrderStateChangeHis>
            {
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-02 17:37:38")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-09 16:46:32")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-06 09:49:14")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-01 15:19:14")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-02 14:13:35")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-02 14:52:03")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-09 16:05:28")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-06 09:58:52")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-10 10:50:37")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-10 15:47:27")},
                new ServiceOrderStateChangeHis(order,Common.enum_OrderStatus.Created,1){CreatTime=DateTime.Parse("2017-03-09 16:29:54")}
            };

            StatisticsOrderCount statisticsCount = new StatisticsOrderCount();
            IList<StatisticsInfo<ServiceOrderStateChangeHis>> StatusTimeLineList = statisticsCount.GetOrderStateTimeLine(orderStatusList);
            Assert.AreEqual(5, StatusTimeLineList.Count);
            Assert.AreEqual(2, StatusTimeLineList[0].List.Count);
            Assert.AreEqual(3, StatusTimeLineList[1].List.Count);
            Assert.AreEqual(2, StatusTimeLineList[2].List.Count);
            Assert.AreEqual(3, StatusTimeLineList[3].List.Count);
            Assert.AreEqual(1, StatusTimeLineList[4].List.Count);
        }
    }
}