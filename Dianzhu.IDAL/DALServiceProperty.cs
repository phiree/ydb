using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.DAL
{
   public interface IDALServiceProperty 
    {





          IList<ServiceProperty> GetList(Guid serviceTypeId)
      ;
          void Save(ServiceProperty property)
      ;
     
    }
}
