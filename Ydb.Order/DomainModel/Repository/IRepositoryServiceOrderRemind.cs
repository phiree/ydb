using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Repository;
using Ydb.Order.DomainModel;


namespace Ydb.Order.DomainModel.Repository
{
    public interface IRepositoryServiceOrderRemind :IRepository<ServiceOrderRemind,Guid>
    {

          ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId)
     ;

      

          int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        ;

          IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
      ;
    }
}
