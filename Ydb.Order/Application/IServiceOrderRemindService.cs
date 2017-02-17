using System;
using System.Collections.Generic;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IServiceOrderRemindService
    {
        void DeleteRemindById(ServiceOrderRemind remind);
        IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime);
        ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId);
        ServiceOrderRemind GetRemindById(Guid RemindId, Guid userId);
        IList<ServiceOrderRemind> GetReminds(TraitFilter filter, Guid orderID, Guid userId, DateTime startTime, DateTime endTime);
        long GetRemindsCount(Guid orderID, Guid userId, DateTime startTime, DateTime endTime);
        int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime);
        void Save(ServiceOrderRemind Remind);
        void Update(ServiceOrderRemind Remind);
    }
}