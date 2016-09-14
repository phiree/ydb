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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_SearchResult.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SearchResult : UserControl,IView.IViewSearchResult
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_SearchResult");

        IInstantMessage.InstantMessage iIm;
        public UC_SearchResult(IInstantMessage.InstantMessage iIm)
        {
            InitializeComponent();
            this.iIm = iIm;
            //DataContext = new SampleClass();
            SearchedService = null;
        }

        IList<DZService> searchedService;
        public IList<DZService> SearchedService
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
                        
                        //foreach (DZService service in searchedService)
                        //{
                        //    LoadServiceToPanel(service);
                        //}Hashtable ht = (Hashtable)list[i];
                        UC_ShelfService shelfService;
                        int num = 1;
                        foreach (DZService service in searchedService)
                        {
                            if (pnlSearchResult.FindName(PHSuit.StringHelper.SafeNameForWpfControl(service.Id.ToString())) != null)
                            {
                                pnlSearchResult.UnregisterName(PHSuit.StringHelper.SafeNameForWpfControl(service.Id.ToString()));
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
        private void ShelfService_PushShelfService(DZService pushedService)
        {
            w = new BackgroundWorker();
            w.DoWork += W_DoWork;
            w.RunWorkerCompleted += W_RunWorkerCompleted;

            if (w.IsBusy)
            {
                return;
            }
            w.RunWorkerAsync(pushedService);
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
            

            //ReceptionChat chat = (ReceptionChat)e.Result;
            //if (chat != null)
            //{
            //    NHibernateUnitOfWork.UnitOfWork.Start();
            //    //NHibernateUnitOfWork.With.Transaction(() => {
            //    //    NHibernateUnitOfWork.UnitOfWork.Current.Refresh(chat);
            //        log.Debug("开始发送消息");
            //        iIm.SendMessage(chat);
            //        log.Debug("消息发送完成");
            //    //});
            //    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //    NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            //}

            ServiceOrder order = e.Result as ServiceOrder;

            if (order != null)
            {
                log.Debug("推送完成");
                PushServiceTimerSend();

                log.Debug("新草稿订单的id：" + order.Id.ToString());
                string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
                string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                                                    <active xmlns = ""http://jabber.org/protocol/chatstates""></active><ext xmlns=""ihelper:notice:draft:new""><orderID>{3}</orderID></ext></message>",
                                                        order.Customer.Id + "@" + server, order.CustomerService.Id, Guid.NewGuid() + "@" + server, order.Id);
                iIm.SendMessage(noticeDraftNew);
            }
        }

        private void W_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                NHibernateUnitOfWork.UnitOfWork.Start();

                string errorMsg = string.Empty;
                e.Result = PushServices(new List<DZService>() { (DZService)e.Argument },out errorMsg);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    MessageBox.Show(errorMsg);
                }

                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }));
        }

        //public bool BtnPush
        //{
        //    get { return btnPush.IsEnabled; }
        //    set
        //    {
        //        Action lambda = () =>
        //        {
        //            btnPush.IsEnabled = value;
        //        };
        //        if (!Dispatcher.CheckAccess())
        //        {
        //            Dispatcher.Invoke(lambda);
        //        }
        //        else
        //        {
        //            lambda();
        //        }
        //    }
        //}

        private void LoadServiceToPanel(DZService service)
        {
            WrapPanel pnl = new WrapPanel();
            
            pnl.Name =  PHSuit.StringHelper.SafeNameForWpfControl(service.Id.ToString());
            
            pnl.FlowDirection = FlowDirection.LeftToRight;

            CheckBox cbx = new CheckBox();
            cbx.Tag = service;
            pnl.Children.Add(cbx);

            Label lblBusinessName = new Label();
           
            lblBusinessName.Content = service.Business.Name;
             pnl.Children.Add(lblBusinessName);
            Label lblServiceName = new Label();
            
            lblServiceName.Content = service.Description.ToString();
            pnl.Children.Add(lblServiceName);
           
            pnlSearchResult.Children.Add(pnl);
            pnlSearchResult.RegisterName(pnl.Name, pnl);

        }

        private void BtnSelectService_Click(object sender, RoutedEventArgs e)
        {
            //将已经选择的 panel 背景颜色还原
            
            foreach (WrapPanel con in pnlSearchResult.Children)
            {
                
                if (con.Background == Brushes.Green)
                {
                    con.Background = Brushes.White; 
                    break;
                }
            }
            DZService selectedService = (DZService)((Button)sender).Tag;

            Panel pnl = (Panel)pnlSearchResult.FindName(PHSuit.StringHelper.SafeNameForWpfControl(selectedService.Id.ToString()));
            pnl.Background = Brushes.Green;
            SelectService(selectedService);
        }

        public event SelectService SelectService;
        public event PushServices PushServices;

        private void btnPush_Click(object sender, RoutedEventArgs e)
        {
            string errorMsg = string.Empty;
            IList<DZService> services = new List<DZService>();
            foreach (Panel p in pnlSearchResult.Children)
            {
                foreach (Control ctl in p.Children)
                {
                    if (ctl is CheckBox)
                    {
                        CheckBox cbx = (CheckBox)ctl;
                    if (cbx.IsChecked.Value==true)
                    {
                        services.Add((DZService)cbx.Tag);
                    }
                    }
                }
            }
            PushServices(services,out errorMsg);
            
        }

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
                    SearchedService = searchedService.OrderBy(x => x.CreatedTime).ToList();
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
            //SearchedService = (IList<DZService>)searchedService.Select(x => x.Business.Name.Contains(txtFilter.Text.Trim())).ToList();
            //NHibernateUnitOfWork.UnitOfWork.Start();
            //Action ac = () =>
            //{
                FilterByBusinessName(txtFilter.Text.Trim());
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);

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
