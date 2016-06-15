using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.APP
{
    public class AppsController : ApiController
    {
        private ApplicationService.Apps.IAppsService iapps = null;
        public AppsController()
        {
            //this.iuserservice = iuserservice;
            iapps = Bootstrap.Container.Resolve<ApplicationService.Apps.IAppsService>();
        }

        /// <summary>
        /// 注册设备,userID 为空，表示匿名注册  , [FromBody]appObj appobj
        /// </summary>
        /// <returns>area实体list</returns>
        public IHttpActionResult PostDeviceBind(string id, [FromBody]appObj appobj)
        {
            try
            {
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
        /// <returns>area实体list</returns>
        public IHttpActionResult DeleteDeviceBind(string id)
        {
            try
            {
                return Json(iapps.DeleteDeviceBind(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
