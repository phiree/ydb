using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using Ydb.Common;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
using Ydb.Common.Specification;
namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
   public class RepositoryServiceOrder : NHRepositoryBase<ServiceOrder, Guid>, IRepositoryServiceOrder
    {
        public ServiceOrder GetOne(Guid id)
        {
            return FindById(id);
        }
        private IQueryOver<ServiceOrder> GetList(Guid userId, enum_OrderSearchType searchType)
        {
            IQueryOver<ServiceOrder, ServiceOrder> iqueryover = session.QueryOver<ServiceOrder>().Where(x => x.CustomerId == userId.ToString());

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus == enum_OrderStatus.Finished
                        || x.OrderStatus == enum_OrderStatus.Appraised
                        || x.OrderStatus == enum_OrderStatus.EndWarranty
                        || x.OrderStatus == enum_OrderStatus.EndCancel
                        || x.OrderStatus == enum_OrderStatus.EndRefund
                        || x.OrderStatus == enum_OrderStatus.EndIntervention
                        || x.OrderStatus == enum_OrderStatus.ForceStop
                        );
                    break;
                case enum_OrderSearchType.Nt:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Search
                         && x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.EndWarranty
                         && x.OrderStatus != enum_OrderStatus.EndCancel
                         && x.OrderStatus != enum_OrderStatus.EndRefund
                         && x.OrderStatus != enum_OrderStatus.EndIntervention
                         && x.OrderStatus != enum_OrderStatus.ForceStop
                    );
                    break;
                default:
                case enum_OrderSearchType.ALL:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search
                        );
                    break;

            }
            iqueryover = iqueryover.OrderBy(x => x.OrderCreated).Desc;
            return iqueryover;
        }
        public int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType)
        {

            var iqueryover = GetList(userId, searchType);
            int rowCount = iqueryover.RowCount();

            return rowCount;

        }
        /// <summary>
        /// 除了草稿(draft,draftpushed)之外的订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="isCustomerService">是客服的,否则是客户的</param>
        /// <returns></returns>
        public int GetServiceOrderCountWithoutDraft(Guid userid, bool isCustomerService)
        {
            //todo:

            var iqueryover = session.QueryOver<ServiceOrder>();
            iqueryover = isCustomerService ? iqueryover.Where(x => x.CustomerServiceId == userid.ToString())
                                        : iqueryover.Where(x => x.CustomerId == userid.ToString());
            iqueryover = iqueryover.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed);

            return iqueryover.RowCount();

        }
        public decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService)
        {
            //todo:

            var iqueryover = session.QueryOver<ServiceOrder>();
            iqueryover = isCustomerService ? iqueryover.Where(x => x.CustomerServiceId == userid.ToString())
                                        : iqueryover.Where(x => x.CustomerId == userid.ToString());
            iqueryover = iqueryover.And(x => (int)x.OrderStatus != (int)enum_OrderStatus.Draft).And(x => (int)x.OrderStatus != (int)enum_OrderStatus.DraftPushed);


            return iqueryover.List().Sum(x => x.DepositAmount);

        }

        public IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {

            var iqueryover = GetList(userId, searchType);
            var result = iqueryover.Skip((pageNum - 1) * pageSize).Take(pageSize).List();

            return result;

        }

        public IList<ServiceOrder> GetOrderListForBusiness(string businessId, int pageNum, int pageSize, out int totalAmount)
        {
            //var iquery = Session.QueryOver<ServiceOrder>()
            // //   .Where(a => a.Details.Select(x => x.OriginalService).Select(y => y.Business).Contains(business));
            //;
            //.JoinQueryOver(x => x.Service)
            //.JoinQueryOver(y => y.Business)


            totalAmount = (int)GetRowCount(x => true);

            IList<ServiceOrder> list = GetAllOrdersForBusiness(new Guid(businessId)).OrderByDescending(x => x.LatestOrderUpdated).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();


            return list;

        }

        public IList<ServiceOrder> GetListForCustomer(string customerId, int pageNum, int pageSize, out int totalAmount)
        {

            var iquery = session.QueryOver<ServiceOrder>().Where(x => x.CustomerId == customerId).Where(x => x.OrderStatus != enum_OrderStatus.Draft).Where(x => x.OrderStatus != enum_OrderStatus.DraftPushed);
            totalAmount = iquery.RowCount();

            IList<ServiceOrder> list = iquery.OrderBy(x => x.OrderFinished).Desc.Skip((pageNum - 1) * pageSize).Take(pageSize).List();



            return list;


        }

        /// <summary>
        /// 获得草稿订单
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ServiceOrder GetDraftOrder(string cId, string csId)
        {
            return FindOne(x => x.CustomerId == cId && x.CustomerServiceId == csId && x.OrderStatus == enum_OrderStatus.Draft);
        }

        public IList<ServiceOrder> GetOrderListByDate(string serviceId, DateTime dateTime)
        {
            return Find(x => x.ServiceId == serviceId && x.OrderCreated.Date == dateTime.Date);
        }

        public ServiceOrder GetOrderByIdAndCustomer(Guid Id, string customerId)
        {
            return FindOne(x => x.Id == Id && x.CustomerId == customerId);
        }
        //public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    var query = "select o from ServiceOrder as o " +
        //                   " inner join o.Details  as d " +
        //                   "  inner join d.OriginalService as s " +
        //                    " inner join s.Business as b  " +
        //                    " where b.Id='" + businessId + "' " +

        //              " and ( o.OrderStatus!=" + (int)enum_OrderStatus.Draft +
        //                    " or o.OrderStatus!=" + (int)enum_OrderStatus.DraftPushed + " )"
        //                    ;

        //    var list = GetList(query, pageIndex, pageSize, out totalRecords);

        //    return list;
        //}
        public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.ServiceSnapShot as s " +
                            " where s.BusinessId='" + businessId + "'";
            throw new NotImplementedException();
            // var list=  GetList(query);


            //return list;
        }

        public IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'" +
                            " and ( o.OrderStatus=" + (int)enum_OrderStatus.Finished +
                            " or o.OrderStatus=" + (int)enum_OrderStatus.Appraised + " )"

                            ;

            throw new NotImplementedException();
            // var list = GetList(query);

            // return list;
        }
        /// <summary>
        /// 用户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetCustomerCancelForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'" +
                            " and ( o.OrderStatus=" + (int)enum_OrderStatus.Finished +
                            " or o.OrderStatus=" + (int)enum_OrderStatus.Appraised + " )"

                            ;

            throw new NotImplementedException();
            //  var list = GetList(query);

            // return list;
        }
        /// <summary>
        /// 商户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetBusinessCancelOrdersForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'" +
                            " and ( o.OrderStatus=" + (int)enum_OrderStatus.Finished +
                            " or o.OrderStatus=" + (int)enum_OrderStatus.Appraised + " )"

                            ;

            throw new NotImplementedException();
            // var list = GetList(query);

            //return list;
        }

        public IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime timeBegin, DateTime timeEnd)
        {
            string query = " select o from ServiceOrder as o "
                           + " inner join o.Details as detail "
                           + " where 1=1"
                           + " and detail.OriginalServiceId='" + serviceId + "' ";
            if (timeBegin != DateTime.MinValue)
            {
                query = query + " and  detail.TargetTime<='" + timeEnd.AddDays(1) + "'";
            }
            if (timeEnd != DateTime.MinValue)
            {
                query = query + " and detail.TargetTime>='" + timeBegin + "'";
            }

            return session.CreateQuery(query).List<ServiceOrder>();

        }

        public IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords)
        {
            var list = session.QueryOver<ServiceOrder>()
                 .Fetch(x => x.Details).Eager
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .List();
            totalRecords = session.QueryOver<ServiceOrder>().RowCount();
            return list;
        }


        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单数量，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        public long GetOrdersCountByBusinessList(IList<string> businessIdList, bool isShared)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            if (businessIdList.Count > 0)
            {
                where = where.And(x => businessIdList.Contains(x.BusinessId));
            }
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                    && x.OrderStatus != enum_OrderStatus.DraftPushed
                    && x.OrderStatus != enum_OrderStatus.Search);
            where = where.And(x => x.IsShared == isShared);
            long count = GetRowCount(where);
            return count;
        }


        /// <summary>
        /// 根据代理区域获取该区域内所有商户的订单列表，区分是否分账
        /// </summary>
        /// <param name="businessIdList">该区域内所有商户Id列表</param>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        public IList<ServiceOrder> GetOrdersByBusinessList(IList<string> businessIdList, bool isShared)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            if (businessIdList.Count > 0)
            {
                where = where.And(x => businessIdList.Contains(x.BusinessId));
            }
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                    && x.OrderStatus != enum_OrderStatus.DraftPushed
                    && x.OrderStatus != enum_OrderStatus.Search);
            where = where.And(x => x.IsShared == isShared);
            return Find(where);
        }

        /// <summary>
        /// 根据分账统计订单
        /// </summary>
        /// <param name="isShared">订单是否分账</param>
        /// <returns></returns>
        public IList<ServiceOrder> GetOrdersByShared(bool isShared, int pageIndex, int pageSize, out long totalRecords)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                    && x.OrderStatus != enum_OrderStatus.DraftPushed
                    && x.OrderStatus != enum_OrderStatus.Search);
            where = where.And(x => x.IsShared == isShared);
            return Find(where,pageIndex,pageSize,out totalRecords);
        }

    }
}
