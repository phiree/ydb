using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.AD
{
    public class adsController : ApiController
    {
        private ApplicationService.ADs.IADsService iads = null;
        public adsController()
        {
            //this.iuserservice = iuserservice;
            iads = Bootstrap.Container.Resolve<ApplicationService.ADs.IADsService>();
        }

        /// <summary>
        /// 条件读取广告
        /// </summary>
        /// <param name="adf"></param>
        /// <returns></returns>
        public IHttpActionResult GetADs([FromUri]common_Trait_AdFiltering adf)
        {
            try
            {
                return Json(iads.GetADs(adf));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
