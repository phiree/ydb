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

        /// <summary>
        /// 获取店铺的平均评价
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public decimal GetBusinessAverageAppraise(string businessId)
        {
            return repoServiceOrderAppraise.GetBusinessAverageAppraise(businessId);
        }
    }

}
