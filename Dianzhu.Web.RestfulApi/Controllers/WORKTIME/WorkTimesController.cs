using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.WORKTIME
{
    [HMACAuthentication]
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
        [Route("api/v1/stores/{storeID}/services/{serviceID}/WorkTimes")]
        public IHttpActionResult PostWorkTime(string storeID,string serviceID, [FromBody]workTimeObj worktimeobj)
        {
            try
            {
                if (worktimeobj == null)
                {
                    worktimeobj = new workTimeObj();
                }
                return Json(iworktime.PostWorkTime(storeID, serviceID,worktimeobj));
                //return Json("{'storeID':'" + storeID + "','serviceID':'" + serviceID + "'}");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 条件读取工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktime"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}/WorkTimes")]
        public IHttpActionResult GetWorkTimes(string storeID, string serviceID, [FromUri]common_Trait_WorkTimeFiltering worktime)
        {
            try
            {
                if (worktime == null)
                {
                    worktime = new common_Trait_WorkTimeFiltering();
                }
                return Json(iworktime.GetWorkTimes(storeID, serviceID,  worktime));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计工作时间的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktime"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}/WorkTimes/count")]
        public IHttpActionResult GetWorkTimesCount(string storeID, string serviceID, [FromUri]common_Trait_WorkTimeFiltering worktime)
        {
            try
            {
                if (worktime == null)
                {
                    worktime = new common_Trait_WorkTimeFiltering();
                }
                return Json(iworktime.GetWorkTimesCount(storeID, serviceID, worktime));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 读取工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}/WorkTimes/{workTimeID}")]
        public IHttpActionResult GetWorkTime(string storeID, string serviceID,string workTimeID)
        {
            try
            {
                return Json(iworktime.GetWorkTime(storeID, serviceID, workTimeID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 更新工作时间信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <param name="worktimeobj"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}/WorkTimes/{workTimeID}")]
        public IHttpActionResult PatchWorkTime(string storeID, string serviceID, string workTimeID, [FromBody]workTimeObj worktimeobj)
        {
            try
            {
                if (worktimeobj == null)
                {
                    worktimeobj = new workTimeObj();
                }
                return Json(iworktime.PatchWorkTime(storeID, serviceID, workTimeID, worktimeobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 删除服务 工作时间信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}/WorkTimes/{workTimeID}")]
        public IHttpActionResult DeleteWorkTime(string storeID, string serviceID, string workTimeID)
        {
            try
            {
                return Json(iworktime.DeleteWorkTime(storeID, serviceID, workTimeID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
