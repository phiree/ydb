using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.IDAL
{
   public interface IRepository<T>
    {
        void Add(T t);
        void Delete(T t);
        T FindById(object identityId);
        IEnumerable<T> Find(string where,int pageIndex,int pageSize,out long totalRecords);
        IEnumerable<T> Find(string where);


    }
}
