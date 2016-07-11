using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.REMIND
{
    [HMACAuthentication]
    public class RemindsController : ApiController
    {
        private ApplicationService.Remind.IRemindService iremind = null;
        public RemindsController()
        {
            //this.iuserservice = iuserservice;
            iremind = Bootstrap.Container.Resolve<ApplicationService.Remind.IRemindService>();
        }

        /// <summary>
        /// 根据ID获取提醒
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetRemindById(string id)
        {
            try
            {
                return Json(iremind.GetRemindById(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }

        }

        /// <summary>
        /// 根据ID删除提醒
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult DeleteRemindById(string id)
        {
            try
            {
                return Json(iremind.DeleteRemindById(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }

        }

        /// <summary>
        /// 条件读取提醒
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="remind"></param>
        /// <returns></returns>
        public IHttpActionResult GetReminds([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_RemindFiltering remind)
        {
            try
            {
                return Json(iremind.GetReminds(filter, remind));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }

        }

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <param name="remind"></param>
        /// <returns></returns>
        [Route("api/v1/Reminds/count")]
        public IHttpActionResult GetRemindsCount([FromUri]common_Trait_RemindFiltering remind)
        {
            try
            {

                if (remind == null)
                {
                    remind = new common_Trait_RemindFiltering();
                }
                return Json(iremind.GetRemindsCount(remind));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
