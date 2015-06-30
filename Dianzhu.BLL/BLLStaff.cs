using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLStaff
    {
      public DALStaff DALStaff=DALFactory.DALStaff;

      
      public IList<Staff> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecords)
      {

          return DALStaff.GetList(businessId, serviceTypeId, pageindex, pagesize, out totalRecords);
      }
      
      public void SaveOrUpdate(Staff staff)
      {
          DALStaff.SaveOrUpdate(staff);
      }
      public void Delete(Staff staff)
      {
          DALStaff.Delete(staff);
      }
      public Staff GetOne(Guid id)
      {
          return DALStaff.GetOne(id);
      }
       
    }
}
