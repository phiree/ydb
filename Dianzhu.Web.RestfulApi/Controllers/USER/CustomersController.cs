using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using System.Threading.Tasks;
using System.Configuration;

namespace Dianzhu.Web.RestfulApi.Controllers.USER
{
    [HMACAuthentication]

    //[Authorize]
    public class CustomersController : ApiController
    {
        private ApplicationService.User.IUserService iuserservice = null;
        public CustomersController()
        {
            //this.iuserservice = iuserservice;
            iuserservice= Bootstrap.Container.Resolve<ApplicationService.User.IUserService>();
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
                //userObj userobj = iuserservice.GetUserById(id);
                //if (userobj == null)
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    return Ok(userobj);
                //}
                //return Ok(iuserservice.GetUserById(id));
                GetRequestHeader.GetTraitHeaders("get/customers/{customerID}");
                return Json(iuserservice.GetUserById(id, "customer")?? new object());
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }


        //public async Task<IHttpActionResult> GetUserById()
        //{
        //    //await Task.Run ();
        //    //return OK("你好！");
        //    //Task<IHttpActionResult> ok=Ok("你好！");
        //    //await ok;
        //    return await Task.Run(() =>
        //    {
        //        //return Ok("你好！");
        //        //return NotFound();
        //        return InternalServerError();
        //    });
        //}


        /// <summary>
        /// 注册新用户
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
                log4net.ILog log = log4net.LogManager.GetLogger("Ydb.post/customers.Rule.v1.RestfulApi.Web.Dianzhu");
                string stamp_TIMES = Request.Headers.GetValues("stamp_TIMES").FirstOrDefault();
                log.Info("Info(UserInfo)" + stamp_TIMES + ":ApiRoute=post/customers,UserName=" + userBody.phone + ",UserId=,UserType=customer");
                return Json(iuserservice.PostUser(userBody,"customer"));
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
        public IHttpActionResult GetUsers([FromUri]common_Trait_Filtering filter,[FromUri]common_Trait_UserFiltering userFilter)
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
                GetRequestHeader.GetTraitHeaders("get/customers");
                return Json(iuserservice.GetUsers(filter,userFilter, "customer"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 第三方用户注册
        /// </summary>
        /// <param name="userBody"></param>
        /// <returns></returns>
        [Route("api/v1/customer3rds")]
        public IHttpActionResult PostUser3rds([FromBody]U3RD_Model u3rd_Model)
        {
            try
            {
                if (u3rd_Model == null)
                {
                    u3rd_Model = new U3RD_Model();
                }
                MySectionCollection1 mysection = (MySectionCollection1)ConfigurationManager.GetSection("MySectionCollection1");
                //MySectionKeyValueSettings kv = mysection.KeyValues[Request.Headers.GetValues("appName").FirstOrDefault()];
                string apiName = Request.Headers.GetValues("appName").FirstOrDefault();
                string apiKey = mysection.KeyValues[apiName].Value;
                u3rd_Model.appName = apiKey;


                log4net.ILog log = log4net.LogManager.GetLogger("Ydb.post/customer3rds.Rule.v1.RestfulApi.Web.Dianzhu");
                string stamp_TIMES = Request.Headers.GetValues("stamp_TIMES").FirstOrDefault();
                var allowedOrigin = Request.GetOwinContext().Get<string>("as:RequestMethodUriSign");
                log.Info("Info(UserInfo)" + stamp_TIMES + ":ApiRoute=post/customer3rds,UserName=" + u3rd_Model.platform + ",UserId=,UserType=customer,RequestMethodUriSign=" + allowedOrigin.ToString());
                return Json(iuserservice.PostUser3rds(u3rd_Model, "customer"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="userFilter"></param>
        /// <returns></returns>
        [Route("api/v1/customers/count")]
        public IHttpActionResult GetUsersCount([FromUri]common_Trait_UserFiltering userFilter)
        {
            try
            {
                if (userFilter == null)
                {
                    userFilter = new common_Trait_UserFiltering();
                }
                log4net.ILog log = log4net.LogManager.GetLogger("Ydb.get/customers/count.Rule.v1.RestfulApi.Web.Dianzhu");
                string stamp_TIMES = Request.Headers.GetValues("stamp_TIMES").FirstOrDefault();

                var allowedOrigin = Request.GetOwinContext().Get<string>("as:RequestMethodUriSign");
                log.Info("Info(UserInfo)" + stamp_TIMES + ":ApiRoute=get/customers/count,UserName=,UserId=,UserType=customer,RequestMethodUriSign=" + allowedOrigin.ToString());
                return Json(iuserservice.GetUsersCount(userFilter, "customer"));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 修改用户信息
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
                Customer customer = GetRequestHeader.GetTraitHeaders("patch/customers/{customerID}");
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
        /// 更新用户当前所定位的信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("api/v1/customers/{userID}/currentGeolocation")]
        public IHttpActionResult PatchCurrentGeolocation(string userID,[FromBody]common_Trait_LocationFiltering common_Body)
        {
            try
            {
                if (common_Body == null)
                {
                    common_Body = new common_Trait_LocationFiltering();
                }
                Customer customer = GetRequestHeader.GetTraitHeaders("patch/customers/{customerID}/currentGeolocation");


                return Json(iuserservice.PatchCurrentGeolocation(userID, common_Body, customer));

                //return Json("没有用户定位信息记录！");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 1、修改客户密码，没有验证，请谨慎调用
        ///2、App 前端需通过短信验证后调用！
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("api/v1/customers/phones/{phone}")]
        public IHttpActionResult PatchPasswordForForget(string phone, [FromBody]UserChangeBody userChangeBody)
        {
            try
            {
                if (userChangeBody == null)
                {
                    userChangeBody = new UserChangeBody();
                }
                log4net.ILog log = log4net.LogManager.GetLogger("Ydb.patch/customers/phones/{phone}.Rule.v1.RestfulApi.Web.Dianzhu");
                string stamp_TIMES = Request.Headers.GetValues("stamp_TIMES").FirstOrDefault();
                var allowedOrigin = Request.GetOwinContext().Get<string>("as:RequestMethodUriSign");
                log.Info("Info(UserInfo)" + stamp_TIMES + ":ApiRoute=patch/customers/phones/{phone},UserName=" + userChangeBody.phone + ",UserId=,UserType=customer,RequestMethodUriSign=" + allowedOrigin.ToString());
                return Json(iuserservice.PatchPasswordForForget(phone, userChangeBody.newPassWord));
                //return Json("没有用户定位信息记录！");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
