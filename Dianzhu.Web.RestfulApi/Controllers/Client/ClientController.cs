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
    [RoutePrefix("api/Client")]
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
        [Route("Register")]
        public async Task<IHttpActionResult> Register(ClientDTO client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            iclientservice.RegisterClient(client);

            return Ok();
        }
    }
}
