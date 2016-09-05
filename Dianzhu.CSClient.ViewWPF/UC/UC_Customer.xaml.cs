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
using System.Windows.Threading;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_CustomerNew.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Customer : UserControl,IView.IViewCustomer
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_CustomerNew");
        /// <summary>
        /// 界面上显示的时间控制器
        /// </summary>
        DispatcherTimer timerReceptionStatus;
        /// <summary>
        /// 客服回复之后得时间控制器
        /// </summary>
        DispatcherTimer FinalChatTimer;
        public UC_Customer()
        {
            InitializeComponent();

            InitTimer();
            InitFinalChatTimer();
        }

        public string AvatarImage
        {
            set
            {
                BitmapImage image;
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        image = new BitmapImage(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + value + "_28X28"));
                    }
                    else
                    {
                        image = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCustomer.png"));
                    }
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    image = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCustomer.png"));
                }
                imgSource.ImageSource = image;
            }
        }

        public string CustomerName
        {
            set
            {
                tbkCustomerNames.Text = value;
            }
        }

        public enum_CustomerReceptionStatus CustomerReceptionStatus
        {
            set
            {
                switch (value)
                {
                    case enum_CustomerReceptionStatus.Unread:
                        CustomerUnread();
                        break;
                    case enum_CustomerReceptionStatus.Readed:
                        CustomerReaded();
                        break;
                    case enum_CustomerReceptionStatus.Actived:
                        CustomerActived();
                        break;
                }
            }
        }

        ServiceOrder order;
        public ServiceOrder Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        public event CustomerClick CustomerClick;        

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CustomerClick != null)
            {
                CustomerReceptionStatus = enum_CustomerReceptionStatus.Actived;
                CustomerClick(order);
            }
        }

        #region 接待状态时间控制器

        public void InitTimer()
        {
            timerReceptionStatus = new DispatcherTimer();
            timerReceptionStatus.Interval = TimeSpan.FromMilliseconds(1000);
            timerReceptionStatus.Tick += TimerReceptionStatus_Tick;
            timerReceptionStatus.Start();
        }

        bool isReaded = false;//判断是否读取过
        public void StartTimer()
        {
            if (timerReceptionStatus != null)
            {
                if (isReaded)
                {                    
                    isReaded = false;
                    tbkMinute.Text = "00";
                    tbkSecond.Text = "00";
                    timerReceptionStatus.Start();
                    gridTimer.Visibility = Visibility.Visible;
                }
            }
        }

        public void StopTimer()
        {
            if (timerReceptionStatus != null)
            {
                gridTimer.Visibility = Visibility.Collapsed;
                timerReceptionStatus.Stop();
            }
        }

        private void TimerReceptionStatus_Tick(object sender, EventArgs e)
        {
            int minute = 0;
            int second = 0;

            int.TryParse(tbkMinute.Text, out minute);
            int.TryParse(tbkSecond.Text, out second);

            if (second < 9)
            {
                second++;
                tbkSecond.Text = "0" + second.ToString();
            }
            else if (second < 59)
            {
                second++;
                tbkSecond.Text = second.ToString();
            }
            else
            {
                second = 0;
                tbkSecond.Text = "0" + second;

                minute++;
                tbkMinute.Text = minute.ToString();
            }
        }

        #endregion

        #region 设置边框的颜色

        public void SetCustomerBorder(string colorUp, string colorDown)
        {
            borderUp.Color = (Color)ColorConverter.ConvertFromString(colorUp);
            borderDown.Color = (Color)ColorConverter.ConvertFromString(colorDown);
        }

        public void CustomerUnread()
        {
            SetCustomerBorder("#FFfb8384", "#FFe85454");

            tbkCustomerStatus.Text = "等待中";

            StartTimer();
        }

        public void CustomerReaded()
        {
            SetCustomerBorder("#FFd1d1d1", "#FF777779");

            tbkCustomerStatus.Text = "已接待";
        }

        public void CustomerActived()
        {
            SetCustomerBorder("#FF7db2dc", "#FF477597");

            tbkCustomerStatus.Text = "当前接待中...";

            StopTimer();

            isReaded = true;
        }

        #endregion

        #region 客服回复之后的时间控制器相关方法

        public event IdleTimerOut IdleTimerOut;

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
                IdleTimerOut(order.Id);
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
