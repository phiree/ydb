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
         public DALServiceOrder(string fortest):base(fortest)
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
                    iqueryover = iqueryover.Where(x => x.OrderStatus == enum_OrderStatus.Ed);
                    break;
                case enum_OrderSearchType.Nt:
                    iqueryover = iqueryover.Where(x => x.OrderStatus != enum_OrderStatus.Ed);
                    break;
                default:
                case enum_OrderSearchType.ALL:

                    break;

            }
            return iqueryover;
        }
        public int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType)
        {
          var  iqueryover = GetList(userId, searchType);
            int rowCount = iqueryover.RowCount();
            return rowCount;
        }


        public  IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            var iqueryover = GetList(userId, searchType);
            var result = iqueryover.Skip(pageNum - 1).Take(pageSize).List();
            return result;
        }
    }
}
