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
using Dianzhu.CSClient.IView;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ShelfService.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ShelfService : UserControl, IView.IViewShelfService
    {
        public UC_ShelfService(DZService service)
        {
            InitializeComponent();
            LoadData(service,1);
           // ClearData();
        }

        public void LoadData(DZService service, int num)
        {
            btnSendService.Tag = service;

            tbkServiceNo.Text = num.ToString();
            tbkServiceName.Text = service.Name;
            tbkServiceTime.Text = service.CreatedTime.TimeOfDay.ToString();
            tbkServiceUnitPrice.Text = service.UnitPrice.ToString("0.00");
            tbkServiceDepPrice.Text = service.DepositAmount.ToString("0.00");
        }

        public void ClearData()
        {
            tbkServiceNo.Text = string.Empty;
            tbkServiceName.Text = string.Empty;
            tbkServiceTime.Text = string.Empty;
            tbkServiceUnitPrice.Text = string.Empty;
            tbkServiceDepPrice.Text = string.Empty;
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
                PushShelfService((DZService)this.btnSendService.Tag);
            }
        }
    }
}
