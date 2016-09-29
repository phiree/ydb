using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;


namespace Dianzhu.Web.RestfulApi.Controllers.USER
{
    [HMACAuthentication]
    public class CustomerServicesController : ApiController
    {
        private ApplicationService.User.IUserService iuserservice = null;
        public CustomerServicesController()
        {
            //this.iuserservice = iuserservice;
            iuserservice = Bootstrap.Container.Resolve<ApplicationService.User.IUserService>();
        }


        /// <summary>
        /// 申请客服资源
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        [Route("api/v1/customerServices")]
        public IHttpActionResult GetCustomerServices()
        {
            try
            {
                return Json(iuserservice.GetCustomerServices(GetRequestHeader.GetTraitHeaders("get/customerServices")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 客服 注册
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IHttpActionResult PostCustomerServices([FromBody]string userID)
        {
            try
            {
                //return Json(iuserservice.GetCustomerServices(userID));
                return Json("没有客服注册的功能！");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
