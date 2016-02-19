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
        public DALOrderAssignment DALOrderAssignment = DALFactory.DALOrderAssignment;

        public void SaveOrUpdate(OrderAssignment db)
        {
            DALOrderAssignment.SaveOrUpdate(db);
        }

        public OrderAssignment FindByOrderAndStaff(ServiceOrder order,Staff staff)
        {
            return DALOrderAssignment.FindByOrderAndStaff(order, staff);
        }

        public IList<OrderAssignment> GetOAListByOrder(ServiceOrder order)
        {
            return DALOrderAssignment.GetOAListByOrder(order);
        }
    }
}
