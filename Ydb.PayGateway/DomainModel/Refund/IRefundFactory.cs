using System;
using Ydb.Common;

namespace Ydb.PayGateway
{
    public interface IRefundFactory
    {
        IRefundApi CreateRefund(enum_PayAPI payApi, Guid orderRefundId, decimal refundAmount, decimal paymentTotalAmount, string platformTradeNo, string operatorId);
    }
}