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
    public partial class UC_CustomerNew : UserControl,IView.IViewCustomer
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_CustomerNew");
        DispatcherTimer timerReceptionStatus;
        public UC_CustomerNew()
        {
            InitializeComponent();

            timerReceptionStatus = new DispatcherTimer();
            timerReceptionStatus.Interval = TimeSpan.FromMilliseconds(1000);
            timerReceptionStatus.Tick += TimerReceptionStatus_Tick;
        }

        public string AvatarImage
        {
            set
            {
                BitmapImage image;
                try
                {
                    image = new BitmapImage(new Uri(value));
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
                        tbkCustomerStatus.Text = "等待中";
                        StartTimer();
                        break;
                    case enum_CustomerReceptionStatus.Readed:
                    case enum_CustomerReceptionStatus.Actived:
                        tbkCustomerStatus.Text = "当前接待中...";
                        StopTimer();
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

        public enum_CustomerReceptionStatus CustomerButtonStyle
        {
            set
            {
                switch (value)
                {
                    case enum_CustomerReceptionStatus.Unread:
                        break;
                    case enum_CustomerReceptionStatus.Readed:
                        break;
                    case enum_CustomerReceptionStatus.Actived:
                        break;
                }
            }
        }

        public event CustomerClick CustomerClick;

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CustomerClick != null)
            {
                CustomerReceptionStatus = enum_CustomerReceptionStatus.Readed;
                CustomerClick(order);
            }
        }

        #region 接待状态时间控制器

        public void StartTimer()
        {
            if (timerReceptionStatus != null)
            {
                timerReceptionStatus.Start();
            }
        }

        public void StopTimer()
        {
            if (timerReceptionStatus != null)
            {
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
    }
}
