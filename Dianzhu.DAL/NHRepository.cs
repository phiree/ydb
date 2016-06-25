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


            
                Session.Update(t); 
              



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
