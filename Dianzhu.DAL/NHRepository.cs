using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DDDCommon;
using NHibernate;
namespace Dianzhu.DAL
{
    public class NHRepository<T> : IDAL.IRepository<T> 
        where T :class
    {

        private NHUnitOfWork _unitOfWork;
        private ISession session { get { return _unitOfWork.Session; } }
        public NHRepository(IDAL.IUnitOfWork iUnitOfWork)
        {
            this._unitOfWork = (NHUnitOfWork)iUnitOfWork;
        }
        public void Add(T t)
        {
            session.Save(t);
        }

        public void Delete(T t)
        {
            session.Delete(t);
        }

        public IEnumerable<T> Find(string where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(string where, int pageIndex, int pageSize, out long totalRecords)
        {

            string where_rowcount = PHSuit.StringHelper.BuildCountQuery(where);
            IQuery query_RowCount = session.CreateQuery(where_rowcount);
            totalRecords = query_RowCount.FutureValue<long>().Value;
            IQuery query = session.CreateQuery(where);
            return query.Enumerable<T>();


        }

        public T FindById(object identityId)
        {
            return session.Get<T>(identityId);
        }

        public long GetRowCount(string where)
        {
            string where_rowcount = PHSuit.StringHelper.BuildCountQuery(where);
            IQuery query_RowCount = session.CreateQuery(where_rowcount);
            long totalRecords = query_RowCount.FutureValue<long>().Value;
            return totalRecords;
        }

        public IEnumerable<T> Find(ISpecification<T> specs, int pageIndex, int pageSize, out long totalRecords)
        {
            var query = session.QueryOver<T>().Where(specs.SpecExpression);
            totalRecords = query.RowCountInt64();
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).Future();
        }

        public IEnumerable<T> Find(ISpecification<T> specs)
        {
            var query = session.QueryOver<T>().Where(specs.SpecExpression);
            return query.Future();
        }

        public long GetRowCount(ISpecification<T> specs)
        {
            var query = session.QueryOver<T>().Where(specs.SpecExpression);
           long totalRecords = query.RowCountInt64();
            return totalRecords;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> express)
        {
            var query = session.QueryOver<T>().Where(express);
            return query.Future();
        }
    }
}
