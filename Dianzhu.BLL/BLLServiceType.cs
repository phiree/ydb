using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLServiceType
    {
      IDALServiceType iDALServiceType;

      public BLLServiceType(IDALServiceType iDALServiceType)
      {
          this.iDALServiceType = iDALServiceType;
      }
      public BLLServiceType()
          : this(new DALServiceType())
      { }
      public ServiceType GetOne(Guid id)
      {
          return iDALServiceType.DalBase.GetOne(id);
      }
      public IList<ServiceType> GetAll()
      {
          return iDALServiceType.DalBase.GetAll<ServiceType>();
      }
      public void SaveOrUpdate(ServiceType serviceType)
      {
          iDALServiceType.DalBase.SaveOrUpdate(serviceType);
      }
    }
}
