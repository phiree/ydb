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
                        && x.OrderStatus == enum_OrderStatus.Appraise
                        );
                    break;
                case enum_OrderSearchType.Nt:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Draft
                        && x.OrderStatus != enum_OrderStatus.Finished
                        && x.OrderStatus != enum_OrderStatus.Aborded
                        && x.OrderStatus != enum_OrderStatus.Appraise
                        && x.OrderStatus != enum_OrderStatus.Search
                    );
                    break;
                default:
                case enum_OrderSearchType.ALL:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Draft
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


        public IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            var iqueryover = GetList(userId, searchType);
            var result = iqueryover.Skip((pageNum - 1) * pageSize).Take(pageSize).List();
            return result;
        }

        public IList<ServiceOrder> GetListForBusiness(Business business)
        {
            string sql = " select so  from  ServiceOrder so  " +
                " left join so.Service s  " +
                " left join s.Business b ";

            IQuery query = Session.CreateQuery(sql);
            var result = query.List<ServiceOrder>();

            return result;
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
                 //.And(x => x.OrderCreated.Date == dateTime.Date)
                 .List();
            return orderList;
        }
    }
}
