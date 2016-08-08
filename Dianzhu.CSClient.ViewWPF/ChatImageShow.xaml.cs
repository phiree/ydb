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
using System.Windows.Shapes;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// ChatImageShow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatImageShow : Window
    {
        bool isInit;
        public ChatImageShow(Uri uri)
        {
            InitializeComponent();

            this.Height = 768;
            this.Width = 1024;

            img.Source = new Uri(uri.ToString() + "_1024X768");
            isInit = true;
        }


        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
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
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragStart = e.GetPosition(root);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
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
