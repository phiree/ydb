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
using Dianzhu.Model;
using Dianzhu.Model.Enums;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_OrderHistory_Order.xaml 的交互逻辑
    /// </summary>
    public partial class UC_OrderHistory_Order : UserControl
    {
        public UC_OrderHistory_Order()
        {
            InitializeComponent();

            ClearData();
        }

        public void LoadData(ServiceOrder order)
        {
            //lbOrderNum.Content = order.OrderNum;

            lbOrderStatus.Text = order.GetStatusTitleFriendly(order.OrderStatus);
            if(order.OrderStatus != enum_OrderStatus.Search)
            {
                lbOrdeSvcName.Text = order.Details.Count > 0 ? order.Details[0].ServieSnapShot.ServiceName : string.Empty;
                //lbOrdrDepositAmount.Content = order.DepositAmount.ToString("0.00");
                lbOrderTotalAmount.Text = order.OrderAmount.ToString("0.00");
                lbOrderAddress.Text = order.TargetAddress;
            }            
            lbOrderStartTime.Text = order.OrderServerStartTime.ToString("yyyy-MM-dd HH:mm:ss");
            lbOrderEndTime.Text = order.OrderServerFinishedTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void ClearData()
        {
            //lbOrderNum.Content = string.Empty;
            lbOrderStatus.Text = string.Empty;
            lbOrdeSvcName.Text = string.Empty;
            //lbOrdrDepositAmount.Content = string.Empty;
            lbOrderTotalAmount.Text = string.Empty;
            lbOrderAddress.Text = string.Empty;
            lbOrderStartTime.Text = string.Empty;
        }
    }
}
