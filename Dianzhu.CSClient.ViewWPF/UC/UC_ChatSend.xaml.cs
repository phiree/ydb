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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatSend.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatSend : UserControl,IView.IViewChatSend
    {
        private readonly RisCaptureLib.ScreenCaputre screenCaputre = new RisCaptureLib.ScreenCaputre();
        private Size? lastSize;

        public UC_ChatSend()
        {
            InitializeComponent();

            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
        }

        public string MessageText
        {
            get
            {
                return tbxTextMessage.Text;
            }

            set
            {
                tbxTextMessage.Text = value;
            }
        }

        public event SendTextClick SendTextClick;
        public event SendMediaClick SendMediaClick;
        public event MediaMessageSent Captured;

        private void btnSendTextMessage_Click(object sender, RoutedEventArgs e)
        {
            if (SendTextClick != null)
            {
                SendTextClick();
            }
        }

        private void btnSendMedia_Click(object sender, RoutedEventArgs e)
        {
            if (SendMediaClick != null)
            {

                // Create OpenFileDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

               

                Button btn = (Button)sender;
                string domain = string.Empty;
                string mediaType = string.Empty;
              
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
                    dlg.Filter = "Audios |*.mp3";
                    domain = "ChatAudio";
                    mediaType = "voice";
                }

                // Display OpenFileDialog by calling ShowDialog method
                    Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    
                    byte[] fileData = File.ReadAllBytes(dlg.FileName);
                    SendMediaClick(fileData,domain,mediaType);
                    
                }
            }
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

            var canvas = new Canvas { Width = bmp.Width, Height = bmp.Height + 50 };
            var canvasSure = new StackPanel { Width = bmp.Width, Height = bmp.Height + 50 };
            var imageSure = new Image { Width = bmp.Width, Height = bmp.Height, Source = bmp };
            var btnSure = new Button { Width = 100, Height = 40,Name="btnSendCapture", Content = "确认" };
            btnSure.Click += BtnSure_Click;
            canvasSure.Children.Add(imageSure);
            canvasSure.Children.Add(btnSure);
            canvas.Children.Add(canvasSure);

            win.Content = canvas;
            win.Show();
        }

        private void BtnSure_Click(object sender, RoutedEventArgs e)
        {
            SendMediaClick(BitmapSource2ByteArray(bmp), "ChatImage", "image");
            win.Close();
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
    }
}
