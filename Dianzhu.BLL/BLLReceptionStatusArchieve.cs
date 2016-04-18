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
            return dalRS.GetCustomerListByCS(cs, pageNum-1, pageSize, out totalAmount);
        }

        public int GetReceptionAmount(DZMembership member)
        {
            return dalRS.GetReceptionAmount(member);
        }
        /// <summary>
        /// 活跃天数
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int GetReceptionDates(DZMembership member)
        {
            return dalRS.GetReceptionDates(member);
        }

    }
}
