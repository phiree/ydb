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
using Dianzhu.CSClient.IVew;
namespace Dianzhu.CSClient.WPFView 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FormLogin : Window,ILoginForm
    {
        public FormLogin()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ViewLogin();
        }

        public string ErrorMessage
        {
            
            set
            {
                Action lambda = () =>
                {
                    MessageBox.Show(value);
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
            
            }
        }

        public bool IsLoginSuccess
        {
            set
            {
              
                if (!Dispatcher.CheckAccess())
                {
                    Action lambda = () =>
                    {
                        this.DialogResult = value;
                    };
                    Dispatcher.Invoke(lambda);
                }
               // else { lambda(); }
               
            }
        }

        public bool LoginButtonEnabled
        {
            set
            {
                Action lambda = () =>
                {
                    btnLogin.IsEnabled = value;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
               
            }
        }

        public string LoginButtonText
        {
            set
            {
                 Action lambda = () =>
                {
                    btnLogin.Content = value;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
            }
        }

        public string LoginMessage
        {
            set
            {
                Action lambda = () =>
                {
                    lblResult.Content = value;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
              
            }
        }

        public string Password
        {
            get
            {
                return tbxPassword.Password;
            }
        }

        public string UserName
        {
            get
            {
                string returnvalue = string.Empty;
                Action lambda = () =>
                {
                    returnvalue= tbxUserName.Text;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
                return returnvalue;
            }
        }

        public event ViewLogin ViewLogin;

        
    }
}
