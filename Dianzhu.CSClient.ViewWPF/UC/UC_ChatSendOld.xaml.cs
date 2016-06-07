using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using System.IO;
using RisCaptureLib;
using System.ComponentModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatSend.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatSendOld : UserControl,IView.IViewChatSend
    {
        private readonly ScreenCaputre screenCaputre = new ScreenCaputre();
        private Size? lastSize;

        public UC_ChatSendOld()
        {
            InitializeComponent();

            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
        }

        public string MessageText
        {
            get
            {
                string msg = string.Empty;
                this.Dispatcher.Invoke((Action)(() =>
                {
                    msg= tbxTextMessage.Text;

                }));
                return msg;
               
            }

            set
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    tbxTextMessage.Text = value;

                }));
               
            }
        }

        public event SendTextClick SendTextClick;
        public event SendMediaClick SendMediaClick;
        
        private void btnSendTextMessage_Click(object sender, RoutedEventArgs e)
        {
            if (SendTextClick != null && MessageText.Trim() != string.Empty)
            {
                SendTextClick();
            }
        }

        Button btnMediaSender;
        private void btnSendMedia_Click(object sender, RoutedEventArgs e)
        {
                btnMediaSender = (Button)sender;
               SendMedia();
            
        }

        private void WorkerSendMedia_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblSendingMsg.Content = string.Empty;
        }
        Microsoft.Win32.OpenFileDialog dlg;
        string domain = string.Empty;
        string mediaType = string.Empty;
        private void SendMedia()
        {
            if (SendMediaClick != null)
            {

                // Create OpenFileDialog
                 dlg = new Microsoft.Win32.OpenFileDialog();

                Button btn = btnMediaSender;
             

                if (btn.Name == "btnSendImage")
                {
                    // Set filter for file extension and default file extension
                    dlg.DefaultExt = ".png;.jpg";
                    dlg.Filter = "Images |*.jpg";
                    domain = "ChatImage";
                    mediaType = "image";
                }
                else if (btn.Name == "btnSendAudio")
                {
                    dlg.DefaultExt = ".wma;.mp3";
                    //dlg.Filter = "Audios |*.mp3";
                    dlg.Filter = "Audios |*.*";
                    domain = "ChatAudio";
                    mediaType = "voice";
                }

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    BackgroundWorker workerSendMedia = new BackgroundWorker();
                    workerSendMedia.DoWork += WorkerSendMedia_DoWork;
                    workerSendMedia.RunWorkerCompleted += WorkerSendMedia_RunWorkerCompleted;
                    workerSendMedia.RunWorkerAsync();
                    

                }
               
            }
        }
        private void WorkerSendMedia_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                lblSendingMsg.Content = "正在发送,请稍后";
               
            }));
            byte[] fileData = File.ReadAllBytes(dlg.FileName);
            SendMediaClick(fileData, domain, mediaType);


        }

        #region 截图

        BitmapSource bmp;
        Window win;
        private void btnCaptureImage_Click(object sender, RoutedEventArgs e)
        {
            //Hide();
            //((Window)((Grid)((Grid)((WrapPanel)this.Parent).Parent).Parent).Parent).Hide();
            Thread.Sleep(300);
            screenCaputre.StartCaputre(30, lastSize);
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            //Show();
            Focus();
        }

        private void OnScreenCaputred(object sender, RisCaptureLib.ScreenCaputredEventArgs e)
        {
            //set last size
            lastSize = new Size(e.Bmp.Width, e.Bmp.Height);


            //Show();

            //test
            bmp = e.Bmp;
            win = new Window { SizeToContent = SizeToContent.WidthAndHeight, ResizeMode = ResizeMode.NoResize,WindowStartupLocation=WindowStartupLocation.CenterScreen };

            var canvas = new Canvas { Width = bmp.Width, Height = bmp.Height + 50, MaxHeight = 650, MaxWidth = 800 };
            var canvasSure = new StackPanel { Width = bmp.Width, Height = bmp.Height + 50, MaxHeight = 650, MaxWidth = 800 };
            var imageSure = new Image { Width = bmp.Width, Height = bmp.Height, Source = bmp, MaxHeight = 600, MaxWidth = 800 };
            var btnSure = new Button { Width = 100, Height = 40, Name = "btnSendCapture", Content = "确认" };
            btnSure.Click += BtnSure_Click;
            canvasSure.Children.Add(imageSure);
            canvasSure.Children.Add(btnSure);
            canvas.Children.Add(canvasSure);

            win.Content = canvas;
            win.Show();
        }

        private void BtnSure_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();

            
            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            win.Close();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] bytes=null;
            this.Dispatcher.Invoke((Action)(() =>
            {
                win.Title = "正在发送,请稍后........";
                bytes= BitmapSource2ByteArray(bmp);
            }));
            SendMediaClick(bytes, "ChatImage", "image");

        }

        private Byte[] BitmapSource2ByteArray(BitmapSource source)
        {
            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();

            encoder.Save(stream);
            return stream.ToArray();
        }
        #endregion

        private void tbxTextMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (SendTextClick != null && MessageText.Trim() != string.Empty && e.Key == Key.Enter)
            {
                SendTextClick();
            }
        }
    }
}
