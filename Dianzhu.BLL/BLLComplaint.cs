using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model;
using DDDCommon;
namespace Dianzhu.BLL
{
    public class BLLComplaint
    {
        //public DALComplaint DALComplaint = DALFactory.DALComplaint;

        public IDALComplaint dalComplaint;
        public BLLComplaint(IDALComplaint dalComplaint)
        {
            this.dalComplaint = dalComplaint;
            // this.iuw = iuw;
        }

        public void SaveOrUpdate(Complaint ad)
        {
            dalComplaint.Update(ad);
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
        /// <returns>area实体list</returns>
        public IList<Model.Complaint> GetComplaints(int pagesize, int pagenum, Guid orderID, Guid storeID, Guid customerServiceID)
        {
            var where = PredicateBuilder.True<Complaint>();
            if(orderID != Guid.Empty)
            {
                where = where.And(x => x.Order.Id == orderID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Order.Business.Id== storeID);
            }
            if (customerServiceID != Guid.Empty)
            {
                where = where.And(x => x.Order.CustomerService.Id== customerServiceID);
            }
            long t = 0;
            var list = pagesize == 0 ? dalComplaint.Find(where).ToList() : dalComplaint.Find(where, pagenum, pagesize, out t).ToList();
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
                where = where.And(x => x.Order.Id == orderID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Order.Business.Id == storeID);
            }
            if (customerServiceID != Guid.Empty)
            {
                where = where.And(x => x.Order.CustomerService.Id == customerServiceID);
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
