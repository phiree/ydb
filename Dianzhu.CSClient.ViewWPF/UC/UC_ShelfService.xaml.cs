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
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ShelfService.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ShelfService : UserControl, IView.IViewShelfService
    {
        public UC_ShelfService(VMShelfService service)
        {
            InitializeComponent();
            LoadData(service);
           // ClearData();
        }

        public void LoadData(VMShelfService service)
        {
            btnSendService.Tag = service.ServiceId;

            tbkServiceNo.Text = service.Number.ToString();
            tbkIsVerify.Text = service.IsVerify ? "已验证" : "未验证";
            for(int i=0;i<service.AppraiseScore;i++)
            {
                tbkAppraiseScore.Text += "★";
            }
            tbkServiceName.Text = service.ServiceName;
            tbkServiceTime.Text = service.TimeInterval;
            tbkServiceUnitPrice.Text = service.UnitPrice.ToString("0.00");
            tbkServiceDepPrice.Text = service.DepositPrice.ToString("0.00");
            tbkBusinessName.Text = service.BusinessName;
        }

        public void ClearData()
        {
            btnSendService.Tag = Guid.Empty;
            tbkServiceNo.Text = string.Empty;
            tbkIsVerify.Text = string.Empty;
            tbkAppraiseScore.Text = string.Empty;
            tbkServiceName.Text = string.Empty;
            tbkServiceTime.Text = string.Empty;
            tbkServiceUnitPrice.Text = string.Empty;
            tbkServiceDepPrice.Text = string.Empty;
            tbkBusinessName.Text = string.Empty;
        }

        #region 鼠标移入时的事件处理
        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //borderShelf.BorderBrush = new SolidColorBrush(Color.FromRgb(0x28,0x4d,0x68));//用16进制转换
            //borderShelf.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF284d68"));//直接识别字符串

            IsMouseEnter(true, "#FFffffff", "#FFffffff", "#FF4b779a", "#FF284d68");
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            IsMouseEnter(false, "#FF6f6f6f", "#FF227dc5", "#FFd7dbde", "#FFd6dbdf");
        }

        public void IsMouseEnter(bool isEnter, string colorNo, string colorVerify, string colorMin, string colorMax)
        {
            if (isEnter)
            {
                btnSendService.Visibility = Visibility.Visible;
            }
            else
            {
                btnSendService.Visibility = Visibility.Collapsed;
            }

            tbkServiceNo.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorNo));
            tbkIsVerify.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorVerify));

            borderShelf.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorMax));
            borderShelf.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorMax));
            title_min.Color = (Color)ColorConverter.ConvertFromString(colorMin);
            title_max.Color = (Color)ColorConverter.ConvertFromString(colorMax);
        }
        #endregion

        public event PushShelfService PushShelfService;
        private void btnSendService_Click(object sender, RoutedEventArgs e)
        {
            if (PushShelfService != null)
            {
                PushShelfService(Guid.Parse(btnSendService.Tag.ToString()));
            }
        }
    }
}
