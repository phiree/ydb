using Ydb.Common;
using Ydb.Order.DomainModel;
namespace Ydb.Order.Application
{
    public interface IPaymentService
    {
        void PayCallBack(enum_PayAPI payApi, string returnstr, string paymentId, string platformTradeNo);
         Payment ApplyPay(string orderId, enum_PayTarget payTarget);


    }
}