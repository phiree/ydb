using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
namespace Dianzhu.DAL
{
    public class NHRepository<T> : IDAL.IRepository<T>
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
    }
}
