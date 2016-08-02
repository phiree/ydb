using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Order
{
    public interface IOrderService
    {
        /// <summary>
        /// 根据orderID获取Order
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        orderObj GetOne(Guid guid);

        /// <summary>
        /// 查询订单合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IList<orderObj> GetOrders(common_Trait_Filtering filter, common_Trait_OrderFiltering orderfilter, common_Trait_Headers headers);

        /// <summary>
        /// 查询订单数量
        /// </summary>
        /// <param name="orderfilter"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        countObj GetOrdersCount(common_Trait_OrderFiltering orderfilter, common_Trait_Headers headers);

        /// <summary>
        /// 根据订单 ID 读取订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        orderObj GetOrder(string orderID);

        /// <summary>
        /// 获得订单历史状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        IList<orderStatusObj> GetAllStatusList(string orderID);

        /// <summary>
        /// 修改订单信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderobj"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        orderObj PatchOrder(string orderID, orderObj orderobj, common_Trait_Headers headers);

        /// <summary>
        /// 获得订单所包含的推送服务
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        IList<servicesObj> GetPushServices(string orderID);

        /// <summary>
        /// 草稿单确定服务
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        orderObj PutConfirmService(string orderID, string serviceID);

        /// <summary>
        /// 提交评价
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="appraiseobj"></param>
        /// <returns></returns>
        orderObj PutAppraisee(string orderID, appraiseObj appraiseobj);

        /// <summary>
        /// 获得该订单的聊天人
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        string GetLinkMan(string orderID);

        /// <summary>
        /// 请求变更订单状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        object PatchCurrentStatus(string orderID, string newStatus);

        /// <summary>
        /// 获得理赔状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="refundfilter"></param>
        /// <returns></returns>
        IList<refundStatusObj> GetRefundStatus(string orderID, common_Trait_Filtering filter, common_Trait_RefundFiltering refundfilter);


        /// <summary>
        /// 提交理赔动作
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="refundobj"></param>
        /// <returns></returns>
        refundStatusObj PostRefundAction(string orderID, refundObj refundobj);

        /// <summary>
        /// 读取订单负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
         staffObj GetForman(string orderID);

        /// <summary>
        /// 更改负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        staffObj PatchForman(string orderID, string staffID);
    }
}
