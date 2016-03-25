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

        public string SearchKeywordTime
        {
            get { return tbxKeywordTime.Text; }
            set { tbxKeywordTime.Text = value; }
        }

        public string SearchKeywordPriceMin
        {
            get { return tbxKeywordPriceMin.Text; }
            set { tbxKeywordPriceMin.Text = value; }
        }

        public string SearchKeywordPriceMax
        {
            get { return tbxKeywordPriceMax.Text; }
            set { tbxKeywordPriceMax.Text = value; }
        }

        public IList<Model.ServiceType> ServiceTypeFirst
        {
            set
            {
                if (value.Count > 0)
                {
                    cbxSearchTypeF.ItemsSource = value;
                    cbxSearchTypeF.SelectedItem = value[0];
                }
            }
        }

        public IList<Model.ServiceType> ServiceTypeSecond
        {
            set
            {
                if (value.Count > 0)
                {
                    cbxSearchTypeS.ItemsSource = value;
                    cbxSearchTypeS.DisplayMemberPath = "Name";
                    cbxSearchTypeS.SelectedItem = value[0];
                    cbxSearchTypeS.Visibility = Visibility.Visible;
                }
                else
                {
                    cbxSearchTypeS.Visibility = Visibility.Hidden;
                }
            }
        }

        public IList<Model.ServiceType> ServiceTypeThird
        {
            set
            {
                if (value.Count > 0)
                {
                    cbxSearchTypeT.ItemsSource = value;
                    cbxSearchTypeT.DisplayMemberPath = "Name";
                    cbxSearchTypeT.SelectedItem = value[0];
                    cbxSearchTypeT.Visibility = Visibility.Visible;
                }
                else
                {
                    cbxSearchTypeT.Visibility = Visibility.Hidden;
                }
            }
        }

        public event SearchService Search;
        public event ServiceTypeFirst_Select ServiceTypeFirst_Select;
        public event ServiceTypeSecond_Select ServiceTypeSecond_Select;
        public event ServiceTypeThird_Select ServiceTypeThird_Select;



        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Search != null)
            {
                Search();
            }
        }

        private void cbxSearchTypeF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceTypeFirst_Select != null)
            {
                ServiceTypeFirst_Select((Model.ServiceType)cbxSearchTypeF.SelectedItem);
            }
        }

        private void cbxSearchTypeS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceTypeSecond_Select != null)
            {
                ServiceTypeSecond_Select((Model.ServiceType)cbxSearchTypeS.SelectedItem);
            }            
        }

        private void cbxSearchTypeT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceTypeThird_Select != null)
            {
                ServiceTypeThird_Select((Model.ServiceType)cbxSearchTypeT.SelectedItem);
            }            
        }
    }
}
