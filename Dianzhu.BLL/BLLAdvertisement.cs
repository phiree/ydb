using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
using System.Linq.Expressions;
using DDDCommon;
using Dianzhu.IDAL;
namespace Dianzhu.BLL
{
    public class BLLAdvertisement
    {
        private IDALAdvertisement repo;
       // IUnitOfWork iuow;
        public BLLAdvertisement(IDALAdvertisement repo )
        {
            this.repo = repo;
           // this.iuow = iuow;
         //   this.iUnitOfWork = iUnitOfWork;
        }
        
        public void Save(Advertisement ad)
        {
           // iuow.BeginTransaction();
            repo.Add(ad);
           // iuow.Commit();
        }
        public void Update(Advertisement ad)
        {
           // iuow.BeginTransaction();
            repo.Update(ad);

           // iuow.Commit();
        }
        public IEnumerable<Advertisement> GetADList(int pageIndex, int pageSize, out long totalRecords)
        {
           // iuow.BeginTransaction();
            var where = PredicateBuilder.True<Advertisement>();
          
               var list = repo.Find(where, pageIndex,pageSize ,out totalRecords).OrderByDescending(x=>x.IsUseful).ToList();
           // iuow.Commit();
            return list;
           // return repo.GetADList(pageIndex, pageSize,out totalRecords);
        }

        public IList<Advertisement> GetADListForUseful()
        {
           
          //ISpecification<Advertisement> advInPerildSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementInPeriod(DateTime.Now);
            //ISpecification<Advertisement> advUsefulSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementUseful(true);
            //ISpecification<Advertisement> specs = advInPerildSpec.And<Advertisement>(advUsefulSpec);

            // Expression < Func < int, bool>> f = i => i % 2 != 0;
            // f = f.Not().And(i => i > 0);
            Expression<Func<Advertisement, bool>> q = i => i.IsUseful&&i.EndTime>DateTime.Now&&i.StartTime<=DateTime.Now;
            // q= q.And(i => i.EndTime > DateTime.Now);
            var list = repo.Find(q, "Num",true,0,null).ToList();
           return list;
        }

        public Advertisement GetByUid(Guid uid)
        {
           // iuow.BeginTransaction();
           var aa= repo.FindById(uid);
           // iuow.Commit();
                return aa;
        }
    }
}
