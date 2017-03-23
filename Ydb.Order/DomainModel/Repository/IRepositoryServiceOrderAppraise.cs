using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Repository;
using Ydb.Order.DomainModel;
namespace Ydb.Order.DomainModel.Repository
{

    public interface IRepositoryServiceOrderAppraise : IRepository<ServiceOrderAppraise, Guid>
    {
        /// <summary>
        /// 获取店铺的平均评价
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        decimal GetBusinessAverageAppraise(string businessId, string staffId);
    }
}
