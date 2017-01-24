using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
   public class RepositoryOrderAssignment : NHRepositoryBase<OrderAssignment, Guid>, IRepositoryOrderAssignment
    {
        public OrderAssignment FindByOrderAndStaff(ServiceOrder order, string staffId)
        {
            return FindOne(x => x.Order.Id == order.Id && x.AssignedStaffId == staffId && x.Enabled == true);
        }

        public IList<OrderAssignment> GetOAListByOrder(ServiceOrder order)
        {
            return Find(x => x.Order.Id == order.Id && x.Enabled == true);
        }

        public IList<OrderAssignment> GetOAListByStaff(string staffId)
        {
            return Find(x => x.AssignedStaffId == staffId && x.Enabled == true);
        }

        public IList<OrderAssignment> GetAllListForAssign(Guid businessId)
        {
            return Find(x => x.Order.BusinessId == businessId.ToString() && x.Enabled == true);

            //   return Find(x => x.AssignedStaff.Belongto.Id == businessId && x.Enabled == true);

            //string sql = "select oa from OrderAssignment oa " +
            //    " inner join oa.AssignedStaff s " +
            //    " where oa.Enabled=true And s.Belongto = '" + businessId + "'";

            //IQuery iquery = Session.CreateQuery(sql);

            //return iquery.List<OrderAssignment>();
        }

    }
}
