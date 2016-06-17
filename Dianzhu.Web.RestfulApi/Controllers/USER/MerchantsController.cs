using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.USER
{
    public class MerchantsController : ApiController
    {
        private ApplicationService.User.IUserService iuserservice = null;
        public MerchantsController()
        {
            //this.iuserservice = iuserservice;
            iuserservice = Bootstrap.Container.Resolve<ApplicationService.User.IUserService>();
        }

        /// <summary>
        /// 根据userID获取user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IHttpActionResult GetUserById(string id)
        {
            try
            {
                return Json(iuserservice.GetUserById(id, "business"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        public IHttpActionResult GetUserByInfo([FromUri]common_Trait_UserFiltering userFilter)
        {
            try
            {
                return Json(iuserservice.GetUserByInfo(userFilter, "business"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
