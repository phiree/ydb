using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.DomainModel.DataStatistics
{
    public interface IStatisticsInstantMessage
    {
        /// <summary>
        ///  统计用户累计在线时间
        /// </summary>
        /// <param name="imUserStatusArchieveList"></param>
        /// <returns></returns>
        string StatisticsUserTotalOnlineTime(IList<IMUserStatusArchieve> imUserStatusArchieveList);
    }
}
