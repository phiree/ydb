using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Common.Specification;

namespace Ydb.Order.Application
{
    public interface IComplaintService
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="c"></param>
        void Update(Complaint c);

        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaint"></param>
        void AddComplaint(Complaint complaint);

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="customerServiceID"></param>
        /// <returns></returns>
        IList<Complaint> GetComplaints(TraitFilter filter, Guid orderID, Guid storeID, Guid customerServiceID);

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <returns>area实体list</returns>
        long GetComplaintsCount(string orderID, string storeID, string customerServiceID);

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <returns>area实体list</returns>
        Complaint GetComplaintById(Guid Id);

        /// <summary>
        /// 根据客户的用户Id获取该客户的投诉数量
        /// </summary>
        /// <returns>area实体list</returns>
        long GetComplaintsCountByOperator(string operatorId);
    }
}
