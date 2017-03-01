using NUnit.Framework;
using Ydb.InstantMessage.DomainModel.DataStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.DomainModel.DataStatisticsTests
{
    [TestFixture()]
    public class StatisticsInstantMessageTests
    {
        [Test()]
        public void StatisticsInstantMessage_StatisticsUserTotalOnlineTime_Test()
        {
            IList<IMUserStatusArchieve> imUserStatusArchieveList = new List<IMUserStatusArchieve>()
            {
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:15:30"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:16:32"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:16:32"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:18:01"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:18:57"), Status = Common.enum_UserStatus.unavailable },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:18:57"), Status = Common.enum_UserStatus.unavailable },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:20:19"), Status = Common.enum_UserStatus.unavailable },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:20:53"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:21:32"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:21:52"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:24:21"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:26:38"), Status = Common.enum_UserStatus.available },
               new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:28:04"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:33:36"), Status = Common.enum_UserStatus.available },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.available },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:36:36"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:48:19"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 10:51:40"), Status = Common.enum_UserStatus.available },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 11:25:37"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-14 11:28:48"), Status = Common.enum_UserStatus.available },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-15 11:04:20"), Status = Common.enum_UserStatus.unavailable },
               //new IMUserStatusArchieve { ArchieveTime = Convert.ToDateTime("2016-9-15 11:07:46"), Status = Common.enum_UserStatus.available }
            };
            string str = new StatisticsInstantMessage().StatisticsUserTotalOnlineTime(imUserStatusArchieveList);
            Assert.AreEqual("19分钟42秒",str);
        }
    }
}