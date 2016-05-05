using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DDDCommon;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    /// <summary>
    /// nhibernate implenmenting
    /// </summary>
    public class DALAdvertisement:IDAL.IRepository<Advertisement>
    {
        NHRepository<Advertisement> repo;
         public DALAdvertisement(NHRepository<Advertisement> repo)
        {
            this.repo = repo;
        }

        public void Add(Advertisement t)
        {
            repo.Add(t);
        }

        public void Delete(Advertisement t)
        {
            repo.Delete(t);
        }
        public Advertisement FindById(object identityId)
        {
            return repo.FindById(identityId);
        }


        public IEnumerable<Advertisement> Find(Expression<Func<Advertisement, bool>> where)
        {
            return repo.Find(where);
        }
        
        public long GetRowCount(Expression<Func<Advertisement, bool>> where)
        {
            return repo.GetRowCount(where);
        }

        public IEnumerable<Advertisement> Find(Expression<Func<Advertisement, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            return repo.Find(where, pageIndex, pageSize, out totalRecords);
        }

        public Advertisement FindOne(Expression<Func<Advertisement, bool>> where)
        {
            return repo.FindOne(where);
        }

        public void Update(Advertisement t)
        {
            repo.Update(t);
        }
    }
}
