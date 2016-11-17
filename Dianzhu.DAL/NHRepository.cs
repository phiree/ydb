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
using Ydb.Common.Specification;
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
           
                Session.Save(t,id);
                
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

        /// <summary>
        /// 根据baseID获取对象，用于截取数据
        /// </summary>
        /// <param name="strBaseID"></param>
        /// <returns></returns>
        public TEntity FindByBaseId(TPrimaryKey strBaseID)
        {
            TEntity result = null;
            try
            {
                result = FindById(strBaseID);
            }
            catch
            {
                result = null;
            }
            if (result == null)
            {
                throw new Exception("传入的filter.baseID没有找到数据！");
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


            
                var query = Session.Query<TEntity>().Where(where);
                totalRecords = query.Count();

                if (pageIndex <= 0)
                {
                    pageIndex = 1;
                }

           var result2 = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);//.ToList(); 
                


            return result2.ToList();
        }

        /// <summary>
        /// orderby and skip
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <param name="offset"></param>
        /// <param name="baseone"></param>
        /// <returns></returns>
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> where, string sortBy,bool ascending,int offset, TEntity baseone)
        {
            long totalRecord;
            return Find(where, 1, 999, out totalRecord, sortBy, ascending,offset, baseone);
        }
        /// <summary>
        /// orderby and skip
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <param name="offset"></param>
        /// <param name="baseone"></param>
        /// <returns></returns>
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords, string sortBy, bool ascending, int offset, TEntity baseone)
        {
            IList<TEntity> result;
            var query = Session.Query<TEntity>().Where(where);
            totalRecords = query.Count();
            //排序
            if ( !string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderBy(sortBy, ascending);
            }
            //跳过
            //query = query.Skip(offset);
            //从baseID开始
            int baseIndex = 0;
            if (baseone != null)
            {
                baseIndex = query.ToList().IndexOf(baseone) + 1;
            }
            //query = query.Skip(baseIndex);
            if (baseIndex < offset)
            {
                baseIndex = offset;
            }
            //分页
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            result = query.Skip(pageSize * (pageIndex - 1)+baseIndex).Take(pageSize).ToList();
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

            //NHibernateUtil.Initialize(result);
            //在这里改为立即加载没有作用
            //Session.Clear();

            return result;
        }

        public void Update(TEntity t)
        {
            

                Session.Update(t); 
             



        }

        public void SaveOrUpdate(TEntity t)
        {
            Session.SaveOrUpdate(t);
        }



       
    }
}
