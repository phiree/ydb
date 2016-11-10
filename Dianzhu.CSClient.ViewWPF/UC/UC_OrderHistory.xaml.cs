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
using System.ComponentModel;
using System.Threading;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_OrderHistory.xaml 的交互逻辑
    /// </summary>
    public partial class UC_OrderHistory : UserControl, IViewOrderHistory
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_OrderHistory");
        UC_Hint hint;
        public UC_OrderHistory()
        {
            InitializeComponent();

            //UC_OrderHistory_Order order = new UC_OrderHistory_Order();
            //pnlOrderHistory.Children.Add(order);

            hint = new UC_Hint(BtnMore_Click);
            ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(hint);
        }
        
        public event SearchOrderHistoryClick SearchOrderHistoryClick;
        public event BtnMoreOrder BtnMoreOrder;

        public string SearchStr
        {
            get { return txtSearchStr.Text.Trim(); }
            set { txtSearchStr.Text = value; }
        }

        int orderPage = 1;
        public int OrderPage
        {
            get { return orderPage; }
            set { orderPage=value;}
        }

        IList<VMOrderHistory> orderList;
        public IList<VMOrderHistory> OrderList
        {
            get { return orderList; }
            set
            {
                orderList = value;
            }
        }

        #region 添加一张订单
        public void AddOneOrder(VMOrderHistory vmOrderHistory)
        {
            Action lamda = () =>
            {
                UC_OrderHistory_Order ucOrder = new UC_OrderHistory_Order();
                ucOrder.LoadData(vmOrderHistory);
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

        public void InsertOneOrder(VMOrderHistory vmOrderHistory)
        {
            Action lamda = () =>
            {
                UC_OrderHistory_Order ucOrder = new UC_OrderHistory_Order();
                ucOrder.LoadData(vmOrderHistory);
                StackPanel sp = ((StackPanel)svChatList.FindName("StackPanel"));
                sp.Children.Insert(sp.Children.Count - 1, ucOrder);
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
        #endregion

        #region 显示提示信息
        //显示当查询列表为空时的提示语
        public void ShowNullListLable()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                hint.lblHint.Content = "当前用户没有历史订单";
                hint.lblHint.Visibility = Visibility.Visible;
                hint.btnMore.Visibility = Visibility.Collapsed;
                btnSearchEnabled = false;
            }));
        }

        public void ShowListLoadingMsg()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                hint.lblHint.Content = "加载用户历史订单中...";
                hint.lblHint.Visibility = Visibility.Visible;
                hint.btnMore.Visibility = Visibility.Collapsed;
                btnSearchEnabled = false;
            }));
        }

        public void ShowNoMoreOrderList()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                hint.lblHint.Content = "没有更多历史订单";
                hint.lblHint.Visibility = Visibility.Visible;
                hint.btnMore.Visibility = Visibility.Collapsed;
                btnSearchEnabled = true;
            }));
        }

        public void ShowMoreOrderList()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                HideMsg();
                hint.btnMore.Visibility = Visibility.Visible;
                btnSearchEnabled = true;
            }));
        }
        #endregion

        public void ClearUCData()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(hint);
            }));
        }

        public void HideMsg()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                hint.lblHint.Content = string.Empty;
                hint.lblHint.Visibility = Visibility.Collapsed;
            }));
        }

        private bool btnSearchEnabled
        {
            set { btnSearchByOrderId.IsEnabled = value; }
        }

        private void btnSearchByOrderId_Click(object sender, RoutedEventArgs e)
        {
            ClearUCData();

            if (string.IsNullOrEmpty(SearchStr))
            {
                foreach (var item in orderList)
                {
                    InsertOneOrder(item);
                }
                return;
            }

            SearchOrderHistoryClick();
        }

        private void BtnMore_Click(object sender, RoutedEventArgs e)
        {
            if (BtnMoreOrder != null)
            {
                BtnMoreOrder();
            }
        }
    }
}
