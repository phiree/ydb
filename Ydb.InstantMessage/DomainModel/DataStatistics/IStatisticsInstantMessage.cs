using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Common.Domain;

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

        /// <summary>
        /// 获取聊天记录时间线
        /// </summary>
        /// <param name="orderStatusList"></param>
        /// <returns></returns>
        IList<StatisticsInfo<ReceptionChatDto>> GetChatTimeLine(IList<ReceptionChatDto> receptionChatList);

    }
}
