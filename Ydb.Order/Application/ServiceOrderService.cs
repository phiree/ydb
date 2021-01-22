using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using Ydb.PayGateway;
using Ydb.Common.Infrastructure;
using Ydb.Order.Infrastructure;
using Ydb.Common.Domain;

namespace Ydb.Order.Application
{

    /// <summary>
    /// 订单业务逻辑
    /// </summary>
    [Serializable]
    public class ServiceOrderService : IServiceOrderService
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");

        IRepositoryServiceOrder repoServiceOrder = null;


        IRepositoryOrderAssignment repoOrderAssignment;


        IRepositoryServiceOrderStateChangeHis repoStateChangeHis = null;
        IRepositoryClaims repoClaims;
        IRepositoryPayment repoPayment;


        IRepositoryRefund repoRefund;

        //20160616_longphui_add
        IHttpRequest httpRequest;
        IRepositoryServiceOrderPushedService repoPushedService;

        IRefundFactory refundFactory;
        DomainModel.DataStatistics.IStatisticsOrderCount statisticsOrderCount;


        public ServiceOrderService(IRepositoryServiceOrder repoServiceOrder, IRepositoryServiceOrderStateChangeHis repoStateChangeHis,
           IRepositoryRefund repoRefund, IRepositoryOrderAssignment repoOrderAssignment,
           IRepositoryClaims repoClaims, IRepositoryPayment repoPayment, IHttpRequest httpRequest,
           IRepositoryServiceOrderPushedService repoPushedService, IRefundFactory refundFactory,
           DomainModel.DataStatistics.IStatisticsOrderCount statisticsOrderCount)
        {

            this.repoServiceOrder = repoServiceOrder;
            this.repoOrderAssignment = repoOrderAssignment;
            this.repoStateChangeHis = repoStateChangeHis;
            this.repoClaims = repoClaims;
            this.repoPayment = repoPayment;

            this.repoRefund = repoRefund;
            this.httpRequest = httpRequest;
            this.repoPushedService = repoPushedService;
            this.refundFactory = refundFactory;
            this.statisticsOrderCount = statisticsOrderCount;
        }

        public int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType)
        {

            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.CustomerId == userId.ToString());

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    where = where.And(x => x.OrderStatus == enum_OrderStatus.Finished
                        || x.OrderStatus == enum_OrderStatus.Appraised
                        || x.OrderStatus == enum_OrderStatus.EndWarranty
                        || x.OrderStatus == enum_OrderStatus.EndCancel
                        || x.OrderStatus == enum_OrderStatus.EndRefund
                        || x.OrderStatus == enum_OrderStatus.EndIntervention
                        || x.OrderStatus == enum_OrderStatus.ForceStop
                        );
                    break;
                case enum_OrderSearchType.Nt:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.EndWarranty
                         && x.OrderStatus != enum_OrderStatus.EndCancel
                         && x.OrderStatus != enum_OrderStatus.EndRefund
                         && x.OrderStatus != enum_OrderStatus.EndIntervention
                         && x.OrderStatus != enum_OrderStatus.ForceStop
                         && x.OrderStatus != enum_OrderStatus.Search);

                    break;
                default:
                case enum_OrderSearchType.ALL:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search)
                      ;
                    break;
            }
            //  iuow.BeginTransaction();
            int rowCount = (int)repoServiceOrder.GetRowCount(where);
            //   iuow.Commit();
            return rowCount;
            // return DALServiceOrder.GetServiceOrderCount(userId, searchType);
        }
        public IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.CustomerId == userId.ToString());

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    where = where.And(x => x.OrderStatus == enum_OrderStatus.Finished

                         || x.OrderStatus == enum_OrderStatus.Appraised)
                        ;
                    break;
                case enum_OrderSearchType.Nt:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished

                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.Search);

                    break;
                default:
                case enum_OrderSearchType.ALL:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search)
                      ;
                    break;
            }
            long totalRecord;
            return repoServiceOrder.Find(where, pageNum, pageSize, out totalRecord).ToList();

            // return DALServiceOrder.GetServiceOrderList(userId, searchType, pageNum, pageSize);
        }

        /// <summary>
        /// 查询订单合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="statusSort"></param>
        /// <param name="status"></param>
        /// <param name="storeID"></param>
        /// <param name="formanID"></param>
        /// <param name="afterThisTime"></param>
        /// <param name="beforeThisTime"></param>
        /// <param name="UserID"></param>
        /// <param name="userType"></param>
        /// <param name="strAssign"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetOrders(TraitFilter filter, string statusSort, string status, Guid storeID, string formanID, DateTime afterThisTime, 
            DateTime beforeThisTime, Guid UserID, string userType, string strAssign)
        {
            var where = PredicateBuilder.True<ServiceOrder>();

            if (UserID != Guid.Empty)
            {
                if (userType == "customer")
                {
                    where = where.And(x => x.CustomerId == UserID.ToString());
                }
                else
                {
                    where = where.And(x => x.Details.Any(y => y.ServiceSnapShot.BusinessOwnerId == UserID.ToString()));
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                where = where.And(x => x.OrderStatus == (enum_OrderStatus)Enum.Parse(typeof(enum_OrderStatus), status));
            }
            //.enum_OrderSearchType searchType
            switch (statusSort)//switch (searchType)
            {
                //case enum_OrderSearchType.De:
                case "done":
                    where = where.And(x => x.OrderStatus == enum_OrderStatus.Finished
                              || x.OrderStatus == enum_OrderStatus.Appraised
                              || x.OrderStatus == enum_OrderStatus.EndCancel
                              || x.OrderStatus == enum_OrderStatus.EndRefund
                              || x.OrderStatus == enum_OrderStatus.EndIntervention
                              || x.OrderStatus == enum_OrderStatus.EndComplaints
                              || x.OrderStatus == enum_OrderStatus.ForceStop)
                        ;
                    break;
                //case enum_OrderSearchType.Nt:
                case "pending":
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.EndCancel
                         && x.OrderStatus != enum_OrderStatus.EndRefund
                         && x.OrderStatus != enum_OrderStatus.EndIntervention
                         && x.OrderStatus != enum_OrderStatus.EndComplaints
                         && x.OrderStatus != enum_OrderStatus.ForceStop
                         && x.OrderStatus != enum_OrderStatus.Search);

                    break;
                default:
                    //case enum_OrderSearchType.ALL:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                     && x.OrderStatus != enum_OrderStatus.DraftPushed
                     && x.OrderStatus != enum_OrderStatus.Search)
                  ;
                    break;
            }
            if (!string.IsNullOrEmpty(strAssign))
            {
                if (strAssign == "false")
                {
                    where = where.And(x => x.StaffId=="" || x.StaffId == null);
                }
                else
                {
                    where = where.And(x => x.StaffId!="" && x.StaffId!=null);
                }
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.BusinessId == storeID.ToString());
            }
            if (formanID != null)
            {
                where = where.And(x => x.StaffId == formanID);
            }
            if (afterThisTime != DateTime.MinValue)
            {
                where = where.And(x => x.OrderCreated >= afterThisTime);
            }
            if (beforeThisTime != DateTime.MinValue)
            {
                where = where.And(x => x.OrderCreated <= beforeThisTime);
            }
            if (!string.IsNullOrEmpty(filter.ids))
            {
                IList<string> ids = filter.ids.Split(',').ToList();
                where = where.And(x => ids.Contains(x.Id.ToString()));
            }
            ServiceOrder baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repoServiceOrder.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long totalRecord;
            var list = filter.pageSize == 0 ? repoServiceOrder.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : repoServiceOrder.Find(where, filter.pageNum, filter.pageSize, out totalRecord, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;

            // return DALServiceOrder.GetServiceOrderList(userId, searchType, pageNum, pageSize);
        }

       
        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单数量，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetOrdersCountByBusinessList(IList<string> businessIdList, bool isShared)
        {
            return repoServiceOrder.GetOrdersCountByBusinessList(businessIdList, isShared);
        }


        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单列表，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        [UnitOfWork]
        public IList<ServiceOrder> GetOrdersByBusinessList(IList<string> businessIdList, bool isShared)
        {
            return repoServiceOrder.GetOrdersByBusinessList(businessIdList, isShared);
        }

        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单数量，按时间统计
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetOrdersCountByBusinessList(IList<string> businessIdList,DateTime beginTime,DateTime endTime, string strDone)
        {
            return repoServiceOrder.GetOrdersCountByBusinessList(businessIdList, beginTime,endTime,strDone);
        }


        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单列表，按时间统计
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        [UnitOfWork]
        public IList<ServiceOrder> GetOrdersByBusinessList(IList<string> businessIdList, DateTime beginTime, DateTime endTime, string strDone)
        {
            return repoServiceOrder.GetOrdersByBusinessList(businessIdList, beginTime, endTime, strDone);
        }

        /// <summary>
        /// 根据商户Id列表订单的营业总额
        /// </summary>
        /// <param name="businessIdList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public decimal GetTotalAmountByBusinessList(IList<string> businessIdList)
        {
            return repoServiceOrder.GetTotalAmountByBusinessList(businessIdList);
        }

        /// <summary>
        /// 根据分账统计订单
        /// </summary>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        [UnitOfWork]
        public IList<ServiceOrder> GetOrdersByShared(bool isShared, int pageIndex, int pageSize, out long totalRecords)
        {
            return repoServiceOrder.GetOrdersByShared(isShared,  pageIndex,  pageSize, out  totalRecords);
        }


        /// <summary>
        /// 查询订单数量
        /// </summary>
        /// <param name="statusSort"></param>
        /// <param name="status"></param>
        /// <param name="storeID"></param>
        /// <param name="formanID"></param>
        /// <param name="afterThisTime"></param>
        /// <param name="beforeThisTime"></param>
        /// <param name="UserID"></param>
        /// <param name="userType"></param>
        /// <param name="strAssign"></param>
        /// <returns></returns>
        public long GetOrdersCount(string statusSort, string status, Guid storeID, string formanID, DateTime afterThisTime, DateTime beforeThisTime, Guid UserID, string userType, string strAssign)
        {

            var where = PredicateBuilder.True<ServiceOrder>();
            if (UserID != Guid.Empty)
            {
                if (userType == "customer")
                {
                    where = where.And(x => x.CustomerId == UserID.ToString());
                }
                else
                {
                    //todo:refactpr
                    where = where.And(x => x.Details.Any(y => y.ServiceSnapShot.BusinessOwnerId == UserID.ToString()));
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                where = where.And(x => x.OrderStatus == (enum_OrderStatus)Enum.Parse(typeof(enum_OrderStatus), status));
            }
            //.enum_OrderSearchType searchType
            switch (statusSort)//switch (searchType)
            {
                //case enum_OrderSearchType.De:
                case "done":
                    where = where.And(x => x.OrderStatus == enum_OrderStatus.Finished
                              || x.OrderStatus == enum_OrderStatus.Appraised
                              || x.OrderStatus == enum_OrderStatus.EndCancel
                              || x.OrderStatus == enum_OrderStatus.EndRefund
                              || x.OrderStatus == enum_OrderStatus.EndIntervention
                              || x.OrderStatus == enum_OrderStatus.EndComplaints
                              || x.OrderStatus == enum_OrderStatus.ForceStop)
                        ;
                    break;
                //case enum_OrderSearchType.Nt: 
                case "pending":
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.EndCancel
                         && x.OrderStatus != enum_OrderStatus.EndRefund
                         && x.OrderStatus != enum_OrderStatus.EndIntervention
                         && x.OrderStatus != enum_OrderStatus.EndComplaints
                         && x.OrderStatus != enum_OrderStatus.ForceStop
                         && x.OrderStatus != enum_OrderStatus.Search);

                    break;
                default:
                    //case enum_OrderSearchType.ALL:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search)
                      ;
                    break;
            }
            if (!string.IsNullOrEmpty(strAssign))
            {
                if (strAssign == "false")
                {
                    where = where.And(x => x.StaffId == "" || x.StaffId == null);
                }
                else
                {
                    where = where.And(x => x.StaffId != "" && x.StaffId != null);
                }
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.BusinessId == storeID.ToString());
            }
            //formanID传""表示未指派
            if (formanID != null)
            {
                where = where.And(x => x.StaffId == formanID);
            }
            if (afterThisTime != DateTime.MinValue)
            {
                where = where.And(x => x.OrderCreated >= afterThisTime);
            }
            if (beforeThisTime != DateTime.MinValue)
            {
                where = where.And(x => x.OrderCreated <= beforeThisTime);
            }
            long count = repoServiceOrder.GetRowCount(where);
            return count;
        }

        /// <summary>
        /// 获取商户的一条订单
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ServiceOrder GetOneOrder(Guid guid, string UserID)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            if (string.IsNullOrEmpty(UserID))
            {
                //throw new NotImplementedException("为何传入UserId");
                where = where.And(x => x.Details.Any(y => y.ServiceSnapShot.BusinessOwnerId == UserID));
            }
            where = where.And(x => x.Id == guid);
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                        && x.OrderStatus != enum_OrderStatus.DraftPushed
                        && x.OrderStatus != enum_OrderStatus.Search)
                     ;
            return repoServiceOrder.FindOne(where);
        }

        public virtual ServiceOrder GetOne(Guid guid)
        {
            return repoServiceOrder.FindById(guid);
        }

        // return DALServiceOrder.GetOne(guid);

        public void Save(ServiceOrder order)
        {
            order.LatestOrderUpdated = DateTime.Now;
            repoServiceOrder.Add(order);
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="order"></param>
        public void Update(ServiceOrder order)
        {
            order.LatestOrderUpdated = DateTime.Now;
            repoServiceOrder.Update(order);
        }

        /// <summary>
        /// 修改订单Memo
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="strMemo"></param>
        [UnitOfWork]
        public void UpdateMemo(Guid orderId,string strMemo)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.Memo = strMemo;
            Update(order);
        }

        /// <summary>
        /// 修改订单指派人员
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="staffId"></param>
        [UnitOfWork]
        public void UpdateStaff(Guid orderId, string staffId,string staffName)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.StaffId = staffId;
            order.StaffName = staffName;
            Update(order);
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>

        public IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords) //获取全部订单
        {
            return repoServiceOrder.GetAll(pageIndex, pageSize, out totalRecords);
           
        }

        public IList<ServiceOrder> GetAllByOrderStatus(enum_OrderStatus status, int pageIndex, int pageSize, out long totalRecords)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.OrderStatus == status);

            if (status == enum_OrderStatus.EndCancel)
            {
                where = where.Or(x => x.OrderStatus == enum_OrderStatus.WaitingDepositWithCanceled);
            }

            // iuow.BeginTransaction();
            var allWithstatus = repoServiceOrder.Find(where, pageIndex, pageSize, out totalRecords);

            // iuow.Commit();
            return allWithstatus;
            //return DALServiceOrder
            //   .GetAll<ServiceOrder>()
            //   .Where(x => x.OrderStatus == status)
            //   .ToList();
        }

        public IList<ServiceOrder> GetListForBusiness(string businessId, int pageNum, int pageSize, out int totalAmount)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.BusinessId == businessId);
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed && x.OrderStatus != enum_OrderStatus.Search);

            long long_totalAmount;
            var result = repoServiceOrder.Find(where, pageNum, pageSize, out long_totalAmount).ToList();
            totalAmount = (int)long_totalAmount;
            return result;
            // return DALServiceOrder.GetAllOrdersForBusiness(business.Id, pageNum, pageSize, out totalAmount);
        }


        public IList<ServiceOrder> GetListForCustomer(Guid customerId, int pageNum, int pageSize, out int totalAmount)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.CustomerId == customerId.ToString());
            //where = where.And(x => x.OrderStatus != enum_OrderStatus.Search
            //                && x.OrderStatus != enum_OrderStatus.Draft
            //                 && x.OrderStatus != enum_OrderStatus.DraftPushed);

            where = where.And(x => x.OrderStatus == enum_OrderStatus.Finished
                              || x.OrderStatus == enum_OrderStatus.Appraised
                              || x.OrderStatus == enum_OrderStatus.EndCancel
                              || x.OrderStatus == enum_OrderStatus.EndRefund
                              || x.OrderStatus == enum_OrderStatus.EndIntervention
                              || x.OrderStatus == enum_OrderStatus.EndComplaints
                              || x.OrderStatus == enum_OrderStatus.ForceStop);

            long long_totalAmount;
            var result = repoServiceOrder.Find(where, pageNum, pageSize, out long_totalAmount).ToList();
            totalAmount = (int)long_totalAmount;
            return result;
            //return DALServiceOrder.GetListForCustomer(customer, pageNum, pageSize, out totalAmount);
        }
        [UnitOfWork]
        public void Delete(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            repoServiceOrder.Delete(order);
            // DALServiceOrder.Delete(order);
        }

        public virtual ServiceOrder GetDraftOrder(string cId, string csId)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.CustomerId == cId && x.CustomerServiceId == csId && x.OrderStatus == enum_OrderStatus.Draft);
            ServiceOrder order = null;
            try
            {
                order = repoServiceOrder.FindOne(where);

            }
            catch (Exception ex)
            {
                string errMsg = "错误:用户和客服有多张草稿单!";
                log.Error(errMsg);
                log.Error(ex);
            }
            return order;


            // return DALServiceOrder.GetDraftOrder(c, cs);
        }
        public IList<ServiceOrder> GetOrderListByDate(string serviceId, DateTime date)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.ServiceId == serviceId && x.OrderCreated.Date == date.Date);

            return repoServiceOrder.Find(where).ToList();

            //return DALServiceOrder.GetOrderListByDate(service, date);
        }
        public IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime timeBegin, DateTime timeEnd)
        {
            return repoServiceOrder.GetOrderListOfServiceByDateRange(serviceId, timeBegin, timeEnd);
        }
        public ServiceOrder GetOrderByIdAndCustomer(Guid Id, string customerId)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.Id == Id && x.CustomerId == customerId);

            return repoServiceOrder.FindOne(where);

            //  return DALServiceOrder.GetOrderByIdAndCustomer(Id, customer);
        }


        #region 订单流程变化

        /// <summary>
        /// 客户端确认服务.  将推送的服务 存入订单详情
        /// </summary>
        /// <param name="order"></param>
        /// <param name="serviceId"></param>
        /// <param name="serviceSnapshot"></param>
        /// <param name="worktimeSnapshot"></param>
        [UnitOfWork]
        public void OrderFlow_ConfirmOrder(Guid orderId, string serviceId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            var pushedService = repoPushedService.FindByOrder(order);

            order.Confirm_Order(repoPushedService, serviceId, repoPayment, repoClaims);
        }

        /// <summary>
        /// 用户定金支付完成,等待后台确认订单是否到帐
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_PayDepositAndWaiting(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.checkPayWithDeposit);
        }

        /// <summary>
        /// 后台确认订单到帐
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_ConfirmDeposit(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.Payed);
        }
        /// <summary>
        /// 商家确认订单,准备执行.
        /// </summary>
        [UnitOfWork]
        public void OrderFlow_BusinessConfirm(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            log.Debug("商家确认服务:" + orderId);
            ChangeStatus(order, enum_OrderStatus.Negotiate);
            log.Debug("确认完成");
        }
        /// <summary>
        /// 商家输入协议
        /// </summary>
        /// <param name="order"></param>
        /// <param name="negotiateAmount"></param>
        [UnitOfWork]
        public void OrderFlow_BusinessNegotiate(Guid orderId, decimal negotiateAmount)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            //order.NegotiateAmount_Modified = negotiateAmount;
            order.NegotiateAmount = negotiateAmount;
            if (negotiateAmount < order.DepositAmount)//尾款可以为0，if (negotiateAmount <= order.DepositAmount)
            {
                log.Warn("协商价格小于订金");
                throw new Exception("协商价格小于订金");
            }

            ChangeStatus(order, enum_OrderStatus.isNegotiate);
        }
        /// <summary>
        /// 商户已经提交新价格，等待用户确认
        /// </summary>
        /// <param name="order"></param>

        [UnitOfWork]
        public void OrderFlow_CustomConfirmNegotiate(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            //order.NegotiateAmount = order.NegotiateAmount_Modified;
            ChangeStatus(order, enum_OrderStatus.Assigned);
        }
        /// <summary>
        /// 用户不同意协商价格
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_CustomerDisagreeNegotiate(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.NegotiateAmount = order.OrderAmount;
            ChangeStatus(order, enum_OrderStatus.Negotiate);
        }
        /// <summary>
        /// 用户确认协商价格,并确定开始服务
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_BusinessStartService(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.OrderServerStartTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Begin);
        }
        /// <summary>
        /// 商家确定服务完成
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_BusinessFinish(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.OrderServerFinishedTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.isEnd);
        }
        /// <summary>
        /// 用户确认服务完成。
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_CustomerFinish(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.OrderServerFinishedTime = DateTime.Now;
            if (order.DepositAmount == order.NegotiateAmount)
            {
                ChangeStatus(order, enum_OrderStatus.Finished);
            }
            else
            {
                ChangeStatus(order, enum_OrderStatus.Ended);
                order.ApplyPay(enum_PayTarget.FinalPayment, repoPayment, repoClaims);
            }
        }
        /// <summary>
        /// 用户支付尾款
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_CustomerPayFinalPayment(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.checkPayWithNegotiate);
        }

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_OrderFinished(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.OrderFinished = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Finished);
        }

        /// <summary>
        /// 订单评价
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_CustomerAppraise(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.Appraised);
        }

        /// <summary>
        /// 用户申请理赔
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public bool OrderFlow_CustomerRefund(string orderId, bool isNeedRefund, decimal refundAmount)
        {
            ServiceOrder order = repoServiceOrder.FindById(new Guid(orderId));
            bool refund = false;
            enum_OrderStatus oldStatus = order.OrderStatus;
            log.Debug("当前订单状态:" + oldStatus.ToString());


            OrderServiceFlow.ChangeStatus(order, enum_OrderStatus.Refund);
            log.Debug("订单状态可改为 isRefund");
            order.OrderStatus = oldStatus;

            int Warranty = 4320;
            double minutes = 0;
            //没有完成的服务.
            if (order.OrderFinished != DateTime.MinValue)
            {
                minutes = (DateTime.Now - order.OrderFinished).TotalMinutes;
            }
            if (oldStatus != enum_OrderStatus.Finished && (minutes < 0 || minutes > Warranty))
            {
                refund = false;
            }
            else
            {
                if (isNeedRefund)
                {
                    //查询尾款
                    Payment payment = repoPayment.GetPayedByTarget(order, enum_PayTarget.FinalPayment);
                    if (payment == null)
                    {
                        log.Debug("订单" + order.Id + "没有尾款支付项");
                        throw new Exception("订单" + order.Id + "没有尾款支付项");
                    }

                    if (refundAmount > payment.Amount)
                    {
                        log.Debug("订单理赔金额不得大于订单尾款，订单id：" + order.Id.ToString());
                        throw new Exception("订单理赔金额不得大于订单尾款，订单id：" + order.Id.ToString());
                    }
                }

                ChangeStatus(order, enum_OrderStatus.Refund);
                ChangeStatus(order, enum_OrderStatus.WaitingRefund);

                refund = true;
            }

            return refund;
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_BusinessIsRefund(Guid orderId, string memberId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            enum_OrderStatus oldStatus = order.OrderStatus;
            log.Debug("当前订单状态:" + oldStatus.ToString());

            OrderServiceFlow.ChangeStatus(order, enum_OrderStatus.isRefund);
            log.Debug("订单状态可改为 isRefund");

            order.OrderStatus = oldStatus;

            log.Debug("开始退还尾款");
            log.Debug("查询订单的理赔");
            Claims claims = repoClaims.GetOneByOrder(order);
            if (claims == null)
            {
                log.Error("订单没有对应的理赔");
                throw new Exception("订单没有对应的理赔");
            }

            log.Debug("查询理赔详情");
            IList<ClaimsDetails> cdList = claims.ClaimsDatailsList.OrderByDescending(x => x.LastUpdateTime).Where(x => x.Target == enum_ChatTarget.user).ToList();
            ClaimsDetails claimsDetails;
            if (cdList.Count > 0)
            {
                claimsDetails = cdList[0];
            }
            else
            {
                log.Error("该订单没有理赔");
                throw new Exception("该订单没有理赔");
            }

            log.Debug("查询订单尾款");
            Payment payment = repoPayment.GetPayedByTarget(order, enum_PayTarget.FinalPayment);
            if (payment == null)
            {
                log.Debug("订单" + order.Id + "没有尾款支付项");
                throw new Exception("订单" + order.Id + "没有尾款支付项");
            }

            if (ApplyRefund(payment, claimsDetails.Amount, "商家同意理赔时退还尾款"))
            {
                log.Debug("更新订单状态");

                ChangeStatus(order, enum_OrderStatus.EndRefund);
                log.Debug("记录商户操作");

                claims.AddDetailsFromClaims(string.Empty, 0, null, enum_ChatTarget.store, memberId);
                //repoClaims.Update(claims);
            }
            else
            {
                log.Error("退款失败");
                throw new Exception("退款失败");
            }
        }

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_RefundSuccess(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.EndRefund);
        }

        /// <summary>
        /// 商户要求支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_BusinessAskPayWithRefund(Guid orderId, string context, decimal amount, IList<string> resourcesUrl, string memberId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            log.Debug("查询订单的理赔");
            Claims claims = repoClaims.GetOneByOrder(order);
            if (claims == null)
            {
                log.Error("订单没有对应的理赔");
                throw new Exception("订单没有对应的理赔");
            }

            log.Debug("记录商户操作");
            claims.AddDetailsFromClaims(context, amount, resourcesUrl, enum_ChatTarget.store, memberId);
            // dalClaims.Update(claims);

            ChangeStatus(order, enum_OrderStatus.AskPayWithRefund);
        }

        /// <summary>
        /// 商户驳回理赔请求
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_BusinessRejectRefund(Guid orderId, string memberId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            log.Debug("查询订单的理赔");
            Claims claims = repoClaims.GetOneByOrder(order);
            if (claims == null)
            {
                log.Error("订单没有对应的理赔");
                throw new Exception("订单没有对应的理赔");
            }

            log.Debug("记录商户操作");

            claims.AddDetailsFromClaims(string.Empty, 0, null, enum_ChatTarget.store, memberId);
            //dalClaims.Update(claims);

            ChangeStatus(order, enum_OrderStatus.RejectRefund);
        }

        /// <summary>
        /// 用户同意支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public void OrderFlow_WaitingPayWithRefund(Guid orderId, string memberId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            log.Debug("查询订单的理赔");
            Claims claims = repoClaims.GetOneByOrder(order);
            if (claims == null)
            {
                log.Error("订单没有对应的理赔");
                throw new Exception("订单没有对应的理赔");
            }

            log.Debug("查询理赔详情");
            IList<ClaimsDetails> cdList = claims.ClaimsDatailsList.OrderByDescending(x => x.LastUpdateTime).Where(x => x.Target == enum_ChatTarget.store).ToList();
            ClaimsDetails claimsDetails;
            if (cdList.Count > 0)
            {
                claimsDetails = cdList[0];
            }
            else
            {
                log.Error("该订单没有理赔");
                throw new Exception("该订单没有理赔");
            }

            order.ApplyPay(enum_PayTarget.Compensation, repoPayment, repoClaims);

            log.Debug("记录用户操作");
            claims.AddDetailsFromClaims(string.Empty, 0, null, enum_ChatTarget.user, memberId);
            //dalClaims.Update(claims);

            ChangeStatus(order, enum_OrderStatus.WaitingPayWithRefund);
        }

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayRefund(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.checkPayWithRefund);
        }

        /// <summary>
        /// 一点办官方介入
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBInsertIntervention(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.InsertIntervention);
        }

        /// <summary>
        /// 等待一点办官方处理
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBHandleWithIntervention(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.HandleWithIntervention);
        }

        /// <summary>
        /// 一点办已确认理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBConfirmNeedRefund(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.NeedRefundWithIntervention);
            //todo:等待退还金额
        }

        /// <summary>
        /// 一点办要求用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_NeedPayInternention(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.NeedPayWithIntervention);
        }

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayInternention(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.checkPayWithIntervention);
        }

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ConfirmInternention(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.EndIntervention);
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ForceStop(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.ForceStop);
        }


        //订单状态改变通用方法

        private void ChangeStatus(ServiceOrder order, enum_OrderStatus targetStatus)
        {
            log.Debug("开始改变订单状态");
            if (order == null) { log.Debug("订单为null"); };
            enum_OrderStatus oldStatus = order.OrderStatus;
            
            log.Debug("1");
            OrderServiceFlow.ChangeStatus(order, targetStatus);

            //保存订单历史记录
            //order.OrderStatus = oldStatus;
            order.SaveOrderHistory(oldStatus, repoStateChangeHis);
            log.Debug("2");
            //更新订单状态
            order.OrderStatus = targetStatus;
 
            log.Debug("3");
 
            Update(order);
 
            //       NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            log.Debug("当前订单状态为:" + targetStatus);

            log.Debug("调用IMServer,发送订单状态变更通知");
            //订单的主要参数
            string uriParameter = "&orderid=" + order.Id
                                + "&ordertitle=" + order.Title
                                + "&orderstatus=" + order.OrderStatus.ToString()
                                + "&ordertype=" + order.ServiceTypeName
                                + "&orderstatusfriendly=" + order.GetStatusTitleFriendly(order.OrderStatus);
            //发送给用户
            string uriParameterByCustomer = uriParameter + "&userid=" + order.CustomerId + "&toresource=" + enum_XmppResource.YDBan_User;

            httpRequest.CreateHttpRequest(Dianzhu.Config.Config.GetAppSetting("NotifyServer") + "type=ordernotice" + uriParameterByCustomer);
            //发送给商户
            if (order.BusinessId == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(order.ServiceBusinessOwnerId))
            {
                return;
            }
            string uriParameterByStore = uriParameter + "&userid=" + order.ServiceBusinessOwnerId
                                                      + "&toresource=" + enum_XmppResource.YDBan_Store;

            httpRequest.CreateHttpRequest(Dianzhu.Config.Config.GetAppSetting("NotifyServer") + "type=ordernotice" + uriParameterByStore);

            //发送给指派的员工
            if (string.IsNullOrEmpty(order.StaffId))
            {
                return;
            }
            string uriParameterByStaff = uriParameter + "&userid=" + order.StaffId
                                                      + "&toresource=" + enum_XmppResource.YDBan_Staff;

            httpRequest.CreateHttpRequest(Dianzhu.Config.Config.GetAppSetting("NotifyServer") + "type=ordernotice" + uriParameterByStaff);

        }

        [UnitOfWork]
        public void OrderFlow_EndCancel(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            ChangeStatus(order, enum_OrderStatus.EndCancel);
        }
        #endregion

        #region 订单取消
        /// <summary>
        /// 申请取消
        /// </summary>
        /// <param name="order"></param>
        [UnitOfWork]
        public bool OrderFlow_Canceled(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            log.Debug("---------开始取消订单---------");
            bool isCanceled = false;
            enum_OrderStatus oldStatus = order.OrderStatus;
            log.Debug("当前订单状态:" + oldStatus.ToString());

            //ChangeStatus(order, enum_OrderStatus.Canceled);.
            OrderServiceFlow.ChangeStatus(order, enum_OrderStatus.Canceled);
            log.Debug("订单状态可以改为Cancled");

            order.OrderStatus = oldStatus;//还原订单状态

            switch (oldStatus)
            {
                case enum_OrderStatus.Created:
                case enum_OrderStatus.DraftPushed:
                    log.Debug("订单为Created，取消成功");
                    //order.OrderStatus = oldStatus;
                    ChangeStatus(order, enum_OrderStatus.Canceled);
                    //   NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                    ChangeStatus(order, enum_OrderStatus.EndCancel);
                    isCanceled = true;
                    break;
                case enum_OrderStatus.checkPayWithDeposit:
                case enum_OrderStatus.Payed:
                case enum_OrderStatus.Negotiate:
                case enum_OrderStatus.isNegotiate:
                case enum_OrderStatus.Assigned:
                    isCanceled = JudgeCanceled(order);
                    break;


                default: break;
            }
            log.Debug("----------取消订单完成----------");
            return isCanceled;
        }

        private bool JudgeCanceled(ServiceOrder order)
        {

            bool isCanceled = false;
            var targetTime = order.Details[0].TargetTime;
            if (DateTime.Now <= targetTime)
            {
                double timeSpan = (targetTime - DateTime.Now).TotalMinutes;
                double bookOrderRefundTimes;
                if (!double.TryParse(Dianzhu.Config.Config.GetAppSetting("BookOrderRefundTimes"), out bookOrderRefundTimes))
                {
                    log.Error("不是数字，转换出错");
                    bookOrderRefundTimes = -1;
                }
                if (bookOrderRefundTimes < 0)
                {
                    log.Debug("取消订单不退订金，取消成功");
                    ChangeStatus(order, enum_OrderStatus.Canceled);
                    //   NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                    ChangeStatus(order, enum_OrderStatus.EndCancel);
                    isCanceled = true;
                }
                else if (bookOrderRefundTimes <= timeSpan)
                {
                    log.Debug("系统判定订金是否到帐");
                    Payment payment = repoPayment.GetPayedByTarget(order, enum_PayTarget.Deposit);
                    if (payment == null)
                    {
                        log.Error("订单" + order.Id + "没有订金支付项!");
                        return false;
                    }

                    if (payment.Amount > 0)
                    {
                        switch (order.OrderStatus)
                        {
                            case enum_OrderStatus.checkPayWithDeposit:
                                log.Debug("系统没有确认到帐，等待确认到帐后退款");
                                log.Debug("更新订单状态");
                                ChangeStatus(order, enum_OrderStatus.Canceled);
                                //   NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                                ChangeStatus(order, enum_OrderStatus.WaitingDepositWithCanceled);

                                isCanceled = true;
                                break;
                            case enum_OrderStatus.Payed:
                                log.Debug("系统确认到帐，直接退款");
                                if (ApplyRefund(payment, payment.Amount, "取消订单退还订金"))
                                {
                                    log.Debug("更新订单状态");
                                    //order.OrderStatus = oldStatus;
                                    ChangeStatus(order, enum_OrderStatus.Canceled);
                                    //   NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                                    ChangeStatus(order, enum_OrderStatus.EndCancel);

                                    isCanceled = true;
                                }
                                else
                                {
                                    isCanceled = false;
                                }
                                break;
                            default:
                                return false;
                        }
                    }
                    else
                    {
                        log.Debug("更新订单状态");
                        //order.OrderStatus = oldStatus;
                        ChangeStatus(order, enum_OrderStatus.Canceled);
                        //  NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                        ChangeStatus(order, enum_OrderStatus.EndCancel);

                        isCanceled = true;
                    }
                }
                else
                {
                    log.Debug("取消订单时间不在订单保险时间内，取消成功");
                    ChangeStatus(order, enum_OrderStatus.Canceled);
                    // NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                    ChangeStatus(order, enum_OrderStatus.EndCancel);
                    isCanceled = true;
                }
            }
            else
            {
                log.Debug("取消订单时间大于预约时间，取消成功");
                ChangeStatus(order, enum_OrderStatus.Canceled);
                //   NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                ChangeStatus(order, enum_OrderStatus.EndCancel);
                isCanceled = true;
            }

            return isCanceled;
        }
        #endregion

        [UnitOfWork]
        public bool ApplyRefund(Payment payment, decimal refundAmount, string refundReason)
        {
            return ApplyRefund(payment, refundAmount, refundReason, string.Empty);
        }

        #region 退款
        /// <summary>
        /// 退款方法：退款类型(订金，尾款，赔偿金)根据payment而来
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [UnitOfWork]
        public bool ApplyRefund(Payment payment, decimal refundAmount, string refundReason, string operatorId)
        {
            //申请退款记录.
            Refund refund = new Refund(payment.Order, payment, payment.Amount, refundAmount, refundReason, payment.PlatformTradeNo, enum_RefundStatus.Fail, string.Empty);
            repoRefund.Add(refund);
            //调用API 执行退款
            IRefundApi refundApi = refundFactory.CreateRefund(payment.PayApi, refund.Id, refundAmount, payment.Amount, payment.PlatformTradeNo, operatorId);
            bool refundResult = refundApi.GetRefundResponse();

            if (refundResult)
            {
                refund.RefundStatus = enum_RefundStatus.Success;
            }
            else
            {
                refund.RefundStatus = enum_RefundStatus.Fail;
            }

            return refundResult;
        }

        #endregion

        #region 分配工作人员
        [UnitOfWork]
        public void AssignStaff(Guid orderId, string staffId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            OrderAssignment oa = new OrderAssignment();
            oa.OrderId = order.Id.ToString();
            oa.AssignedStaffId = staffId;
            repoOrderAssignment.Add(oa);
        }
        [UnitOfWork]
        public void DeassignStaff(Guid orderId, string staffId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            OrderAssignment oa = repoOrderAssignment.FindByOrderAndStaff(order, staffId);

            oa.DeAssignedTime = DateTime.Now;
            oa.Enabled = false;

            repoOrderAssignment.Update(oa);
        }
        #endregion


        public enum_OrderStatus GetOrderStatusPrevious(Guid orderId, enum_OrderStatus status)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            return repoStateChangeHis.GetOrderStatusPrevious(order, status);
        }

        public int GetServiceOrderCountWithoutDraft(Guid userid, bool isCustomerService)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            if (isCustomerService)
            {
                where = where.And(x => x.CustomerServiceId == userid.ToString());
            }
            else
            {
                where = where.And(x => x.CustomerId == userid.ToString());
            }
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed && x.OrderStatus != enum_OrderStatus.Search);

            return (int)repoServiceOrder.GetRowCount(where);

            // return DALServiceOrder.GetServiceOrderCountWithoutDraft(userid, isCustomerService);
        }
        public decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService)
        {

            var where = PredicateBuilder.True<ServiceOrder>();
            if (isCustomerService)
            {
                where = where.And(x => x.CustomerServiceId == userid.ToString());
            }
            else
            {
                where = where.And(x => x.CustomerId == userid.ToString());
            }
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed && x.OrderStatus != enum_OrderStatus.Search);

            var list = repoServiceOrder.Find(where).ToList();

            return list.Sum(x => x.DepositAmount);
            //   return DALServiceOrder.GetServiceOrderAmountWithoutDraft(userid, isCustomerService);
        }

        //查询店铺的所有订单
        public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId)
        {
            var where = PredicateBuilder.True<ServiceOrder>()
                .And(x => x.BusinessId == businessId.ToString());
            // .And(x=>x.Details.);
            return repoServiceOrder.Find(where).ToList();
            //   return DALServiceOrder.GetAllOrdersForBusiness(businessId);
        }
        //查询全部已经完成的订单
        public IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId)
        {
            var where = PredicateBuilder.True<ServiceOrder>()
                .And(x => x.BusinessId == businessId.ToString())
                .And(x => x.OrderStatus == enum_OrderStatus.Finished || x.OrderStatus == enum_OrderStatus.Appraised)
                ;
            return repoServiceOrder.Find(where).ToList();

            // return DALServiceOrder.GetAllCompleteOrdersForBusiness(businessId);
        }

        public IList<ServiceOrder> GetOrdersForShare()
        {
            var where = PredicateBuilder.True<ServiceOrder>()
                .And(x => x.IsShared == false)
                .And(x => x.OrderStatus == enum_OrderStatus.Finished || x.OrderStatus == enum_OrderStatus.Appraised);


            return repoServiceOrder.Find(where).ToList();
        }
        [UnitOfWork]
        public void OrderShared(Guid orderId)
        {
            ServiceOrder order = repoServiceOrder.FindById(orderId);
            order.IsShared = true;
            Update(order);
        }

        [UnitOfWork]
        public ServiceOrder CreateDraftOrder(string customerSericeId, string customerId)
        {
            ServiceOrder order = new ServiceOrder { CustomerServiceId = customerSericeId, CustomerId = customerId };
            repoServiceOrder.Add(order);
            return order;
        }



        /// <summary>
        /// 获取订单状态的时间线
        /// </summary>
        /// <param name="orderStatusList"></param>
        /// <returns></returns>
        public IList<StatisticsInfo<ServiceOrderStateChangeHis>> GetOrderStateTimeLine(string orderId)
        {
            ServiceOrder serviceOrder = repoServiceOrder.FindById(StringHelper.CheckGuidID(orderId, "订单Id"));
            IList<ServiceOrderStateChangeHis> serviceOrderStateChangeHis = repoStateChangeHis.GetOrderHisList(serviceOrder);
            return statisticsOrderCount.GetOrderStateTimeLine(serviceOrderStateChangeHis);
        }

        //查询订单的总金额
        //查询订单的曝光率.
    }
    /// <summary>
    /// 支付宝退款返回数据
    /// </summary>


}
