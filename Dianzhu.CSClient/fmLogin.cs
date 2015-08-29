using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using Dianzhu.Model;
using agsXMPP.protocol.client;
using System.Text.RegularExpressions;
using Dianzhu.CSClient.Presenter;
namespace Dianzhu.CSClient
{
    public partial class fmLogin : Form,IVew.ILoginForm
    {
       log4net.ILog log = log4net.LogManager.GetLogger("cs");

       Presenter.LoginPresenter loginPresenter;
        public fmLogin()
        {
            
            loginPresenter = new LoginPresenter(this );
            InitializeComponent();
             btnLogin.Click += new EventHandler(btnLogin_Click2);
  
        }
 
    
        private void btnLogin_Click2(object sender, EventArgs e)
        {

            ViewLogin();
        }
        
        void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            ///todo:如何对这部分解耦?????????
            //如果无法将xmpp部分解耦出去, 那么 iview的 username password属性是多余的 
            if (InvokeRequired)
            {
                BeginInvoke(new XmppElementHandler(XMPPConnection_OnAuthError), new object[] { sender, e });
                return;
            }
            log.Error(e.InnerXml);
            lblResult.Text = "用户名密码有误";
        }

        void XMPPConnection_OnError(object sender, Exception ex)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ErrorHandler(XMPPConnection_OnError), new object[] { sender, ex });
                return;
            }
            log.Error(ex.Message);
            lblResult.Text = "错误." + ex.Message;
        }
        //这是一个异步的进程.
        
        public  bool IsLoginSuccess
        {
            set {
                this.DialogResult = value ? System.Windows.Forms.DialogResult.OK : System.Windows.Forms.DialogResult.Abort;
                //return;
                //Action lambda = () =>this.DialogResult=value? System.Windows.Forms.DialogResult.OK: System.Windows.Forms.DialogResult.Abort;
                //if (InvokeRequired)
                //    Invoke(lambda);
                //else
                //    lambda();

            } 
        }



        public event IVew.ViewLogin ViewLogin;
          public string UserName
          {
              get { return tbxUserName.Text; }
              
          }
          public string Password
          {
              get { return tbxPassword.Text; }
          }
          public string LoginMessage {
              set {
                  Action lambda = () =>
                  {
                      lblResult.Text = value;
                  };
              if(InvokeRequired) Invoke(lambda);
              else lambda();
              }
          }
          public string ErrorMessage { get; set; }
          public void ShowError()
          {
            
              Action lambda = () =>
              {
                  MessageBox.Show(ErrorMessage);
              };
              if (InvokeRequired) Invoke(lambda);
              else lambda();
          }
          public string LoginButtonText { set { btnLogin.Text = value; } }
          public bool LoginButtonEnabled { set { btnLogin.Enabled = value; } }
    }
}
