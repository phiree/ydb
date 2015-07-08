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

        public IList<ServiceOrder> GetListByUser(Guid userId)
        {
            var list = Session.QueryOver<ServiceOrder>().Where(x => x.Customer.Id == userId).List();
            return list;
        }
        public int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType)
        {
            IQueryOver<ServiceOrder,ServiceOrder> iqueryover = Session.QueryOver<ServiceOrder>().Where(x=>x.Customer.Id==userId);
           
            switch (searchType)
            {
               
                case enum_OrderSearchType.De:
                    iqueryover = iqueryover.Where(x => x.OrderStatus == enum_OrderStatus.Ed);
                    break;
                case enum_OrderSearchType.Nt: 
                    iqueryover = iqueryover.Where(x => x.OrderStatus != enum_OrderStatus.Ed);
                    break;
                default:
                case enum_OrderSearchType.ALL:

                    break;
                
            }
            int rowCount = iqueryover.RowCount();
            return rowCount;
        }


        public  IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            IQueryOver<ServiceOrder, ServiceOrder> iqueryover = Session.QueryOver<ServiceOrder>().Where(x => x.Customer.Id == userId);

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    iqueryover = iqueryover.Where(x => x.OrderStatus == enum_OrderStatus.Ed);
                    break;
                case enum_OrderSearchType.Nt:
                    iqueryover = iqueryover.Where(x => x.OrderStatus != enum_OrderStatus.Ed);
                    break;
                default:
                case enum_OrderSearchType.ALL:
                    break;
            }
            var result = iqueryover.Skip(pageNum - 1).Take(pageSize).List();
            return result;
        }
    }
}
