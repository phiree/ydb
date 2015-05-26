using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLStaff
    {
      IDALStaff iDALStaff;

      public BLLStaff(IDALStaff iDALStaff)
      {
          this.iDALStaff = iDALStaff;
      }
      public BLLStaff()
          : this(new DALStaff())
      { }
      public IList<Staff> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecords)
      {

          return iDALStaff.GetList(businessId, serviceTypeId, pageindex, pagesize, out totalRecords);
      }
      /// <summary>
      /// 保存
      /// </summary>
      /// <param name="code">员工编号</param>
      /// <param name="name">姓名</param>
      /// <param name="nickname">昵称</param>
      /// <param name="gender">性别</param>
      /// <param name="phone">电话</param>
      /// <param name="photo">照片</param>
      /// <param name="serviceTypes">服务分类</param>
      /// <returns></returns>
      public void SaveOrUpdate(Staff staff)
      {
          iDALStaff.DalBase.SaveOrUpdate(staff);
      }
      public void Delete(Staff staff)
      {
          iDALStaff.DalBase.Delete(staff);
      }
       
    }
}
