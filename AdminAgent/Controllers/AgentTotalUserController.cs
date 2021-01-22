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
    public class AgentTotalUserController : AgentBaseController
    {
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult total_user()
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                ViewData["NewCustomerNumber"] = dzMembershipService.GetCountOfNewMembershipsYesterdayByArea(CurrentUser.AreaIdList, UserType.customer);
                ViewData["AllCustomerNumber"] = dzMembershipService.GetCountOfAllMembershipsByArea(CurrentUser.AreaIdList, UserType.customer);
                ViewData["LoginCustomerNumber"] = dzMembershipService.GetCountOfLoginMembershipsLastMonthByArea(CurrentUser.AreaIdList, UserType.customer);
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
            ViewBag.UserName = CurrentUser.UserName;
            return View();
        }

        /// <summary>
        /// 助理统计
        /// </summary>
        /// <returns></returns>
        public ActionResult total_assistant()
        {
            ViewBag.UserName = CurrentUser.UserName;
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
                    statisticsInfo = dzMembershipService.GetStatisticsNewMembershipsCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now, CheckEnums.CheckUserType(usertype));
                }
                else
                {
                    statisticsInfo = dzMembershipService.GetStatisticsNewMembershipsCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), CheckEnums.CheckUserType(usertype));
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
        /// 统计用户总数
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public ActionResult total_user_Count(string usertype)
        {
            try
            {
                Models.TotalCount totalCount = new Models.TotalCount();
                totalCount.count = dzMembershipService.GetCountOfAllMembershipsByArea(CurrentUser.AreaIdList, UserType.customer);
                return Json(totalCount, JsonRequestBehavior.AllowGet);
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
                    statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now, CheckEnums.CheckUserType(usertype));
                }
                else
                {
                    statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), CheckEnums.CheckUserType(usertype));
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
                    statisticsInfo = dzMembershipService.GetStatisticsLoginCountListByTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now, CheckEnums.CheckUserType(usertype));
                }
                else
                {
                    statisticsInfo = dzMembershipService.GetStatisticsLoginCountListByTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), CheckEnums.CheckUserType(usertype));
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
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListBySex(CurrentUser.AreaIdList, CheckEnums.CheckUserType(usertype));
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
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByAppName(CurrentUser.AreaIdList, CheckEnums.CheckUserType(usertype));
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
                StatisticsInfo statisticsInfo = dzMembershipService.GetStatisticsAllMembershipsCountListByArea(CurrentUser.AreaList, CheckEnums.CheckUserType(usertype));
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