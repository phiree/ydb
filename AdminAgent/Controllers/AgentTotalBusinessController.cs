using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Domain;
using com = Ydb.Common.Application;
using Ydb.Common;


namespace AdminAgent.Controllers
{
    public class AgentTotalBusinessController : AgentBaseController
    {
        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
        /// <summary>
        /// 商户统计信息
        /// </summary>
        /// <returns></returns>
        public ActionResult total_business()
        {
            try
            {
                ViewData["NewBusinessNumber"] = businessService.GetCountOfNewBusinessesYesterdayByArea(CurrentUser.AreaIdList);
                ViewData["AllBusinessNumber"] = businessService.GetCountOfAllBusinessesByArea(CurrentUser.AreaIdList);
                ViewData["YearBusinessNumber"] = businessService.GetStatisticsRatioYearOnYear(CurrentUser.AreaIdList);
                ViewData["MonthBusinessNumber"] = businessService.GetStatisticsRatioMonthOnMonth(CurrentUser.AreaIdList);
                return View();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 商户分析
        /// </summary>
        /// <returns></returns>
        public ActionResult total_business_detail()
        {
            return View();
        }
        /// <summary>
        /// 新增店铺数量列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult total_business_NewList(string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = businessService.GetStatisticsNewBusinessesCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now);
                }
                else
                {
                    statisticsInfo = businessService.GetStatisticsNewBusinessesCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true));
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
        /// 统计店铺总数
        /// </summary>
        /// <returns></returns>
        public ActionResult total_business_Count()
        {
            try
            {
                Models.TotalCount totalCount = new Models.TotalCount();
                totalCount.count = businessService.GetCountOfAllBusinessesByArea(CurrentUser.AreaIdList); ;
                return Json(totalCount, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 累计店铺数量列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult total_business_AllList( string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now);
                }
                else
                {
                    statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true));
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
        /// 按年限统计店铺数量
        /// </summary>
        /// <returns></returns>
        public ActionResult total_business_LifeList()
        {
            try
            {
                StatisticsInfo statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByLife(CurrentUser.AreaIdList);
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 按员工数量统计店铺数量
        /// </summary>
        /// <returns></returns>
        public ActionResult total_business_StaffList()
        {
            try
            {
                StatisticsInfo statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByStaff(CurrentUser.AreaIdList);
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 按子区域来统计店铺数量
        /// </summary>
        /// <returns></returns>
        public ActionResult total_business_AreaList()
        {
            try
            {
                StatisticsInfo statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByArea(CurrentUser.AreaList);
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