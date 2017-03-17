using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Common;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Common.Domain;

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

        /// <summary>
        /// 获取聊天记录时间线
        /// </summary>
        /// <param name="orderStatusList"></param>
        /// <returns></returns>
        public IList<StatisticsInfo<ReceptionChatDto>> GetChatTimeLine(IList<ReceptionChatDto> receptionChatList)
        {
            IList<StatisticsInfo<ReceptionChatDto>> ChatTimeLineList = new List<StatisticsInfo<ReceptionChatDto>>();
            receptionChatList = receptionChatList.OrderByDescending(x => x.SavedTime).ToList();
            DateTime date = DateTime.MinValue;
            foreach (ReceptionChatDto chat in receptionChatList)
            {
                DateTime dt = DateTime.Parse(chat.SavedTime.ToString("yyyy-MM-dd"));
                if (date != dt)
                {
                    date = dt;
                    StatisticsInfo<ReceptionChatDto> ChatTimeLine = new StatisticsInfo<ReceptionChatDto> { Date = date, List = new List<ReceptionChatDto>() };
                    ChatTimeLineList.Add(ChatTimeLine);
                }
                ChatTimeLineList[ChatTimeLineList.Count - 1].List.Add(chat);
            }
            return ChatTimeLineList;
        }
    }
}
