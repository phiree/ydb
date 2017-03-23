using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
using Ydb.Common;
using Ydb.Common.Specification;
namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
   public class RepositoryServiceOrderAppraise : NHRepositoryBase<ServiceOrderAppraise, Guid>, IRepositoryServiceOrderAppraise
    {
        /// <summary>
        /// 获取店铺的平均评价
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public decimal GetBusinessAverageAppraise(string businessId)
        {
            var where = PredicateBuilder.True<ServiceOrderAppraise>();
            where = where.And(x => x.Order.BusinessId== businessId);
            var list = Find(where);
            decimal d = list.Count==0?5:list.Average(x => x.Value);
            return Math.Ceiling(d);
        }
    }
}
