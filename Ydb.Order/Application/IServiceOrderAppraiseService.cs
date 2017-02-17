using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IServiceOrderAppraiseService
    {
        void Save(ServiceOrderAppraise appraise);
    }
}