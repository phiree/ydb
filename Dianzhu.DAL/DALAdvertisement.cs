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

        public IEnumerable<Advertisement> Find(ISpecification<Advertisement> specs)
        {
           return repo.Find(specs);
        }

        public IEnumerable<Advertisement> Find(Expression<Func<Advertisement, bool>> express)
        {
            return repo.Find(express);
        }

        public IEnumerable<Advertisement> Find(string where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Advertisement> Find(ISpecification<Advertisement> specs, int pageIndex, int pageSize, out long totalRecords)
        {
            return repo.Find(specs, pageIndex, pageSize, out totalRecords);
        }

        public IEnumerable<Advertisement> Find(string where,int pageIndex,int pageSize, out long totalRecords)
        {
            return repo.Find(where,pageIndex,pageSize, out totalRecords);
        }

        public Advertisement FindById(object identityId)
        {
            return repo.FindById(identityId);
        }

        public long GetRowCount(ISpecification<Advertisement> specs)
        {
            throw new NotImplementedException();
        }

        public long GetRowCount(string where)
        {
            return repo.GetRowCount(where);
        }
    }
}
