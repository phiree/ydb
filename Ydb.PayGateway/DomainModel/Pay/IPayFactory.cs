using Ydb.Common;

namespace Ydb.PayGateway.DomainModel.Pay
{
    public interface IPayFactory
    {
        IPayRequest CreatePayAPI(enum_PayAPI payApi, decimal amount, string paymentId, string subject);
    }
}