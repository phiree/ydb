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
            throw new NotImplementedException();
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
                Jid jid = new Jid(tbxUserName.Text+"@"+GlobalViables.ServerName);
                GlobalViables.XMPPConnection = new XmppClientConnection(jid.Server);
                GlobalViables.XMPPConnection.Open(jid.User, tbxPassword.Text);
                GlobalViables.XMPPConnection.OnLogin += new ObjectHandler(XMPPConnection_OnLogin);
                GlobalViables.XMPPConnection.OnAuthError += new XmppElementHandler(XMPPConnection_OnAuthError);
                 
            }
            else
            {
                lblResult.Text = "用户认证失败失败";
            }
           
        }

        void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            lblResult.Text = "登录通讯服务器失败";
        }

        void XMPPConnection_OnError(object sender, Exception ex)
        {
            
        }

        void XMPPConnection_OnLogin(object sender)
        {
            //告诉世界,我,上线,,,了.
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            GlobalViables.XMPPConnection.Send(p);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
