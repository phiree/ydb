﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using System.Threading.Tasks;
using System.Configuration;

namespace Dianzhu.Web.RestfulApi.Controllers.Client
{
    [HMACAuthentication]
    //[RoutePrefix("api/Client")]
    public class _AuthorizationController : ApiController
    {
        //  private AuthRepository _repo = null;
        private ApplicationService.Client.IClientService iclientservice  = null;
        
        public _AuthorizationController()
        {
            // _repo = new AuthRepository();
            iclientservice = Bootstrap.Container.Resolve<ApplicationService.Client.IClientService>();
        }

        //[AllowAnonymous]
        //[Route("api/v1/Client/Register")]
        //public async Task<IHttpActionResult> Register(ClientDTO client)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    iclientservice.RegisterClient(client);

        //    return Ok();
        //}

        
        [Route("api/v1/authorization")]//authorization//Token
        public IHttpActionResult PostToken([FromBody]Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    customer = new Customer();
                }
                //HMACAuthenticationAttribute hmac = new HMACAuthenticationAttribute();
                //string appName = Request.Headers.GetValues("appName").FirstOrDefault();
                //ConfigurationManager .GetSection
                MySectionCollection mysection = (MySectionCollection)ConfigurationManager.GetSection("MySectionCollection");
                //MySectionKeyValueSettings kv = mysection.KeyValues[Request.Headers.GetValues("appName").FirstOrDefault()];
                string apiKey = mysection.KeyValues[Request.Headers.GetValues("appName").FirstOrDefault()].Value; //hmac.getAllowedApps(Request.Headers.GetValues("appName").FirstOrDefault());
                return Json(iclientservice.CreateToken(customer.loginName, customer.password, apiKey, "http://"+Request.RequestUri.Authority) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
