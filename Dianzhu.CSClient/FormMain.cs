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
        BLL.DZMembershipProvider BLLMember = BLLFactory.BLLMember;
        BLL.BLLReception BLLReception = BLLFactory.BLLReception;
        BLL.BLLDZService BLLDZService = BLLFactory.BLLDZService;
        BLL.BLLServiceOrder BLLServiceOrder = BLLFactory.BLLServiceOrder;

        FormController FormController;
        public FormMain()
        {
            InitializeComponent();
            FormController = new FormController(this, BLLMember, BLLReception, BLLDZService,
            BLLServiceOrder);
        }

       
        IList<ReceptionChat> chatLog;
        public IList<ReceptionChat> ChatLog
        {

            set
            {
                Action lambda = () =>
                {
                    pnlChat.Controls.Clear();
                    foreach (ReceptionChat chat in value)
                    {
                        LoadOneChat(chat);
                    }
                    chatLog = value;
                };
                if (InvokeRequired)
                {
                    Invoke(lambda);
                }
                else { lambda(); }

            }
            get
            {
                return chatLog;
            }
        }
        public string MessageTextBox
        {
            get { return tbxChatMsg.Text; }
            set { tbxChatMsg.Text = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chat"></param>
        public void LoadOneChat(ReceptionChat chat)
        {
            
            Label lblTime = new Label();
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();
            pnlOneChat.Controls.AddRange(new Control[] { lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight;
            _AutoSize(pnlOneChat);
 
            lblTime.Text = chat.SavedTime.ToShortTimeString() + " ";
 
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
                pb.Load(GlobalViables.WebServerRoot + chat.MessageMediaUrl);
                pnlOneChat.Controls.Add(pb);
            }
            switch (chat.ChatType)
            {
                case Model.Enums.enum_ChatType.PushedService: break;
                case Model.Enums.enum_ChatType.ConfirmedService:
                    
                    FlowLayoutPanel pnl = new FlowLayoutPanel();
                    Label lblServiceId = new Label();
                    //lblServiceId.Text = chat.ServiceId;
                    pnl.Controls.Add(lblServiceId);

                    Button btnSendPayLink = new Button();
                   // btnSendPayLink.Tag = chat.ServiceId;
                    btnSendPayLink.Text = "发送支付链接";
                    //todo:create order for this
                    btnSendPayLink.Click += new EventHandler(btnSendPayLink_Click);
                    pnl.Controls.Add(btnSendPayLink);
                    pnlOneChat.Controls.Add(pnl);
                break;
                case Model.Enums.enum_ChatType.Order: break;
                case Model.Enums.enum_ChatType.Text: break;
                default: break;
            }
          

            Action lambda = () =>
            {
                pnlChat.Controls.Add(pnlOneChat);
                pnlChat.ScrollControlIntoView(pnlOneChat);
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else
            {
                lambda();
            }

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
            Action lambda=()=>{
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
            btn.ForeColor = foreColor;};
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else
            {
                lambda();
            }

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
        #region Impletion of Iview
        public event SendMessageHandler SendMessageHandler;
        public event ActiveCustomerHandler ActiveCustomerHandler;

        #endregion




        private void tbxChatMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSend.PerformClick();
            }
        }
        

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
           // m.To = StringHelper.EnsureOpenfireUserName(CurrentCustomerName) + "@" + GlobalViables.Domain;
            // GlobalViables.XMPPConnection.Send(m);
            //业务逻辑
           // FormController.PushService(new Guid(serviceId));
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
