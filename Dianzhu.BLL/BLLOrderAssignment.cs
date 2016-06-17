using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLOrderAssignment
    {
        public IDAL.IDALOrderAssignment DALOrderAssignment;

        public BLLOrderAssignment(IDAL.IDALOrderAssignment dal)
        {
            DALOrderAssignment = dal;
        }

        public OrderAssignment FindByOrderAndStaff(ServiceOrder order,Staff staff)
        {
            return DALOrderAssignment.FindByOrderAndStaff(order, staff);
        }

        public IList<OrderAssignment> GetOAListByOrder(ServiceOrder order)
        {
            return DALOrderAssignment.GetOAListByOrder(order);
        }

        public IList<OrderAssignment> GetOAListByStaff(Staff staff)
        {
            return DALOrderAssignment.GetOAListByStaff(staff);
        }

        public IList<OrderAssignment> GetAllListForAssign(Guid bid)
        {
            return DALOrderAssignment.GetAllListForAssign(bid);
        }
    }
}
