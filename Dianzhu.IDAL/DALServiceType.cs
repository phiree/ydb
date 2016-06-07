using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.IDAL
{
    public interface IDALServiceType 
    {

          IList<ServiceType> GetTopList()
       ;
          ServiceType GetOneByCode(string code)
      ;

          ServiceType GetOneByName(string name, int level)
        ;

    }
}
