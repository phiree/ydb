using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IServiceOrderAppraiseService
    {
        void Save(ServiceOrderAppraise appraise);
        /// <summary>
        /// 获取店铺的平均评价
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        decimal GetBusinessAverageAppraise(string businessId);
    }
}