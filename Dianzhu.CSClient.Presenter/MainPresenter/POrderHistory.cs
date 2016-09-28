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
using Dianzhu.CSClient.ViewModel;
using Dianzhu.CSClient.Presenter.VMAdapter;

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
        LocalStorage.LocalHistoryOrderManager localHistoryOrderManager;
        IVMOrderHistoryAdapter vmOrderHistoryAdapter;

        public POrderHistory() { }

        public POrderHistory(IViewOrderHistory viewOrderHistory, IViewIdentityList viewIdentityList, IBLLServiceOrder bllServiceOrder, IInstantMessage.InstantMessage iIM, LocalStorage.LocalHistoryOrderManager localHistoryOrderManager, IVMOrderHistoryAdapter vmOrderHistoryAdapter)
        {
            this.viewOrderHistory = viewOrderHistory;
            this.orderList = new List<ServiceOrder>();
            this.bllServiceOrder = bllServiceOrder;
            this.localHistoryOrderManager = localHistoryOrderManager;
            this.vmOrderHistoryAdapter = vmOrderHistoryAdapter;

            viewOrderHistory.SearchOrderHistoryClick += ViewOrderHistory_SearchOrderHistoryClick;
            viewOrderHistory.BtnMoreOrder += ViewOrderHistory_BtnMoreOrder;
            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;

            viewOrderHistory.OrderList = new List<VMOrderHistory>();
        }

        private void ViewOrderHistory_BtnMoreOrder()
        {
            viewOrderHistory.ShowListLoadingMsg();
            int totalAmount;
            IList<ServiceOrder> orderList = bllServiceOrder.GetListForCustomer(IdentityManager.CurrentIdentity.Customer.Id, viewOrderHistory.OrderPage, 5, out totalAmount);
            if (orderList.Count > 0)
            {
                if (orderList.Count == 5)
                {
                    viewOrderHistory.ShowMoreOrderList();
                    viewOrderHistory.OrderPage++;
                }
                else
                {
                    viewOrderHistory.ShowNoMoreOrderList();
                }

                VMOrderHistory vmOrderHistory;
                foreach (ServiceOrder order in orderList)
                {
                    vmOrderHistory = vmOrderHistoryAdapter.OrderToVMOrderHistory(order);

                    viewOrderHistory.OrderList.Add(vmOrderHistory);

                    localHistoryOrderManager.Add(IdentityManager.CurrentIdentity.Customer.Id.ToString(), vmOrderHistory);

                    viewOrderHistory.InsertOneOrder(vmOrderHistory);
                }
            }
            else
            {
                viewOrderHistory.ShowNoMoreOrderList();
            }
        }

        BackgroundWorker worker;
        private void ViewIdentityList_IdentityClick(VMIdentity vmIdentity)
        {
            if (IdentityManager.CurrentIdentity == null)
            { return; }

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(vmIdentity.CustomerId);

            log.Debug("开始异步加载历史订单");
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IList<VMOrderHistory> orderList = e.Result as List<VMOrderHistory>;
            
            if (orderList.Count > 0)
            {
                if (orderList.Count == 5)
                {
                    viewOrderHistory.ShowMoreOrderList();
                    viewOrderHistory.OrderPage++;
                }
                else
                {
                    viewOrderHistory.ShowNoMoreOrderList();
                }
                
                foreach (var vmOrderHistory in orderList)
                {
                    viewOrderHistory.InsertOneOrder(vmOrderHistory);

                    viewOrderHistory.OrderList.Add(vmOrderHistory);
                }
            }
            else
            {
                viewOrderHistory.ShowNullListLable();
            }

            log.Debug("异步加载历史订单完成");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                var customerId = Guid.Parse(e.Argument.ToString());
                int totalAmount;
                viewOrderHistory.OrderPage = 1;

                IList<VMOrderHistory> vmList = new List<VMOrderHistory>();
                if (localHistoryOrderManager.LocalHistoryOrders.ContainsKey(customerId.ToString()))
                {
                    vmList = localHistoryOrderManager.LocalHistoryOrders[customerId.ToString()];
                }
                else
                {
                    IList<ServiceOrder> list = bllServiceOrder.GetListForCustomer(customerId, 1, 5, out totalAmount);

                    if (list.Count > 0)
                    {
                        VMOrderHistory vmOrderHistory;
                        foreach (var item in list)
                        {
                            vmOrderHistory = vmOrderHistoryAdapter.OrderToVMOrderHistory(item);
                            localHistoryOrderManager.Add(customerId.ToString(), vmOrderHistory);
                        }

                        vmList = localHistoryOrderManager.LocalHistoryOrders[customerId.ToString()];
                    }
                }

                e.Result = vmList;

                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
            catch (Exception ee)
            {
                PHSuit.ExceptionLoger.ExceptionLog(log, ee);
            }
        }

        private void ViewOrderHistory_SearchOrderHistoryClick()
        {
            string orderId = IdentityManager.CurrentIdentity.Customer.Id.ToString();
            //fix #143
            if (IdentityManager.CurrentIdentity == null)
            {
                return;
            }
            if (localHistoryOrderManager.LocalHistoryOrders[orderId].Count == 0)
            {
                return;
            }

            IList<VMOrderHistory> searchList = new List<VMOrderHistory>();
            if (viewOrderHistory.SearchStr == string.Empty)
            {
                searchList = localHistoryOrderManager.LocalHistoryOrders[orderId];
            }
            else
            {
                foreach (var order in localHistoryOrderManager.LocalHistoryOrders[orderId])
                {
                    if (order.ServiceName.Contains(viewOrderHistory.SearchStr))
                    {
                        searchList.Add(order);
                    }

                    //todo：有了订单编号，查询订单编号
                    //if (order.OrderNum == searchStr)
                    //{
                    //    searchList = new List<ServiceOrder>();
                    //    searchList.Add(order);
                    //}
                }
            }
            
            if (searchList.Count > 0)
            {
                foreach (var item in searchList)
                {
                    viewOrderHistory.InsertOneOrder(item);
                }
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
