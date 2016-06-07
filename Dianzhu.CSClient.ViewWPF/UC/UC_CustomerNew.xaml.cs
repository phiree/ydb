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
    /// UC_CustomerNew.xaml 的交互逻辑
    /// </summary>
    public partial class UC_CustomerNew : UserControl
    {
        public UC_CustomerNew()
        {
            InitializeComponent();
        }

        public void CustomerNormal()
        {
            SetCustomerBorder("#FFd1d1d1", "#FF777779");

            tbkCustomerStatus.Text = "等待中";
            tbkCustomerMinutes.Visibility = Visibility.Visible;
            tbkCustomerMinutes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4b7799"));
        }

        public void CustomerCurrent()
        {
            SetCustomerBorder("#FF7db2dc", "#FF477597");
            tbkCustomerStatus.Text = "当前接待中...";

            tbkCustomerMinutes.Visibility = Visibility.Collapsed;
        }

        public void CustomerNewMsg()
        {
            SetCustomerBorder("#FFfb8384", "#FFe85454");

            tbkCustomerStatus.Text = "等待中";
            tbkCustomerMinutes.Visibility = Visibility.Visible;
            tbkCustomerMinutes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFf65f5f"));
        }

        public void SetCustomerBorder(string colorUp,string colorDown)
        {
            borderUp.Color = (Color)ColorConverter.ConvertFromString(colorUp);
            borderDown.Color = (Color)ColorConverter.ConvertFromString(colorDown);
        }
    }
}
