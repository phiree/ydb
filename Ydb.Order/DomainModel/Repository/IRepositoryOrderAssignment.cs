using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Repository;

namespace Ydb.Order.DomainModel.Repository
{
    public interface IRepositoryOrderAssignment  :IRepository<OrderAssignment,Guid>
    {


          OrderAssignment FindByOrderAndStaff(ServiceOrder order, string staffId);

          IList<OrderAssignment> GetOAListByOrder(ServiceOrder order);

          IList<OrderAssignment> GetOAListByStaff(string staffId);

          IList<OrderAssignment> GetAllListForAssign(Guid businessId);
    }
}
