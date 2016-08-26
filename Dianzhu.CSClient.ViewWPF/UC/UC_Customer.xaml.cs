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
using Dianzhu.Model;
using System.Windows.Threading;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace Dianzhu.CSClient.ViewWPF
{
    public delegate void IdleTimerOut(Guid orderId);
    /// <summary>
    /// UC_Customer.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Customer : UserControl
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_Customer");
        public event IdleTimerOut IdleTimerOut;

        DispatcherTimer FinalChatTimer;
        Guid OrderTempId;
        public UC_Customer(ServiceOrder order)
        {
            InitializeComponent();

            OrderTempId = order.Id;

            LoadData(order.Customer);
            TimerLoad();
        }

        public void SetOrderTempData(Guid orderId)
        {
            OrderTempId = orderId;
        }

        protected void TimerLoad()
        {
            FinalChatTimer = new DispatcherTimer();
            FinalChatTimer.Interval = new TimeSpan(0, 10, 0);
            FinalChatTimer.Tick += FinalChatTimer_Tick;
        }

        public void TimerStart()
        {
            if (FinalChatTimer != null)
            {
                FinalChatTimer.Stop();
                TimerLoad();
                FinalChatTimer.Start();
            }            
        }

        public void TimerStop()
        {
            if (FinalChatTimer != null)
            {
                FinalChatTimer.Stop();
            }            
        }

        private void FinalChatTimer_Tick(object sender, EventArgs e)
        {
            IdleTimerOut(OrderTempId);
            TimerStop();
        }

        BackgroundWorker worker;
        public void LoadData(DZMembership customer)
        {
            tbkCustomerNames.Text = customer.DisplayName;

            //加载用户头像
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(customer.AvatarUrl);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BitmapImage image = e.Result as BitmapImage;
            imgSource.ImageSource = image;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action lamda = () =>
            {
                try
                {
                    Thread.Sleep(1000);

                    string avatarTemp = e.Argument.ToString();
                    string imgPath = PHSuit.LocalFileManagement.LocalFilePath + avatarTemp;

                    using (BinaryReader loader = new BinaryReader(File.Open(imgPath, FileMode.Open)))
                    {
                        FileInfo fd = new FileInfo(imgPath);
                        int Length = (int)fd.Length;
                        byte[] buf = new byte[Length];
                        buf = loader.ReadBytes((int)fd.Length);
                        loader.Dispose();
                        loader.Close();


                        //开始加载图像  
                        BitmapImage bim = new BitmapImage();
                        bim.BeginInit();
                        bim.StreamSource = new MemoryStream(buf);
                        bim.EndInit();
                        e.Result = bim;
                        //image1.Source = bim;
                        GC.Collect(); //强制回收资源  
                    }
                }
                catch (Exception ee)
                {
                    log.Error(ee.Message);
                    e.Result = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCustomer.png"));
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        public void ClearData()
        {
            tbkCustomerNames.Text = string.Empty;
            imgSource.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/logourl.png", UriKind.Absolute));
        }

        public void CustomerNormal()
        {
            SetCustomerBorder("#FFd1d1d1", "#FF777779");

            tbkCustomerStatus.Text = "等待中";
            //tbkCustomerMinutes.Visibility = Visibility.Visible;
            //tbkCustomerMinutes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4b7799"));

            TimeControl.Visibility = Visibility.Visible;
        }

        public void CustomerCurrent()
        {
            SetCustomerBorder("#FF7db2dc", "#FF477597");
            tbkCustomerStatus.Text = "当前接待中...";

            //tbkCustomerMinutes.Visibility = Visibility.Collapsed;

            TimeControlCollapsed();
        }

        public void TimeControlVisibility()
        {
            TimeControl.Visibility = Visibility.Visible;
            TimeControl.StartTimer();
        }

        public void TimeControlCollapsed()
        {
            TimeControl.Visibility = Visibility.Collapsed;
            TimeControl.StopTimer();
        }

        public void CustomerUnread()
        {
            SetCustomerBorder("#FFfb8384", "#FFe85454");

            tbkCustomerStatus.Text = "等待中";
            //tbkCustomerMinutes.Visibility = Visibility.Visible;
            //tbkCustomerMinutes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFf65f5f"));

            TimeControl.Visibility = Visibility.Visible;
        }

        public void SetCustomerBorder(string colorUp, string colorDown)
        {
            borderUp.Color = (Color)ColorConverter.ConvertFromString(colorUp);
            borderDown.Color = (Color)ColorConverter.ConvertFromString(colorDown);
        }
    }
}
