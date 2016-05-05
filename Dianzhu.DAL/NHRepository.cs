using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DDDCommon;
using NHibernate;
using NHibernate.Linq;
using Dianzhu.IDAL;
using DDDCommon.Domain;
namespace Dianzhu.DAL
{
    public abstract class NHRepositoryBase<TEntity,TPrimaryKey> :  IRepository<TEntity, TPrimaryKey>
        where TEntity:Entity<TPrimaryKey>
    {

       
        private ISession session { get { return NHUnitOfWork.Current.Session; } }
      
        public void Add(TEntity t)
        {
            session.Save(t);
        }

        public void Delete(TEntity t)
        {
            session.Delete(t);
        }


        public TEntity FindById(TPrimaryKey identityId)
        {
            return session.Get<TEntity>(identityId);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where)
        {
            long totalRecord;
            return Find(where, 1, 999, out totalRecord);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            
            var query = session.Query<TEntity>().Where(where);
            totalRecords = query.Count();
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToFuture();
        }

     
        public long GetRowCount(Expression<Func<TEntity, bool>> where)
        {
            var query = session.Query<TEntity>().Where(where);
           long totalRecords = query.Count();
            return totalRecords;
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> where)
        {
            return  session.Query<TEntity>().Where(where).SingleOrDefault();
        }

        public void Update(TEntity t)
        {
              session.Update(t);
        }
    }
}
