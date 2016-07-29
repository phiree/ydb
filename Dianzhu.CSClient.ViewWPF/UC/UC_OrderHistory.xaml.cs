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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_OrderHistory.xaml 的交互逻辑
    /// </summary>
    public partial class UC_OrderHistory : UserControl, IView.IViewOrderHistory
    {
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
                Action lambda = () =>
                {
                    orderList = value;
                    //pnlOrderHistory.Children.Clear();
                    ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();

                    if (orderList == null)
                    {
                        orderList = new List<ServiceOrder>();
                        ShowNullListLable();
                        return;
                    }

                    if (orderList.Count > 0)
                    {
                        UC_OrderHistory_Order ucOrder;
                        foreach (ServiceOrder order in orderList)
                        {
                            ucOrder = new UC_OrderHistory_Order();
                            ucOrder.LoadData(order);
                            //pnlOrderHistory.Children.Add(ucOrder);
                            ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(ucOrder);
                        }
                        HideMsg();
                    }
                    else
                    {
                        ShowNullListLable();
                    }
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
            }
        }
        //显示当查询列表为空时的提示语
        private void ShowNullListLable()
        {
            tbkHint.Text = "当前用户没有历史订单";
            tbkHint.Visibility = Visibility.Visible;
            btnSearchEnabled = false;
        }

        public void ShowListLoadingMsg()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                tbkHint.Text = "加载用户历史订单中...";
                tbkHint.Visibility = Visibility.Visible;
                btnSearchEnabled = false;
            }));
        }

        private void HideMsg()
        {
            tbkHint.Visibility = Visibility.Collapsed;
            btnSearchEnabled = true;
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
