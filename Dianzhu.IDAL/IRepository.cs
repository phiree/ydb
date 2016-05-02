using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDDCommon;
namespace Dianzhu.IDAL
{
   public interface IRepository<T> where T :class
    {
        void Add(T t);
        void Delete(T t);
        T FindById(object identityId);
        IEnumerable<T> Find(string where,int pageIndex,int pageSize,out long totalRecords);
        IEnumerable<T> Find(string where);
        long GetRowCount(string where);
        IEnumerable<T> Find(DDDCommon.ISpecification<T> specs, int pageIndex, int pageSize, out long totalRecords);
        IEnumerable<T> Find(DDDCommon.ISpecification<T> specs);
        long GetRowCount(ISpecification<T> specs);
        IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T,bool>> express);

    }
}
