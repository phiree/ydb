using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.IDAL
{
    public interface IDALOrderAssignment  :IRepository<OrderAssignment,Guid>
    {


          OrderAssignment FindByOrderAndStaff(ServiceOrder order, Staff staff);

          IList<OrderAssignment> GetOAListByOrder(ServiceOrder order);

          IList<OrderAssignment> GetOAListByStaff(Staff staff);

          IList<OrderAssignment> GetAllListForAssign(Guid businessId);
    }
}
