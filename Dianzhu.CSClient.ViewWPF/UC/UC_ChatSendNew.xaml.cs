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
    /// UC_ChatSendNew.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatSendNew : UserControl
    {
        public UC_ChatSendNew()
        {
            InitializeComponent();
        }

        private void btnCaptureImage_Click(object sender, RoutedEventArgs e)
        {
            if (btnVoiceImage.Visibility == Visibility.Collapsed)
            {
                btnVoiceImage.Visibility = Visibility.Visible;
            }
            else
            {
                btnVoiceImage.Visibility = Visibility.Collapsed;
            }
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            if (btnPhizImage.Visibility == Visibility.Collapsed)
            {
                btnPhizImage.Visibility = Visibility.Visible;
            }
            else
            {
                btnPhizImage.Visibility = Visibility.Collapsed;
            }
        }
    }
}
