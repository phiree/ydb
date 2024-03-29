﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dianzhu.Model;

using Dianzhu.DAL;
using Ydb.Common;
 
using PHSuit;
using Newtonsoft.Json;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using Ydb.Common.Specification;

namespace Dianzhu.BLL
{

    /// <summary>
    /// 订单业务逻辑
    /// </summary>
    public interface IBLLServiceOrder
    {


        #region 基本操作


        int GetServiceOrderCount(Guid userId,  enum_OrderSearchType searchType);
        IList<ServiceOrder> GetServiceOrderList(Guid userId,  enum_OrderSearchType searchType, int pageNum, int pageSize);

        /// <summary>
        /// 查询订单合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="statusSort"></param>
        /// <param name="status"></param>
        /// <param name="storeID"></param>
        /// <param name="formanID"></param>
        /// <param name="afterThisTime"></param>
        /// <param name="beforeThisTime"></param>
        /// <param name="UserID"></param>
        /// <param name="userType"></param>
        /// <param name="strAssign"></param>
        /// <returns></returns>
        IList<ServiceOrder> GetOrders(TraitFilter filter, string statusSort, string status, Guid storeID, string formanID, DateTime afterThisTime, DateTime beforeThisTime, Guid UserID, string userType, string strAssign);

        /// <summary>
        /// 查询订单数量
        /// </summary>
        /// <param name="statusSort"></param>
        /// <param name="status"></param>
        /// <param name="storeID"></param>
        /// <param name="formanID"></param>
        /// <param name="afterThisTime"></param>
        /// <param name="beforeThisTime"></param>
        /// <param name="UserID"></param>
        /// <param name="userType"></param>
        /// <param name="strAssign"></param>
        /// <returns></returns>
        long GetOrdersCount(string statusSort, string status, Guid storeID, string formanID, DateTime afterThisTime, DateTime beforeThisTime, Guid UserID, string userType, string strAssign);

        /// <summary>
        /// 获取商户的一条订单
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        ServiceOrder GetOneOrder(Guid guid, string UserID);

        ServiceOrder GetOne(Guid guid);
        void Update(ServiceOrder order);
        IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords);

        IList<ServiceOrder> GetAllByOrderStatus(enum_OrderStatus status, int pageIndex, int pageSize, out long totalRecords);





        IList<ServiceOrder> GetListForBusiness(string businessId, int pageNum, int pageSize, out int totalAmount);

        IList<ServiceOrder> GetListForCustomer(Guid customerId, int pageNum, int pageSize, out int totalAmount);

        void Delete(ServiceOrder order);
        ServiceOrder GetDraftOrder(string cId, string  csId);
        IList<ServiceOrder> GetOrderListByDate(string serviceId, DateTime date);
        ServiceOrder GetOrderByIdAndCustomer(Guid Id, string customerId);
        #endregion

        #region 订单流程变化

        /// <summary>
        /// 用户确认订单：有订金生成支付项，没有订金直接变为已支付状态
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_ConfirmOrder(ServiceOrder order);

        /// <summary>
        /// 用户定金支付完成,等待后台确认订单是否到帐
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_PayDepositAndWaiting(ServiceOrder order);

        /// <summary>
        /// 后台确认订单到帐
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_ConfirmDeposit(ServiceOrder order);
        /// <summary>
        /// 商家确认订单,准备执行.
        /// </summary>
        void OrderFlow_BusinessConfirm(ServiceOrder order);
        /// <summary>
        /// 商家输入协议
        /// </summary>
        /// <param name="order"></param>
        /// <param name="negotiateAmount"></param>
        void OrderFlow_BusinessNegotiate(ServiceOrder order, decimal negotiateAmount);
        /// <summary>
        /// 商户已经提交新价格，等待用户确认
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_CustomConfirmNegotiate(ServiceOrder order);
        void OrderFlow_CustomerDisagreeNegotiate(ServiceOrder order);

        /// <summary>
        /// 用户确认协商价格,并确定开始服务
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_BusinessStartService(ServiceOrder order);
        /// <summary>
        /// 商家确定服务完成
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_BusinessFinish(ServiceOrder order);
        /// <summary>
        /// 用户确认服务完成。
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_CustomerFinish(ServiceOrder order);
        /// <summary>
        /// 用户支付尾款
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_CustomerPayFinalPayment(ServiceOrder order);

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_OrderFinished(ServiceOrder order);

        /// <summary>
        /// 订单评价
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_CustomerAppraise(ServiceOrder order);
        /// <summary>
        /// 订单已经分成.
        /// </summary>
        /// <param name="order"></param>
        void OrderShared(ServiceOrder order);
        /// <summary>
        /// 用户申请理赔
        /// </summary>
        /// <param name="order"></param>
        bool OrderFlow_CustomerRefund(ServiceOrder order, bool isNeedRefund, decimal refundAmount);

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_BusinessIsRefund(ServiceOrder order, string memberId);

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_RefundSuccess(ServiceOrder order);

        /// <summary>
        /// 商户要求支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_BusinessAskPayWithRefund(ServiceOrder order, string context, decimal amount, IList<string> resourcesUrl, string memberId);

        /// <summary>
        /// 商户驳回理赔请求
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_BusinessRejectRefund(ServiceOrder order, string memberId);

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_WaitingPayWithRefund(ServiceOrder order, string memberId);

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_CustomerPayRefund(ServiceOrder order);

        /// <summary>
        /// 一点办官方介入
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_YDBInsertIntervention(ServiceOrder order);

        /// <summary>
        /// 等待一点办官方处理
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_YDBHandleWithIntervention(ServiceOrder order);

        /// <summary>
        /// 一点办已确认理赔
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_YDBConfirmNeedRefund(ServiceOrder order);

        /// <summary>
        /// 一点办要求用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_NeedPayInternention(ServiceOrder order);

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_CustomerPayInternention(ServiceOrder order);

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_ConfirmInternention(ServiceOrder order);

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        void OrderFlow_ForceStop(ServiceOrder order);

        void OrderFlow_EndCancel(ServiceOrder order);

        //订单状态改变通用方法

        #endregion

        #region 订单取消
        /// <summary>
        /// 申请取消
        /// </summary>
        /// <param name="order"></param>
        bool OrderFlow_Canceled(ServiceOrder order);
        #endregion

        #region 分配工作人员
        void AssignStaff(ServiceOrder order, string staffId);
        void DeassignStaff(ServiceOrder order, string staffId);
        #endregion

        #region 申请退款
        bool ApplyRefund(Payment payment, decimal refundAmount, string refundReason);
        #endregion

        enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order, enum_OrderStatus status);
        int GetServiceOrderCountWithoutDraft(Guid userid, bool isCustomerService);
        decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService);

        //查询店铺的所有订单
        IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId);
        //查询全部已经完成的订单
        IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId);
        //查询订单的总金额
        //查询订单的曝光率.
        void Save(ServiceOrder order);
        //查询可以分账的订单
        IList<ServiceOrder> GetOrdersForShare();
        IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime dateBegin, DateTime dateEnd);



    }
}
