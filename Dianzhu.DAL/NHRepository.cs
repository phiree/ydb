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

       
        protected ISession Session { get { return NHUnitOfWork.Current.Session; } }
      
        public void Add(TEntity t)
        {

                Session.Save(t);
               
            

        }

        public void Delete(TEntity t)
        {
            using (var tra = Session.BeginTransaction())
            {
                Session.Delete(t); tra.Commit();
            }
        }


        public TEntity FindById(TPrimaryKey identityId)
        {
            
                var result= Session.Get<TEntity>(identityId);
              
                return result;
            
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where)
        {
            long totalRecord;
            return Find(where, 1, 999, out totalRecord);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            
            var query = Session.Query<TEntity>().Where(where);
            totalRecords = query.Count();
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToFuture();
        }

     
        public long GetRowCount(Expression<Func<TEntity, bool>> where)
        {
            var query = Session.Query<TEntity>().Where(where);
           long totalRecords = query.Count();
            return totalRecords;
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> where)
        {
            return  Session.Query<TEntity>().Where(where).SingleOrDefault();
        }

        public void Update(TEntity t)
        {
              Session.Update(t);
        }
    }
}
