using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ydb.Common.Domain;
namespace Ydb.Common.Repository
{    

    public interface IRepository { }
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        void Add(TEntity t);

        void Delete(TEntity t);
        void Update(TEntity t);

        /// <summary>
        /// 仿DALBase.Save(T o, object id)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="id"></param>
        void Add(TEntity t, TPrimaryKey id);

        TEntity FindById(TPrimaryKey identityId);

        /// <summary>
        /// 根据baseID获取对象，用于截取数据
        /// </summary>
        /// <param name="strBaseID"></param>
        /// <returns></returns>
        TEntity FindByBaseId(TPrimaryKey strBaseID);

        TEntity FindOne(Expression<Func<TEntity, bool>> where);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords);
        long GetRowCount(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 仿DALBase. void SaveOrUpdate(T o)
        /// </summary>
        /// <param name="t"></param>
        void SaveOrUpdate(TEntity t);

        /// <summary>
        /// orderby and skip
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <param name="offset"></param>
        /// <param name="baseone"></param>
        /// <returns></returns>
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where, string sortBy, bool ascending, int offset, TEntity baseone);
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
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords, string sortBy, bool ascending, int offset, TEntity baseone);


        //IList<object[]> SelectObject(string strHQL);
    }
}
