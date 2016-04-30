using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
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
            var list= repo.Find("select adv from Advertisement adv", pageIndex,pageSize ,out totalRecords);
            iUnitOfWork.Commit();
            return list;
            //return repo.GetADList(pageIndex, pageSize,out totalRecords);
        }

        public IList<Advertisement> GetADListForUseful()
        {
            long totalRecords;
            iUnitOfWork.BeginTransaction();
            var list= repo.Find("select adv from Advertisement adv where IsUseful=1", 1, 999, out totalRecords).ToList();
            // return DALAdvertisement.GetADListForUseful();
            iUnitOfWork.Commit();
            return list;
        }

        public Advertisement GetByUid(Guid uid)
        {
            return repo.FindById(uid);
        }
    }
}
