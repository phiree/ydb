using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLDZService
    {
      IDALDZService iDALDZService;

      public BLLDZService(IDALDZService iDALDZService)
      {
          this.iDALDZService = iDALDZService;
      }
      public BLLDZService()
          : this(new DALDZService())
      { }

      public IList<DZService> GetServiceByBusiness(Guid businessId,Guid serviceTypeId,int pageindex,int pagesize,out int totalRecords)
      {
          return iDALDZService.GetList(businessId, serviceTypeId, pageindex, pagesize, out totalRecords);
      }
      public DZService GetOne(Guid serviceId)
      {
          return iDALDZService.DalBase.GetOne(serviceId);
      }
       
    }
}
