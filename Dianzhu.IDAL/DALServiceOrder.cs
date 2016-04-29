using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.DAL
{
    public interface IDALServiceOrder  
    {

          IList<ServiceOrder> GetListByUser(Guid userId)
      ;
   
          int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType)
        ;
        /// <summary>
        /// 除了草稿(draft,draftpushed)之外的订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="isCustomerService">是客服的,否则是客户的</param>
        /// <returns></returns>
          int GetServiceOrderCountWithoutDraft(Guid userid, bool isCustomerService)
        ;
          decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService)
        ;

          IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        ;
          IList<ServiceOrder> GetListForBusiness(Business business, int pageNum, int pageSize, out int totalAmount)
        ;

          IList<ServiceOrder> GetListForCustomer(DZMembership customer, int pageNum, int pageSize, out int totalAmount)
        ;

        /// <summary>
        /// 获得草稿订单
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
          ServiceOrder GetDraftOrder(DZMembership c, DZMembership cs)
        ;

          IList<ServiceOrder> GetOrderListByDate(DZService service, DateTime dateTime)
        ;

          ServiceOrder GetOrderByIdAndCustomer(Guid Id, DZMembership customer)
        ;
          IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId, int pageIndex, int pageSize, out int totalRecords)
        ;
          IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId)
        ;

          IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId)
        ;
        /// <summary>
        /// 用户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
          IList<ServiceOrder> GetCustomerCancelForBusiness(Guid businessId)
        ;
        /// <summary>
        /// 商户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
          IList<ServiceOrder> GetBusinessCancelOrdersForBusiness(Guid businessId)
       ;
    }
}
