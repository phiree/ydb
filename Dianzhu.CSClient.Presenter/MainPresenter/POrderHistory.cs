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
using System.ComponentModel;

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
        Dictionary<Guid, IList<ServiceOrder>> allList;
           public POrderHistory() { }

        public POrderHistory(IViewOrderHistory viewOrderHistory,IViewIdentityList viewIdentityList,IBLLServiceOrder bllServiceOrder,IInstantMessage.InstantMessage iIM)
        {
            this.viewOrderHistory = viewOrderHistory;
            this.orderList = new List<ServiceOrder>();
            this.bllServiceOrder = bllServiceOrder;
            this.allList = new Dictionary<Guid, IList<ServiceOrder>>();

            viewOrderHistory.SearchOrderHistoryClick += ViewOrderHistory_SearchOrderHistoryClick;
            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
        }

        private void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            if (IdentityManager.CurrentIdentity == null)
            { return; }

            log.Debug("开始异步加载历史订单");
            //viewOrderHistory.ShowListLoadingMsg();

            int totalAmount;
            IList<ServiceOrder> orderList = bllServiceOrder.GetListForCustomer(serviceOrder.Customer.Id, 1, 5, out totalAmount);
            viewOrderHistory.OrderList = orderList;
            viewOrderHistory.HideMsg();
            if (orderList.Count > 0)
            {
                foreach (ServiceOrder order in orderList)
                {
                    viewOrderHistory.AddOneOrder(order);
                }
            }
            else
            {
                viewOrderHistory.ShowNullListLable();
            }

            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //worker.RunWorkerAsync(serviceOrder.Customer.Id);            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.Debug("异步加载历史订单完成");
            viewOrderHistory.OrderList = e.Result as List<ServiceOrder>;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //加载历史订单
                log.Debug("开始异步加载历史订单");
                viewOrderHistory.ShowListLoadingMsg();
                Guid customerId = Guid.Parse(e.Argument.ToString());
                NHibernateUnitOfWork.UnitOfWork.Start();
                int totalAmount;
                IList<ServiceOrder> orderList = bllServiceOrder.GetListForCustomer(customerId, 1, 999, out totalAmount);
                if (!allList.ContainsKey(customerId))
                {
                    allList.Add(customerId, orderList);
                }
                e.Result = orderList;
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
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
            if (allList[IdentityManager.CurrentIdentity.Customer.Id].Count == 0)
            {
                return;
            }
            IList<ServiceOrder> searchList = new List<ServiceOrder>();
            if (viewOrderHistory.SearchStr == string.Empty)
            {
                viewOrderHistory.OrderList = allList[IdentityManager.CurrentIdentity.Customer.Id];
            }
            else
            {
                foreach (ServiceOrder order in allList[IdentityManager.CurrentIdentity.Customer.Id])
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
