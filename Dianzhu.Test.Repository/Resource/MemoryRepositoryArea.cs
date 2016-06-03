
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using System.Linq.Expressions;
namespace Dianzhu.Test.Repository.Resource
{

    public class MemoryRepositoryArea : IRepository<Area>
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
            return repoArea.FindById(identityId);
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
