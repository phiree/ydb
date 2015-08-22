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
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    public partial class FormMain : Form, IView
    {
        BLL.DZMembershipProvider BLLMember = new BLL.DZMembershipProvider();
        BLL.BLLReception BLLReception = new BLL.BLLReception();
        BLL.BLLDZService BLLDZService = new BLL.BLLDZService();
        FormController FormController;
        public FormMain()
        {
            InitializeComponent();
            FormController = new CSClient.FormController(this, BLLMember, BLLReception, BLLDZService);
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
        public string CurrentCustomerName
        {
            get { return currentCustomerName; }
            set { currentCustomerName = value; }
        }
        public IList<Dianzhu.Model.ReceptionChat> ChatHistory
        {
             
            set
            {
                pnlChat.Controls.Clear();
                foreach (ReceptionChat chat in value)
                {
                    LoadOneChat(chat);
                }
                 
            }
        }
        public void LoadOneChat(ReceptionChat chat)
        {
            Label lblTime = new Label();
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();
            pnlOneChat.Controls.AddRange(new Control[] {  lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight;
            _AutoSize(pnlOneChat);
            pnlOneChat.Dock = DockStyle.Top;
            foreach (Label c in pnlOneChat.Controls)
            {
                c.BorderStyle = BorderStyle.FixedSingle;
            }


            lblTime.Text = chat.SavedTime.ToShortTimeString() + " ";
             
            if (GlobalViables.CurrentCustomerService == chat.To)
            {
                pnlOneChat.FlowDirection = FlowDirection.RightToLeft;
                lblMessage.TextAlign = ContentAlignment.MiddleRight;
            }
            
            lblFrom.Text = chat.From.UserName;
            
            lblMessage.Text = chat.MessageBody;
              _AutoSize(lblMessage);
              pnlOneChat.Width = pnlChat.Size.Width - 6;
                   
            pnlChat.Controls.Add(pnlOneChat);
            pnlChat.ScrollControlIntoView(pnlOneChat);
             
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
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(CurrentCustomerName))
            {
                return;
            }

            GlobalViables.XMPPConnection.Send(new agsXMPP.protocol.client.Message(
            StringHelper.EnsureOpenfireUserName(CurrentCustomerName) + "@" + GlobalViables.Domain, MessageType.chat, message));
            SendMessage(tbxChatMsg.Text);
            tbxChatMsg.Text = string.Empty;
        }

        public void SendMessage(string message)
        {
            FormController.SendMessage(message, currentCustomerName);
        }



        private void tbxChatMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSend.PerformClick();
            }
        }
        #endregion

        #region searchService

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FormController.SearchService(SerachKeyword);
        }


        public string SerachKeyword
        {
            get
            {
                return tbxKeywords.Text;
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

            }
        }
        public void LoadSearchHistory(IList<Model.DZService> serviceList)
        {
            pnlResultService.Controls.Clear();
            foreach (Model.DZService service in serviceList)
            {
                LoadServiceToPanel(service);
            }
        }
        public void LoadServiceToPanel(Model.DZService service)
        {
            FlowLayoutPanel pnl = new FlowLayoutPanel();
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl.FlowDirection = FlowDirection.LeftToRight;
            Label lblBusinessName = new Label();
            lblBusinessName.BorderStyle = BorderStyle.FixedSingle;
            lblBusinessName.Text = service.Business.Name;
            lblBusinessName.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
            pnl.Controls.Add(lblBusinessName);
            Label lblServiceName = new Label();
            lblServiceName.BorderStyle = BorderStyle.FixedSingle;
            lblServiceName.Text = service.Name;
            pnl.Controls.Add(lblServiceName);
            Button btnPushService = new Button();
            btnPushService.Text = "推送";
            btnPushService.Tag = service.Id;
            btnPushService.Click += new EventHandler(btnPushService_Click);
            pnl.Controls.Add(btnPushService);
            pnlResultService.Controls.Add(pnl);

        }

        void btnPushService_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            
        }
        private void _AutoSize(Control c)
        {
            c.Size = new Size(0, 0);
            if (c is Label)
            {
                ((Label)c).AutoSize = true;
            }
            if (c is Panel)
            {
                ((Panel)c).AutoSize = true;
            }
             
            
        }

    }
}
