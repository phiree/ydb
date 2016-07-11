using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using System.Threading.Tasks;

namespace Dianzhu.Web.RestfulApi.Controllers.Client
{
    [HMACAuthentication]
    //[RoutePrefix("api/Client")]
    public class ClientController : ApiController
    {
        //  private AuthRepository _repo = null;
        private ApplicationService.Client.IClientService iclientservice  = null;
        
        public ClientController()
        {
            // _repo = new AuthRepository();
            iclientservice = Bootstrap.Container.Resolve<ApplicationService.Client.IClientService>();
        }

        [AllowAnonymous]
        [Route("api/v1/Client/Register")]
        public async Task<IHttpActionResult> Register(ClientDTO client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            iclientservice.RegisterClient(client);

            return Ok();
        }

        
        [Route("api/v1/authorization")]//authorization//Token
        public IHttpActionResult PostToken([FromBody]Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    customer = new Customer();
                }
                HMACAuthenticationAttribute hmac = new HMACAuthenticationAttribute();
                string apiKey = hmac.getAllowedApps(Request.Headers.GetValues("appName").FirstOrDefault());
                return Json(iclientservice.CreateToken(customer.loginName, customer.password, apiKey, "http://"+Request.RequestUri.Authority));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
