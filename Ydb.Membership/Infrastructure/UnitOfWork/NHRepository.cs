using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


using NHibernate;
using NHibernate.Linq;
using Ydb.Common.Domain;
using Ydb.Common.Repository;
using Ydb.Common.Specification;


namespace Ydb.Membership.Infrastructure
{
    public class NHRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {


        
        protected ISession session { get { return NhUnitOfWork.Current.Session; } }


        public void Add(TEntity t)
        {

            session.Save(t);


        }

        /// <summary>
        /// 仿DALBase.Save(T o, object id)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="id"></param>
        public void Add(TEntity t, TPrimaryKey id)
        {

            session.Save(t, id);

        }

        public void Delete(TEntity t)
        {

            session.Delete(t);

        }



        public TEntity FindById(TPrimaryKey identityId)
        {
            TEntity result;
            result = session.Get<TEntity>(identityId);


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



            var query = session.Query<TEntity>().Where(where);
            totalRecords = query.Count();

            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            result = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();



            return result;
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
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> where, string sortBy, bool ascending, int offset, TEntity baseone)
        {
            long totalRecord;
            return Find(where, 1, 999, out totalRecord, sortBy, ascending, offset, baseone);
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
            IQueryable<TEntity> query = session.Query<TEntity>().Where(where);
            totalRecords = query.Count();
            //排序
            if (!string.IsNullOrEmpty(sortBy))
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
            result = query.Skip(pageSize * (pageIndex - 1) + baseIndex).Take(pageSize).ToList();
            return result;
        }


        public long GetRowCount(Expression<Func<TEntity, bool>> where)
        {
            long totalRecords;




            var query = session.Query<TEntity>().Where(where);
            totalRecords = query.Count();



            return totalRecords;
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> where)
        {
            TEntity result;
            result = session.Query<TEntity>().Where(where).SingleOrDefault();
            return result;
        }

        public void Update(TEntity t)
        {


            session.Update(t);




        }

        public void SaveOrUpdate(TEntity t)
        {
            session.SaveOrUpdate(t);
        }




    }
}
