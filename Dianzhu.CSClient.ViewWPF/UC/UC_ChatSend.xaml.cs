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
using System.IO;
namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatSend.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatSend : UserControl,IView.IViewChatSend
    {
        public UC_ChatSend()
        {
            InitializeComponent();
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
        public event SendImageClick SendImageClick;

        private void btnSendTextMessage_Click(object sender, RoutedEventArgs e)
        {
            if (SendTextClick != null)
            {
                SendTextClick();
            }
        }

        private void btnSendImage_Click(object sender, RoutedEventArgs e)
        {
            if (SendImageClick != null)
            {

                // Create OpenFileDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".png;.jpg";
                dlg.Filter = "Images |*.jpg";

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    
                    byte[] fileData = File.ReadAllBytes(dlg.FileName);
                    SendImageClick(fileData, "ChatImage", "image");
                    
                }
            }
        }
    }
}
