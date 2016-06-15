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
    public abstract class NHRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {

        protected ISession Session
        {
            get
            {
                return new HybridSessionBuilder().GetSession();
            }
            ////    get { return NHUnitOfWork.Current.Session; }

        }

        public void Add(TEntity t)
        {
            using(var tr=Session.BeginTransaction())
            { 
            Session.Save(t);
                tr.Commit();
            }

        }

        public void Delete(TEntity t)
        {
            using (var tr = Session.BeginTransaction())
            {
                Session.Delete(t);
            }
        }



        public TEntity FindById(TPrimaryKey identityId)
        {
            TEntity result;


            using (var tr = Session.BeginTransaction())
            {

                result = Session.Get<TEntity>(identityId); 
                tr.Commit();
            }




            return result;

        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> where)
        {
            long totalRecord;
            return Find(where, 1, 999, out totalRecord);
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            IList<TEntity> result;


            using (var tr = Session.BeginTransaction())
            {

                var query = Session.Query<TEntity>().Where(where);
                totalRecords = query.Count();
                result = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(); 
                tr.Commit();
            }


            return result;
        }


        public long GetRowCount(Expression<Func<TEntity, bool>> where)
        {
            long totalRecords;


            using (var tr = Session.BeginTransaction())
            {

                var query = Session.Query<TEntity>().Where(where);
                totalRecords = query.Count();   
                tr.Commit();
            }


            return totalRecords;
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> where)
        {
            TEntity result;


            using (var tr = Session.BeginTransaction())
            {

                result = Session.Query<TEntity>().Where(where).SingleOrDefault(); 
                tr.Commit();
            }


            return result;
        }

        public void Update(TEntity t)
        {


            using (var tr = Session.BeginTransaction())
            {

                Session.Merge(t); 
                tr.Commit();
            }



        }

        public void SaveList(IList<TEntity> list)
        {
            foreach (TEntity t in list)
            {
                Add(t);
            }
        }
    }
}
