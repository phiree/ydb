using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using Dianzhu.ApplicationService.Client;
using log4net;

namespace Dianzhu.Web.RestfulApi.Controllers.Client
{
    [HMACAuthentication]
    //[RoutePrefix("api/Client")]
    public class _AuthorizationController : ApiController
    {
        //  private AuthRepository _repo = null;
        private readonly IClientService iclientservice;

        public _AuthorizationController()
        {
            // _repo = new AuthRepository();
            iclientservice = Bootstrap.Container.Resolve<IClientService>();
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

        [Route("api/v1/authorization")] //authorization//Token
        public IHttpActionResult PostToken([FromBody] Customer customer)
        {
            var log = LogManager.GetLogger("Ydb.post/authorization.Rule.v1.RestfulApi.Web.Dianzhu");
            try
            {
                if (customer == null)
                    customer = new Customer();
                var stamp_TIMES = Request.Headers.GetValues("stamp_TIMES").FirstOrDefault();
                var allowedOrigin = Request.GetOwinContext().Get<string>("as:RequestMethodUriSign");
                log.Info("Info(UserInfo)" + stamp_TIMES + ":ApiRoute=post/authorization,UserName=" + customer.loginName +
                         ",UserId=" + customer.UserID + ",UserType=" + customer.UserType + ",RequestMethodUriSign=" +
                         allowedOrigin);
                //log.Info("Info(Route)" + stamp_TIMES + ":post/api/v1/authorization");
                //HMACAuthenticationAttribute hmac = new HMACAuthenticationAttribute();
                //string appName = Request.Headers.GetValues("appName").FirstOrDefault();
                //ConfigurationManager .GetSection
                var mysection = (MySectionCollection)ConfigurationManager.GetSection("MySectionCollection");
                //MySectionKeyValueSettings kv = mysection.KeyValues[Request.Headers.GetValues("appName").FirstOrDefault()];
                var apiName = Request.Headers.GetValues("appName").FirstOrDefault();
                var apiKey = mysection.KeyValues[apiName].Value;
                //hmac.getAllowedApps(Request.Headers.GetValues("appName").FirstOrDefault());

                var mysection1 = (MySectionCollection1)ConfigurationManager.GetSection("MySectionCollection1");
                var appName = mysection1.KeyValues[apiName].Value;

                //log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.ClientController");
                //ilog.Debug("PostToken(Baegin):"+customer.loginName+"_"+DateTime.Now.ToString("yyyyMMddHHmmss"));
                return
                    Json(
                        iclientservice.CreateToken(customer.loginName, customer.password, apiName, apiKey,
                            Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority, appName) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}