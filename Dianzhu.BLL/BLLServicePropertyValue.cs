using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLServicePropertyValue
    {

   
      IDALServicePropertyValue iDALServicePropertyValue;
      
      public BLLServicePropertyValue(IDALServicePropertyValue iDALServicePropertyValue)
      {
          this.iDALServicePropertyValue = iDALServicePropertyValue;
      }
      public BLLServicePropertyValue()
          : this(new DALServicePropertyValue())
      { }
      public ServicePropertyValue GetOne(Guid id)
      {
          return iDALServicePropertyValue.DalBase.GetOne(id);
      }
     
    }
}
