using Ydb.PayGateway.DomainModel;

namespace Ydb.PayGateway.Application
{
    public interface IRefundLogService
    {
        void Save(PaymentLog log);
        void Update(PaymentLog log);
    }
}