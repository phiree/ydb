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

using Dianzhu.CSClient.IVew;
using Dianzhu.CSClient.Presenter;
//using Dianzhu.CSClient.Model;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    public partial class FormMain : Form, MainFormView
    {
        BLL.DZMembershipProvider BLLMember = new BLL.DZMembershipProvider();
        BLL.BLLReception BLLReception = new BLL.BLLReception();
        BLL.BLLDZService BLLDZService = new BLL.BLLDZService();
        BLL.BLLServiceOrder BLLServiceOrder = new BLL.BLLServiceOrder();
        
        FormController FormController;
        public FormMain()
        {
            InitializeComponent();
            FormController = new FormController(this, BLLMember, BLLReception, BLLDZService,
             BLLServiceOrder);
            //GlobalViables.XMPPConnection.OnMessage += new MessageHandler(XMPPConnection_OnMessage);
            //GlobalViables.XMPPConnection.OnPresence += new PresenceHandler(XMPPConnection_OnPresence);
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
            //解析消息文件,包含里面的多媒体文件. 为了降低controller对agsxmpp的依赖,将解析放在v
            string mediaUrl=string.Empty;
            string confirm_service_id = string.Empty;

            if (msg.Attributes["media"] != null)
            {
                mediaUrl = msg.Attributes["media"].ToString();
            }
            if (msg.HasAttribute("selected_service_id"))
            {
                confirm_service_id = msg.GetAttribute("selected_service_id");
            }
            FormController.ReceiveMessage(StringHelper.EnsureNormalUserName(msg.From.User), msg.Body, mediaUrl,confirm_service_id);
        }

        string currentCustomerName;
        public string CurrentCustomerName
        {
            get { return currentCustomerName; }
            set { currentCustomerName = value; }
        }
        IList<ReceptionChat> chatLog;
        public IList<ReceptionChat> ChatLog
        {

            set
            {
                pnlChat.Controls.Clear();
                foreach (ReceptionChat chat in value)
                {
                    LoadOneChat(chat);
                }
                chatLog = value;

            }
            get
            {
                return chatLog;
            }
        }
        public string MessageTextBox {
            get { return tbxChatMsg.Text; }
            set { tbxChatMsg.Text = value; }
        }
        public void LoadOneChat(ReceptionChat chat)
        {
            
            Label lblTime = new Label();
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();
            pnlOneChat.Controls.AddRange(new Control[] { lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight;
            _AutoSize(pnlOneChat);
            pnlOneChat.Dock = DockStyle.Top;
            foreach (Label c in pnlOneChat.Controls)
            {
                c.BorderStyle = BorderStyle.FixedSingle;
            }


            lblTime.Text = chat.SavedTime.ToShortTimeString() + " ";

            //if (GlobalViables.CurrentCustomerService.UserName == chat.To.UserName)
            //{
            //    pnlOneChat.FlowDirection = FlowDirection.RightToLeft;
            //    lblMessage.TextAlign = ContentAlignment.MiddleRight;
            //}

            lblFrom.Text = chat.From.UserName;
            lblMessage.Text = chat.MessageBody;
            _AutoSize(lblMessage);
            pnlOneChat.Width = pnlChat.Size.Width - 36;

            //显示多媒体信息.

            if (!string.IsNullOrEmpty(chat.MessageMediaUrl))
            {

                PictureBox pb = new PictureBox();
                pb.Click += new EventHandler(pb_Click);
                pb.Size = new System.Drawing.Size(100, 100);
                pb.Load("http://localhost:8033"+ chat.MessageMediaUrl);
                pnlOneChat.Controls.Add(pb);
            }
            if (!string.IsNullOrEmpty(chat.ServiceId))
            {
                FlowLayoutPanel pnl = new FlowLayoutPanel();
                Label lblServiceId=new Label();
                lblServiceId.Text=chat.ServiceId;
                pnl.Controls.Add(lblServiceId);

                Button btnSendPayLink = new Button();
                btnSendPayLink.Tag = chat.ServiceId;
                btnSendPayLink.Text = "发送支付链接";
                //todo:create order for this
                btnSendPayLink.Click += new EventHandler(btnSendPayLink_Click);
                pnl.Controls.Add(btnSendPayLink);
                pnlOneChat.Controls.Add(pnl);
            }


            pnlChat.Controls.Add(pnlOneChat);
            pnlChat.ScrollControlIntoView(pnlOneChat);

        }
        //点击支付
        void btnSendPayLink_Click(object sender, EventArgs e)
        {
            //agsXMPP.protocol.client.Message msg=new agsXMPP.protocol.client.Message
            //    (
            //    currentCustomerName+"@"+GlobalViables.ServerName,
            //   // StringHelper.EnsureOpenfireUserName(GlobalViables.CurrentCustomerService.UserName)+"@"+GlobalViables.ServerName
                
            //    );
            //string service_id=((Button)sender).Tag.ToString();
           // FormController.SendPayLink(service_id);
           // GlobalViables.XMPPConnection.Send(msg);
        }

        void pb_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Form fm = new ShowFullImage(pb.Image);
            fm.ShowDialog();
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
            //Action lambda = () =>this.DialogResult=value? System.Windows.Forms.DialogResult.OK: System.Windows.Forms.DialogResult.Abort;
           
            Action lamda = () =>
            {
                Button btn = new Button();
                btn.Text = buttonText;
                btn.Name = GlobalViables.ButtonNamePrefix + buttonText;
                btn.Click += new EventHandler(btnCustomer_Click);
                pnlCustomerList.Controls.Add(btn);
                SetCustomerButtonStyle(buttonText, buttonStyle);
            };
            if (InvokeRequired)
                Invoke(lamda);
            else
                lamda();
        }

        void btnCustomer_Click(object sender, EventArgs e)
        {
            if (ActiveCustomerHandler != null)
            { 
                ActiveCustomerHandler(((Button)sender).Text);
            }
            FormController.ActiveCustomer(((Button)sender).Text);
        }
        public event SendMessageHandler SendMessageHandler;
        public event ActiveCustomerHandler ActiveCustomerHandler;
         
        //xmpp发送消息
        public void XMPP_SendMessage(string message,string customerName)
        {
            //string message = tbxChatMsg.Text;
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(CurrentCustomerName))
            {
                return;
            }

           // GlobalViables.XMPPConnection.Send(new agsXMPP.protocol.client.Message(
          //  StringHelper.EnsureOpenfireUserName(customerName) + "@" + GlobalViables.Domain, MessageType.chat, message));
            //FormController.SendMessage(message, currentCustomerName);
            //SendMessageHandler(sender, e);
            //SendMessage(tbxChatMsg.Text);
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
        IList<DZService> searchedService;
        public IList<DZService> SearchedService
        {
            get
            {
                return searchedService;
            }
            set
            {
                searchedService = value;
                pnlResultService.Controls.Clear();
                foreach (DZService service in searchedService)
                {
                    LoadServiceToPanel(service);
                }
            }
        }

        private void LoadServiceToPanel(DZService service)
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
            btnPushService.Tag = service;
            btnPushService.Click += new EventHandler(btnPushService_Click);
            pnl.Controls.Add(btnPushService);
            pnlResultService.Controls.Add(pnl);

        }

        void btnPushService_Click(object sender, EventArgs e)
        {
            //xmpp发送消息 由xmpp实现,在csclient
            agsXMPP.protocol.client.Message m = new agsXMPP.protocol.client.Message();
            m.Type = MessageType.chat;
            DZService service = (DZService)((Button)sender).Tag;
            string serviceId = service.Id.ToString();
            string serviceName = service.Name;
            m.SetAttribute("service_name", service.Name);
            m.SetAttribute("service_id", serviceId);

            m.SetAttribute("t", "push");
           // m.SetAttribute("service_name",
            m.To = StringHelper.EnsureOpenfireUserName(CurrentCustomerName) + "@" + GlobalViables.Domain;
           // GlobalViables.XMPPConnection.Send(m);
            //业务逻辑
            FormController.PushService(new Guid(serviceId));
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (SendMessageHandler != null)
            {
                SendMessageHandler();
            }
        }

    }
}
