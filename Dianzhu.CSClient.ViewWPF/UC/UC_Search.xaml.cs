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
using Newtonsoft.Json;
using Dianzhu.Model;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_Search.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Search : UserControl,IView.IViewSearch
    {
        //IView.IViewSearchResult viewSearchResult;
        public UC_Search(/*IView.IViewSearchResult viewSearchResult*/)
        {
            //this.viewSearchResult = viewSearchResult;
            InitializeComponent();

            InitDateControl();
        }

        #region 属性

        public string ServiceCustomerName
        {
            get { return tbxKeywordPeople.Text.Trim(); }
            set
            {
                Action lamda = () =>
                {
                    tbxKeywordPeople.Text = value;
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }

        Model.ServiceType selectedType = null;
        public Model.ServiceType ServiceType
        {
            get
            {
                selectedType = (Model.ServiceType)(
                                    cbxSearchTypeT.SelectedItem == null || cbxSearchTypeT.IsVisible == false ?
                                    (cbxSearchTypeS.SelectedItem == null || cbxSearchTypeS.IsVisible == false ?
                                    (cbxSearchTypeF.SelectedItem == null || cbxSearchTypeF.IsVisible == false ? null
                                    : cbxSearchTypeF.SelectedItem)
                                    : cbxSearchTypeS.SelectedItem)
                                    : cbxSearchTypeT.SelectedItem
                                 );
                return selectedType;
            }
            set
            {
                selectedType = value;
            }
        }

        public string ServiceName
        {
            get { return tbxKeywordServiceName.Text.Trim(); }
            set
            {
                Action lamda = () =>
                {
                    tbxKeywordServiceName.Text = value;
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }

        public string ServiceCustomerPhone
        {
            get { return tbxKeywordPhone.Text.Trim(); }
            set
            {
                Action lamda = () =>
                {
                    tbxKeywordPhone.Text = value;
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }

        LocalStorage.TargetAddressObj serviceTargetAddressObj;
        /// <summary>
        /// 通过地址的json数据转换成的对象
        /// </summary>
        public LocalStorage.TargetAddressObj ServiceTargetAddressObj
        {
            get { return serviceTargetAddressObj; }
            set
            {
                serviceTargetAddressObj = value;
            }
        }

        string serviceTargetAddress;
        /// <summary>
        /// 地址的josn数据
        /// </summary>
        public string ServiceTargetAddressStr
        {
            get
            {
                return serviceTargetAddress;
            }
            set
            {
                serviceTargetAddressObj = JsonConvert.DeserializeObject<LocalStorage.TargetAddressObj>(value);

                serviceTargetAddress = serviceTargetAddressObj.address.province
                                        + serviceTargetAddressObj.address.city
                                        + serviceTargetAddressObj.address.district
                                        + serviceTargetAddressObj.address.street
                                        + serviceTargetAddressObj.address.streetNumber;

                if (SaveUIData != null)
                {
                    SaveUIData("TargetAddressObj", ServiceTargetAddressObj);
                }

                ServiceTargetAddress = serviceTargetAddress;
            }
        }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceTargetAddress
        {
            get
            {
                string address = string.Empty;
                Action lamda = () =>
                {
                    address = tbxKeywordAddress.Text.Trim();
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
                return address;
            }
            set
            {
                Action lamda = () =>
                {
                    tbxKeywordAddress.Text = value;
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }

        public int UnitAmount
        {
            get
            {
                int amount = 1;
                int.TryParse(tbxUnitAmount.Text, out amount);
                return amount;
            }
            set
            {
                Action lamda = () =>
                {
                    tbxUnitAmount.Text = value.ToString();
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }

        public DateTime SearchKeywordTime
        {
            get
            {
                DateTime searchKeywordTime;
                //DateTime.TryParse(tbxKeywordTime.Text, out searchKeywordTime);

                string datetime = dateServiceDate.Text + " " + cbxHours.Text + ":" + cbxMinutes.Text;
                if(!DateTime.TryParse(datetime, out searchKeywordTime))
                {
                    MessageBox.Show("日期时间有误");
                }
                
                return searchKeywordTime;
            }
            set
            {
                Action lamda = () =>
                {
                    //tbxKeywordTime.Text = value.ToString();

                    dateServiceDate.Text = value.ToString("yyyy-MM-dd");
                    cbxHours.Text = value.Hour.ToString();
                    cbxMinutes.Text = value.Minute.ToString();

                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }
        
        public decimal ServiceTargetPriceMin
        {
            get
            {
                decimal searchKeywordPriceMin;
                decimal.TryParse(tbxKeywordPriceMin.Text.Trim(), out searchKeywordPriceMin);

                return searchKeywordPriceMin;
            }
            set
            {
                Action lamda = () =>
                {
                    tbxKeywordPriceMin.Text = value.ToString();
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }
        
        public decimal ServiceTargetPriceMax
        {
            get
            {
                decimal searchKeywordPriceMax;
                decimal.TryParse(tbxKeywordPriceMax.Text.Trim(), out searchKeywordPriceMax);

                return searchKeywordPriceMax;
            }
            set
            {
                Action lamda = () =>
                {
                    tbxKeywordPriceMax.Text = value.ToString();
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }

        public string ServiceMemo
        {
            get
            {
                return tbxKeywordMemo.Text.Trim();
            }
            set
            {
                Action lamda = () =>
                {
                    tbxKeywordMemo.Text = value.ToString();
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }
        /// <summary>
        /// 绑定一级下拉框的值
        /// </summary>
        public IList<Model.ServiceType> ServiceTypeFirst
        {
            set
            {
                Action lamda = () =>
                {
                    if (value.Count > 0)
                    {
                        cbxSearchTypeF.ItemsSource = value;
                        cbxSearchTypeF.SelectedItem = value[0];
                    }
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }
        /// <summary>
        /// 绑定二级下拉框的值
        /// </summary>
        public IList<Model.ServiceType> ServiceTypeSecond
        {
            set
            {
                Action lamda = () =>
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
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }                
            }
        }
        /// <summary>
        /// 绑定三级下拉框的值
        /// </summary>
        public IList<Model.ServiceType> ServiceTypeThird
        {
            set
            {
                Action lamda = () =>
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
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }               
            }
        }
        /// <summary>
        /// 设置一级下拉框的值
        /// </summary>
        public ServiceType setServiceTypeFirst
        {
            set
            {
                Action lamda = () =>
                {
                    cbxSearchTypeF.SelectedItem = value;
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }
            }
        }
        /// <summary>
        /// 设置二级下拉框的值
        /// </summary>
        public ServiceType setServiceTypeSecond
        {
            set
            {
                Action lamda = () =>
                {
                    cbxSearchTypeS.SelectedItem = value;
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }                
            }
        }
        /// <summary>
        /// 设置三级下拉框的值
        /// </summary>
        public ServiceType setServiceTypeThird
        {
            set
            {
                Action lamda = () =>
                {
                    cbxSearchTypeT.SelectedItem = value;
                };
                if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(lamda); }
                else { lamda(); }                
            }
        }

        #endregion

        #region 接口委托的事件

        public event SearchService Search;
        public event ServiceTypeFirst_Select ServiceTypeFirst_Select;
        public event ServiceTypeSecond_Select ServiceTypeSecond_Select;
        public event ServiceTypeThird_Select ServiceTypeThird_Select;
        public event SaveUIData SaveUIData;

        #endregion

        #region 部分方法

        /// <summary>
        /// 清空数据
        /// </summary>
        public void ClearData()
        {
            ServiceCustomerName = string.Empty;
            SearchKeywordTime = DateTime.Now;
            ServiceTargetPriceMin = 0;
            ServiceTargetPriceMax = 0;
            ServiceCustomerPhone = string.Empty;
            ServiceTargetAddress = string.Empty;
            UnitAmount = 1;
        }

        /// <summary>
        /// 初始化日期时间选择控件
        /// </summary>
        private void InitDateControl()
        {
            dateServiceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateServiceDate.DisplayDateStart = DateTime.Now;
            dateServiceDate.DisplayDateEnd = DateTime.Now.AddDays(7);

            cbxHours.Text = DateTime.Now.Hour.ToString();
            cbxMinutes.Text = DateTime.Now.Minute.ToString();
            for (int i = 0; i < 24; i++)
            {
                cbxHours.Items.Add(i);
            }
            for (int j = 0; j < 60; j++)
            {
                cbxMinutes.Items.Add(j);
            }
        }

        /// <summary>
        /// 判断输入的文本是否为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxKeywordNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        /// <summary>
        /// 屏蔽中文输入和非法字符粘贴输入
        /// </summary>
        private void ForbidTextInput(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        #endregion

        #region 搜索按钮处理事件

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (serviceTargetAddressObj == null)
            {
                MessageBox.Show("请点击地图获取地址");
            }
            else if (SearchKeywordTime < DateTime.Now)
            {
                MessageBox.Show("预约时间不得小于当前时间!");
            }
            else if (ServiceType == null)
            {
                MessageBox.Show("请选择服务类型");
            }
            else if (string.IsNullOrEmpty(ServiceCustomerName))
            {
                MessageBox.Show("请填写联系人");
            }
            else if (string.IsNullOrEmpty(ServiceCustomerPhone))
            {
                MessageBox.Show("请填写联系电话");
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
            DateTime targetTime=DateTime.Now;
            decimal minPrice=0, maxPrice=0;
            string serviceName = string.Empty;
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.btnSearch.Content = "正在搜索......";
                this.btnSearch.IsEnabled = false;
                //this.viewSearchResult.LoadingText = "正在搜索服务,请稍后";

                targetTime = SearchKeywordTime;
                minPrice = ServiceTargetPriceMin;
                maxPrice = ServiceTargetPriceMax;
                serviceName = ServiceName;
            }));
            

            if (Search != null)
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                //Action ac = () =>
                //{
                    Search(targetTime, minPrice, maxPrice, selectedType.Id, serviceName, serviceTargetAddressObj.point.lng, serviceTargetAddressObj.point.lat);
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
            //this.viewSearchResult.LoadingText =string.Empty;
        }

        #endregion

        #region 服务类型下拉框处理事件

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

        #endregion

        #region 文本框改变时，写入缓存中
        private void tbxKeywordPeople_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                SaveUIData("Name", ServiceCustomerName);
            }
        }

        #region 日期时间控件
        private void dateServiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                SaveUIData("Date", SearchKeywordTime);
            }
        }

        private void cbxHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                cbxHours.Text = cbxHours.SelectedValue.ToString();
                SaveUIData("Date", SearchKeywordTime);
            }
        }

        private void cbxMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                cbxMinutes.Text = cbxMinutes.SelectedValue.ToString();
                SaveUIData("Date", SearchKeywordTime);
            }
        }
        #endregion

        //private void tbxKeywordTime_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (SaveUIData != null)
        //    {
        //        SaveUIData("Date", SearchKeywordTime);
        //    }
        //}

        private void tbxKeywordServiceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                SaveUIData("ServiceName", ServiceName);
            }
        }

        private void tbxKeywordPriceMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            ForbidTextInput(sender, e);

            if (SaveUIData != null)
            {
                SaveUIData("PriceMin", ServiceTargetPriceMin);
            }
        }

        private void tbxKeywordPriceMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            ForbidTextInput(sender, e);

            if (SaveUIData != null)
            {
                SaveUIData("PriceMax", ServiceTargetPriceMax);
            }
        }

        private void tbxKeywordPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                SaveUIData("Phone", ServiceCustomerPhone);
            }
        }

        private void tbxUnitAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            ForbidTextInput(sender, e);

            if (SaveUIData != null)
            {
                SaveUIData("Amount", UnitAmount);
            }
        }

        private void tbxKeywordAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                SaveUIData("Address", ServiceTargetAddress);
            }
        }

        private void tbxKeywordMemo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaveUIData != null)
            {
                SaveUIData("Memo", ServiceMemo);
            }
        }
        
        #endregion
    }
}
