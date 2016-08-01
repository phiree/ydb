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
        private ApplicationService.Snapshot.ISnapshotService isnapshot = null;
        public SnapshotsController()
        {
            isnapshot = Bootstrap.Container.Resolve<ApplicationService.Snapshot.ISnapshotService>();
        }

        public IHttpActionResult GetSnapshots(string id, [FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_SnapshotFiltering sna)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (sna == null)
                {
                    sna = new common_Trait_SnapshotFiltering();
                }
                return Json(isnapshot.GetSnapshots(id,filter, sna, GetRequestHeader.GetTraitHeaders()));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
