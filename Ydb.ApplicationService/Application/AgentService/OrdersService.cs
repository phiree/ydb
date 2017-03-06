using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.ApplicationService.ModelDto;
using Ydb.Common.Domain;
using Ydb.Order.DomainModel;
using Ydb.ApplicationService.Application.AgentService.DataStatistics;

namespace Ydb.ApplicationService.Application.AgentService
{
    public class OrdersService: IOrdersService
    {
        IBusinessService businessService;
        IServiceOrderService serviceOrderService;
        IStatisticsCount statisticsCount;
        public OrdersService(IBusinessService businessService,
            IServiceOrderService serviceOrderService, IStatisticsCount statisticsCount)
        {
            this.businessService = businessService;
            this.serviceOrderService = serviceOrderService;
            this.statisticsCount = statisticsCount;
        }
        public long GetOrdersCountByArea(IList<string> areaIdList,bool isSharea)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList= businessList.Select(x => x.Id.ToString()).ToList();
            return serviceOrderService.GetOrdersCountByBusinessList(businessIdList, isSharea);
        }

        public long GetCountOfNewOrdersYesterdayByArea(IList<string> areaIdList)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            DateTime beginTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            return serviceOrderService.GetOrdersCountByBusinessList(businessIdList, beginTime, beginTime.AddDays(1),"");
        }

        public long GetCountOfAllOrdersByArea(IList<string> areaIdList,enum_IsDone enumDone)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            return serviceOrderService.GetOrdersCountByBusinessList(businessIdList, DateTime.MinValue, DateTime.MinValue, enumDone.ToString());
        }

        /// <summary>
        /// 计算上个月同比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        public string GetStatisticsOrderRatioYearOnYear(IList<string> areaIdList)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            long orderCountLastMonth = serviceOrderService.GetOrdersCountByBusinessList(businessIdList, dtBase.AddMonths(-1), dtBase,"");
            long orderCountBeforeLastMonth = serviceOrderService.GetOrdersCountByBusinessList(businessIdList, dtBase.AddMonths(-1).AddYears(-1), dtBase.AddYears(-1),"");
            return Ydb.Common.MathHelper.GetCalculatedRatio(orderCountLastMonth, orderCountBeforeLastMonth);
        }

        /// <summary>
        /// 计算上个月环比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        public string GetStatisticsOrderRatioMonthOnMonth(IList<string> areaIdList)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            long orderCountLastMonth = serviceOrderService.GetOrdersCountByBusinessList(businessIdList, dtBase.AddMonths(-1), dtBase, "");
            long orderCountBeforeLastMonth = serviceOrderService.GetOrdersCountByBusinessList(businessIdList, dtBase.AddMonths(-2), dtBase.AddMonths(-1), "");
            return Ydb.Common.MathHelper.GetCalculatedRatio(orderCountLastMonth, orderCountBeforeLastMonth);
        }

        /// <summary>
        /// 统计订单每日或每时新增数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <returns></returns>
        public StatisticsInfo GetStatisticsNewOrdersCountListByTime(IList<string> areaIdList, string strBeginTime, string strEndTime)
        {
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            IList<ServiceOrder> orderList = serviceOrderService.GetOrdersByBusinessList(businessIdList, BeginTime, EndTime,"");
            StatisticsInfo statisticsInfo = statisticsCount.StatisticsNewOrdersCountListByTime(orderList, BeginTime, EndTime, strBeginTime == strEndTime);
            return statisticsInfo;
        }
        /// <summary>
        /// 统计订单每日或每时累计数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <returns></returns>
        public StatisticsInfo GetStatisticsAllOrdersCountListByTime(IList<string> areaIdList, string strBeginTime, string strEndTime,enum_IsDone enumDone)
        {
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            IList<ServiceOrder> orderList = serviceOrderService.GetOrdersByBusinessList(businessIdList, DateTime.MinValue, DateTime.MinValue, enumDone.ToString());
            StatisticsInfo statisticsInfo = statisticsCount.StatisticsAllOrdersCountListByTime(orderList, BeginTime, EndTime, strBeginTime == strEndTime);
            return statisticsInfo;
        }

        /// <summary>
        /// 根据订单所在子区域统计订单数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        public StatisticsInfo GetStatisticsAllOrdersCountListByArea(IList<Area> areaList)
        {
            IList<string> AreaIdList = areaList.Select(x => x.Id.ToString()).ToList();
            IList<Business> businessList = businessService.GetAllBusinessesByArea(AreaIdList);
            IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
            IList<ServiceOrder> orderList = serviceOrderService.GetOrdersByBusinessList(businessIdList, DateTime.MinValue, DateTime.MinValue,"");
            StatisticsInfo statisticsInfo = statisticsCount.StatisticsAllOrdersCountGroupByArea(orderList,businessList, areaList);
            return statisticsInfo;
        }
    }
}
