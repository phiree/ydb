using Ydb.PayGateway.DomainModel;

namespace Ydb.PayGateway.Application
{
    public interface IPaymentLogService
    {
        void Save(PaymentLog log);
        void Update(PaymentLog log);
    }
}