using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.DAL
{
    public interface IDALServiceOrderRemind 
    {

          ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId)
     ;

      

          int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        ;

          IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
      ;
    }
}
