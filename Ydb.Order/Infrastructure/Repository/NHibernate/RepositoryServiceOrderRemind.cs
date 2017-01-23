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
namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
   public class RepositoryServiceOrderRemind : NHRepositoryBase<ServiceOrderRemind, Guid>, IRepositoryServiceOrderRemind
    {
        public ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId)
        {
            return Session.QueryOver<ServiceOrderRemind>().Where(x => x.Id == Id).And(x => x.UserId == UserId).SingleOrDefault();
        }

        public IQueryOver<ServiceOrderRemind> GetList(Guid userId, DateTime startTime, DateTime endTime)
        {
            return Session.QueryOver<ServiceOrderRemind>().Where(x => x.UserId == userId).And(x => x.RemindTime >= startTime).And(x => x.RemindTime <= endTime);
        }

        public int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            var query = GetList(userId, startTime, endTime);
            return query.RowCount();
        }

        public IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            var query = GetList(userId, startTime, endTime);
            return query.List();
        }

    }
}
