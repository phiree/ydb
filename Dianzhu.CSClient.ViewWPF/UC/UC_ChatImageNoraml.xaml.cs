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
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatImageNoraml.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatImageNoraml : UserControl
    {
        Uri imgUri;
        string imgPath;
        string uri;
        public UC_ChatImageNoraml(string uri)
        {
            InitializeComponent();
            this.imgUri = new Uri(@"\Download\"+uri,UriKind.Relative);
            imgPath = PHSuit.DownloadSoft.DownloadPath + uri;
            this.uri = uri;

            img.Source = new BitmapImage(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + uri.ToString() + "_150X100"));
        }

        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChatImageShow image = new ChatImageShow(uri);
            image.ShowDialog();
        }

        private void img_MouseMove(object sender, MouseEventArgs e)
        {
            img.Cursor = Cursors.Hand;
        }

        //private void gif_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        //}

        //private void gif_MouseMove(object sender, MouseEventArgs e)
        //{
        //    gif.Cursor = Cursors.Hand;
        //}

        //private void gif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    ChatImageShow image = new ChatImageShow(uri);
        //    image.ShowDialog();
        //}
    }
}
