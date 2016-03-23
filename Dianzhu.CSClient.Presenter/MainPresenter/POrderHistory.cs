using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using Dianzhu.DAL;
namespace Dianzhu.CSClient.Presenter
{
     /// <summary>
     /// 订单列表显示
     /// 1)筛选订单
     /// </summary>
    public  class POrderHistory
    {
        IViewOrderHistory viewOrderHistory;
        IList<ServiceOrder> orderList;
        BLLServiceOrder bllServiceOrder;
        Dictionary<DZMembership, IList<ServiceOrder>> allList;

        public POrderHistory() { }
        public POrderHistory(IViewOrderHistory viewOrderHistory,IViewIdentityList viewIdentityList)
        {
            this.viewOrderHistory = viewOrderHistory;
            this.orderList = new List<ServiceOrder>();
            this.bllServiceOrder = new BLLServiceOrder();
            this.allList = new Dictionary<DZMembership, IList<ServiceOrder>>();

            viewOrderHistory.SearchOrderHistoryClick += ViewOrderHistory_SearchOrderHistoryClick;
            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
        }

        private void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            //加载历史订单
            int totalAmount;
            IList<ServiceOrder> orderList = bllServiceOrder.GetListForCustomer(serviceOrder.Customer, 1, 999, out totalAmount);
            if (!allList.ContainsKey(serviceOrder.Customer))
            {
                allList.Add(serviceOrder.Customer, orderList);
            }            
            viewOrderHistory.OrderList = orderList;
        }

        private void ViewOrderHistory_SearchOrderHistoryClick()
        {
            IList<ServiceOrder> searchList = new List<ServiceOrder>();
            if (viewOrderHistory.SearchStr == string.Empty)
            {
                viewOrderHistory.OrderList = allList[IdentityManager.CurrentIdentity.Customer];
            }
            else
            {
                foreach (ServiceOrder order in allList[IdentityManager.CurrentIdentity.Customer])
                {
                    if (order.Details.Count > 0)
                    {
                        if (order.Details[0].ServieSnapShot.ServiceName.Contains(viewOrderHistory.SearchStr))
                        {
                            searchList.Add(order);
                        }
                    }
                    //todo：有了订单编号，查询订单编号
                    //if (order.OrderNum == searchStr)
                    //{
                    //    searchList = new List<ServiceOrder>();
                    //    searchList.Add(order);
                    //}
                }
                viewOrderHistory.OrderList = searchList;
            }            
        }
    }

}
