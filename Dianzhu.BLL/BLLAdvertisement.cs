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
        private IDALAdvertisement repo;
        private IDAL.IUnitOfWork iUnitOfWork;

        public BLLAdvertisement(IDALAdvertisement repo, IDAL.IUnitOfWork iUnitOfWork)
        {
            this.repo = repo;
            this.iUnitOfWork = iUnitOfWork;
        }
        
        public void Save(Advertisement ad)
        {
            iUnitOfWork.BeginTransaction();
            repo.Add(ad);
            iUnitOfWork.Commit();
        }
        public void Update(Advertisement ad)
        {
            iUnitOfWork.BeginTransaction();
            repo.Update(ad);
             
            iUnitOfWork.Commit();
        }
        public IEnumerable<Advertisement> GetADList(int pageIndex, int pageSize, out long totalRecords)
        {
            iUnitOfWork.BeginTransaction();

            var where = PredicateBuilder.True<Advertisement>();
          
               var list = repo.Find(where, pageIndex,pageSize ,out totalRecords).ToList();
            iUnitOfWork.Commit();
            return list;
           // return repo.GetADList(pageIndex, pageSize,out totalRecords);
        }

        public IList<Advertisement> GetADListForUseful()
        {
           
            iUnitOfWork.BeginTransaction();
            //ISpecification<Advertisement> advInPerildSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementInPeriod(DateTime.Now);
            //ISpecification<Advertisement> advUsefulSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementUseful(true);
            //ISpecification<Advertisement> specs = advInPerildSpec.And<Advertisement>(advUsefulSpec);

            // Expression < Func < int, bool>> f = i => i % 2 != 0;
            // f = f.Not().And(i => i > 0);
            Expression<Func<Advertisement, bool>> q = i => i.IsUseful&&i.EndTime>DateTime.Now&&i.StartTime<=DateTime.Now;
            // q= q.And(i => i.EndTime > DateTime.Now);
            var list = repo.Find(q).ToList();
           iUnitOfWork.Commit();
            return list;
        }

        public Advertisement GetByUid(Guid uid)
        {
            iUnitOfWork.BeginTransaction();
            var aa= repo.FindById(uid);
            iUnitOfWork.Commit();
                return aa;
        }
    }
}
