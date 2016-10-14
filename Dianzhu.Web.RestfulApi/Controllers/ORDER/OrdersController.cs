using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;


namespace Dianzhu.Web.RestfulApi.Controllers.ORDER
{
    [HMACAuthentication]
    public class OrdersController : ApiController
    {

        private ApplicationService.Order.IOrderService iorder = null;
        public OrdersController()
        {
            iorder = Bootstrap.Container.Resolve<ApplicationService.Order.IOrderService>();
        }

        /// <summary>
        /// 查询订单合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <returns></returns>
        public IHttpActionResult GetOrders([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_OrderFiltering orderfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (orderfilter == null)
                {
                    orderfilter = new common_Trait_OrderFiltering();
                }
                return Json(iorder.GetOrders(filter, orderfilter, GetRequestHeader.GetTraitHeaders("get/orders")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 查询订单合集的校验
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <returns></returns>
        [Route("api/v1/orders/verify")]
        public IHttpActionResult GetOrdersVerify([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_OrderFiltering orderfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (orderfilter == null)
                {
                    orderfilter = new common_Trait_OrderFiltering();
                }
                return Json(iorder.GetOrdersVerify(filter, orderfilter, GetRequestHeader.GetTraitHeaders("get/orders/verify")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 查询订单超媒体合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <returns></returns>
        [Route("api/v1/orderHypermedias")]
        public IHttpActionResult GetOrdersHypermedias([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_OrderFiltering orderfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (orderfilter == null)
                {
                    orderfilter = new common_Trait_OrderFiltering();
                }
                return Json(iorder.GetOrdersHypermedias(filter, orderfilter, GetRequestHeader.GetTraitHeaders("get/orderHypermedias")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 查询订单数量
        /// </summary>
        /// <param name="orderfilter"></param>
        /// <returns></returns>
        [Route("api/v1/orders/count")]
        public IHttpActionResult GetOrdersCount([FromUri]common_Trait_OrderFiltering orderfilter)
        {
            try
            {
                if (orderfilter == null)
                {
                    orderfilter = new common_Trait_OrderFiltering();
                }
                return Json(iorder.GetOrdersCount(orderfilter, GetRequestHeader.GetTraitHeaders("get/orders/count")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 根据订单 ID 读取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetOrder(string id)
        {
            try
            {
                return Json(iorder.GetOrder(id) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 获得订单历史状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/allStatusList")]
        public IHttpActionResult GetAllStatusList(string orderID)
        {
            try
            {
                return Json(iorder.GetAllStatusList(orderID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 修改订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderobj"></param>
        /// <returns></returns>
        public IHttpActionResult PatchOrder(string id,[FromBody]orderObj orderobj)
        {
            try
            {
                if (orderobj == null)
                {
                    orderobj = new orderObj();
                }
                return Json(iorder.PatchOrder(id, orderobj, GetRequestHeader.GetTraitHeaders("patch/orders/{orderID}")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 修改订单价格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderobj"></param>
        /// <returns></returns>negotiate
        [Route("api/v1/orders/{orderID}/negotiateAmount")]
        public IHttpActionResult PatchOrderPrice(string orderID, [FromBody]orderObj orderobj)
        {
            try
            {
                if (orderobj == null)
                {
                    orderobj = new orderObj();
                }
                return Json(iorder.PatchOrderPrice(orderID, orderobj, GetRequestHeader.GetTraitHeaders("patch/orders/{orderID}/negotiateAmount")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 获得订单所包含的推送服务
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/pushServices")]
        public IHttpActionResult GetPushServices(string orderID)
        {
            try
            {
                return Json(iorder.GetPushServices(orderID, GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/pushServices")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 草稿单确定服务
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/confirmService")]
        public IHttpActionResult PutConfirmService(string orderID, [FromBody]Common_Body commonBody)
        {
            try
            {
                if (commonBody == null)
                {
                    commonBody = new Common_Body();
                }
                return Json(iorder.PutConfirmService(orderID, commonBody.serviceID, GetRequestHeader.GetTraitHeaders("put/orders/{orderID}/confirmService")) ?? new object());
            }
            catch (Exception ex)
            {
                log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.OrdersController.confirmService");
                ilog.Error(ex.ToString());
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 提交评价
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="appraiseobj"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/appraise")]
        public IHttpActionResult PutAppraisee(string orderID, [FromBody]appraiseObj appraiseobj)
        {
            try
            {
                if (appraiseobj == null)
                {
                    appraiseobj = new appraiseObj();
                }
                return Json(iorder.PutAppraisee(orderID, appraiseobj, GetRequestHeader.GetTraitHeaders("put/orders/{orderID}/appraise")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 获得该订单的聊天人
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/linkMan")]
        public IHttpActionResult GetLinkMan(string orderID)
        {
            try
            {
                return Json(iorder.GetLinkMan(orderID, GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/linkMan")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 请求变更订单状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/currentStatus")]
        public IHttpActionResult PatchCurrentStatus(string orderID, [FromBody]Common_Body commonBody)
        {
            try
            {
                if (commonBody == null)
                {
                    commonBody = new Common_Body();
                }
                return Json(iorder.PatchCurrentStatus(orderID, commonBody.newStatus, GetRequestHeader.GetTraitHeaders("patch/orders/{orderID}/currentStatus")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 获得理赔状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="refundfilter"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/refunds")]
        public IHttpActionResult GetRefundStatus(string orderID, [FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_RefundFiltering refundfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (refundfilter == null)
                {
                    refundfilter = new common_Trait_RefundFiltering();
                }
                return Json(iorder.GetRefundStatus(orderID, filter, refundfilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 提交理赔动作
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="refundobj"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/refunds")]
        public IHttpActionResult PostRefundAction(string orderID, [FromBody]refundObj refundobj)
        {
            try
            {
                if (refundobj == null)
                {
                    refundobj = new refundObj();
                }
                return Json(iorder.PostRefundAction(orderID, refundobj, GetRequestHeader.GetTraitHeaders("post/orders/{orderID}/refunds")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 读取订单负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/forman")]
        public IHttpActionResult GetForman(string orderID)
        {
            try
            {
                return Json(iorder.GetForman(orderID) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 更改负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/forman")]
        public IHttpActionResult PatchForman(string orderID,[FromBody]assignObj assignobj)
        {
            try
            {
                //string strStaffID = "";
                //if (assignobj != null)
                //{
                //    strStaffID = assignobj.staffID;
                //}
                return Json(iorder.PatchForman(orderID, assignobj.staffID, GetRequestHeader.GetTraitHeaders("patch/orders/{orderID}/forman")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 指派负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="assignobj"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/forman")]
        public IHttpActionResult PostForman(string orderID, [FromBody]assignObj assignobj)
        {
            try
            {
                string strStaffID = "";
                if (assignobj != null)
                {
                    strStaffID = assignobj.staffID;
                }
                return Json(iorder.PostForman(orderID, strStaffID, GetRequestHeader.GetTraitHeaders("post/orders/{orderID}/forman")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 取消指派
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/forman/{staffID}")]
        public IHttpActionResult DeleteForman(string orderID, string staffID)
        {
            try
            {
                return Json(iorder.DeleteForman(orderID, staffID, GetRequestHeader.GetTraitHeaders("delete/orders/{orderID}/forman/{staffID}")) ?? new object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
