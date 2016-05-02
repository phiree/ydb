using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
using System.Linq.Expressions;
using DDDCommon;
namespace Dianzhu.BLL
{
    public class BLLAdvertisement
    {
        private IDAL.IRepository<Advertisement> repo;
        private IDAL.IUnitOfWork iUnitOfWork;

        public BLLAdvertisement(IDAL.IRepository<Advertisement> repo, IDAL.IUnitOfWork iUnitOfWork)
        {
            this.repo = repo;
            this.iUnitOfWork = iUnitOfWork;
        }
        public void SaveOrUpdate(Advertisement ad)
        {
           // DALAdvertisement.SaveOrUpdate(ad);
        }
 
        public IEnumerable<Advertisement> GetADList(int pageIndex, int pageSize, out long totalRecords)
        {
            iUnitOfWork.BeginTransaction();
            DDDCommon.ISpecification<Advertisement> advInPerildSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementInPeriod(DateTime.Now);
            ISpecification<Advertisement> advUsefulSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementUseful(true);
            ISpecification<Advertisement> specs = advInPerildSpec.And<Advertisement>(advUsefulSpec);
            var list = repo.Find(specs, pageIndex,pageSize ,out totalRecords);
            iUnitOfWork.Commit();
            return list;
            //return repo.GetADList(pageIndex, pageSize,out totalRecords);
        }

        public IList<Advertisement> GetADListForUseful()
        {
            long totalRecords;
            iUnitOfWork.BeginTransaction();
            ISpecification<Advertisement> advInPerildSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementInPeriod(DateTime.Now);
            ISpecification<Advertisement> advUsefulSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementUseful(true);
            ISpecification<Advertisement> specs = advInPerildSpec.And<Advertisement>(advUsefulSpec);

            // Expression < Func < int, bool>> f = i => i % 2 != 0;
            // f = f.Not().And(i => i > 0);
            Expression<Func<Advertisement, bool>> q = i => i.IsUseful;
             q= q.And(i => i.EndTime > DateTime.Now);
            var list = repo.Find(q).ToList();
            iUnitOfWork.Commit();
            return list;
        }

        public Advertisement GetByUid(Guid uid)
        {
            return repo.FindById(uid);
        }
    }
}
