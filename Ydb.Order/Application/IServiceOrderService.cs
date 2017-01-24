using System;
using System.Collections.Generic;
using Ydb.Common;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IServiceOrderService
    {
        //执行退款
        bool ApplyRefund(Payment payment, decimal refundAmount, string refundReason,string operatorId);
        bool ApplyRefund(Payment payment, decimal refundAmount, string refundReason);

        void AssignStaff(ServiceOrder order, string staffId);
        void DeassignStaff(ServiceOrder order, string staffId);
        void Delete(ServiceOrder order);
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
        enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order, enum_OrderStatus status);
        decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService);
        int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType);
        int GetServiceOrderCountWithoutDraft(Guid userid, bool isCustomerService);
        IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize);
        void OrderFlow_BusinessAskPayWithRefund(ServiceOrder order, string context, decimal amount, IList<string> resourcesUrl, string memberId);
        void OrderFlow_BusinessConfirm(ServiceOrder order);
        void OrderFlow_BusinessFinish(ServiceOrder order);
        void OrderFlow_BusinessIsRefund(ServiceOrder order, string memberId);
        void OrderFlow_BusinessNegotiate(ServiceOrder order, decimal negotiateAmount);
        void OrderFlow_BusinessRejectRefund(ServiceOrder order, string memberId);
        void OrderFlow_BusinessStartService(ServiceOrder order);
        bool OrderFlow_Canceled(ServiceOrder order);
        void OrderFlow_ConfirmDeposit(ServiceOrder order);
        void OrderFlow_ConfirmInternention(ServiceOrder order);
          void OrderFlow_ConfirmOrder(ServiceOrder order, string serviceId );
        void OrderFlow_CustomConfirmNegotiate(ServiceOrder order);
        void OrderFlow_CustomerAppraise(ServiceOrder order);
        void OrderFlow_CustomerDisagreeNegotiate(ServiceOrder order);
        void OrderFlow_CustomerFinish(ServiceOrder order);
        void OrderFlow_CustomerPayFinalPayment(ServiceOrder order);
        void OrderFlow_CustomerPayInternention(ServiceOrder order);
        void OrderFlow_CustomerPayRefund(ServiceOrder order);
        bool OrderFlow_CustomerRefund(string orderId, bool isNeedRefund, decimal refundAmount);
        void OrderFlow_EndCancel(ServiceOrder order);
        void OrderFlow_ForceStop(ServiceOrder order);
        void OrderFlow_NeedPayInternention(ServiceOrder order);
        void OrderFlow_OrderFinished(ServiceOrder order);
        void OrderFlow_PayDepositAndWaiting(ServiceOrder order);
        void OrderFlow_RefundSuccess(ServiceOrder order);
        void OrderFlow_WaitingPayWithRefund(ServiceOrder order, string memberId);
        void OrderFlow_YDBConfirmNeedRefund(ServiceOrder order);
        void OrderFlow_YDBHandleWithIntervention(ServiceOrder order);
        void OrderFlow_YDBInsertIntervention(ServiceOrder order);
        void OrderShared(ServiceOrder order);
        void Save(ServiceOrder order);
        void Update(ServiceOrder order);
    }
}