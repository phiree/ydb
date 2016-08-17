using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using NHibernate;
using NHibernate.Criterion;
namespace Dianzhu.DAL
{
 
    public class DALOrderAssignment : NHRepositoryBase<OrderAssignment,Guid>,IDAL.IDALOrderAssignment
    {
 
        public OrderAssignment FindByOrderAndStaff(ServiceOrder order, Staff staff)
        {
            return FindOne(x => x.Order.Id == order.Id && x.AssignedStaff == staff && x.Enabled == true);
        }

        public IList<OrderAssignment> GetOAListByOrder(ServiceOrder order)
        {
            return Find(x => x.Order.Id == order.Id && x.Enabled == true);
        }

        public IList<OrderAssignment> GetOAListByStaff(Staff staff)
        {
            return Find(x => x.AssignedStaff.Id == staff.Id && x.Enabled == true);
        }

        public IList<OrderAssignment> GetAllListForAssign(Guid businessId)
        {
            return Find(x => x.AssignedStaff.Belongto.Id == businessId && x.Enabled == true);

            //string sql = "select oa from OrderAssignment oa " +
            //    " inner join oa.AssignedStaff s " +
            //    " where oa.Enabled=true And s.Belongto = '" + businessId + "'";

            //IQuery iquery = Session.CreateQuery(sql);

            //return iquery.List<OrderAssignment>();
        }
    }
}
