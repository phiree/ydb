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
        /// <param name="areaIdList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsNewOrdersCountListByTime(IList<string> areaIdList, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 统计店铺每日或每时累计数量列表
        /// </summary>
        /// <param name="areaIdList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="enumDone"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllOrdersCountListByTime(IList<string> areaIdList, DateTime beginTime, DateTime endTime, enum_IsDone enumDone);

        /// <summary>
        /// 根据订单所在子区域统计订单数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllOrdersCountListByArea(IList<Area> areaList);

        /// <summary>
        /// 根据服务类型统计订单交易额
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="deepLevel"></param>
        /// <returns></returns>
        StatisticsInfo<string, decimal> GetStatisticsAllOrdersAmountListByType(IList<Area> areaList, int deepLevel);

        /// <summary>
        /// 根据服务类型统计订单数量
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="deepLevel"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllOrdersCountListByType(IList<Area> areaList, int deepLevel);

        /// <summary>
        /// 订单金额合计
        /// </summary>
        /// <param name="areaIdList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="enumDone"></param>
        /// <returns></returns>
        decimal GetStatisticsTotalAmountByArea(IList<string> areaIdList, enum_IsDone enumDone);

        /// <summary>
        /// 统计订单列表
        /// </summary>
        /// <param name = "areaIdList" ></ param >
        /// < param name="beginTime"></param>
        /// <param name = "endTime" ></ param >
        /// < param name="enumDone"></param>
        /// <returns></returns>
        IList<ServiceOrderDto> GetOrdersListByAreaAndTime(IList<string> areaIdList, DateTime beginTime, DateTime endTime, enum_IsDone enumDone);
    }
}
