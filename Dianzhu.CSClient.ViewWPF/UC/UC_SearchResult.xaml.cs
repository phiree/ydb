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
using Dianzhu.Model;
using System.ComponentModel;
using System.Globalization;
using Ydb.InstantMessage.Application;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_SearchResult.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SearchResult : UserControl,IView.IViewSearchResult
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_SearchResult");

        IInstantMessage iIm;
        public UC_SearchResult(IInstantMessage iIm)
        {
            InitializeComponent();
            this.iIm = iIm;
            //DataContext = new SampleClass();
            SearchedService = null;
        }

        IList<VMShelfService> searchedService;
        public IList<VMShelfService> SearchedService
        {
            get
            {
                return searchedService;
            }
            set
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    pnlSearchResult.Children.Clear();
                    if (value == null)
                    {
                        wpFilter.IsEnabled = false;
                    }
                    else if (value.Count == 0)
                    {
                        wpFilter.IsEnabled = false;
                    }
                    else
                    {
                        searchedService = value;
                        
                        UC_ShelfService shelfService;
                        int num = 1;
                        foreach (VMShelfService service in searchedService)
                        {
                            if (pnlSearchResult.FindName(PHSuit.StringHelper.SafeNameForWpfControl(service.ServiceId.ToString())) != null)
                            {
                                pnlSearchResult.UnregisterName(PHSuit.StringHelper.SafeNameForWpfControl(service.ServiceId.ToString()));
                            }
                            //LoadServiceToPanel(service);
                            shelfService = new UC_ShelfService(service);
                            // shelfService.LoadData(service, num);
                            shelfService.PushShelfService += ShelfService_PushShelfService;
                            pnlSearchResult.Children.Add(shelfService);

                            num++;
                        }

                        wpFilter.IsEnabled = true;
                    }

                }));
                
            }
        }

        BackgroundWorker w;
        private void ShelfService_PushShelfService(Guid pushedServiceId)
        {
            w = new BackgroundWorker();
            w.DoWork += W_DoWork;
            w.RunWorkerCompleted += W_RunWorkerCompleted;

            if (w.IsBusy)
            {
                return;
            }
            w.RunWorkerAsync(pushedServiceId);
            log.Debug("开始推送");

            //ReceptionChat chat = null;
            
            //if (chat != null)
            //{
                
            //    NHibernateUnitOfWork.With.Transaction(() => {
            //        NHibernateUnitOfWork.UnitOfWork.Current.Refresh(chat);
            //        iIm.SendMessage(chat); });
            //}
        }

        public event PushServiceTimerSend PushServiceTimerSend;

        private void W_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //清空订单搜索内容
            SearchedService = null;

            ServiceOrder order = e.Result as ServiceOrder;

            if (order != null)
            {
                log.Debug("推送完成");
                //todo:timerControl
                //PushServiceTimerSend();

                log.Debug("新草稿订单的id：" + order.Id.ToString());
                string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
                //string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                //                                    <active xmlns = ""http://jabber.org/protocol/chatstates""></active><ext xmlns=""ihelper:notice:draft:new""><orderID>{3}</orderID></ext></message>",
                //                                        order.Customer.Id + "@" + server+ "/YDBan_User", order.CustomerService.Id, Guid.NewGuid() + "@" + server, order.Id);
                //iIm.SendMessage(noticeDraftNew);
                iIm.SendNoticeNewOrder(Guid.NewGuid(), order.CustomerId.ToString(), "YDBan_User", order.Id.ToString());
            }
        }

        private void W_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                try
                {
                    NHibernateUnitOfWork.UnitOfWork.Start();

                    string errorMsg = string.Empty;
                    e.Result = PushServices(new List<Guid>() { Guid.Parse( e.Argument.ToString()) }, out errorMsg);

                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        MessageBox.Show(errorMsg);
                    }
                }
                catch (Exception ee)
                {
                    log.Error(ee);
                }
                finally
                {
                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                    NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
                }
            }));
        }
        
        public event PushServices PushServices;
        

        public void AddSearchItem(IViewShelfService service)
        {
            pnlSearchResult.Children.Add((UIElement)service);
        }

        public string LoadingText
        {
            set {
                lblLoadingText.Content = value;
            }
        }

        #region radiobutton 排序
        
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Model.Enums.enum_FilterType filterType;
            filterType = (Model.Enums.enum_FilterType)Enum.Parse(typeof(Model.Enums.enum_FilterType), ((RadioButton)sender).Tag.ToString());
            switch (filterType)
            {
                case Model.Enums.enum_FilterType.ByApprise:
                    break;
                case Model.Enums.enum_FilterType.ByDistance:
                    SearchedService = searchedService.OrderBy(x => x.Number).ToList();
                    break;
                case Model.Enums.enum_FilterType.ByPrice:
                    SearchedService = searchedService.OrderBy(x => x.UnitPrice).ToList();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 按店铺搜索        

        public event FilterByBusinessName FilterByBusinessName;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilterByBusinessName(txtFilter.Text.Trim());
        }

        #endregion
    }

    //public class EnumToBooleanConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return value == null ? false : value.Equals(parameter);
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
    //    }
    //}

    //public enum FilterType
    //{
    //    /// <summary>
    //    /// 按距离筛选
    //    /// </summary>
    //    ByDistance,
    //    /// <summary>
    //    /// 按价格筛选
    //    /// </summary>
    //    ByPrice,
    //    /// <summary>
    //    /// 按评价筛选
    //    /// </summary>
    //    ByApprise
    //}

    //public class SampleClass : INotifyPropertyChanged
    //{
    //    private FilterType _sampleEnum;

    //    public FilterType SampleEnum
    //    {
    //        get { return _sampleEnum; }
    //        set
    //        {
    //            _sampleEnum = value;
    //            HitCount++;
    //            //OnPropertyChanged(_sampleEnum.ToString());
    //        }
    //    }

    //    //为了显示Set的触发次数
    //    private int _hitCount;
    //    public int HitCount
    //    {
    //        get { return _hitCount; }
    //        set
    //        {
    //            _hitCount = value;
    //            OnPropertyChanged(SampleEnum.ToString());
    //        }
    //    }


    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private void OnPropertyChanged(string p_propertyName)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            Console.WriteLine("radiobutton成功触发:"+ _hitCount+",propertyName:"+ p_propertyName);
    //            PropertyChanged(this, new PropertyChangedEventArgs(p_propertyName));
    //        }
    //    }
    //}
}
