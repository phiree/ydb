using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
 
//using Dianzhu.CSClient.Model;
 
namespace Dianzhu.CSClient.WinformView
{
    public partial class FormMain : Form, IMainFormView
    {
       
        /// <summary>
        /// todo:view 不需要知道formcontroller , 应该在该窗体初始化的时候 初始化controller
        /// 
        /// </summary>
        
        public FormMain()
        {
            InitializeComponent();
       
        }
        #region Impletion view event
        public event SendMessageHandler SendMessageHandler;
        public event ActiveCustomerHandler ActiveCustomerHandler;
        public event PushExternalService PushExternalService;
        public event PushInternalService PushInternalService;
        public event SearchService SearchService;
        public event SendPayLink SendPayLink;
        public event CreateOrder CreateOrder;
        public event BeforeCustomerChanged BeforeCustomerChanged;

        #endregion



        /// <summary>
        /// 界面当前绑定的客户名称,用于判断聊天记录的呈现方式
        /// </summary>
        private string currentCustomerName;
        public string CurrentCustomerName {
            get { return currentCustomerName; }
            set { currentCustomerName = value; }
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
        string mediaServerRoot;
        public string MediaServerRoot {
            get { return mediaServerRoot; }
            set { mediaServerRoot = value; }
        }
        string buttonNamePrefix;

        public string ButtonNamePrefix
        {
            get { return buttonNamePrefix; }
            set { buttonNamePrefix = value; }
        }

        /// <summary>
        /// 加载一条聊天记录,
        /// </summary>
        
        /// <param name="chat"></param>
        public void LoadOneChat(ReceptionChat chat)
        {

            bool isSender = chat.From.UserName == currentCustomerName;
            Label lblTime = new Label();
            _AutoSize(lblTime);
            lblTime.ForeColor = Color.FromArgb(200);
            Label lblFrom = new Label ();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();

            pnlOneChat.Controls.AddRange(new Control[] {lblTime,lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight  ;
            pnlOneChat.Dock=isSender? DockStyle.Left: DockStyle.Right;
                lblTime.BorderStyle 
                = lblMessage.BorderStyle 
                = lblFrom.BorderStyle
                =pnlOneChat.BorderStyle = BorderStyle.FixedSingle;
            
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
                pb.Load(mediaServerRoot + chat.MessageMediaUrl);
                pnlOneChat.Controls.Add(pb);
            }
            //bye bye. you are abandoned. 2015-9-2

            #region 不再需要判断.
            Type chatType=chat.GetType();
           if(chatType==typeof(ReceptionChatService))
           {

               pnlOneChat.Controls.Add(new Label { Text = "已推送服务:" + ((ReceptionChatService)chat).Service.Name });

                    ReceptionChatService chatService = (ReceptionChatService)chat;

                    Button btnSendPayLink = new Button();
                    // btnSendPayLink.Tag = chat.ServiceId;
                    btnSendPayLink.Text = "发送支付链接";//创建订单,生成支付链接.
                    //todo:create order for this
                    btnSendPayLink.Tag = chatService;
                    btnSendPayLink.Click += new EventHandler(btnSendPayLink_Click);
                    pnlOneChat.Controls.AddRange(new Control[]
                    { new Label{ Text="已选择服务:"+chatService.Service.Name },
                        btnSendPayLink});
           }
           else if (chatType == typeof(ReceptionChatOrder))
           { }
           else { 
           //only text loaded
           }
          
            
            #endregion

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
            ReceptionChatService chat = (ReceptionChatService)((Button)sender).Tag;
            SendPayLink(chat);
        }

        void pb_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Form fm = new  ShowFullImage(pb.Image);
            fm.ShowDialog();
        }
 
        public void SetCustomerButtonStyle(string buttonText, em_ButtonStyle buttonStyle)
        {
            Action lambda=()=>{
            Button btn = (Button)pnlCustomerList.Controls.Find
                (buttonNamePrefix + buttonText, true)[0];
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
                btn.Name = buttonNamePrefix + buttonText;
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
                BeforeCustomerChanged();
                ActiveCustomerHandler(((Button)sender).Text);
            }
            
        }
 

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
            SearchService();    
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
            PushInternalService((DZService)((Button)sender).Tag);
            //xmpp发送消息 由xmpp实现,在csclient
            //agsXMPP.protocol.client.Message m = new agsXMPP.protocol.client.Message();
            //m.Type = MessageType.chat;
            //DZService service = (DZService)((Button)sender).Tag;
            //string serviceId = service.Id.ToString();
            //string serviceName = service.Name;
            //m.SetAttribute("service_name", service.Name);
            //m.SetAttribute("service_id", serviceId);
            //m.SetAttribute("t", "push");
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

        private void btnPushExternalService_Click(object sender, EventArgs e)
        {
            PushExternalService();
        }



        public string ServiceName
        {
            get
            {
                return tbxServiceName.Text;
            }
            set
            {
                tbxServiceName.Text = value;
            }
        }

        public string ServiceBusinessName
        {
            get
            {
                return tbxServiceBusinessName.Text;
            }
            set
            {
                tbxServiceBusinessName.Text = value;
            }
        }

        public string ServiceDescription
        {
            get
            {
                return tbxServiceDescription.Text;
            }
            set
            {
                tbxServiceDescription.Text = value;
            }
        }

        public string ServiceUnitPrice
        {
            get
            {
                return tbxServiceUnitPrice.Text ;
            }
            set
            {
                tbxServiceUnitPrice.Text = value ;
            }
        }

        public string ServiceUrl
        {
            get
            {
                return tbxServiceUrl.Text;
            }
            set
            {
                tbxServiceUrl.Text = value;
            }
        }
        public string OrderAmount
        {
            get { return tbxAmount.Text ; }
            set { tbxAmount.Text = value; }
        }
        public string TargetAddress
        {
            get { return tbxTargetAddress.Text; }
            set { tbxTargetAddress.Text = value; }
        }
        public string Memo {
            get { return tbxMemo.Text; }
            set { tbxMemo.Text = value; }
        }
        public string ServiceTime {
            get { return   tbxServiceTime.Text  ; }
            set { tbxServiceTime.Text = value ; }
        }
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            CreateOrder();
        }

        private void btnSearchOut_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        
    }
}
