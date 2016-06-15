using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.IDAL
{
    public interface IDALStaff :IRepository<Staff,Guid>
    {

          IList<Staff> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecord)
    ;

          int GetEnableSum(Business business)
     ;

          IList<Staff> GetAllListByBusiness(Business business)
       ;
    }
}
