using System;
using System.Collections.Generic;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IOrderAssignmentService
    {
        OrderAssignment FindByOrderAndStaff(ServiceOrder order, string staffId);
        IList<OrderAssignment> GetAllListForAssign(Guid bid);
        OrderAssignment GetAssignById(Guid AssignId);
        IList<OrderAssignment> GetAssigns(TraitFilter filter, Guid staffID, Guid orderID, Guid storeID, Guid userID);
        long GetAssignsCount(Guid staffID, Guid orderID, Guid storeID, Guid userID);
        IList<OrderAssignment> GetOAListByOrder(ServiceOrder order);
        IList<OrderAssignment> GetOAListByStaff(string staffId);
        void Save(OrderAssignment db);

        void DeleteStaffOfOrder(ServiceOrder order, string staffId);
    }
}