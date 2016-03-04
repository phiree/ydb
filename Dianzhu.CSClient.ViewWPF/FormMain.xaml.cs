using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// FormMain.xaml 的交互逻辑
    /// </summary>
    public partial class FormMain : Window, IMainFormView
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public string FormText
        {
            get { return this.Title; }
            set { this.Title = value; }
        }

        #region Impletion view event

        public event BeforeCustomerChanged BeforeCustomerChanged;
        public event CreateNewOrder CreateNewOrder;
        public event CreateOrder CreateOrder;
        public event IdentityItemActived IdentityItemActived;
        public event MessageSentAndNew MessageSentAndNew;
        public event NoticeCustomerService NoticeCustomerService;
        public event NoticeOrder NoticeOrder;
        public event NoticePromote NoticePromote;
        public event NoticeSystem NoticeSystem;
        public event OrderStateChanged OrderStateChanged;
        public event AudioPlay PlayAudio;
        public event PushExternalService PushExternalService;
        public event PushInternalService PushInternalService;
        public event ReAssign ReAssign;
        public event SaveReAssign SaveReAssign;
        public event SearchService SearchService;
        public event SelectService SelectService;
        public event MediaMessageSent SendMediaHandler;
        public event MessageSent SendMessageHandler;
        public event SendPayLink SendPayLink;
        public event ViewClosed ViewClosed;

        #endregion

        #region 属性

        string buttonNamePrefix;
        public string ButtonNamePrefix
        {
            get { return buttonNamePrefix; }
            set { buttonNamePrefix = value; }
        }

        /// <summary>
        /// 界面当前绑定的客户名称,用于判断聊天记录的呈现方式
        /// </summary>
        private string currentCustomerService;
        public string CurrentCustomerService
        {
            set { currentCustomerService = value; }
        }

        /// <summary>
        /// 当前选择的服务
        /// </summary>
        private DZService currentService;
        public DZService CurrentService
        {
            get { return currentService; }
            set { currentService = value; }
        }

        public string currentServiceId;
        public string CurrentServiceId
        {
            get { return currentServiceId; }
            set { currentServiceId = value; }
        }

        private string localMediaSaveDir;
        public string LocalMediaSaveDir
        {
            get
            { return localMediaSaveDir; }
            set { localMediaSaveDir = value; }
        }

        #endregion

        #region 控件属性

        public string OrderNumber
        {

            get { return lblOrderNumber.Content.ToString(); }
            set { lblOrderNumber.Content = value; }
        }

        public string OrderStatus
        {
            get
            { return lblOrderStatus.Content.ToString(); }
            set { lblOrderStatus.Content = value; }
        }

        public string ServiceName
        {
            get { return tbxServiceName.Text; }
            set { tbxServiceName.Text = value; }
        }

        public string ServiceBusinessName
        {
            get { return tbxServiceBusinessName.Text; }
            set { tbxServiceBusinessName.Text = value; }
        }

        public string ServiceDescription
        {
            get { return tbxServiceDescription.Text; }
            set { tbxServiceDescription.Text = value; }
        }

        public string ServiceUnitPrice
        {
            get { return tbxServiceUnitPrice.Text; }
            set { tbxServiceUnitPrice.Text = value; }
        }

        public string ServiceDepositAmount
        {
            get { return tbxDepositAmount.Text; }
            set { tbxDepositAmount.Text = value; }
        }

        public string ServiceTime
        {
            get { return tbxServiceTime.Text; }
            set { tbxServiceTime.Text = value; }
        }

        public string TargetAddress
        {
            get { return tbxTargetAddress.Text; }
            set { tbxTargetAddress.Text = value; }
        }

        public string OrderAmount
        {
            get { return tbxAmount.Text; }
            set { tbxAmount.Text = value; }
        }

        public string Memo
        {
            get { return tbxMemo.Text; }
            set { tbxMemo.Text = value; }
        }

        public int OverTimeForCancel
        {
            get { return string.IsNullOrEmpty(tbxOvertimeForCancel.Text) ? 0 : Convert.ToInt32(tbxOvertimeForCancel.Text); }
            set { tbxOvertimeForCancel.Text = value.ToString(); }
        }

        public decimal CancelCompensation
        {
            get { return string.IsNullOrEmpty(tbxCancelCompensation.Text) ? 0 : Convert.ToDecimal(tbxCancelCompensation.Text); }
            set { tbxCancelCompensation.Text = value.ToString(); }
        }

        public string SerachKeyword
        {
            get { return tbxKeywords.Text; }
            set { tbxKeywords.Text = value; }
        }

        bool canEditor = true;
        public bool CanEditOrder
        {
            get { return canEditor; }
            set { pnlOrder.IsEnabled = value; }
        }

        IList<ReceptionChat> chatLog;
        public IList<ReceptionChat> ChatLog
        {
            set
            {
                Action lamda = () =>
                {
                    pnlChat.Children.Clear();
                    foreach (ReceptionChat chat in value)
                    {
                        LoadOneChat(chat);
                    }
                    chatLog = value;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lamda);
                }
                else
                {
                    lamda();
                }
            }
            get
            {
                return chatLog;
            }
        }
        
        
        public string MessageTextBox
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
                
        public IList<ServiceOrder> OrdersList
        {
            set
            {
                throw new NotImplementedException();
            }
        }
        
        public IList<DZMembership> ReceptionCustomerList
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<DZMembership> RecptingCustomList
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDictionary<DZMembership, string> RecptingCustomServiceList
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

        public IList<DZService> SearchedService
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

        public string SelectedImageName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Stream SelectedImageStream
        {
            get
            {
                throw new NotImplementedException();
            }
        }
                
        public string ServiceUrl
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

        decimal IMainFormView.OrderAmount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public decimal OrderDepositAmount
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

        public ServiceOrder CurrentOrder
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

        #endregion

        #region 事件

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder();
        }

        private void btnCreateNewDraft_Click(object sender, RoutedEventArgs e)
        {
            if (CreateNewOrder != null)
            {
                CreateNewOrder();
            }
        }

        private void btnReAssign_Click(object sender, RoutedEventArgs e)
        {
            ReAssign();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchService();
        }

        public void SetCustomerButtonStyle(DZMembership dm, em_ButtonStyle buttonStyle)
        {
            Action lambda = () => {
                if ((bool)pnlCustomerList.FindName(buttonNamePrefix + dm.Id))
                {
                    Button btn = (Button)pnlCustomerList.FindName(buttonNamePrefix + dm.Id);
                    Color foreColor = Colors.White;
                    switch (buttonStyle)
                    {
                        case em_ButtonStyle.Login:
                            foreColor = Colors.Green;
                            break;
                        case em_ButtonStyle.LogOff:
                            foreColor = Colors.Gray;
                            break;
                        case em_ButtonStyle.Readed: foreColor = Colors.Black; break;
                        case em_ButtonStyle.Unread: foreColor = Colors.Red; break;
                        case em_ButtonStyle.Actived: foreColor = Colors.Yellow; break;
                        default: break;
                    }
                    btn.Foreground = new SolidColorBrush(foreColor);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }

        public void AddCustomerButtonWithStyle(DZMembership dm, em_ButtonStyle buttonStyle)
        {
            Action lamda = () =>
            {
                if (!(bool)pnlCustomerList.FindName(buttonNamePrefix + dm.Id))
                {
                    Button btn = new Button();
                    btn.Content = dm.DisplayName + dm.Id;
                    btn.Tag = dm;
                    btn.Name = buttonNamePrefix + dm.Id;
                    btn.Click += btnCustomer_Click;

                    pnlCustomerList.Children.Add(btn);
                    SetCustomerButtonStyle(dm, buttonStyle);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        void btnCustomer_Click(object sender, EventArgs e)
        {
            if (IdentityItemActived != null)
            {
                DZMembership custom = (DZMembership)((Button)sender).Tag;
                Button btn = (Button)pnlCustomerList.FindName(buttonNamePrefix + custom.Id);
                if (btn.Foreground == new  SolidColorBrush(Colors.Gray))
                {
                    MessageBox.Show("该用户已下线！");
                    RemoveCustomBtn(custom.Id.ToString());
                    return;
                }
                BeforeCustomerChanged();
                //IdentityItemActived((ServiceOrder)((Button)sender).Tag);
                IdentityItemActived((DZMembership)((Button)sender).Tag);
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (SendMessageHandler != null)
            {
                SendMessageHandler();
            }
        }

        public void LoadOneChat(ReceptionChat chat)
        {
            bool isSender = chat.From.UserName == currentCustomerService;
            Label lblTime = new Label();
            //_AutoSize(lblTime);
            lblTime.Foreground =new SolidColorBrush( Colors.Black);
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            WrapPanel pnlOneChat = new WrapPanel();

            pnlOneChat.Children.Add(lblFrom);
            pnlOneChat.Children.Add(lblTime);
            pnlOneChat.Children.Add(lblMessage);
            if (isSender) { pnlOneChat.FlowDirection = FlowDirection.RightToLeft; }
            else { pnlOneChat.FlowDirection = FlowDirection.LeftToRight; }
            //pnlOneChat.Dock = isSender ? DockStyle.Left : DockStyle.Right;
            //lblTime.BorderStyle            = lblMessage.BorderStyle            = lblFrom.BorderStyle            = pnlOneChat.BorderStyle = BorderStyle.FixedSingle;

            //_AutoSize(pnlOneChat);

            lblTime.Content = chat.SavedTime.ToShortTimeString() + " ";

            lblFrom.Content = chat.From.UserName;

            if (chat.MessageBody == null)
            {
                return;
            }
            LoadBody(chat.MessageBody, pnlOneChat);

            //lblMessage.Text = chat.MessageBody;

            //如果包含了url信息
            //_AutoSize(lblMessage);
            //pnlOneChat.Width = pnlChat.Size.Width - 36;

            //显示多媒体信息.

            if (chat is ReceptionChatMedia)
            {

                string mediaType = ((ReceptionChatMedia)chat).MediaType;
                string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                switch (mediaType)
                {
                    case "image":
                        //PictureBox pb = new PictureBox();
                        //pb.Click += new EventHandler(pb_Click);
                        //string filename = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);
                        //string localFile = LocalMediaSaveDir + filename;
                        //if (File.Exists(localFile))
                        //{
                        //    pb.ImageLocation = localFile;
                        //}
                        //else {
                        //    pb.Load(mediaUrl);
                        //}
                        //pb.Size = new System.Drawing.Size(100, 100);
                        //pb.SizeMode = PictureBoxSizeMode.Zoom;
                        //pnlOneChat.Controls.Add(pb);
                        break;
                    case "voice":
                        //Button btnAudio = new Button();
                        //btnAudio.Text = "播放音频---";
                        //btnAudio.Tag = mediaUrl;
                        //btnAudio.AutoSize = true;
                        //btnAudio.Click += BtnAudio_Click;
                        //pnlOneChat.Controls.Add(btnAudio);
                        break;
                    case "video":
                        //Button btnVideo = new Button();
                        //btnVideo.Text = "播放视频";
                        //pnlOneChat.Controls.Add(btnVideo);
                        break;
                    case "url":
                        //LinkLabel ll = new LinkLabel { Text = chat.MessageBody };
                        //ll.Links.Add(0, ll.Text.Length, mediaUrl);
                        //ll.LinkClicked += Ll_LinkClicked;
                        //pnlOneChat.Controls.Add(ll);
                        break;
                }
            }
            //bye bye. you are abandoned. 2015-9-2

            //对当前窗体已存在控件的操作
            Action lamda = () =>
            {
                // WindowNotification();
                pnlChat.Children.Add(pnlOneChat);
                //pnlChat.ScrollControlIntoView(pnlOneChat);
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        //private void _AutoSize(Control c)
        //{
        //    c.Size = new Size(0, 0);
        //    if (c is Label)
        //    {
        //        ((Label)c).AutoSize = true;
        //    }
        //    if (c is Panel)
        //    {
        //        ((Panel)c).AutoSize = true;
        //    }
        //}

        private void LoadBody(string messageBody, Panel pnlContainer)
        {
            bool containsUrls;
            IList<string> urls = PHSuit.StringHelper.ParseUrl(messageBody, out containsUrls);
            if (!containsUrls)
            {
                Label lblPlainText = new Label();
                lblPlainText.Content = messageBody;
                //_AutoSize(lblPlainText);
                pnlContainer.Children.Add(lblPlainText);
            }
            else
            {
                //LinkLabel lb = new LinkLabel();
                //lb.Text = messageBody;
                //foreach (string s in urls)
                //{
                //    int startIndex = messageBody.IndexOf(s);
                //    int endIndex = startIndex + s.Length;
                //    lb.Links.Add(startIndex, s.Length, s);
                //    lb.LinkClicked += Ll_LinkClicked;
                //}
                //_AutoSize(lb);
                //pnlContainer.Controls.Add(lb);
                throw new NotImplementedException();
            }
        }

        public void RemoveCustomBtn(string cid)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomBtnAndClear(string cid)
        {
            throw new NotImplementedException();
        }

        

        public void ShowMsg(string msg)
        {
            throw new NotImplementedException();
        }

        public void ShowNotice(string noticeContent)
        {
            throw new NotImplementedException();
        }

        public void ShowStreamError(string streamError)
        {
            throw new NotImplementedException();
        }

        public void WindowNotification()
        {
            throw new NotImplementedException();
        }


        #endregion

        
    }
}
