using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALArea :IDAL.IRepository<Model.Area>
    {
        NHRepository<Model.Area> repo;
        public DALArea(NHRepository<Model.Area> repo)
        {
            this.repo = repo;
        }
 

        public void Add(Area t)
        {
            repo.Add(t);
        }

        public void Delete(Area t)
        {
            repo.Delete(t);
        }

        public Area FindById(object identityId)
        {
           return  repo.FindById(identityId);
        }

        public IEnumerable<Area> Find(Expression<Func<Area, bool>> where)
        {
            return repo.Find(where);
        }

        public IEnumerable<Area> Find(Expression<Func<Area, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            return repo.Find(where, pageIndex, pageSize, out totalRecords);
        }

        public long GetRowCount(Expression<Func<Area, bool>> where)
        {
            return repo.GetRowCount(where);
        }

        public Area FindOne(Expression<Func<Area, bool>> where)
        {
            return repo.FindOne(where);
        }

        public void Update(Area t)
        {
            repo.Update(t);
        }
    }
}
