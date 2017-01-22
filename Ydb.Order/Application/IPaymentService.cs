using System;
using System.Collections.Generic;
using Ydb.Common;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel;
namespace Ydb.Order.Application
{
    public interface IPaymentService
    {
        void PayCallBack(enum_PayAPI payApi, string returnstr, string paymentId, string platformTradeNo);
         Payment ApplyPay(string orderId, enum_PayTarget payTarget);
        void RefundCallBack(enum_PayAPI payApi, string returnstr, string refundId, string platformTradeNo);
          IList<Payment> GetPays(TraitFilter filter, string payStatus, string payType, Guid orderID, Guid userID);
        long GetPaysCount(string payStatus, string payType, Guid orderID, Guid userID);
          Payment GetPay(Guid orderID, Guid payID, Guid userID);
        void Update(Payment payment);
        Payment GetOne(Guid id);
        decimal GetPayAmount(ServiceOrder order, enum_PayTarget payTarget);
    }
}