﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using System.Configuration;

namespace Dianzhu.Web.RestfulApi.Controllers.APP
{
    [HMACAuthentication]
    public class AppsController : ApiController
    {
        private ApplicationService.App.IAppService iapps = null;
        public AppsController()
        {
            //this.iuserservice = iuserservice;
            iapps = Bootstrap.Container.Resolve<ApplicationService.App.IAppService>();
        }

        /// <summary>
        /// 注册设备,userID 为空，表示匿名注册  , [FromBody]appObj appobj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appobj"></param>
        /// <returns></returns>
        public IHttpActionResult PostDeviceBind(string id, [FromBody]appObj appobj)
        {
            try
            {
                MySectionCollection1 mysection = (MySectionCollection1)ConfigurationManager.GetSection("MySectionCollection1");
                //MySectionKeyValueSettings kv = mysection.KeyValues[Request.Headers.GetValues("appName").FirstOrDefault()];
                string apiName = Request.Headers.GetValues("appName").FirstOrDefault();
                appobj.appName = mysection.KeyValues[apiName].Value;
                GetRequestHeader.GetTraitHeaders("post/apps/{appUUID}");
                return Json(iapps.PostDeviceBind(id, appobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult DeleteDeviceBind(string id)
        {
            try
            {
                GetRequestHeader.GetTraitHeaders("delete/apps/{appUUID}");
                return Json(iapps.DeleteDeviceBind(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 更新设备推送计数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pushCount"></param>
        /// <returns></returns>
        public IHttpActionResult PatchDeviceBind(string id, [FromBody]Common_Body pushCount)
        {
            try
            {
                GetRequestHeader.GetTraitHeaders("patch/apps/{appUUID}");
                return Json(iapps.PatchDeviceBind(id, pushCount.pushCount));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
