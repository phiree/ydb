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
        TEntity FindById(TPrimaryKey identityId);

        TEntity FindOne(Expression<Func<TEntity, bool>> where);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, out long totalRecords);
        long GetRowCount(Expression<Func<TEntity, bool>> where);


    }
}
