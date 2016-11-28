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
    public class MerchantsController : ApiController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.Rule");
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
                GetRequestHeader.GetTraitHeaders("get/merchants/{merchantID}");
                return Json(iuserservice.GetUserById(id, "business")?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 条件读取用户合集
        /// </summary>
        /// <param name="userFilter"></param>
        /// <returns></returns>
        public IHttpActionResult GetUsers([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_UserFiltering userFilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (userFilter == null)
                {
                    userFilter = new common_Trait_UserFiltering();
                }
                GetRequestHeader.GetTraitHeaders("get/merchants/list");
                return Json(iuserservice.GetUsers(filter, userFilter, "business"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计商户数量
        /// </summary>
        /// <param name="userFilter"></param>
        /// <returns></returns>
        [Route("api/v1/merchants/count")]
        public IHttpActionResult GetUsersCount([FromUri]common_Trait_UserFiltering userFilter)
        {
            try
            {
                if (userFilter == null)
                {
                    userFilter = new common_Trait_UserFiltering();
                }
                string stamp_TIMES = Request.Headers.GetValues("stamp_TIMES").FirstOrDefault();
                log.Info("Info(UserInfo)" + stamp_TIMES + ":ApiRoute=get/merchants/count,UserName=,UserId=,UserType=business");
                return Json(iuserservice.GetUsersCount(userFilter, "business"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        //public IHttpActionResult PostUserByInfo([FromBody]common_Trait_UserFiltering userFilter)
        //{
        //    try
        //    {
        //        return Json(iuserservice.GetUserByInfo(userFilter, "business"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
        //    }
        //}

        /// <summary>
        /// 修改商户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userChangeBody"></param>
        /// <returns></returns>
        public IHttpActionResult PatchUser(string id, [FromBody]UserChangeBody userChangeBody)
        {
            try
            {
                if (userChangeBody == null)
                {
                    userChangeBody = new UserChangeBody();
                }
                Customer customer = GetRequestHeader.GetTraitHeaders("patch/merchants/{merchantID}");
                if (id != customer.UserID)
                {
                    throw new Exception("不能修改别人的信息！");
                }
                return Json(iuserservice.PatchUser(id, userChangeBody, customer.UserType));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 注册新商户
        /// </summary>
        /// <param name="userBody"></param>
        /// <returns></returns>
        public IHttpActionResult PostUser([FromBody]Common_Body userBody)
        {
            try
            {
                if (userBody == null)
                {
                    userBody = new Common_Body();
                }
                string stamp_TIMES = Request.Headers.GetValues("stamp_TIMES").FirstOrDefault();
                log.Info("Info(UserInfo)" + stamp_TIMES + ":ApiRoute=post/merchants,UserName=" + userBody.email + ",UserId=,UserType=business");
                return Json(iuserservice.PostUser(userBody, "business"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
