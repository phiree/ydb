using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.ApplicationService.ModelDto;
using Ydb.Common.Domain;
using Ydb.Common;
using com = Ydb.Common.Application;


namespace AdminAgent.Controllers
{
    public class AgentTotalOrderController : AgentBaseController
    {

        IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
        /// <summary>
        /// 订单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult total_order()
        {
            try
            {
                //接口
                ViewData["NewOrderNumber"] = ordersService.GetCountOfNewOrdersYesterdayByArea(CurrentUser.AreaIdList);
                ViewData["AllOrderNumber"] = ordersService.GetCountOfAllOrdersByArea(CurrentUser.AreaIdList, enum_IsDone.None);
                ViewData["AllDoneOrderNumber"] = ordersService.GetCountOfAllOrdersByArea(CurrentUser.AreaIdList, enum_IsDone.OrderIsDone);
                ViewData["AllNotDoneOrderNumber"] = ordersService.GetCountOfAllOrdersByArea(CurrentUser.AreaIdList, enum_IsDone.OrderNotDone);
                ViewData["YearOrderNumber"] = ordersService.GetStatisticsOrderRatioYearOnYear(CurrentUser.AreaIdList);
                ViewData["MonthOrderNumber"] = ordersService.GetStatisticsOrderRatioMonthOnMonth(CurrentUser.AreaIdList);

                //模拟数据
                //ViewData["NewOrderNumber"] = MockData.NewOrderNumber;
                //ViewData["AllOrderNumber"] = MockData.AllOrderNumber;
                //ViewData["AllDoneOrderNumber"] = MockData.AllDoneOrderNumber;
                //ViewData["AllNotDoneOrderNumber"] = MockData.AllNotDoneOrderNumber;
                //ViewData["YearOrderNumber"] = MockData.YearOrderNumber;
                //ViewData["MonthOrderNumber"] = MockData.MonthOrderNumber;
                return View();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 新增订单数量列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult total_order_NewList(string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = ordersService.GetStatisticsNewOrdersCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now);
                }
                else
                {
                    statisticsInfo = ordersService.GetStatisticsNewOrdersCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true));
                }
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 累计订单数量列表
        /// </summary>
        /// <param name="usertype"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult total_order_AllList(string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = ordersService.GetStatisticsAllOrdersCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now,enum_IsDone.None);
                }
                else
                {
                    statisticsInfo = ordersService.GetStatisticsAllOrdersCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), enum_IsDone.None);
                }
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 累计已完成订单数量列表
        /// </summary>
        /// <param name="usertype"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult total_order_IsDoneList(string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = ordersService.GetStatisticsAllOrdersCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now, enum_IsDone.OrderIsDone);
                }
                else
                {
                    statisticsInfo = ordersService.GetStatisticsAllOrdersCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), enum_IsDone.OrderIsDone);
                }
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 订单属性
        /// </summary>
        /// <returns></returns>
        public ActionResult total_order_detail()
        {
            return View();
        }

        /// <summary>
        /// 按子区域统计订单
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_order_AreaList()
        {
            try
            {
                StatisticsInfo statisticsInfo = ordersService.GetStatisticsAllOrdersCountListByArea(CurrentUser.AreaList);
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 根据服务类型统计订单交易额
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_order_AmountList()
        {
            try
            {
                StatisticsInfo<string,decimal> statisticsInfo = ordersService.GetStatisticsAllOrdersAmountListByType(CurrentUser.AreaList, 0);
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 根据服务类型统计订单数量
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_order_TypeList()
        {
            try
            {
                StatisticsInfo statisticsInfo = ordersService.GetStatisticsAllOrdersCountListByType(CurrentUser.AreaList, 0);
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

    }
}