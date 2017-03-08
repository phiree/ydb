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
        IList<string> areaList = new List<string> { "2445", "2446", "2447", "2448", "2449", "2450" };
        /// <summary>
        /// 商户统计信息
        /// </summary>
        /// <returns></returns>
        public ActionResult total_business()
        {
            try
            {
                ViewData["NewBusinessNumber"] = businessService.GetCountOfNewBusinessesYesterdayByArea(areaList);
                ViewData["AllBusinessNumber"] = businessService.GetCountOfAllBusinessesByArea(areaList);
                ViewData["YearBusinessNumber"] = businessService.GetStatisticsRatioYearOnYear(areaList);
                ViewData["MonthBusinessNumber"] = businessService.GetStatisticsRatioMonthOnMonth(areaList);
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
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_business_NewList(string usertype,string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = businessService.GetStatisticsNewBusinessesCountListByTime(areaList, DateTime.Now.AddMonths(-1), DateTime.Now);
                }
                else
                {
                    statisticsInfo = businessService.GetStatisticsNewBusinessesCountListByTime(areaList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true));
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
        /// 累计店铺数量列表
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_business_AllList(string usertype, string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByTime(areaList, DateTime.Now.AddMonths(-1), DateTime.Now);
                }
                else
                {
                    statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByTime(areaList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true));
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
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_business_LifeList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByLife(areaList);
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
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_business_StaffList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByStaff(areaList);
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
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_business_AreaList(string usertype)
        {
            try
            {
                com.IAreaService areaService = Bootstrap.Container.Resolve<com.IAreaService>();
                IList<Area> AreaList = areaService.GetSubArea("460100");
                StatisticsInfo statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByArea(AreaList);
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