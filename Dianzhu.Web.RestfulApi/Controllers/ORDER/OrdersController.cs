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
                return Json(iorder.GetOrders(filter, orderfilter));
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
        [Route("api/orders/count")]
        public IHttpActionResult GetOrdersCount([FromUri]common_Trait_OrderFiltering orderfilter)
        {
            try
            {
                if (orderfilter == null)
                {
                    orderfilter = new common_Trait_OrderFiltering();
                }
                return Json(iorder.GetOrdersCount(orderfilter));
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
                return Json(iorder.GetOrder(id));
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
        [Route("api/orders/{orderID}/allStatusList")]
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
                return Json(iorder.PatchOrder(id, orderobj));
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
        [Route("api/orders/{orderID}/confirmService")]
        public IHttpActionResult PutConfirmService(string orderID, [FromBody]string serviceID)
        {
            try
            {
                return Json(iorder.PutConfirmService(orderID,serviceID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 提交评价
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="appraiseobj"></param>
        /// <returns></returns>
        [Route("api/orders/{orderID}/appraise")]
        public IHttpActionResult PutAppraisee(string orderID, [FromBody]appraiseObj appraiseobj)
        {
            try
            {
                if (appraiseobj == null)
                {
                    appraiseobj = new appraiseObj();
                }
                return Json(iorder.PutAppraisee(orderID, appraiseobj));
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
        [Route("api/orders/{orderID}/linkMan")]
        public IHttpActionResult GettLinkMan(string orderID)
        {
            try
            {
                return Json(iorder.GettLinkMan(orderID));
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
        [Route("api/orders/{orderID}/currentStatus")]
        public IHttpActionResult PatchCurrentStatus(string orderID, [FromBody]string newStatus)
        {
            try
            {
                return Json(iorder.PatchCurrentStatus(orderID, newStatus));
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
        [Route("api/orders/{orderID}/refunds")]
        public IHttpActionResult GetRefundStatus(string orderID, [FromUri]common_Trait_RefundFiltering refundfilter)
        {
            try
            {
                if (refundfilter == null)
                {
                    refundfilter = new common_Trait_RefundFiltering();
                }
                return Json(iorder.GetRefundStatus(orderID, refundfilter));
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
        [Route("api/orders/{orderID}/refunds")]
        public IHttpActionResult PostRefundAction(string orderID, [FromBody]refundObj refundobj)
        {
            try
            {
                if (refundobj == null)
                {
                    refundobj = new refundObj();
                }
                return Json(iorder.PostRefundAction(orderID, refundobj));
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
        [Route("api/orders/{orderID}/forman")]
        public IHttpActionResult GetForman(string orderID)
        {
            try
            {
                return Json(iorder.GetForman(orderID));
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
        [Route("api/orders/{orderID}/forman")]
        public IHttpActionResult PatchForman(string orderID,[FromUri]string staffID)
        {
            try
            {
                return Json(iorder.PatchForman(orderID, staffID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

    }
}
