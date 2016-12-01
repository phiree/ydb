using System.Windows.Controls;
using Dianzhu.CSClient.ViewModel;

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

        public void LoadData(VMOrderHistory vmOrderHistory)
        {
            //lbOrderNum.Content = order.OrderNum;
            lbOrderName.Text = vmOrderHistory.BusinessName;
            lbOrderStatus.Text = vmOrderHistory.OrderStatusStr;
            lbOrdeSvcName.Text = vmOrderHistory.ServiceName;
            //lbOrdrDepositAmount.Content = order.DepositAmount.ToString("0.00");
            lbOrderTotalAmount.Text = vmOrderHistory.OrderAmount.ToString("0.00");
            lbOrderAddress.Text = vmOrderHistory.TargetAddress;
            lbOrderStartTime.Text = vmOrderHistory.StartTime.ToString("yyyy年MM月dd HH:mm:ss");
            lbOrderEndTime.Text = vmOrderHistory.EntTime.ToString("yyyy年MM月dd HH:mm:ss");
        }

        private void ClearData()
        {
            //lbOrderNum.Content = string.Empty;
            lbOrderName.Text = string.Empty;
            lbOrderStatus.Text = string.Empty;
            lbOrdeSvcName.Text = string.Empty;
            //lbOrdrDepositAmount.Content = string.Empty;
            lbOrderTotalAmount.Text = string.Empty;
            lbOrderAddress.Text = string.Empty;
            lbOrderStartTime.Text = string.Empty;
        }
    }
}
