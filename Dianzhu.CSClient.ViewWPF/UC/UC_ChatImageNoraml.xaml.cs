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
        string imgPath;
        string fileName;
        string imgUri;
        public UC_ChatImageNoraml(string name)
        {
            InitializeComponent();
            //this.imgUri = new Uri(@"\Download\"+fileName,UriKind.Relative);
            imgPath = PHSuit.DownloadSoft.DownloadPath + name;
            fileName = imgUri = name;

            if (!name.Contains(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl")))
            {
                imgUri = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + name;
            }
            else
            {
                fileName = name.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), string.Empty);
            }
            img.Source = new BitmapImage(new Uri( imgUri + "_150X100"));
        }

        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChatImageShow image = new ChatImageShow(fileName);
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
