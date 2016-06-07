using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.IDAL
{
    /// <summary>
    ///
    /// </summary>
    public interface IDALReceptionStatusArchieve :IRepository<ReceptionStatusArchieve,Guid>
    {


            IList<DZMembership> GetCustomerListByCS(DZMembership cs, int pageNum, int pageSize, out int totalAmount)
     ;
          int GetReceptionAmount(DZMembership member)
     ;
       
          int GetReceptionDates(DZMembership member)
       ;
    }
}
