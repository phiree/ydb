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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatImageNoraml.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatImageNoraml : UserControl
    {
        Uri uri;
        public UC_ChatImageNoraml(Uri uri)
        {
            InitializeComponent();
            this.uri = uri;

            gif.Source = new Uri(uri.ToString() + "_150X100");
        }

        private void gif_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }

        private void gif_MouseMove(object sender, MouseEventArgs e)
        {
            gif.Cursor = Cursors.Hand;
        }

        private void gif_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChatImageShow image = new ChatImageShow(uri);
            image.ShowDialog();
        }
    }
}
