using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;


namespace Dianzhu.Web.RestfulApi.Controllers.LOCATION
{
    public class CitysController : ApiController
    {
        public IHttpActionResult GetCitys([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_LocationFiltering location)
        {
            try
            {
                RespGeo geoObj = utils.Deserialize<RespGeo>(utils.GetCity(location.longitude, location.latitude));
                return Ok(geoObj);
            }
            catch (Exception ex)
            {
                common_Trait_400_Rsponses res_Error = new common_Trait_400_Rsponses();
                res_Error.errCode = "009001";
                res_Error.errString = ex.Message;
                return Content(HttpStatusCode.BadRequest, res_Error);
            }
        }
    }
}
