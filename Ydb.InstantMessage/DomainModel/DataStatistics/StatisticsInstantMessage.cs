using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Common;

namespace Ydb.InstantMessage.DomainModel.DataStatistics
{
    public class StatisticsInstantMessage: IStatisticsInstantMessage
    {

        /// <summary>
        /// 统计用户累计在线时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string StatisticsUserTotalOnlineTime(IList<IMUserStatusArchieve> imUserStatusArchieveList)
        {
            long lDateDiff = 0;
            IList<DateTime> OnLineTimeList = new List<DateTime>();
            foreach (IMUserStatusArchieve imUserStatusArchieve in imUserStatusArchieveList)
            {
                if (OnLineTimeList.Count == 0 && imUserStatusArchieve.Status == enum_UserStatus.unavailable)
                {
                    continue;
                }
                if (imUserStatusArchieve.Status == enum_UserStatus.available)
                {
                    OnLineTimeList.Add(imUserStatusArchieve.ArchieveTime);
                }
                if (imUserStatusArchieve.Status == enum_UserStatus.unavailable)
                {
                    lDateDiff = lDateDiff + MathHelper.DateDiffTicks(imUserStatusArchieve.ArchieveTime, OnLineTimeList[0]);
                    OnLineTimeList.Remove(OnLineTimeList[0]);
                }
            }
            return MathHelper.ChangeDateDiff(lDateDiff);
        }
    }
}
