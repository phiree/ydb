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
using System.ComponentModel;
namespace Dianzhu.CSClient.ViewWPF 
{
    /// <summary>
    /// IViewLogin的Wpf实现
    /// </summary>
    public partial class FormLogin : Window,ILoginForm
    {
        BackgroundWorker bgw = new BackgroundWorker();
        public FormLogin()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
            //登录时的异步处理
            bgw.DoWork += Bgw_DoWork;
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
            
        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
              MessageBoxResult r=  MessageBox.Show(e.Error.Message);
                
                    this.Close();
                 
            }
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            ViewLogin();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            bgw.RunWorkerAsync();
        }
        public string FormText
        {
            get { return this.Title; }
            set { this.Title = value; }
        }
        public string ErrorMessage
        {
            
            set
            {
                Action lambda = () =>
                {
                    MessageBox.Show(value);
                };
                //more about  Dispacher: https://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcher(v=vs.110).aspx
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
        public event EventHandler TestClick;

        private void tbxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //登录时的异步处理
                ViewLogin();
                e.Handled=true;
            }
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {

        }

        bool ILoginForm.ShowDialog()
        {
           bool? result= ShowDialog();
            if (result.HasValue)
            {
                return result.Value;
            }
            else { return false; }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            TestClick(sender, e);
        }
    }
}
