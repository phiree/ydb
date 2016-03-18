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

            UC_OrderHistory_Order order = new UC_OrderHistory_Order();
            pnlOrderHistory.Children.Add(order);
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
                pnlOrderHistory.Children.Clear();
                UC_OrderHistory_Order ucOrder;
                foreach (ServiceOrder order in orderList)
                {
                    ucOrder = new UC_OrderHistory_Order();
                    ucOrder.LoadData(order);
                    pnlOrderHistory.Children.Add(ucOrder);
                }
            }
        }

        private void btnSearchByOrderId_Click(object sender, RoutedEventArgs e)
        {
            SearchOrderHistoryClick();
        }
    }
}
