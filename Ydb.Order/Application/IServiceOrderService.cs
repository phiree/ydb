using System;
using System.Collections.Generic;
using Ydb.Common;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IServiceOrderService
    {
        bool ApplyRefund(Payment payment, decimal refundAmount, string refundReason);
        bool ApplyRefund(Payment payment, decimal refundAmount, string refundReason, string operatorId);
        void AssignStaff(Guid orderId, string staffId);
        ServiceOrder CreateDraftOrder(string customerSericeId, string customerId);
        void DeassignStaff(Guid orderId, string staffId);
        void Delete(Guid orderId);
        IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords);
        IList<ServiceOrder> GetAllByOrderStatus(enum_OrderStatus status, int pageIndex, int pageSize, out long totalRecords);
        IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId);
        IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId);
        ServiceOrder GetDraftOrder(string cId, string csId);
        IList<ServiceOrder> GetListForBusiness(string businessId, int pageNum, int pageSize, out int totalAmount);
        IList<ServiceOrder> GetListForCustomer(Guid customerId, int pageNum, int pageSize, out int totalAmount);
        ServiceOrder GetOne(Guid guid);
        ServiceOrder GetOneOrder(Guid guid, string UserID);
        ServiceOrder GetOrderByIdAndCustomer(Guid Id, string customerId);
        IList<ServiceOrder> GetOrderListByDate(string serviceId, DateTime date);
        IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime timeBegin, DateTime timeEnd);
        IList<ServiceOrder> GetOrders(TraitFilter filter, string statusSort, string status, Guid storeID, string formanID, DateTime afterThisTime, DateTime beforeThisTime, Guid UserID, string userType, string strAssign);
        long GetOrdersCount(string statusSort, string status, Guid storeID, string formanID, DateTime afterThisTime, DateTime beforeThisTime, Guid UserID, string userType, string strAssign);
        IList<ServiceOrder> GetOrdersForShare();
        enum_OrderStatus GetOrderStatusPrevious(Guid orderId, enum_OrderStatus status);
        decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService);
        int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType);
        int GetServiceOrderCountWithoutDraft(Guid userid, bool isCustomerService);
        IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize);
        void OrderFlow_BusinessAskPayWithRefund(Guid orderId, string context, decimal amount, IList<string> resourcesUrl, string memberId);
        void OrderFlow_BusinessConfirm(Guid orderId);
        void OrderFlow_BusinessFinish(Guid orderId);
        void OrderFlow_BusinessIsRefund(Guid orderId, string memberId);
        void OrderFlow_BusinessNegotiate(Guid orderId, decimal negotiateAmount);
        void OrderFlow_BusinessRejectRefund(Guid orderId, string memberId);
        void OrderFlow_BusinessStartService(Guid orderId);
        bool OrderFlow_Canceled(Guid orderId);
        void OrderFlow_ConfirmDeposit(Guid orderId);
        void OrderFlow_ConfirmInternention(Guid orderId);
        void OrderFlow_ConfirmOrder(Guid orderId, string serviceId);
        void OrderFlow_CustomConfirmNegotiate(Guid orderId);
        void OrderFlow_CustomerAppraise(Guid orderId);
        void OrderFlow_CustomerDisagreeNegotiate(Guid orderId);
        void OrderFlow_CustomerFinish(Guid orderId);
        void OrderFlow_CustomerPayFinalPayment(Guid orderId);
        void OrderFlow_CustomerPayInternention(Guid orderId);
        void OrderFlow_CustomerPayRefund(Guid orderId);
        bool OrderFlow_CustomerRefund(string orderId, bool isNeedRefund, decimal refundAmount);
        void OrderFlow_RefundSuccess(Guid orderId);

        void OrderFlow_EndCancel(Guid orderId);
        void OrderFlow_ForceStop(Guid orderId);
        void OrderFlow_NeedPayInternention(Guid orderId);
        void OrderFlow_OrderFinished(Guid orderId);
        void OrderFlow_PayDepositAndWaiting(Guid orderId);
        void OrderFlow_WaitingPayWithRefund(Guid orderId, string memberId);
        void OrderFlow_YDBConfirmNeedRefund(Guid orderId);
        void OrderFlow_YDBHandleWithIntervention(Guid orderId);
        void OrderFlow_YDBInsertIntervention(Guid orderId);
        void OrderShared(Guid orderId);
        void Save(ServiceOrder order);
        void Update(ServiceOrder order);

        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单数量，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        long GetOrdersCountByBusinessList(IList<string> businessIdList, bool isShared);

        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单列表，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        IList<ServiceOrder> GetOrdersByBusinessList(IList<string> businessIdList, bool isShared);
    }
}