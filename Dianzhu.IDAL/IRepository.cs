using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DDDCommon;
namespace Dianzhu.IDAL
{
    public interface IRepository<T> where T : class
    {
        void Add(T t);
        void Delete(T t);
        void Update(T t);
        T FindById(object identityId);

        T FindOne(Expression<Func<T, bool>> where);
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        IEnumerable<T> Find(Expression<Func<T, bool>> where, int pageIndex, int pageSize, out long totalRecords);
        long GetRowCount(Expression<Func<T, bool>> where);


    }
}
