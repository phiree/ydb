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
    public class DALOrderAssignment : DALBase<OrderAssignment>
    {
        public DALOrderAssignment()
        {

        }
        //注入依赖,供测试使用;
        public DALOrderAssignment(string fortest) : base(fortest)
        {

        }

        public OrderAssignment FindByOrderAndStaff(ServiceOrder order, Staff staff)
        {
            return Session.QueryOver<OrderAssignment>().Where(x => x.Order == order).And(x => x.AssignedStaff == staff).SingleOrDefault();
        }

        public IList<OrderAssignment> GetOAListByOrder(ServiceOrder order)
        {
            return Session.QueryOver<OrderAssignment>().Where(x => x.Order == order).And(x=>x.Enabled==true).List();
        }

        public IList<OrderAssignment> GetOAListByStaff(Staff staff)
        {
            return Session.QueryOver<OrderAssignment>().Where(x => x.AssignedStaff == staff).And(x => x.Enabled == true).List();
        }

        public IList<OrderAssignment> GetAllListForAssign(Guid businessId)
        {
            string sql = "select oa from OrderAssignment oa " +
                " inner join oa.AssignedStaff s " +
                " where oa.Enabled=true And s.Belongto = '" + businessId + "'";

            IQuery iquery = Session.CreateQuery(sql);

            return iquery.List<OrderAssignment>();
        }
    }
}
