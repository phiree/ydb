using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;
using System.Threading.Tasks;


namespace Dianzhu.Web.RestfulApi.Controllers.SNAPSHOT
{
    [HMACAuthentication]
    public class SnapshotsController : ApiController
    {
        public IHttpActionResult GetSnapshots([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_SnapshotFiltering sna)
        {
            common_Trait_400_Rsponses res_Error = new common_Trait_400_Rsponses();
            res_Error.errCode = "009001";
            res_Error.errString = "暂时没有查询方法!";
            return Content(HttpStatusCode.BadRequest, res_Error);
        }
    }
}
