using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// FormMainOld.xaml 的交互逻辑
    /// </summary>
    public partial class FormMainOld : Window,IViewMainForm
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF");
        public FormMainOld(IViewIdentityList viewIdentityList, IView.IViewChatList viewChatList, IViewChatSend viewChatSend,
            IViewOrder viewOrder, IViewSearch viewSearch, IViewSearchResult viewSearchResult, IViewOrderHistory viewOrderHistory,
            IViewNotice viewNotice)
        {
            InitializeComponent();
            pnlNotice.Children.Add((UC_Notice) viewNotice);
            pnlCustomerList.Children.Add((UC_IdentityList) viewIdentityList);
            pnlSearch.Children.Add((UC_Search) viewSearch);
            pnlChatList.Children.Add((UC_ChatList)viewChatList);
            pnlSearchResult.Children.Add((UC_SearchResult)viewSearchResult);
            pnlOrder.Children.Add((UC_Order)viewOrder);
            pnlChatSend.Children.Add((UC_ChatSend)viewChatSend);
            pnlOrderHistory.Children.Add( (UC_OrderHistory)viewOrderHistory);
        }

        public void CloseApplication()
        {
            Action lambda = () =>
            {
                this.Close();
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
