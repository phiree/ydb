using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model;
using DDDCommon;
namespace Dianzhu.BLL
{
    public class BLLOrderAssignment
    {
        //20150616_longphui_modify
        //public DALOrderAssignment DALOrderAssignment = DALFactory.DALOrderAssignment;
        private IDALOrderAssignment DALOrderAssignment;
        public BLLOrderAssignment(IDALOrderAssignment DALOrderAssignment)
        {
            this.DALOrderAssignment = DALOrderAssignment;
        }

        public void SaveOrUpdate(OrderAssignment db)
        {
            //DALOrderAssignment.SaveOrUpdate(db);
            DALOrderAssignment.Update(db);
        }

        /// <summary>
        /// 新建指派
        /// </summary>
        /// <param name="db"></param>
        public void Save(OrderAssignment db)
        {
            //DALOrderAssignment.SaveOrUpdate(db);
            DALOrderAssignment.Add(db);
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

        public OrderAssignment GetAssignById(Guid AssignId)
        {
            return DALOrderAssignment.FindById(AssignId);
        }
        
        /// <summary>
        /// 条件读取指派
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pagenum"></param>
        /// <param name="staffID"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public IList<Model.OrderAssignment> GetAssigns(int pagesize, int pagenum, Guid staffID, Guid orderID, Guid storeID)
        {
            var where = PredicateBuilder.True<OrderAssignment>();
            if (staffID != Guid.Empty)
            {
                where = where.And(x => x.AssignedStaff.Id == staffID);
            }
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.Order.Id == orderID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Order.Business.Id == storeID);
            }
            long t = 0;
            var list = pagesize == 0 ? DALOrderAssignment.Find(where).ToList() : DALOrderAssignment.Find(where, pagenum, pagesize, out t).ToList();
            return list;

        }
        

        /// <summary>
        /// 统计指派的数量
        /// </summary>
        /// <param name="staffID"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public long GetAssignsCount(Guid staffID, Guid orderID, Guid storeID)
        {
            var where = PredicateBuilder.True<OrderAssignment>();
            if (staffID != Guid.Empty)
            {
                where = where.And(x => x.AssignedStaff.Id == staffID);
            }
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.Order.Id == orderID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Order.Business.Id == storeID);
            }
            long count = DALOrderAssignment.GetRowCount(where);
            return count;
        }
    }
}
