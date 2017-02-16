using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Domain;
using com = Ydb.Common.Application;


namespace AdminAgent.Controllers
{
    public class AgentTotalBusinessController : Controller
    {
        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
        IList<string> areaList = new List<string> { "2445", "2446", "2447", "2448", "2449", "2450" };
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

        public ActionResult total_business_detail()
        {
            return View();
        }

        public ActionResult total_business_NewList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = businessService.GetStatisticsNewBusinessesCountListByTime(areaList, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult total_business_AllList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = businessService.GetStatisticsAllBusinessesCountListByTime(areaList, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

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