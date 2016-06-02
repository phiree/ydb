using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using Dianzhu.CSClient.IView;
namespace Dianzhu.IntegrationTest.Bootstraper
{
    public class LoginView : ILoginForm
    {
        string userName;
        string plainPassword;
        public LoginView(string username,string plainpassword)
        {
            this.userName = username;
            this.plainPassword = plainpassword;

        }
        public string ErrorMessage
        {
            set
            {
                 
            }
        }

        public string FormText
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                
            }
        }

        public bool IsLoginSuccess
        {
            set
            {

               
                // else { lambda(); }

            }
        }

        public bool LoginButtonEnabled
        {
            set
            {
                
            }
        }

        public string LoginButtonText
        {
            set
            {
              
            }
        }

        public string LoginMessage
        {
            set
            {
                 
            }
        }

        public string Password
        {
            get
            {
                return plainPassword;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
        }

        public event EventHandler TestClick;
        public event ViewLogin ViewLogin;

        public bool ShowDialog()
        {
            return true;
        }
    }
}
