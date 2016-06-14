using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;


namespace Dianzhu.Web.RestfulApi.Controllers.COMPLAINT
{
    public class ComplaintsController : ApiController
    {
        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="filter">接口通用筛选器</param>
        /// <param name="location">location筛选器</param>
        /// <returns></returns>
        public IHttpActionResult PostCreateComplaint([FromBody]complaintObj complaintobj)
        {
            try
            {
                return Json("dd");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
