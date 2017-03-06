using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.ApplicationService.ModelDto;
using Ydb.Common.Domain;
namespace Ydb.ApplicationService.Application.AgentService
{
   public interface IOrdersService
    {
        long GetOrdersCountByArea(IList<string> areaIdList, bool isSharea);
        long GetCountOfNewOrdersYesterdayByArea(IList<string> areaIdList);
        long GetCountOfAllOrdersByArea(IList<string> areaIdList, enum_IsDone enumDone);
        /// <summary>
        /// 计算上个月同比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        string GetStatisticsOrderRatioYearOnYear(IList<string> areaIdList);
        /// <summary>
        /// 计算上个月环比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        string GetStatisticsOrderRatioMonthOnMonth(IList<string> areaIdList);

        /// <summary>
        /// 统计订单每日或每时新增数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <returns></returns>
         StatisticsInfo GetStatisticsNewOrdersCountListByTime(IList<string> areaIdList, string strBeginTime, string strEndTime);

        /// <summary>
        /// 统计店铺每日或每时累计数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllOrdersCountListByTime(IList<string> areaIdList, string strBeginTime, string strEndTime, enum_IsDone enumDone);

        StatisticsInfo GetStatisticsAllOrdersCountListByArea(IList<Area> areaList);
    }
}
