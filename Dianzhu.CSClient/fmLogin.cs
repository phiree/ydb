using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;
 
namespace Dianzhu.CSClient
{
    public partial class fmLogin : Form
    {
       log4net.ILog log = log4net.LogManager.GetLogger("cs");
        public fmLogin()
        {
            InitializeComponent();
            GlobalViables.XMPPConnection.OnPresence += new PresenceHandler(XMPPConnection_OnPresence);
        }

        /// <summary>
        /// 接收新消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pres"></param>
        void XMPPConnection_OnPresence(object sender, Presence pres)
        {
             
        }
        void SendMessage(string customerId, string message)
        { 
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            
            //validate User
            bool useValid = true;
            if (useValid)
            {
                Jid jid = new Jid(tbxUserName.Text+"@"+GlobalViables.Domain);
                //GlobalViables.XMPPConnection = new XmppClientConnection(jid.Server);
                GlobalViables.XMPPConnection.Open(jid.User, tbxPassword.Text);
                GlobalViables.XMPPConnection.OnLogin += new ObjectHandler(XMPPConnection_OnLogin);
                GlobalViables.XMPPConnection.OnError+=new ErrorHandler(XMPPConnection_OnError);
                GlobalViables.XMPPConnection.OnAuthError += new XmppElementHandler(XMPPConnection_OnAuthError);
                GlobalViables.CurrentUserName = tbxUserName.Text;
            }
            else
            {
                lblResult.Text = "用户认证失败失败";
            }
           
        }

        void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XmppElementHandler(XMPPConnection_OnAuthError), new object[] { sender, e });
                return;
            }
            log.Error(e.InnerXml);
            lblResult.Text = "登录通讯服务器失败";
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
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            GlobalViables.XMPPConnection.Send(p);
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender });
                return;
            }
            //保存当前用户
            GlobalViables.CurrentCustomerService=  BLLFactory.BLLMembership.GetUserByName(tbxUserName.Text);
            this.DialogResult = DialogResult.OK;
            
        }

        private void fmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

             
        }
    }
}
