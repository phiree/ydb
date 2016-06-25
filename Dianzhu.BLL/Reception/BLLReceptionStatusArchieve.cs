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

        IDAL.IDALReceptionStatusArchieve dalRS;
        public BLLReceptionStatusArchieve(IDAL.IDALReceptionStatusArchieve dalRsA)
        {
            this.dalRS = dalRsA;
        }
        
        public void Save(ReceptionStatusArchieve rsa)
        {
            dalRS.Add(rsa);
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
