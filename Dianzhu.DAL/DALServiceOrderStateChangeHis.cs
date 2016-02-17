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
    public class DALServiceOrderStateChangeHis : DALBase<ServiceOrderStateChangeHis>
    {
        public DALServiceOrderStateChangeHis()
        {

        }
        //注入依赖,供测试使用;
        public DALServiceOrderStateChangeHis(string fortest) : base(fortest)
        {

        }

        public IQueryOver<ServiceOrderStateChangeHis> Query(ServiceOrder order)
        {
            IQueryOver<ServiceOrderStateChangeHis, ServiceOrderStateChangeHis> iqueryover = Session.QueryOver<ServiceOrderStateChangeHis>().Where(x => x.Order == order).OrderBy(x => x.Number).Desc;
            return iqueryover;
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

        public IList<ServiceOrderStateChangeHis> GetFirstTwoOrderHisListByOrder(ServiceOrder order)
        {
            var query = Query(order);
            IList<ServiceOrderStateChangeHis> orderList = query.Take(2).List();
            if (orderList.Count > 0)
            {
                return orderList;
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
    }
}
