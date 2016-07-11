using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.STORE
{
    [HMACAuthentication]
    public class StoresController : ApiController
    {
        private ApplicationService.Store.IStoreService istore = null;
        public StoresController()
        {
            istore = Bootstrap.Container.Resolve<ApplicationService.Store.IStoreService>();
        }

        /// <summary>
        /// 新建店铺
        /// </summary>
        /// <param name="storeobj"></param>
        /// <returns></returns>
        public IHttpActionResult PostStore([FromBody]storeObj storeobj)
        {
            try
            {
                if (storeobj == null)
                {
                    storeobj = new storeObj();
                }
                return Json(istore.PostStore(storeobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="storefilter"></param>
        /// <returns></returns>
        public IHttpActionResult GetStores([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_StoreFiltering storefilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (storefilter == null)
                {
                    storefilter = new common_Trait_StoreFiltering();
                }
                return Json(istore.GetStores(filter, storefilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计店铺数量
        /// </summary>
        /// <param name="storefilter"></param>
        /// <returns></returns>
        [Route("api/v1/stores/count")]
        public IHttpActionResult GetStoresCount([FromUri]common_Trait_StoreFiltering storefilter)
        {
            try
            {
                if (storefilter == null)
                {
                    storefilter = new common_Trait_StoreFiltering();
                }
                return Json(istore.GetStoresCount(storefilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 读取店铺 根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetStore(string id)
        {
            try
            {
                return Json(istore.GetStore(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 更新店铺信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storeobj"></param>
        /// <returns></returns>
        public IHttpActionResult PatchStore(string id, [FromBody]storeObj storeobj)
        {
            try
            {
                if (storeobj == null)
                {
                    storeobj = new storeObj();
                }
                return Json(istore.PatchStore(id, storeobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult DeleteStore(string id)
        {
            try
            {
                return Json(istore.DeleteStore(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
