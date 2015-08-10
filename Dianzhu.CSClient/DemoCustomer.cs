using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsc=agsXMPP.protocol.client;
using agsXMPP;
using agsXMPP.protocol.client;
namespace Dianzhu.CSClient
{
    public partial class DemoCustomer : Form
    {
        public DemoCustomer()
        {
            InitializeComponent();
            GlobalViables.XMPPConnection.OnLogin += new agsXMPP.ObjectHandler(XMPPConnection_OnLogin);
            GlobalViables.XMPPConnection.OnMessage += new agsc.MessageHandler(XMPPConnection_OnMessage);
        }

        void XMPPConnection_OnMessage(object sender, agsc.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender,msg });
                return;
            }
            AddLog(msg.From, msg.Body);
        }

        void XMPPConnection_OnLogin(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender});
                return;
            }
            lblLoginStatus.Text = "登录成功";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //为该用户分配客服
            string cs = "yuanfei";
            GlobalViables.XMPPConnection.Send(new agsc.Message(cs, tbxMessage.Text));
            AddLog(tbxUserName.Text, tbxMessage.Text);

        }
        void AddLog(string user, string body)
        {
            tbxLog.Text = user + ":" + body + Environment.NewLine + tbxLog.Text;
       
        }
    }
}
