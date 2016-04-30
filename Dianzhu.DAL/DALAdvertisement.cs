using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<Advertisement> Find(string where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Advertisement> Find(string where,int pageIndex,int pageSize, out long totalRecords)
        {
            return repo.Find(where,pageIndex,pageSize, out totalRecords);
        }

        public Advertisement FindById(object identityId)
        {
            return repo.FindById(identityId);
        }

        //注入依赖,供测试使用;


        public IList <Advertisement> GetADList(int pageIndex, int pageSize, out int totalRecord)
        {
            /*
            IQueryOver<Advertisement, Advertisement> iquery = Session.QueryOver<Advertisement>();
            totalRecord = iquery.ToRowCountQuery().FutureValue<int>().Value;
            return iquery.OrderBy(x => x.Num).Asc.Skip((pageIndex-1) * pageSize).Take(pageSize).List();
            */
            throw new NotImplementedException();
        }

        public IList<Advertisement> GetADListForUseful()
        {
            throw new NotImplementedException();
           // return Session.QueryOver<Advertisement>().Where(x => x.IsUseful == true).OrderBy(x => x.Num).Asc.List();
        }

        public Advertisement GetByUid(Guid uid)
        {
            return repo.FindById(uid);
        }
    }
}
