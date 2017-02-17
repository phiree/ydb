using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
namespace Ydb.Order.Application
{
    public class ServiceOrderAppraiseService : IServiceOrderAppraiseService
    {
      IRepositoryServiceOrderAppraise   repoServiceOrderAppraise;

        public ServiceOrderAppraiseService(IRepositoryServiceOrderAppraise repoServiceOrderAppraise)
        {
            this.repoServiceOrderAppraise = repoServiceOrderAppraise;
        }

        public void Save(ServiceOrderAppraise appraise)
        {
            repoServiceOrderAppraise.Add(appraise);
        }
    }

}
