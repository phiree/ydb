using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using NHibernate;
using NHibernate.Criterion;
namespace Dianzhu.DAL
{
    public class DALServiceOrder : DALBase<ServiceOrder>
    {
        public DALServiceOrder()
        {

        }
        //注入依赖,供测试使用;
        public DALServiceOrder(string fortest) : base(fortest)
        {

        }
        public IList<ServiceOrder> GetListByUser(Guid userId)
        {
            var iqueryover = GetList(userId, enum_OrderSearchType.ALL);
            return iqueryover.List();
        }
        private IQueryOver<ServiceOrder> GetList(Guid userId, enum_OrderSearchType searchType)
        {
            IQueryOver<ServiceOrder, ServiceOrder> iqueryover = Session.QueryOver<ServiceOrder>().Where(x => x.Customer.Id == userId);

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus == enum_OrderStatus.Finished
                        && x.OrderStatus == enum_OrderStatus.Aborded
                        && x.OrderStatus == enum_OrderStatus.Appraised
                        );
                    break;
                case enum_OrderSearchType.Nt:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Aborded
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.Search
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
        public int GetServiceOrderCountWithoutDraft(Guid userid,bool isCustomerService)
        {
            var iqueryover = Session.QueryOver<ServiceOrder>();
            iqueryover = isCustomerService ? iqueryover.Where(x => x.CustomerService.Id == userid)
                                        : iqueryover.Where(x => x.Customer.Id == userid);
            iqueryover = iqueryover.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed);
            return iqueryover.RowCount();
        }
        public decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService)
        {
            var iqueryover = Session.QueryOver<ServiceOrder>();
            iqueryover = isCustomerService ? iqueryover.Where(x => x.CustomerService.Id == userid)
                                        : iqueryover.Where(x => x.Customer.Id == userid);
            iqueryover = iqueryover.And(x => (int)x.OrderStatus !=(int) enum_OrderStatus.Draft).And(x=>(int) x.OrderStatus != (int)enum_OrderStatus.DraftPushed);

           return iqueryover.List().Sum(x => x.DepositAmount);

            
        }

        public IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            var iqueryover = GetList(userId, searchType);
            var result = iqueryover.Skip((pageNum - 1) * pageSize).Take(pageSize).List();
            return result;
        }

        public IList<ServiceOrder> GetListForBusiness(Business business, int pageNum, int pageSize, out int totalAmount)
        {
            var iquery = Session.QueryOver<ServiceOrder>()
             //   .Where(a => a.Details.Select(x => x.OriginalService).Select(y => y.Business).Contains(business));
            ;
                //.JoinQueryOver(x => x.Service)
                //.JoinQueryOver(y => y.Business)
               
            totalAmount = iquery.RowCount();

            IList<ServiceOrder> list = GetAllOrdersForBusiness(business.Id).OrderByDescending(x=>x.LatestOrderUpdated).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            return list;
        }

        public IList<ServiceOrder> GetListForCustomer(DZMembership customer,int pageNum,int pageSize,out int totalAmount)
        {
            using (var t = Session.BeginTransaction())
            {
                var iquery = Session.QueryOver<ServiceOrder>().Where(x => x.Customer == customer).And(x => x.OrderStatus != enum_OrderStatus.Draft).And(x => x.OrderStatus != enum_OrderStatus.DraftPushed);
                totalAmount = iquery.RowCount();

                IList<ServiceOrder> list = iquery.OrderBy(x => x.OrderFinished).Desc.Skip((pageNum - 1) * pageSize).Take(pageSize).List();

              
                t.Commit();
                return list;
            }
           
        }

        /// <summary>
        /// 获得草稿订单
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ServiceOrder GetDraftOrder(DZMembership c, DZMembership cs)
        {
            var order = Session.QueryOver<ServiceOrder>().
                Where(x => x.Customer == c).
                And(x => x.CustomerService == cs).
                And(x => x.OrderStatus == enum_OrderStatus.Draft).List();

            if (order.Count > 0)
            {
                return (ServiceOrder)order[0];
            }
            else
            {
                return null;
            }
        }

        public IList<ServiceOrder> GetOrderListByDate(DZService service, DateTime dateTime)
        {
            var orderList = Session.QueryOver<ServiceOrder>()
                 .Where(x => x.Service == service)
                 .And(x => x.OrderCreated.Date == dateTime.Date)
                 .List();
            return orderList;
        }

        public ServiceOrder GetOrderByIdAndCustomer(Guid Id, DZMembership customer)
        {
            return Session.QueryOver<ServiceOrder>().Where(x => x.Id == Id).And(x => x.Customer == customer).SingleOrDefault();
        }
        public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId,int pageIndex,int pageSize,out int totalRecords)
        {
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "' "+
                           
                      " and ( o.OrderStatus!=" + (int)enum_OrderStatus.Draft +
                            " or o.OrderStatus!=" + (int)enum_OrderStatus.DraftPushed + " )"
                            ;
            
            var list = GetList(query,pageIndex,pageSize,out totalRecords );

            return list;
        }
        public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId)
        {
            var query = "select o from ServiceOrder as o "+
                           " inner join o.Details  as d "+
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId+"'";
           var list=  GetList(query);

            return list;
        }

        public IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId)
        {
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'"+
                            " and ( o.OrderStatus="+(int)enum_OrderStatus.Finished+
                            " or o.OrderStatus="+(int)enum_OrderStatus.Appraised+" )"
                            
                            ;

            var list = GetList(query);

            return list;
        }
        /// <summary>
        /// 用户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetCustomerCancelForBusiness(Guid businessId)
        {
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'" +
                            " and ( o.OrderStatus=" + (int)enum_OrderStatus.Finished +
                            " or o.OrderStatus=" + (int)enum_OrderStatus.Appraised + " )"

                            ;

            var list = GetList(query);

            return list;
        }
        /// <summary>
        /// 商户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetBusinessCancelOrdersForBusiness(Guid businessId)
        {
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'" +
                            " and ( o.OrderStatus=" + (int)enum_OrderStatus.Finished +
                            " or o.OrderStatus=" + (int)enum_OrderStatus.Appraised + " )"

                            ;

            var list = GetList(query);

            return list;
        }
    }
}
