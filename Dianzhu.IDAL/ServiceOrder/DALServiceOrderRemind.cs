﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
 
namespace Dianzhu.IDAL
{
    public interface IDALServiceOrderRemind :IRepository<ServiceOrderRemind,Guid>
    {

          ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId)
     ;

      

          int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        ;

          IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
      ;
    }
}
