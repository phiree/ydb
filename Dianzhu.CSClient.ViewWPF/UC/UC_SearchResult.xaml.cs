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
                    searchedService = value;
                    pnlSearchResult.Children.Clear();
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
            log.Debug("推送完成");
            PushServiceTimerSend();

            ReceptionChat chat = (ReceptionChat)e.Result;
            if (chat != null)
            {
                NHibernateUnitOfWork.With.Transaction(() => {
                    NHibernateUnitOfWork.UnitOfWork.Current.Refresh(chat);
                    log.Debug("开始发送消息");
                    iIm.SendMessage(chat);
                    log.Debug("消息发送完成");
                });
            }
        }

        private void W_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Action ac = () =>
                {
                    e.Result = PushServices(new List<DZService>() { (DZService)e.Argument });
                };
                NHibernateUnitOfWork.With.Transaction(ac);
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
            PushServices(services);
            
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
    }
}
