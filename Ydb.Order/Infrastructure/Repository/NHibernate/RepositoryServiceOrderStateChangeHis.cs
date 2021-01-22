using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
using Ydb.Common;
namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
   public class RepositoryServiceOrderStateChangeHis : NHRepositoryBase<ServiceOrderStateChangeHis, Guid>, IRepositoryServiceOrderStateChangeHis
    {
        public IQueryOver<ServiceOrderStateChangeHis> Query(ServiceOrder order)
        {
            IQueryOver<ServiceOrderStateChangeHis, ServiceOrderStateChangeHis> iqueryover = session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.Order == order).OrderBy(x => x.Number).Desc;
            return iqueryover;
        }

        public ServiceOrderStateChangeHis GetOrderHis(ServiceOrder order)
        {
            IList<ServiceOrderStateChangeHis> i = Find(x => x.Order.Id == order.Id && x.NewStatus == order.OrderStatus).OrderByDescending(x => x.Number).ToList();
            if (i.Count > 0)
            {
                return i[0];
            }
            else
            {
                return null;
            }

            //return FindOne(x => x.Order.Id == order.Id && x.NewStatus == order.OrderStatus);
        }

        public ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order)
        {


            IList<ServiceOrderStateChangeHis> statusList = session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.NewStatus == order.OrderStatus).List();
            
            if (statusList.Count > 0)
            {
                return statusList[0];
            }
            return null;

        }

        public IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order)
        {

            var query = Query(order);
            IList<ServiceOrderStateChangeHis> orderList = query.List().OrderBy(x => x.Number).ToList();
            
            return orderList;

        }

        public DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status)
        {
            return FindOne(x => x.Order.Id == order.Id && x.NewStatus == status).CreatTime;
        }

        public enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order, enum_OrderStatus status)
        {
            return FindOne(x => x.Order.Id == order.Id && x.NewStatus == status).OldStatus;
        }


    }
}
