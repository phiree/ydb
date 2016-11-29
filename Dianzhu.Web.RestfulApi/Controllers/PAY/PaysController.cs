using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.PAY
{
    [HMACAuthentication]
    public class PaysController : ApiController
    {
        private ApplicationService.Pay.IPayService ipay = null;
        public PaysController()
        {
            ipay = Bootstrap.Container.Resolve<ApplicationService.Pay.IPayService>();
        }

        /// <summary>
        /// 条件读取支付项
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="payfilter"></param>
        /// <returns></returns>
        [Route("api/v1/Orders/{orderID}/Pays")]
        public IHttpActionResult GetPays(string orderID, [FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_PayFiltering payfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (payfilter == null)
                {
                    payfilter = new common_Trait_PayFiltering();
                }
                return Json(ipay.GetPays(orderID, filter, payfilter,GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/pays")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计支付项的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payfilter"></param>
        /// <returns></returns>
        [Route("api/v1/Orders/{orderID}/Pays/count")]
        public IHttpActionResult GetPaysCount(string orderID, [FromUri]common_Trait_PayFiltering payfilter)
        {
            try
            {
                if (payfilter == null)
                {
                    payfilter = new common_Trait_PayFiltering();
                }
                return Json(ipay.GetPaysCount(orderID, payfilter, GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/pays/count")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 读取支付项 根据ID
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <returns></returns>
        [Route("api/v1/Orders/{orderID}/Pays/{payID}")]
        public IHttpActionResult GetPay(string orderID, string payID)
        {
            try
            {
                return Json(ipay.GetPay(orderID, payID, GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/pays/{payID}")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 更新支付信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="payobj"></param>
        /// <returns></returns>
        [Route("api/v1/Orders/{orderID}/Pays/{payID}")]
        public IHttpActionResult PatchPay(string orderID, string payID,[FromBody]payObj payobj)
        {
            try
            {
                return Json(ipay.PatchPay(orderID, payID, payobj, GetRequestHeader.GetTraitHeaders("patch/orders/{orderID}/pays/{payID}")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 获得第三方支付字符串
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="payTarget"></param>
        /// <returns></returns>
        [Route("api/v1/Orders/{orderID}/Pays/{payID}/pay3rdString")]
        public IHttpActionResult GetPay3rd(string orderID, string payID, [FromUri]string payTarget)
        {
            try
            {
                return Json(ipay.GetPay3rdString(orderID, payID, payTarget, GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/pays/{payID}/pay3rdString")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
