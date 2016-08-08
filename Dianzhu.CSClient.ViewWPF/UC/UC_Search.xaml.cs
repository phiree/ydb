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
        IView.IViewSearchResult viewSearchResult;
        public UC_Search(IView.IViewSearchResult viewSearchResult)
        {
            this.viewSearchResult = viewSearchResult;
            InitializeComponent();
        }

        public string ServicePeople
        {
            get { return tbxKeywordPeople.Text.Trim(); }
            set { tbxKeywordPeople.Text = value; }
        }

        public string ServicePhone
        {
            get { return tbxKeywordPhone.Text.Trim(); }
            set { tbxKeywordPhone.Text = value; }
        }

        public string ServiceAddress
        {
            get { return tbxKeywordAddress.Text.Trim(); }
            set { tbxKeywordAddress.Text = value; }
        }

        public int UnitAmount
        {
            get
            {
                int amount = 1;
                int.TryParse(tbxUnitAmount.Text, out amount);
                return amount;
            }
            set { tbxUnitAmount.Text = value.ToString(); }
        }

        public DateTime SearchKeywordTime
        {
            get
            {
                DateTime searchKeywordTime;
                DateTime.TryParse(tbxKeywordTime.Text, out searchKeywordTime);
                
                return searchKeywordTime;
            }
            set { tbxKeywordTime.Text = value.ToString(); }
        }
        
        public decimal SearchKeywordPriceMin
        {
            get
            {
                decimal searchKeywordPriceMin;
                decimal.TryParse(tbxKeywordPriceMin.Text.Trim(), out searchKeywordPriceMin);

                return searchKeywordPriceMin;
            }
            set { tbxKeywordPriceMin.Text = value.ToString(); }
        }
        
        public decimal SearchKeywordPriceMax
        {
            get
            {
                decimal searchKeywordPriceMax;
                decimal.TryParse(tbxKeywordPriceMax.Text.Trim(), out searchKeywordPriceMax);

                return searchKeywordPriceMax;
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

        /// <summary>
        /// 清空数据
        /// </summary>
        public void ClearData()
        {
            ServicePeople = string.Empty;
            SearchKeywordTime = DateTime.Now;
            SearchKeywordPriceMin = 0;
            SearchKeywordPriceMax = 0;
            ServicePhone = string.Empty;
            ServiceAddress = string.Empty;
            UnitAmount = 1;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (SearchKeywordTime < DateTime.Now)
            {
                MessageBox.Show("预约时间不得小于当前时间!");
            }
            else
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Model.ServiceType selectedType = null;
            DateTime targetTime=DateTime.Now;
            decimal minPrice=0, maxPrice=0;
            string serviceName = string.Empty;
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.btnSearch.Content = "正在搜索......";
                this.btnSearch.IsEnabled = false;
                this.viewSearchResult.LoadingText = "正在搜索服务,请稍后";

                selectedType = (Model.ServiceType)(
                                    cbxSearchTypeT.SelectedItem == null||cbxSearchTypeT.IsVisible==false ? 
                                    (cbxSearchTypeS.SelectedItem == null || cbxSearchTypeS.IsVisible == false ? 
                                    (cbxSearchTypeF.SelectedItem==null|| cbxSearchTypeF.IsVisible == false ? null
                                    :cbxSearchTypeF.SelectedItem) 
                                    : cbxSearchTypeS.SelectedItem) 
                                    : cbxSearchTypeT.SelectedItem
                                 );

                targetTime = SearchKeywordTime;
                minPrice = SearchKeywordPriceMin;
                maxPrice = SearchKeywordPriceMax;
                serviceName = tbxKeywordServiceName.Text.Trim();
            }));
            

            if (Search != null)
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                //Action ac = () =>
                //{
                    Search(targetTime, minPrice, maxPrice, selectedType.Id, serviceName);
                //};
                //NHibernateUnitOfWork.With.Transaction(ac);

                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }


        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            this.btnSearch.Content = "搜索";
            this.btnSearch.IsEnabled = true;
            this.viewSearchResult.LoadingText =string.Empty;
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
