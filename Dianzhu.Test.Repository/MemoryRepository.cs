using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.IDAL;
using Dianzhu.Model;
namespace Dianzhu.Test.Repository
{
    public class MemoryRepository<T> : IRepository<T>
        where T : class
    {
        List<T> list = new List<T>();
        public MemoryRepository(List<T>  list)
        {
            this.list = list;
        }
        public void Add(T t)
        {
            list.Add(t);
        }

        public void Delete(T t)
        {
            list.Remove(t);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            var result = list.Where(where.Compile());
            return result;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            var query = list.Where(where.Compile());
            totalRecords = query.Count();
            var result=query.Skip(pageIndex * pageSize).Take(pageSize);
            return result;
        }

        public T FindById(object identityId)
        {
            throw new NotImplementedException();
        }

        public T FindOne(Expression<Func<T, bool>> where)
        {
            return list.Where(where.Compile()).SingleOrDefault();
        }

        public long GetRowCount(Expression<Func<T, bool>> where)
        {
            return list.Where(where.Compile()).Count();
        }
    }

    public class MemoryRepositoryArea:IRepository<Area>
    {
        MemoryRepository<Area> repoArea;
        public MemoryRepositoryArea(MemoryRepository<Area> repoArea)
        {
            this.repoArea = repoArea;
        }

        public void Add(Area t)
        {
            repoArea.Add(t);
        }

        public void Delete(Area t)
        {
            repoArea.Delete(t);
        }

        public IEnumerable<Area> Find(Expression<Func<Area, bool>> where)
        {
           return repoArea.Find(where);
        }

        public IEnumerable<Area> Find(Expression<Func<Area, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            return repoArea.Find(where, pageIndex, pageSize, out totalRecords);
        }

        public Area FindById(object identityId)
        {
            throw new NotImplementedException();
        }

        public Area FindOne(Expression<Func<Area, bool>> where)
        {
            return repoArea.FindOne(where);
        }

        public long GetRowCount(Expression<Func<Area, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}
