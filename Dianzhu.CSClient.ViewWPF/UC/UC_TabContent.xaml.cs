using Dianzhu.CSClient.IView;
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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_TabContent.xaml 的交互逻辑
    /// </summary>
    public partial class UC_TabContent : UserControl
    {
        public UC_TabContent(
            IViewSearch viewSearch, IViewSearchResult viewSearchResult,
            IViewChatList viewChatList, IViewChatSend viewChatSend,
            IViewOrderHistory viewOrderHistory, IViewToolsControl viewTabControl)
        {
            InitializeComponent();

            pnlSearch.Children.Add((UC_Search)viewSearch);
            pnlSearchResult.Children.Add((UC_SearchResult)viewSearchResult);
            pnlChatList.Children.Add((UC_ChatList)viewChatList);
            pnlChatSend.Children.Add((UC_ChatSend)viewChatSend);
            pnlOrderHistory.Children.Add((UC_OrderHistory)viewOrderHistory);
            pnlTools.Children.Add((UC_TabControlTools)viewTabControl);
        }
    }
}
