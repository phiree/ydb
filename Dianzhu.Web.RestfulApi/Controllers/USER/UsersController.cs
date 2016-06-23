﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using System.Threading.Tasks;

namespace Dianzhu.Web.RestfulApi.Controllers.USER
{

    //[Authorize]
    public class UsersController : ApiController
    {
        private ApplicationService.User.IUserService iuserservice = null;
        public UsersController()
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
                return Json(iuserservice.GetUserById(id, "customer"));
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
        [Route("api/user3rds")]
        public IHttpActionResult PostUser3rds([FromBody]U3RD_Model u3rd_Model)
        {
            try
            {
                if (u3rd_Model == null)
                {
                    u3rd_Model = new U3RD_Model();
                }
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
        [Route("api/users/count")]
        public IHttpActionResult GetUsersCount([FromUri]common_Trait_UserFiltering userFilter)
        {
            try
            {
                if (userFilter == null)
                {
                    userFilter = new common_Trait_UserFiltering();
                }
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
                return Json(iuserservice.PatchUser(id, userChangeBody, "customer"));
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
        [Route("api/users/{userID}/currentGeolocation")]
        public IHttpActionResult PatchCurrentGeolocation(string userID,[FromBody]string code)
        {
            try
            {
                //return Json(iuserservice.PatchCurrentGeolocation(userFilter, "customer"));
                return Json("没有用户定位信息记录！");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
