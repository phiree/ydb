using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.IDAL
{
    public interface IDALStaff :IRepository<Staff,Guid>
    {

         
          int GetEnableSum(Business business)
     ;

          IList<Staff> GetAllListByBusiness(Business business)
       ;
    }
}
