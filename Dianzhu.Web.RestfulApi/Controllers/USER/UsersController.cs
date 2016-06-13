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
                return Json(iuserservice.GetUserById(id));
            }
            catch(Exception ex)
            {
                //return InternalServerError();
                common_Trait_400_Rsponses res_Error = new common_Trait_400_Rsponses();
                if (ex.Message == "009004")
                {
                    res_Error.errCode = "009004";
                    res_Error.errString = "没有找到符合条件的数据！";
                }
                else
                {
                    res_Error.errCode = "009001";
                    res_Error.errString = ex.Message;
                }
                return Content(HttpStatusCode.BadRequest, res_Error);
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
    }
}
