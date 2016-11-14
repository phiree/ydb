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


          OrderAssignment FindByOrderAndStaff(ServiceOrder order, string staffId);

          IList<OrderAssignment> GetOAListByOrder(ServiceOrder order);

          IList<OrderAssignment> GetOAListByStaff(string staffId);

          IList<OrderAssignment> GetAllListForAssign(Guid businessId);
    }
}
