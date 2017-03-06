using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
using Ydb.Common.Repository;
using Ydb.Order.DomainModel.Repository;

namespace Ydb.Order.DomainModel.Repository
{
    public interface IRepositoryServiceOrder : IRepository<ServiceOrder, Guid>
    {
        IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime timeBegin, DateTime dateEnd);
        IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords);



        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单数量，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        long GetOrdersCountByBusinessList(IList<string> businessIdList, bool isShared);

        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单列表，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        IList<ServiceOrder> GetOrdersByBusinessList(IList<string> businessIdList, bool isShared);


        /// <summary>
        /// 根据分账统计订单
        /// </summary>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        IList<ServiceOrder> GetOrdersByShared(bool isShared, int pageIndex, int pageSize, out long totalRecords);

        /// <summary>
        /// 根据商户Id列表和时间获取代理及其助理的订单列表
        /// </summary>
        /// <param name="businessIdList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IList<ServiceOrder> GetOrdersByBusinessList(IList<string> businessIdList, DateTime beginTime, DateTime endTime, string strDone);

        /// <summary>
        /// 根据商户Id列表和时间获取代理及其助理的订单数量
        /// </summary>
        /// <param name="businessIdList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        long GetOrdersCountByBusinessList(IList<string> businessIdList, DateTime beginTime, DateTime endTime, string strDone);
    }
}
