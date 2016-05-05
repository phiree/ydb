using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DDDCommon;
using NHibernate;
using NHibernate.Linq;
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


        public T FindById(object identityId)
        {
            return session.Get<T>(identityId);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            long totalRecord;
            return Find(where, 1, 999, out totalRecord);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            
            var query = session.Query<T>().Where(where);
            totalRecords = query.Count();
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToFuture();
        }

     
        public long GetRowCount(Expression<Func<T, bool>> where)
        {
            var query = session.Query<T>().Where(where);
           long totalRecords = query.Count();
            return totalRecords;
        }

        public T FindOne(Expression<Func<T, bool>> where)
        {
            return  session.Query<T>().Where(where).SingleOrDefault();
        }

        public void Update(T t)
        {
              session.Update(t);
        }
    }
}
