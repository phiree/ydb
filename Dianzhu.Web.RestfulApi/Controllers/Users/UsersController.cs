﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using System.Threading.Tasks;

namespace Dianzhu.Web.RestfulApi.Controllers.Users
{

    [Authorize]
    public class UsersController : ApiController
    {
        //  private AuthRepository _repo = null;
        private ApplicationService.User.IUserService iuserservice = null;
        public UsersController()
        {
            // _repo = new AuthRepository();
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
                userObj userobj = iuserservice.GetUserById(id);
                if (userobj == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(userobj);
                }
            }
            catch
            {
                return InternalServerError();
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
