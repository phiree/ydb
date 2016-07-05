using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.ASSIGN
{
    [HMACAuthentication]
    public class AssignsController : ApiController
    {
        private ApplicationService.Assign.IAssignService iassign = null;
        public AssignsController()
        {
            //this.iuserservice = iuserservice;
            iassign = Bootstrap.Container.Resolve<ApplicationService.Assign.IAssignService>();
        }

        /// <summary>
        /// 新建指派
        /// </summary>
        /// <param name="assignobj"></param>
        /// <returns></returns>
        public IHttpActionResult PostAssign([FromBody]assignObj assignobj)
        {
            try
            {
                return Json(iassign.PostAssign(assignobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 条件读取指派
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <returns></returns>
        public IHttpActionResult GetAssigns([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_AssignFiltering assign)
        {
            try
            {
                return Json(iassign.GetAssigns(filter,assign));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计指派的数量
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        [Route("api/v1/Assigns/count")]
        public IHttpActionResult GetAssignsCount([FromUri]common_Trait_AssignFiltering assign)
        {
            try
            {

                if (assign == null)
                {
                    assign = new common_Trait_AssignFiltering();
                }
                return Json(iassign.GetAssignsCount(assign));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
