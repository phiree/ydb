using Dianzhu.CSClient.IView;
using System.Windows.Controls;
using System;
using System.Windows.Threading;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_TabContentTimer.xaml 的交互逻辑
    /// </summary>
    public partial class UC_TabContentTimer : UserControl,IViewTabContentTimer
    {
        /// <summary>
        /// 客服回复之后得时间控制器
        /// </summary>
        DispatcherTimer FinalChatTimer;

        public UC_TabContentTimer()
        {
            InitializeComponent();

            InitTimer();
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

        public event TimeOver TimeOver;

        public void InitTimer()
        {
            FinalChatTimer = new DispatcherTimer();
            FinalChatTimer.Interval = new TimeSpan(0, 10, 0);
            FinalChatTimer.Tick += FinalChatTimer_Tick;
        }

        private void FinalChatTimer_Tick(object sender, EventArgs e)
        {
            if (TimeOver != null)
            {
                TimeOver(identity);
                StopTimer();
            }
        }

        public void StartTimer()
        {
            if (FinalChatTimer != null)
            {
                FinalChatTimer.Stop();
                InitTimer();
                FinalChatTimer.Start();
            }
        }

        public void StopTimer()
        {
            if (FinalChatTimer != null)
            {
                FinalChatTimer.Stop();
            }
        }
    }
}
