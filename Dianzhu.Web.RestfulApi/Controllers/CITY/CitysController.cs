using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;


namespace Dianzhu.Web.RestfulApi.Controllers.LOCATION
{
    [HMACAuthentication]
    public class CitysController : ApiController
    {

        private ApplicationService.City.ICityService icityservice = null;
        public CitysController()
        {
            //this.iuserservice = iuserservice;
            icityservice = Bootstrap.Container.Resolve<ApplicationService.City.ICityService>();
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="filter">接口通用筛选器</param>
        /// <param name="location">location筛选器</param>
        /// <returns></returns>
        public IHttpActionResult GetCitys([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_LocationFiltering location)
        {
            try
            {
               Customer customer= GetRequestHeader.GetTraitHeaders("get/citys");
                return Json(icityservice.GetAllCity(filter, location, customer));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }


        /// <summary>
        /// 根据code获取城市
        /// </summary>
        /// <param name="id">城市code</param>
        /// <returns></returns>
        public IHttpActionResult GetCitysByCode(string id)
        {
            try
            {
                GetRequestHeader.GetTraitHeaders("get/citys/{code}");
                return Json(icityservice.GetCityByAreaCode(id) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
