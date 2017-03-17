﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

namespace AdminAgent.Controllers
{
    public class AgentBusinessManageController : AgentBaseController
    {
        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
        IBusinessOwnerService businessOwnerService = Bootstrap.Container.Resolve<IBusinessOwnerService>();
        /// <summary>
        /// 获取活跃商家列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessList()
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                return View(businessOwnerService.GetBusinessOwnerListByArea(CurrentUser.AreaIdList,false));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取封停商家列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessFrozenList()
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                return View(businessOwnerService.GetBusinessOwnerListByArea(CurrentUser.AreaIdList, true));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取封停店铺列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessStoreFrozenList()
        {
            try
            {
                return View(businessOwnerService.GetBusinessListByArea(CurrentUser.AreaIdList, false));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取封停服务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessServiceFrozenList()
        {
            try
            {
                return View(businessOwnerService.GetBusinessListByArea(CurrentUser.AreaIdList, false));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
    }
}