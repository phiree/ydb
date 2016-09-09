using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// ChatImageShow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatImageShow : Window
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.ChatImageShow");
        bool isInit;
        BackgroundWorker worker;
        public ChatImageShow(string fileName)
        {
            InitializeComponent();

            this.Height = 768;
            this.Width = 1024;

            //img.Source = new Uri(uri.ToString() + "_1024X768");

            //BitmapImage image = new BitmapImage();
            //image.BeginInit();
            //StreamResourceInfo info = Application.GetRemoteStream(uri);
            //image.StreamSource = info.Stream;
            //image.EndInit();

            //img.Source = image;
            //img.Stretch = Stretch.Uniform;
            //isInit = true;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(fileName);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Action lamda = () =>
            {
                BitmapImage imgUri = e.Result as BitmapImage;

                ShowImage(imgUri);
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

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action lamda = () =>
            {
                try
                {
                    string imgName = e.Argument.ToString();
                    string imgPath = PHSuit.LocalFileManagement.LocalFilePath + imgName;
                    string imgUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + imgName;

                    if (!File.Exists(imgPath))
                    {
                        if (PHSuit.LocalFileManagement.DownLoad(string.Empty, imgUrl, imgName))
                        {
                            
                        }
                        else
                        {
                            throw new Exception("下载失败");
                        }
                    }

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
                    e.Result = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/NoImage.png"));
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

        public void ShowLoadingMsg()
        {
            Action lamda = () =>
            {
                img.Visibility = Visibility.Collapsed;
                lblLoading.Visibility = Visibility.Visible;                
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

        public void ShowImage(BitmapImage image)
        {
            Action lamda = () =>
            {
                lblLoading.Visibility = Visibility.Collapsed;
                img.Visibility = Visibility.Visible;

                img.Source = image;
                img.Stretch = Stretch.Uniform;
                isInit = true;
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


        private void img_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var mosePos = e.GetPosition(img);

            var scale = transform.ScaleX * (e.Delta > 0 ? 1.2 : 1 / 1.2);
            scale = Math.Max(scale, 1);

            transform.ScaleX = scale;
            transform.ScaleY = scale;

            if (scale == 1)        //缩放率为1的时候，复位
            {
                translate.X = 0;
                translate.Y = 0;
            }
            else                //保持鼠标相对图片位置不变
            {
                var newPos = e.GetPosition(img);

                translate.X += (newPos.X - mosePos.X);
                translate.Y += (newPos.Y - mosePos.Y);
            }
        }

        Point dragStart;
        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragStart = e.GetPosition(root);
        }

        private void img_MouseMove(object sender, MouseEventArgs e)
        {
            img.Cursor = Cursors.Hand;

            if (!isInit)
            {
                if (e.LeftButton != MouseButtonState.Pressed)
                {
                    return;
                }

                var current = e.GetPosition(root);

                translate.X += (current.X - dragStart.X) / transform.ScaleX;
                translate.Y += (current.Y - dragStart.Y) / transform.ScaleY;

                dragStart = current;
            }
            else
            {
                isInit = false;
            }
        }

        private void img_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }
    }
}
