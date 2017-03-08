using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.Membership.Application;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Common.Domain;
using com = Ydb.Common.Application;
using Ydb.Common;

namespace AdminAgent.Controllers
{
    public class AgentTotalUserController : Controller
    {
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IList<string> areaList = new List<string> { "2445", "2446", "2447", "2448", "2449", "2450" };
        public ActionResult total_user()
        {
            try
            {
                ViewData["NewCustomerNumber"] = dzMembershipService.GetCountOfNewMembershipsYesterdayByArea(areaList, UserType.customer);
                ViewData["AllCustomerNumber"] = dzMembershipService.GetCountOfAllMembershipsByArea(areaList, UserType.customer);
                ViewData["LoginCustomerNumber"] = dzMembershipService.GetCountOfLoginMembershipsLastMonthByArea(areaList, UserType.customer);
                return View();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult total_user_detail()
        {
            return View();
        }

        public ActionResult total_assistant()
        {
            return View();
        }

        public ActionResult total_user_NewList(string usertype,string start,string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = dzMembershipService.GetStatisticsNewMembershipsCountListByTime(areaList, DateTime.Now.AddMonths(-1), DateTime.Now, CheckEnums.CheckUserType(usertype));
                }
                else
                {
                    statisticsInfo = dzMembershipService.GetStatisticsNewMembershipsCountListByTime(areaList, StringHelper.CheckDateTime(start,"yyyyMMdd","查询的开始时间",false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间",true), CheckEnums.CheckUserType(usertype));
                }
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult total_user_AllList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByTime(areaList, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), CheckEnums.CheckUserType(usertype));
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult total_user_LoginList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsLoginCountListByTime(areaList, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), CheckEnums.CheckUserType(usertype));
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult total_user_SexList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListBySex(areaList, CheckEnums.CheckUserType(usertype));
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult total_user_AppNameList(string usertype)
        {
            try
            {
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByAppName(areaList, CheckEnums.CheckUserType(usertype));
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult total_user_AreaList(string usertype)
        {
            try
            {
                com.IAreaService areaService = Bootstrap.Container.Resolve<com.IAreaService>();
                IList<Area> AreaList = areaService.GetSubArea("460100");
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByArea(AreaList, CheckEnums.CheckUserType(usertype));
                return Json(statisticsInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        //public ActionResult total_user_model(Area area,string code)
        //{
        //    try
        //    {
        //        return Json(area, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 400;
        //        return Content(ex.Message);
        //    }
        //}
    }
}