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
    public partial class FormMain : Form,IView
    {
        BLL.DZMembershipProvider BLLMember = new BLL.DZMembershipProvider();
        BLL.BLLReception BLLReception = new BLL.BLLReception();
        FormController FormController;
        public FormMain()
        {
            InitializeComponent();
            FormController = new CSClient.FormController(this, BLLMember, BLLReception);
            GlobalViables.XMPPConnection.OnMessage += new MessageHandler(XMPPConnection_OnMessage);
            GlobalViables.XMPPConnection.OnPresence += new PresenceHandler(XMPPConnection_OnPresence);
        }
        #region XMPP
       
        void XMPPConnection_OnPresence(object sender, Presence pres)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new PresenceHandler(XMPPConnection_OnPresence), new object[] { sender, pres });
                return;
            }
            FormController.OnPresent((int)pres.Type, pres.From.User);
        }
        void XMPPConnection_OnMessage(object sender, agsXMPP.protocol.client.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }
            FormController.ReceiveMessage(StringHelper.EnsureNormalUserName(msg.From.User), msg.Body);
        }
 
        string currentCustomerName;
        public string CurrentCustomerName {
            get { return currentCustomerName; }
            set { currentCustomerName = value; }
        }
        public string ChatHistory
        {
            get
            {
                return tbxChatLog.Text;
            }
            set
            {
                tbxChatLog.Text = value;
            }
        }
        public void SetCustomerButtonStyle(string buttonText, em_ButtonStyle buttonStyle)
        {
            Button btn = (Button)pnlCustomerList.Controls.Find
                (GlobalViables.ButtonNamePrefix + buttonText, true)[0];
            Color foreColor = Color.White;
            switch (buttonStyle)
            {
                case em_ButtonStyle.Login:
                    foreColor = Color.Green;
                    break;
                case em_ButtonStyle.LogOff:
                    foreColor = Color.Gray;
                    break;
                case em_ButtonStyle.Readed: foreColor = Color.Black; break;
                case em_ButtonStyle.Unread: foreColor = Color.Red; break;
                default: break;
            }
            btn.ForeColor = foreColor;
             
        }
        public void AddCustomerButtonWithStyle(string buttonText, em_ButtonStyle buttonStyle)
        {
            Button btn = new Button();
            btn.Text = buttonText;
            btn.Name = GlobalViables.ButtonNamePrefix + buttonText;
            btn.Click += new EventHandler(btnCustomer_Click);
            pnlCustomerList.Controls.Add(btn);
            SetCustomerButtonStyle(buttonText, buttonStyle);
        }

        void btnCustomer_Click(object sender, EventArgs e)
        {
            FormController.ActiveCustomer(((Button)sender).Text);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = tbxChatMsg.Text;
            if (string.IsNullOrEmpty(message)||string.IsNullOrEmpty(CurrentCustomerName))
            {
                return;
            }
          
            GlobalViables.XMPPConnection.Send(new agsXMPP.protocol.client.Message(
            StringHelper.EnsureOpenfireUserName(CurrentCustomerName) + "@" + GlobalViables.Domain, MessageType.chat, message));
            SendMessage(tbxChatMsg.Text);
            tbxChatMsg.Text = string.Empty;
        }
 
        public void SendMessage (string message)
        {
            FormController.SendMessage(message, currentCustomerName);
        }

        

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        #endregion


        public string SerachKeyword
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Model.DZService> SearchedService
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private void tbxChatMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSend.PerformClick();
            }
        }
    }
}
