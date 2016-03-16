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
                searchedService = value;
                pnlSearchResult.Children.Clear();
                //foreach (DZService service in searchedService)
                //{
                //    LoadServiceToPanel(service);
                //}Hashtable ht = (Hashtable)list[i];
                foreach (DZService service in searchedService)
                {
                    LoadServiceToPanel(service);
                }
            }
        }
        private void LoadServiceToPanel(DZService service)
        {
            WrapPanel pnl = new WrapPanel();
            pnl.Name =  PHSuit.StringHelper.SafeNameForWpfControl(service.Id.ToString());
            
            pnl.FlowDirection = FlowDirection.LeftToRight;
            Label lblBusinessName = new Label();
            
            lblBusinessName.Content = service.Business.Name;
             pnl.Children.Add(lblBusinessName);
            Label lblServiceName = new Label();
            
            lblServiceName.Content = service.Description.ToString();
            pnl.Children.Add(lblServiceName);

            Button btnSelectService = new Button();
            btnSelectService.Content = "选择";
            btnSelectService.Tag = service;// service.Id.ToString();
            btnSelectService.Click += BtnSelectService_Click;
            pnl.Children.Add(btnSelectService);
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
    }
}
