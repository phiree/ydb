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

    public class DALServiceOrderStateChangeHis : NHRepositoryBase<ServiceOrderStateChangeHis,Guid>,IDAL.IDALServiceOrderStateChangeHis
    {
        public IQueryOver<ServiceOrderStateChangeHis> Query(ServiceOrder order)
        {
            IQueryOver<ServiceOrderStateChangeHis, ServiceOrderStateChangeHis> iqueryover = Session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.Order == order).OrderBy(x => x.Number).Desc;
            return iqueryover;
        }

        public ServiceOrderStateChangeHis GetOrderHis(ServiceOrder order)
        {
            IList<ServiceOrderStateChangeHis> i = Find(x => x.Order.Id == order.Id && x.NewStatus == order.OrderStatus).OrderByDescending(x=>x.Number).ToList();
            if (i.Count > 0)
            { return i[0]; }
            else
            {
                return null;
            }
            //return Find(x => x.Order.Id == order.Id && x.NewStatus == order.OrderStatus)[0];
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
            return FindOne(x => x.Order.Id == order.Id && x.NewStatus == status).CreatTime;
        }

        public enum_OrderStatus GetOrderStatusPrevious(ServiceOrder order,enum_OrderStatus status)
        {
            return FindOne(x => x.Order.Id == order.Id && x.NewStatus == status).OldStatus;
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
