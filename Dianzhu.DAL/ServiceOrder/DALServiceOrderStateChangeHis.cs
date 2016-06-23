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
    //20160622_longphui_modify
    public class DALServiceOrderStateChangeHis : NHRepositoryBase<ServiceOrderStateChangeHis, Guid>, IDAL.IDALServiceOrderStateChangeHis //DALBase<ServiceOrderStateChangeHis>
    {
        public DALServiceOrderStateChangeHis()
        {

        }
        //注入依赖,供测试使用;
        //public DALServiceOrderStateChangeHis(string fortest) : base(fortest)
        //{

        //}

        public IQueryOver<ServiceOrderStateChangeHis> Query(ServiceOrder order)
        {
            IQueryOver<ServiceOrderStateChangeHis, ServiceOrderStateChangeHis> iqueryover = Session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.Order == order).OrderBy(x => x.Number).Desc;
            return iqueryover;
        }

        public ServiceOrderStateChangeHis GetOrderHis(ServiceOrder order)
        {
            //20160622_longphui_modify
            var query = Session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.Order == order).And(x => x.NewStatus == order.OrderStatus);
            DALBase<ServiceOrderStateChangeHis> b = new DALBase<ServiceOrderStateChangeHis>();
            var item = b.GetOneByQuery(query);
            return item;
        }

        public ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order)
        {
            var query = Query(order);
            IList<ServiceOrderStateChangeHis> orderList = query.Take(1).List();
            if (orderList.Count > 0)
            {
                return orderList[0];
            }
            return null;
        }

        public IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order)
        {
            var query = Query(order);
            IList<ServiceOrderStateChangeHis> orderList = query.List().OrderBy(x => x.Number).ToList();
            if (orderList.Count > 0)
            {
                return orderList;
            }
            return null;
        }

        public DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status)
        {
            var query = Session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.Order == order).And(x => x.NewStatus == status);
            DALBase<ServiceOrderStateChangeHis> b = new DALBase<ServiceOrderStateChangeHis>();
            var item = b.GetOneByQuery(query);
            return item.CreatTime;
        }

        public enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order,enum_OrderStatus status)
        {
            var query = Session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.Order == order).And(x => x.NewStatus == status);
            DALBase<ServiceOrderStateChangeHis> b = new DALBase<ServiceOrderStateChangeHis>();
            var item = b.GetOneByQuery(query).OldStatus;
            return item;
        }

        /// <summary>
        /// 更新为该状态时的时间
        /// </summary>
        /// <returns></returns>
        //public DateTime GetOrderStatusTime(ServiceOrder order, enum_OrderStatus status)
        //{
        //    return null;
        //}
    }
}
