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
             //   return new Dianzhu.DAL_Hyber.HybridSessionBuilder().GetSession();
              //  return DianzhuUW.Session;
                   return NHibernateUnitOfWork.UnitOfWork.CurrentSession;
            }
            ////    get { return NHUnitOfWork.Current.Session; }

        }

        public void Add(TEntity t)
        {
             
            Session.Save(t);
                

        }

        /// <summary>
        /// 仿DALBase.Save(T o, object id)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="id"></param>
        public void Add(TEntity t, TPrimaryKey id)
        {
            using (var tr = Session.BeginTransaction())
            {
                Session.Save(t,id);
                tr.Commit();
            }

        }

        public void Delete(TEntity t)
        {
            
                Session.Delete(t);
            
        }



        public TEntity FindById(TPrimaryKey identityId)
        {
            TEntity result;


           

                result = Session.Get<TEntity>(identityId); 
               



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


            
                var query = Session.Query<TEntity>().Where(where);
                totalRecords = query.Count();

                if (pageIndex <= 0)
                {
                    pageIndex = 1;
                }

                result = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(); 
                


            return result;
        }


        public long GetRowCount(Expression<Func<TEntity, bool>> where)
        {
            long totalRecords;


            

                var query = Session.Query<TEntity>().Where(where);
                totalRecords = query.Count();   
                


            return totalRecords;
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> where)
        {
            TEntity result;


            

                result = Session.Query<TEntity>().Where(where).SingleOrDefault(); 
                
            

            return result;
        }

        public void Update(TEntity t)
        {
            using (var tr = Session.BeginTransaction())
            {

                Session.Update(t); 
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

        /// <summary>
        /// 仿DALBase. void SaveOrUpdate(T o)
        /// </summary>
        /// <param name="t"></param>
        public void SaveOrUpdate(TEntity t)
        {
            if (Session.Transaction.IsActive)
            { Session.SaveOrUpdate(t); }
            else
            {
                using (var t1 = Session.BeginTransaction())
                {
                    Session.SaveOrUpdate(t); t1.Commit();

                }
            }

        }
    }
}
