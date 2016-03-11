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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_Search.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Search : UserControl,IView.IViewSearch
    {
        public UC_Search()
        {
            InitializeComponent();
        }

        public string SearchKeyword
        {
            get
            {
                return tbxKeyword.Text;
            }

            set
            {
                tbxKeyword.Text = value;
            }
        }

        public event SearchService Search;

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Search != null)
            {
                Search();
            }
        }
    }
}
