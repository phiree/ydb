using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
using System.Diagnostics;
namespace Dianzhu.BLL
{
    public class BLLReceptionStatusArchieve
    {

        DAL.DALReceptionStatusArchieve dalRS;
        public BLLReceptionStatusArchieve(DALReceptionStatusArchieve dalRsA)
        {
            this.dalRS = dalRsA;
        }
        public BLLReceptionStatusArchieve()
        {
            this.dalRS = DALFactory.DALReceptionStatusArchieve;
        }

        public void SaveOrUpdate(ReceptionStatusArchieve rsa)
        {
            dalRS.SaveOrUpdate(rsa);
        }

        public IList<DZMembership> GetCustomerListByCS(DZMembership cs,int pageNum,int pageSize,out int totalAmount)
        {
            return dalRS.GetCustomerListByCS(cs, pageNum, pageSize, out totalAmount);
        }

    }
}
