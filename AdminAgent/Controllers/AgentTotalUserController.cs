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
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 用户属性
        /// </summary>
        /// <returns></returns>
        public ActionResult total_user_detail()
        {
            return View();
        }

        /// <summary>
        /// 助理统计
        /// </summary>
        /// <returns></returns>
        public ActionResult total_assistant()
        {
            return View();
        }

        /// <summary>
        /// 新增用户数量列表
        /// </summary>
        /// <param name="usertype"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 累计用户数量列表
        /// </summary>
        /// <param name="usertype"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult total_user_AllList(string usertype, string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByTime(areaList, DateTime.Now.AddMonths(-1), DateTime.Now, CheckEnums.CheckUserType(usertype));
                }
                else
                {
                    statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByTime(areaList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), CheckEnums.CheckUserType(usertype));
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
        /// 用户活跃度列表
        /// </summary>
        /// <param name="usertype"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult total_user_LoginList(string usertype, string start, string end)
        {
            try
            {
                StatisticsInfo statisticsInfo;
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                {
                    statisticsInfo = dzMembershipService.GetStatisticsLoginCountListByTime(areaList, DateTime.Now.AddMonths(-1), DateTime.Now, CheckEnums.CheckUserType(usertype));
                }
                else
                {
                    statisticsInfo = dzMembershipService.GetStatisticsLoginCountListByTime(areaList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), CheckEnums.CheckUserType(usertype));
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
        /// 按性别统计用户
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 按手机系统统计用户
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 按子区域统计用户
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
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