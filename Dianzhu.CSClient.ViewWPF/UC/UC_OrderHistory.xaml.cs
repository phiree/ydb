using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using System.ComponentModel;
using System.Threading;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_OrderHistory.xaml 的交互逻辑
    /// </summary>
    public partial class UC_OrderHistory : UserControl, IView.IViewOrderHistory
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_OrderHistory");
        public UC_OrderHistory()
        {
            InitializeComponent();

            //UC_OrderHistory_Order order = new UC_OrderHistory_Order();
            //pnlOrderHistory.Children.Add(order);
            ShowNullListLable();
        }
        
        public event SearchOrderHistoryClick SearchOrderHistoryClick;

        public string SearchStr
        {
            get { return txtSearchStr.Text.Trim(); }
            set { txtSearchStr.Text = value; }
        }

        IList<ServiceOrder> orderList;
        public IList<ServiceOrder> OrderList
        {
            get { return orderList; }
            set
            {
                orderList = value;
                //Action lambda = () =>
                //{
                //    orderList = value;
                //    //pnlOrderHistory.Children.Clear();
                //    ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();

                //    if (orderList == null)
                //    {
                //        orderList = new List<ServiceOrder>();
                //        ShowNullListLable();
                //        return;
                //    }

                //    if (orderList.Count > 0)
                //    {
                //        //BackgroundWorker worker = new BackgroundWorker();
                //        //worker.DoWork += Worker_DoWork;
                //        //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                //        //worker.RunWorkerAsync();

                //        UC_OrderHistory_Order ucOrder;
                //        foreach (ServiceOrder order in orderList)
                //        {
                //            //ucOrder = new UC_OrderHistory_Order();
                //            //ucOrder.LoadData(order);
                //            ////pnlOrderHistory.Children.Add(ucOrder);
                //            //((StackPanel)svChatList.FindName("StackPanel")).Children.Add(ucOrder);
                //            AddOneOrder(order);

                //            Thread.Sleep(1000);
                //        }
                //        HideMsg();
                //    }
                //    else
                //    {
                //        ShowNullListLable();
                //    }
                //};
                //if (!Dispatcher.CheckAccess())
                //{
                //    Dispatcher.Invoke(lambda);
                //}
                //else { lambda(); }
            }
        }

        public void AddOneOrder(ServiceOrder order)
        {
            Action lamda = () =>
            {
                UC_OrderHistory_Order ucOrder = new UC_OrderHistory_Order();
                ucOrder.LoadData(order);
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(ucOrder);
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.Debug("订单界面异步加载完成");
            HideMsg();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            log.Debug("开始异步加载订单界面");
            UC_OrderHistory_Order ucOrder;
            foreach (ServiceOrder order in orderList)
            {
                ucOrder = new UC_OrderHistory_Order();
                ucOrder.LoadData(order);
                //pnlOrderHistory.Children.Add(ucOrder);
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(ucOrder);
            }
        }

        //显示当查询列表为空时的提示语
        public void ShowNullListLable()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                tbkHint.Text = "当前用户没有历史订单";
                tbkHint.Visibility = Visibility.Visible;
                btnSearchEnabled = false;
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();
            }));
        }

        public void ShowListLoadingMsg()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                tbkHint.Text = "加载用户历史订单中...";
                tbkHint.Visibility = Visibility.Visible;
                btnSearchEnabled = false;
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();
            }));
        }

        public void HideMsg()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                tbkHint.Visibility = Visibility.Collapsed;
                btnSearchEnabled = true;
            }));
        }

        private bool btnSearchEnabled
        {
            set { btnSearchByOrderId.IsEnabled = value; }
        }

        private void btnSearchByOrderId_Click(object sender, RoutedEventArgs e)
        {
            SearchOrderHistoryClick();
        }        
    }
}
