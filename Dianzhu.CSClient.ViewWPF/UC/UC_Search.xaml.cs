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
using System.ComponentModel;
using System.Threading;

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

        public DateTime SearchKeywordTime
        {
            get
            {
                try
                {
                    return DateTime.Parse(tbxKeywordTime.Text);
                }
                catch (Exception)
                {
                    return DateTime.Parse("1970-01-01 00:00:00");
                }
            }
            set { tbxKeywordTime.Text = value.ToString(); }
        }

        public decimal SearchKeywordPriceMin
        {
            get
            {
                try
                {
                    return decimal.Parse(tbxKeywordPriceMin.Text.Trim());
                }
                catch(Exception)
                {
                    return 0;
                }
            }
            set { tbxKeywordPriceMin.Text = value.ToString(); }
        }

        public decimal SearchKeywordPriceMax
        {
            get
            {
                try
                {
                    return decimal.Parse(tbxKeywordPriceMax.Text.Trim());
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { tbxKeywordPriceMax.Text = value.ToString(); }
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
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.btnSearch.Content = "正在搜索......";
            }));

            Thread.Sleep(1000);


        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Search != null)
            {
                Search();
            }
            this.btnSearch.Content = "搜索";
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

        private void tbxKeywordPriceMin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.All(t => char.IsDigit(t)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tbxKeywordPriceMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }
    }
}
