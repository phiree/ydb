using Dianzhu.CSClient.ViewModel;
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
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_PushService");

        public UC_PushService()
        {
            InitializeComponent();

            ClearData();
        }

        public void LoadData(VMChatPushServie pushService)
        {
            tbkServiceName.Text = pushService.ServiceName;
            
            BitmapImage img;
            try
            {
                img = new BitmapImage(new Uri(pushService.ImageUrl));
            }
            catch (Exception ee)
            {
                log.Error(ee);
                img = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/logourl.png"));
            }
            imgBusinessAvatar.Source = img;
            lblCreditPoint.Content = string.Empty;
            for (int i = 1; i <= pushService.CreditPoint; i++)
            {
                lblCreditPoint.Content += "★";
            }
            lblUnitPrice.Content = pushService.UnitPrice.ToString("0.00");
            lblDepostiAmount.Content = pushService.DepositAmount.ToString("0.00");
            tbxMemo.Text = pushService.ServiceMemo;
        }

        private void ClearData()
        {
            tbxMemo.Text = string.Empty;
        }
    }
}
