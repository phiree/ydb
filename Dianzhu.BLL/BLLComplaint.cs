﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model;
using DDDCommon;
using Ydb.Common.Specification;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
namespace Dianzhu.BLL
{
    public class BLLComplaint
    {
        //public DALComplaint DALComplaint = DALFactory.DALComplaint;

        public IDALComplaint dalComplaint;
       IServiceOrderService orderService;
        public BLLComplaint(IDALComplaint dalComplaint, IServiceOrderService orderService)
        {
            this.dalComplaint = dalComplaint;
            this.orderService = orderService;
            // this.iuw = iuw;
        }

       
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="c"></param>
        public void Update(Complaint c)
        { 
            dalComplaint.Update(c);
        }

        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaint"></param>
        public void AddComplaint(Complaint complaint)
        {
            dalComplaint.Add(complaint);
        }

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="customerServiceID"></param>
        /// <returns></returns>
        public IList<Model.Complaint> GetComplaints( TraitFilter filter, Guid orderID, Guid storeID, Guid customerServiceID)
        {
            ServiceOrder order = orderService.GetOne(orderID);
            var where = PredicateBuilder.True<Complaint>();
            if(orderID != Guid.Empty)
            {
                where = where.And(x => x.OrderId == orderID.ToString());
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.BusinessId== storeID.ToString());
            }
            if (customerServiceID != Guid.Empty)
            {
                where = where.And(x => x.CustomerServiceId== customerServiceID.ToString());
            }
            Model.Complaint baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = dalComplaint.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? dalComplaint.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : dalComplaint.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;

        }

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <returns>area实体list</returns>
        public long GetComplaintsCount(Guid orderID, Guid storeID, Guid customerServiceID)
        {
            var where = PredicateBuilder.True<Complaint>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.OrderId == orderID.ToString());
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.BusinessId == storeID.ToString());
            }
            if (customerServiceID != Guid.Empty)
            {
                where = where.And(x => x.CustomerServiceId == customerServiceID.ToString());
            }
            long count= dalComplaint.GetRowCount(where) ;
            return count;
        }

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <returns>area实体list</returns>
        public Model.Complaint GetComplaintById(Guid Id)
        {
            return dalComplaint.FindById(Id);

 
        }
    }
}
