﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model;
 
using Ydb.Common.Specification;
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
 

        /// <summary>
        /// 新建指派
        /// </summary>
        /// <param name="db"></param>
        public void Save(OrderAssignment db)
        {
            //DALOrderAssignment.SaveOrUpdate(db);
            DALOrderAssignment.Add(db);
 
        }

        public OrderAssignment FindByOrderAndStaff(ServiceOrder order,string staffId)
        {
            return DALOrderAssignment.FindByOrderAndStaff(order, staffId);
        }

        public IList<OrderAssignment> GetOAListByOrder(ServiceOrder order)
        {
            return DALOrderAssignment.GetOAListByOrder(order);
        }

        public IList<OrderAssignment> GetOAListByStaff(string staffId)
        {
            return DALOrderAssignment.GetOAListByStaff(staffId);
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
        /// <param name="filter"></param>
        /// <param name="staffID"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IList<Model.OrderAssignment> GetAssigns(Ydb.Common.Specification.TraitFilter filter,  Guid staffID, Guid orderID, Guid storeID,Guid userID)
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
                where = where.And(x => x.Order.Id == orderID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Order.BusinessId == storeID.ToString());
            }

            OrderAssignment baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = DALOrderAssignment.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? DALOrderAssignment.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : DALOrderAssignment.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
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
                where = where.And(x => x.Order.Id == orderID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Order.BusinessId == storeID.ToString());
            }
            long count = DALOrderAssignment.GetRowCount(where);
            return count;
        }
    }
}
