using Dianzhu.Model;
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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_PushService.xaml 的交互逻辑
    /// </summary>
    public partial class UC_PushService : UserControl
    {
        public UC_PushService()
        {
            InitializeComponent();

            ClearData();
        }

        public void LoadData(ServiceOrderPushedService pushService)
        {
            lblUnitPrice.Content = pushService.UnitPrice.ToString("0.00");
            lblDepostiAmount.Content = pushService.DepositAmount.ToString("0.00");
            tbxMemo.Text = pushService.Description;
        }

        private void ClearData()
        {
            tbxMemo.Text = string.Empty;
        }
    }
}
