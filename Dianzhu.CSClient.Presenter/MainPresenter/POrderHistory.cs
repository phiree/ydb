using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

using Dianzhu.CSClient.IView;
using System.ComponentModel;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.CSClient.Presenter.VMAdapter;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;


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
        IServiceOrderService bllServiceOrder;
        IVMOrderHistoryAdapter vmOrderHistoryAdapter;

        string identity;
        public IViewOrderHistory ViewOrderHistory
        {
            get
            {
                return viewOrderHistory;
            }
        }

        public POrderHistory() { }

        public POrderHistory(
            IViewOrderHistory viewOrderHistory, 
            IViewIdentityList viewIdentityList,
            IServiceOrderService bllServiceOrder, 
            IVMOrderHistoryAdapter vmOrderHistoryAdapter,
            string identity)
        {
            this.viewOrderHistory = viewOrderHistory;
            this.bllServiceOrder = bllServiceOrder;
            this.vmOrderHistoryAdapter = vmOrderHistoryAdapter;
            this.identity = identity;

            viewOrderHistory.SearchOrderHistoryClick += ViewOrderHistory_SearchOrderHistoryClick;
            viewOrderHistory.BtnMoreOrder += ViewOrderHistory_BtnMoreOrder;
            
            viewOrderHistory.OrderList = new List<VMOrderHistory>();

            LoadDatas();
        }

        private void LoadDatas()
        {
            try
            {
                int totalAmount;
                viewOrderHistory.OrderPage = 1;

                IList<VMOrderHistory> vmList = new List<VMOrderHistory>();

                IList<ServiceOrder> list = bllServiceOrder.GetListForCustomer(new Guid(identity), 1, 5, out totalAmount);

                if (list.Count > 0)
                {
                    VMOrderHistory vmOrderHistory;
                    for (int i = 0; i < list.Count; i++)
                    {
                        vmOrderHistory = vmOrderHistoryAdapter.OrderToVMOrderHistory(list[i]);
                        vmList.Add(vmOrderHistory);
                    }
                }

                if (vmList.Count > 0)
                {
                    if (vmList.Count == 5)
                    {
                        viewOrderHistory.ShowMoreOrderList();
                        viewOrderHistory.OrderPage++;
                    }
                    else
                    {
                        viewOrderHistory.ShowNoMoreOrderList();
                    }

                    for (int i = 0; i < vmList.Count; i++)
                    {
                        viewOrderHistory.InsertOneOrder(vmList[i]);

                        viewOrderHistory.OrderList.Add(vmList[i]);
                    }
                }
                else
                {
                    viewOrderHistory.ShowNullListLable();
                }
            }
            catch (Exception ee)
            {
                log.Error(ee);
            }
            finally
            {
                //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
               // NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
        }

        private void ViewOrderHistory_BtnMoreOrder()
        {
            try
            {
               // NHibernateUnitOfWork.UnitOfWork.Start();//查询订单需开启

                viewOrderHistory.ShowListLoadingMsg();
                int totalAmount;
                IList<ServiceOrder> orderList = bllServiceOrder.GetListForCustomer(new Guid(identity), viewOrderHistory.OrderPage, 5, out totalAmount);

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
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        vmOrderHistory = vmOrderHistoryAdapter.OrderToVMOrderHistory(orderList[i]);

                        viewOrderHistory.OrderList.Add(vmOrderHistory);

                        viewOrderHistory.InsertOneOrder(vmOrderHistory);
                    }
                }
                else
                {
                    viewOrderHistory.ShowNoMoreOrderList();
                }
            }
            catch (Exception ee)
            {
                log.Error(ee);
            }
            finally
            {
               // NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
               // NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
        }

        private void ViewOrderHistory_SearchOrderHistoryClick()
        {            
            if (string.IsNullOrEmpty(identity))
            {
                return;
            }

            IList<VMOrderHistory> searchList = new List<VMOrderHistory>();
            foreach (var order in viewOrderHistory.OrderList)
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

            if (searchList.Count > 0)
            {
                foreach (var item in searchList)
                {
                    viewOrderHistory.InsertOneOrder(item);
                }
            }
        }
    }

}
