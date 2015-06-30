using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLServicePropertyValue
    {


      public DALServicePropertyValue DALServicePropertyValue = DALFactory.DALServicePropertyValue;
       
      
      
      public ServicePropertyValue GetOne(Guid id)
      {
          return DALServicePropertyValue.GetOne(id);
      }
     
    }
}
