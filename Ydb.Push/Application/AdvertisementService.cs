using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Push.DomainModel.IRepository;
using Ydb.Push.DomainModel;
using System.Linq.Expressions;

using Ydb.Common.Specification;
using Ydb.Push.Infrastructure.NHibernate.UnitOfWork;

namespace Ydb.Push.Application
{
    public class AdvertisementService : IAdvertisementService
    {
        private IRepositoryAdvertisement repoAdv;
       // IUnitOfWork iuow;
        public AdvertisementService(IRepositoryAdvertisement repoAdv)
        {
            this.repoAdv = repoAdv;
           // this.iuow = iuow;
         //   this.iUnitOfWork = iUnitOfWork;
        }
        [UnitOfWork]
        public void Save(Advertisement ad)
        {
            // iuow.BeginTransaction();
            repoAdv.Add(ad);
           // iuow.Commit();
        }
        [UnitOfWork]
        public void Update(Advertisement ad)
        {
            // iuow.BeginTransaction();
            repoAdv.Update(ad);

           // iuow.Commit();
        }
        public IEnumerable<Advertisement> GetADList(int pageIndex, int pageSize, out long totalRecords)
        {
           // iuow.BeginTransaction();
            var where = PredicateBuilder.True<Advertisement>();
          
               var list = repoAdv.Find(where, pageIndex,pageSize ,out totalRecords).OrderByDescending(x=>x.IsUseful).ToList();
           // iuow.Commit();
            return list;
           // return repo.GetADList(pageIndex, pageSize,out totalRecords);
        }

        public IList<Advertisement> GetADListForUseful(string userType)
        {
           
          //ISpecification<Advertisement> advInPerildSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementInPeriod(DateTime.Now);
            //ISpecification<Advertisement> advUsefulSpec = new Model.Resource.Specs.AdvertisementSpec.AdvertisementUseful(true);
            //ISpecification<Advertisement> specs = advInPerildSpec.And<Advertisement>(advUsefulSpec);

            // Expression < Func < int, bool>> f = i => i % 2 != 0;
            // f = f.Not().And(i => i > 0);
            Expression<Func<Advertisement, bool>> q = i => i.IsUseful&&i.EndTime>DateTime.Now&&i.StartTime<=DateTime.Now && i.PushType==userType;
            // q= q.And(i => i.EndTime > DateTime.Now);
            var list = repoAdv.Find(q, "Num",true,0,null).ToList();
           return list;
        }

        public Advertisement GetByUid(Guid uid)
        {
           // iuow.BeginTransaction();
           var aa= repoAdv.FindById(uid);
           // iuow.Commit();
                return aa;
        }
    }
}
