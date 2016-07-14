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

namespace Dianzhu.CSClient.ViewWPF.UC
{
    /// <summary>
    /// UC_ChatCustomer.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatCustomer : UserControl
    {
        public UC_ChatCustomer(ReceptionChat chat)
        {
            InitializeComponent();

            imgAvatar.Source = new BitmapImage(new Uri(chat.From.AvatarUrl));
            tbChat.Text = chat.MessageBody;
        }
    }
}
