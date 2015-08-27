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
            CSClient.IInstantMessage.IXMPP xmpp = GlobalViables.XMPP;
            loginPresenter = new LoginPresenter(this, xmpp);
            InitializeComponent();
            
             //GlobalViables.XMPPConnection.OnPresence += new PresenceHandler(XMPPConnection_OnPresence);
             //GlobalViables.XMPPConnection.OnLogin += new ObjectHandler(XMPPConnection_OnLogin);
             //GlobalViables.XMPPConnection.OnAuthError += new XmppElementHandler(XMPPConnection_OnAuthError);
             btnLogin.Click += new EventHandler(btnLogin_Click2);

        }
        
        /// <summary>
        /// 接收客服状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pres"></param>
        void XMPPConnection_OnPresence(object sender, Presence pres)
        {
             
        }
    
        private void btnLogin_Click2(object sender, EventArgs e)
        {
            
            //Jid jid = new Jid(StringHelper.EnsureOpenfireUserName( tbxUserName.Text) + "@" + GlobalViables.ServerName);
           
            //GlobalViables.XMPPConnection.Open(jid.User, tbxPassword.Text);
            LoginHandler(sender, e);
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
        void XMPPConnection_OnLogin(object sender)
        {
            
            //告诉世界,我,上线,,,了.
           // Presence p = new Presence(ShowType.chat, "Online");
           // p.Type = PresenceType.available;
           // GlobalViables.XMPPConnection.Send(p);
           // if (InvokeRequired)
           // {
           //     BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender });
           //     return;
           // }
           // //保存当前用户
           // BLL.DZMembershipProvider bllMembership = new BLL.DZMembershipProvider();
           // DZMembership customerService = BLLFactory.BLLMembership.GetUserByName(tbxUserName.Text);
           // GlobalViables.CurrentCustomerService = customerService;
           //// this.DialogResult = DialogResult.OK;
             
        }
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
  

           
          public event EventHandler LoginHandler;
          public string UserName
          {
              get { return tbxUserName.Text; }
              
          }
          public string Password
          {
              get { return tbxPassword.Text; }
          }
    }
}
