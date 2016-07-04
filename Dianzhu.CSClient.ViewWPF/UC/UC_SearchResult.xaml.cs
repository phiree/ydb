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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_SearchResult.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SearchResult : UserControl,IView.IViewSearchResult
    {
        public UC_SearchResult()
        {
            InitializeComponent();
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

        private void ShelfService_PushShelfService(DZService pushedService)
        {
            Action ac = () =>
            {
                //NHibernateUnitOfWork.UnitOfWork.Start();
                PushServices(new List<DZService>() { pushedService });
                //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                //NHibernateUnitOfWork.UnitOfWork.Current.Dispose();
            };
            NHibernateUnitOfWork.With.Transaction(ac);
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
