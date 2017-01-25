using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order;
using Ydb.Order.DomainModel;
using Ydb.Order.Application;
using Ydb.Order.DomainModel.Repository;
using Ydb.Common.Specification;
namespace Ydb.Order.Application
{
    public class OrderAssignmentService : IOrderAssignmentService
    {
 
        //20150616_longphui_modify
        //public DALOrderAssignment DALOrderAssignment = DALFactory.DALOrderAssignment;
          IRepositoryOrderAssignment repoOrderAssignment;
        public OrderAssignmentService(IRepositoryOrderAssignment repoOrderAssignment)
        {
            this.repoOrderAssignment = repoOrderAssignment;
        }
 

        /// <summary>
        /// 新建指派
        /// </summary>
        /// <param name="db"></param>
        public void Save(OrderAssignment db)
        {
            //DALOrderAssignment.SaveOrUpdate(db);
            repoOrderAssignment.Add(db);
 
        }

        public OrderAssignment FindByOrderAndStaff(ServiceOrder order,string staffId)
        {
            return repoOrderAssignment.FindByOrderAndStaff(order, staffId);
        }

        public IList<OrderAssignment> GetOAListByOrder(ServiceOrder order)
        {
            return repoOrderAssignment.GetOAListByOrder(order);
        }

        public IList<OrderAssignment> GetOAListByStaff(string staffId)
        {
            return repoOrderAssignment.GetOAListByStaff(staffId);
        }

        public IList<OrderAssignment> GetAllListForAssign(Guid bid)
        {
            return repoOrderAssignment.GetAllListForAssign(bid);
        }

        public OrderAssignment GetAssignById(Guid AssignId)
        {
            return repoOrderAssignment.FindById(AssignId);
        }

        /// <summary>
        /// 条件读取指派
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="staffID"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IList< OrderAssignment> GetAssigns(Ydb.Common.Specification.TraitFilter filter,  Guid staffID, Guid orderID, Guid storeID,Guid userID)
        {
            var where = PredicateBuilder.True<OrderAssignment>();
            where = where.And(x => x.Enabled);
            if (userID != Guid.Empty)
            {
                //todo: 这个判断有何用?
                //where = where.And(x => x.Order.Business.OwnerId == userID);
            }
            if (staffID != Guid.Empty)
            {
                where = where.And(x => x.AssignedStaffId == staffID.ToString());
            }
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.OrderId == orderID.ToString());
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x. BusinessId == storeID.ToString());
            }

            OrderAssignment baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repoOrderAssignment.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repoOrderAssignment.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList()
                : repoOrderAssignment.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;

        }


        /// <summary>
        /// 统计指派的数量
        /// </summary>
        /// <param name="staffID"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetAssignsCount(Guid staffID, Guid orderID, Guid storeID, Guid userID)
        {
            var where = PredicateBuilder.True<OrderAssignment>();
            where = where.And(x => x.Enabled);
            if (userID != Guid.Empty)
            {
               // where = where.And(x => x.Order.Business.OwnerId == userID);
            }
            if (staffID != Guid.Empty)
            {
                where = where.And(x => x.AssignedStaffId == staffID.ToString());
            }
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.OrderId == orderID.ToString());
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x. BusinessId == storeID.ToString());
            }
            long count = repoOrderAssignment.GetRowCount(where);
            return count;
        }
    }
}
