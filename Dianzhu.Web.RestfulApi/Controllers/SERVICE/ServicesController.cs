﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.SERVICE
{
    [HMACAuthentication]
    public class ServicesController : ApiController
    {

        private ApplicationService.Service.IServiceService iservice = null;
        public ServicesController()
        {
            iservice = Bootstrap.Container.Resolve<ApplicationService.Service.IServiceService>();
        }

        /// <summary>
        /// 新建服务
        /// </summary>
        /// <param name="servicesobj"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services")]
        public IHttpActionResult PostService(string storeID,[FromBody]servicesObj servicesobj)
        {
            try
            {
                if (servicesobj == null)
                {
                    servicesobj = new servicesObj();
                }
                return Json(iservice.PostService(storeID,servicesobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="filter"></param>
        /// <param name="servicefilter"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services")]
        public IHttpActionResult GetServices(string storeID, [FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_ServiceFiltering servicefilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (servicefilter == null)
                {
                    servicefilter = new common_Trait_ServiceFiltering();
                }
                return Json(iservice.GetServices(storeID, filter, servicefilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计服务的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="servicefilter"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/count")]
        public IHttpActionResult GetServicesCount(string storeID, [FromUri]common_Trait_ServiceFiltering servicefilter)
        {
            try
            {
                if (servicefilter == null)
                {
                    servicefilter = new common_Trait_ServiceFiltering();
                }
                return Json(iservice.GetServicesCount(storeID,servicefilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 读取服务 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}")]
        public IHttpActionResult GetService(string storeID, string serviceID)
        {
            try
            {
                return Json(iservice.GetService(storeID, serviceID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 更新服务信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="servicesobj"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}")]
        public IHttpActionResult PatchService(string storeID, string serviceID,[FromBody]servicesObj servicesobj)
        {
            try
            {
                if (servicesobj == null)
                {
                    servicesobj = new servicesObj();
                }
                return Json(iservice.PatchService(storeID, serviceID, servicesobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 删除服务 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        [Route("api/v1/stores/{storeID}/services/{serviceID}")]
        public IHttpActionResult DeleteService(string storeID, string serviceID)
        {
            try
            {
                return Json(iservice.DeleteService(storeID, serviceID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
