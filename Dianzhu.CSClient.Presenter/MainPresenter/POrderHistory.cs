using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.IInstantMessage;
 
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
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.POrderHistory");

        IViewOrderHistory viewOrderHistory;
        IList<ServiceOrder> orderList;
        IBLLServiceOrder bllServiceOrder;
        Dictionary<DZMembership, IList<ServiceOrder>> allList;
           public POrderHistory() { }

        public POrderHistory(IViewOrderHistory viewOrderHistory,IViewIdentityList viewIdentityList,IBLLServiceOrder bllServiceOrder,IInstantMessage.InstantMessage iIM)

        {
            this.viewOrderHistory = viewOrderHistory;
            this.orderList = new List<ServiceOrder>();
            this.bllServiceOrder = bllServiceOrder;
            this.allList = new Dictionary<DZMembership, IList<ServiceOrder>>();

            viewOrderHistory.SearchOrderHistoryClick += ViewOrderHistory_SearchOrderHistoryClick;
            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
        }

        private void IIM_IMReceivedMessage(ReceptionChat chat)
        {
            //判断信息类型
            if(chat.ChatType== enum_ChatType.UserStatus)
            {
                ReceptionChatUserStatus rcus = (ReceptionChatUserStatus)chat;

                if (rcus.Status == Model.Enums.enum_UserStatus.unavailable)
                {
                    if (IdentityManager.CurrentIdentity == null || IdentityManager.CurrentIdentity == chat.ServiceOrder)
                    {
                        ClearSearchList();
                    }
                }
            }
        }

        private void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            try
            {
                if (IdentityManager.CurrentIdentity == null)
                { return; }
                //加载历史订单
                int totalAmount;
                IList<ServiceOrder> orderList = bllServiceOrder.GetListForCustomer(serviceOrder.Customer, 1, 5, out totalAmount);
                if (!allList.ContainsKey(serviceOrder.Customer))
                {
                    allList.Add(serviceOrder.Customer, orderList);
                }
                viewOrderHistory.OrderList = orderList;
            }
            catch (Exception ex)
            {
                log.Error("异常");
                PHSuit.ExceptionLoger.ExceptionLog(log, ex);
            }
        }

        private void ViewOrderHistory_SearchOrderHistoryClick()
        {
            //fix #143
            if (IdentityManager.CurrentIdentity == null)
            {
                return;
            }
            if (allList[IdentityManager.CurrentIdentity.Customer].Count == 0)
            {
                return;
            }
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

        /// <summary>
        /// 清楚历史记录面板
        /// </summary>
        public void ClearSearchList()
        {
            viewOrderHistory.OrderList = null;
        }
    }

}
