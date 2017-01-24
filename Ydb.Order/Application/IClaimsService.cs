using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IClaimsService
    {
        Claims GetOneByOrder(ServiceOrder order);
        void Save(Claims c);
        void Update(Claims c);
    }
}