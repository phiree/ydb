using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;


namespace Dianzhu.Web.RestfulApi.Controllers.COMPLAINT
{
    [HMACAuthentication]
    public class ComplaintsController : ApiController
    {

        private ApplicationService.Complaint.IComplaintService icomplaintservice = null;
        public ComplaintsController()
        {
            //this.iuserservice = iuserservice;
            icomplaintservice = Bootstrap.Container.Resolve<ApplicationService.Complaint.IComplaintService>();
        }

        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaintobj">投诉信息</param>
        /// <returns></returns>
        public IHttpActionResult PostCreateComplaint([FromBody]complaintObj complaintobj)
        {
            try
            {
                return Json(icomplaintservice.AddComplaint(complaintobj, GetRequestHeader.GetTraitHeaders("post/complaints")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaint">查询条件</param>
        /// <returns></returns>
        ///[ActionName("list")]
        public IHttpActionResult GetComplaints([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_ComplainFiltering complaint)
        {
            try
            {
                return Json(icomplaintservice.GetComplaints(filter, complaint));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }


        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaint">查询条件</param>
        /// <returns></returns>
        ///[HttpGet]
        ///[ActionName("count")]
        [Route("api/v1/Complaints/count")]
        public IHttpActionResult GetComplaintsCount([FromUri]common_Trait_ComplainFiltering complaint)
        {
            try
            {
                if (complaint==null)
                {
                    complaint = new common_Trait_ComplainFiltering();
                }
                return Json(icomplaintservice.GetComplaintsCount(complaint));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 读取投诉信息
        /// </summary>
        /// <param name="id">投诉ID</param>
        /// <returns></returns>
        public IHttpActionResult GetComplaint(string id)
        {
            try
            {
                return Json(icomplaintservice.GetOneComplaint(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
