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
 
        public IList<Advertisement> GetADList()
        {
            return DALAdvertisement.GetADList();
        }
    }
}
