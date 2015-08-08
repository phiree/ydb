using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using xmppMessage = agsXMPP.protocol.client;
using agsXMPP.protocol.client;
namespace Dianzhu.CSClient
{
    public partial class fmMain : Form
    {

        public fmMain()
        {
            Form fmLogin=new fmLogin();
            if (fmLogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
            InitializeComponent();
            GlobalViables.XMPPConnection.OnMessage += new MessageHandler(XMPPConnection_OnMessage);
        }

        void XMPPConnection_OnMessage(object sender, xmppMessage.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }
            //判断该客户是否已经出现在列表中.
            AddNewMessage(msg.From.User, msg.Body);
        }

        /// <summary>
        /// 新增一个客户
        /// 顶部增加一个item,并高亮显示(未读标签),声音提示
        /// </summary>
        private void AddNewCustomer(string customerName)
        {
            Button btnNewCustom = new Button();
            btnNewCustom.Text = customerName;
            gbCustomerList.Controls.Add(btnNewCustom);
        }

        private void btnDemoAddCustomer_Click(object sender, EventArgs e)
        {
            AddNewCustomer("new customer");
        }

        /// <summary>
        /// 在聊天窗口显示新信息
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="msg"></param>
        private void AddNewMessage(string customerName, string msg)
        {
            Label lbl = new Label();
            lbl.Text =customerName+":"+msg;
            pnlChatHistory.Controls.Add(lbl);
            string outMessage = "请求服务";
            GlobalViables.XMPPConnection.Send(new xmppMessage.Message(new Jid("dianzhu@yuanfei-pc"),
                                                MessageType.chat,
                                            outMessage));
        }
        
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            AddNewMessage("staff", tbxMsg.Text);
        }

        #region xmpp
        
        #endregion
    }



}
