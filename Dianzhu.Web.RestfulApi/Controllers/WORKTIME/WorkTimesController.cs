using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.WORKTIME
{
    public class WorkTimesController : ApiController
    {
        private ApplicationService.WorkTime.IWorkTimeService iworktime = null;
        public WorkTimesController()
        {
            //this.iuserservice = iuserservice;
            iworktime = Bootstrap.Container.Resolve<ApplicationService.WorkTime.IWorkTimeService>();
        }

        /// <summary>
        /// 新建工作时间
        /// </summary>
        /// <param name="worktimeobj"></param>
        /// <returns></returns>
        [Route("api/stores/{storeID}/services/{serviceID}/WorkTimes")]
        public IHttpActionResult PostWorkTime(string storeID,string serviceID, [FromBody]workTimeObj worktimeobj)
        {
            try
            {
                //return Json(iworktime.PostWorkTime(storeID, serviceID,worktimeobj));
                return Json("{'storeID':'" + storeID + "','serviceID':'" + serviceID + "'}");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
