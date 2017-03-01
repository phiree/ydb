using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel.Repository;


namespace Ydb.Order.Application
{
    public class ComplaintService: IComplaintService
    {

        IRepositoryComplaint repositoryComplaint;
        IRepositoryServiceOrder repositoryServiceOrder;
        public ComplaintService(IRepositoryComplaint repositoryComplaint, IRepositoryServiceOrder repositoryServiceOrder)
        {
            this.repositoryComplaint = repositoryComplaint;
            this.repositoryServiceOrder = repositoryServiceOrder;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="c"></param>
        public void Update(Complaint c)
        {
            repositoryComplaint.Update(c);
        }

        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaint"></param>
        public void AddComplaint(Complaint complaint)
        {
            repositoryComplaint.Add(complaint);
        }

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="customerServiceID"></param>
        /// <returns></returns>
        public IList<Complaint> GetComplaints(TraitFilter filter, Guid orderID, Guid storeID, Guid customerServiceID)
        {
            ServiceOrder order = repositoryServiceOrder.FindById(orderID);
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
            Complaint baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repositoryComplaint.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repositoryComplaint.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : repositoryComplaint.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
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
            long count = repositoryComplaint.GetRowCount(where);
            return count;
        }

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <returns>area实体list</returns>
        public Complaint GetComplaintById(Guid Id)
        {
            return repositoryComplaint.FindById(Id);


        }
    }
}
