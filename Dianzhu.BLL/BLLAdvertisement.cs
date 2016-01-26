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
        public DALAdvertisement DALAdvertisement = DALFactory.DALAdvertisement;

        public void SaveOrUpdate(Advertisement ad)
        {
            DALAdvertisement.SaveOrUpdate(ad);
        }
 
        public IList<Advertisement> GetADList(int pageIndex, int pageSize, out int totalRecords)
        {
            return DALAdvertisement.GetADList(pageIndex, pageSize,out totalRecords);
        }

        public Advertisement GetByUid(Guid uid)
        {
            return DALAdvertisement.GetByUid(uid);
        }
    }
}
