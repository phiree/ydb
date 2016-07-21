using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// UC_UsefulLinks.xaml 的交互逻辑
    /// </summary>
    public partial class UC_UsefulLinks : UserControl, IView.IViewUsefulLinks
    {
        public UC_UsefulLinks()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(((Button)sender).Tag.ToString());
        }
    }
}
