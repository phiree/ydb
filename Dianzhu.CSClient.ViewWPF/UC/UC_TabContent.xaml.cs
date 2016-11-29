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
using System.Windows.Threading;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_TabContent.xaml 的交互逻辑
    /// </summary>
    public partial class UC_TabContent : UserControl, IViewTabContent
    {
        IViewSearch viewSearch;
        IViewSearchResult viewSearchResult;
        IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        IViewOrderHistory viewOrderHistory;
        IViewToolsControl viewToolsControl;

        #region 界面相关各个控件的属性
        public IViewSearch ViewSearch
        {
            get
            {
                return viewSearch;
            }
        }

        public IViewSearchResult ViewSearchResult
        {
            get
            {
                return viewSearchResult;
            }
        }

        public IViewChatList ViewChatList
        {
            get
            {
                return viewChatList;
            }
        }

        public IViewChatSend ViewChatSend
        {
            get
            {
                return viewChatSend;
            }
        }

        public IViewOrderHistory ViewOrderHistory
        {
            get
            {
                return viewOrderHistory;
            }
        }

        public IViewToolsControl ViewToolsControl
        {
            get
            {
                return viewToolsControl;
            }
        }

        string identity;
        public string Identity
        {
            get
            {
                return identity;
            }

            set
            {
                identity = value;
            }
        }
        #endregion

        /// <summary>
        /// 客服回复之后得时间控制器
        /// </summary>
        DispatcherTimer FinalChatTimer;

        public UC_TabContent(
            IViewSearch viewSearch, IViewSearchResult viewSearchResult,
            IViewChatList viewChatList, IViewChatSend viewChatSend,
            IViewOrderHistory viewOrderHistory, IViewToolsControl viewToolsControl,
            string identity)
        {
            InitializeComponent();

            this.viewSearch = viewSearch;
            this.viewSearchResult = viewSearchResult;
            this.viewChatList = viewChatList;
            this.viewChatSend = viewChatSend;
            this.viewOrderHistory = viewOrderHistory;
            this.viewToolsControl = viewToolsControl;

            this.identity = identity;

            pnlSearch.Children.Add((UC_Search)viewSearch);
            pnlSearchResult.Children.Add((UC_SearchResult)viewSearchResult);
            pnlChatList.Children.Add((UC_ChatList)viewChatList);
            pnlChatSend.Children.Add((UC_ChatSend)viewChatSend);
            pnlOrderHistory.Children.Add((UC_OrderHistory)viewOrderHistory);
            pnlTools.Children.Add((UC_TabControlTools)viewToolsControl);

            InitFinalChatTimer();
        }


        #region 客服回复之后的时间控制器相关方法

        public event IdleTimerOut IdleTimerOut;

        /// <summary>
        /// 初始化时间控件，每隔10分钟触发一次
        /// </summary>
        protected void InitFinalChatTimer()
        {
            FinalChatTimer = new DispatcherTimer();
            FinalChatTimer.Interval = new TimeSpan(0, 0, 10);
            FinalChatTimer.Tick += FinalChatTimer_Tick;
        }

        private void FinalChatTimer_Tick(object sender, EventArgs e)
        {
            if (IdleTimerOut != null)
            {
                IdleTimerOut(identity);
                StopFinalChatTimer();
            }
        }

        public void StartFinalChatTimer()
        {
            if (FinalChatTimer != null)
            {
                FinalChatTimer.Stop();
                InitFinalChatTimer();
                FinalChatTimer.Start();
            }
        }

        public void StopFinalChatTimer()
        {
            if (FinalChatTimer != null)
            {
                FinalChatTimer.Stop();
            }
        }

        #endregion

    }
}
