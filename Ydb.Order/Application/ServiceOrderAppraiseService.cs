using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
namespace Ydb.Order.Application
{
    public class BLLServiceOrderAppraise
    {
      IRepositoryServiceOrderAppraise   repoServiceOrderAppraise;

        public BLLServiceOrderAppraise(IRepositoryServiceOrderAppraise repoServiceOrderAppraise)
        {
            this.repoServiceOrderAppraise = repoServiceOrderAppraise;
        }

        public void Save(ServiceOrderAppraise appraise)
        {
            repoServiceOrderAppraise.Add(appraise);
        }
    }

}
