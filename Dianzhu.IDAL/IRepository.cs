using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DDDCommon.Domain;
namespace Dianzhu.IDAL
{
    public interface IRepository { }
    public interface IRepository<TEntity, TPrimaryKey>:IRepository where TEntity : Entity<TPrimaryKey>
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

        TEntity FindOne(Expression<Func<TEntity, bool>> where);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords);
        long GetRowCount(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 仿DALBase. void SaveOrUpdate(T o)
        /// </summary>
        /// <param name="t"></param>
        void SaveOrUpdate(TEntity t);


    }
}
