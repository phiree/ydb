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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dianzhu.CSClient.IVew;
using Dianzhu.Model;
namespace Dianzhu.CSClient.WPFView 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FormMain : Window,IMainFormView
    {
        public FormMain()
        {
            InitializeComponent();
             
        }

        private string currentCustomerName;
        public void LoadOneChat(Dianzhu.Model.ReceptionChat chat)
        {
            bool isSender = chat.From.UserName == currentCustomerName;
            Label lblTime = new Label();
            _AutoSize(lblTime);
            lblTime.ForeColor = Color.FromArgb(200);
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();

            pnlOneChat.Controls.AddRange(new Control[] { lblTime, lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight;
            pnlOneChat.Dock = isSender ? DockStyle.Left : DockStyle.Right;
            lblTime.BorderStyle
            = lblMessage.BorderStyle
            = lblFrom.BorderStyle
            = pnlOneChat.BorderStyle = BorderStyle.FixedSingle;

            _AutoSize(pnlOneChat);

            lblTime.Text = chat.SavedTime.ToShortTimeString() + " ";

            lblFrom.Text = chat.From.UserName;
            lblMessage.Text = chat.MessageBody;
            _AutoSize(lblMessage);
            pnlOneChat.Width = pnlChat.Size.Width - 36;

            //显示多媒体信息.

            if (chat is ReceptionChatMedia)
            {

                string mediaType = ((ReceptionChatMedia)chat).MediaType;
                string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                switch (mediaType)
                {
                    case "image":
                        PictureBox pb = new PictureBox();
                        pb.Click += new EventHandler(pb_Click);
                        string filename = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);
                        string localFile = LocalMediaSaveDir + filename;
                        if (File.Exists(localFile))
                        {
                            pb.ImageLocation = localFile;
                        }
                        else
                        {
                            pb.Load(mediaUrl);
                        }
                        pb.Size = new System.Drawing.Size(100, 100);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pnlOneChat.Controls.Add(pb);
                        break;
                    case "voice":
                        Button btnAudio = new Button();
                        btnAudio.Text = "播放音频---";
                        btnAudio.Tag = mediaUrl;
                        btnAudio.AutoSize = true;
                        btnAudio.Click += BtnAudio_Click;
                        pnlOneChat.Controls.Add(btnAudio);
                        break;
                    case "video":
                        Button btnVideo = new Button();
                        btnVideo.Text = "播放视频";
                        pnlOneChat.Controls.Add(btnVideo);
                        break;
                }
            }
            //bye bye. you are abandoned. 2015-9-2


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

        public void SetCustomerButtonStyle(Dianzhu.Model.DZMembership customer, em_ButtonStyle buttonStyle)
        {
            throw new NotImplementedException();
        }

        public void AddCustomerButtonWithStyle(Dianzhu.Model.DZMembership customer, em_ButtonStyle buttonStyle)
        {
            throw new NotImplementedException();
        }

        public string FormText
        {
            get { return this.Title; }
            set { this.Title = value; }
        }
        public string ErrorMessage
        {
            
            set
            {
                Action lambda = () =>
                {
                    MessageBox.Show(value);
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
            
            }
        }

        public bool IsLoginSuccess
        {
            set
            {
              
                if (!Dispatcher.CheckAccess())
                {
                    Action lambda = () =>
                    {
                        this.DialogResult = value;
                    };
                    Dispatcher.Invoke(lambda);
                }
               // else { lambda(); }
               
            }
        }

        public bool LoginButtonEnabled
        {
            set
            {
                Action lambda = () =>
                {
                    btnLogin.IsEnabled = value;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
               
            }
        }

        public string LoginButtonText
        {
            set
            {
                 Action lambda = () =>
                {
                    btnLogin.Content = value;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
            }
        }

        public string LoginMessage
        {
            set
            {
                Action lambda = () =>
                {
                    lblResult.Content = value;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
              
            }
        }

        public string Password
        {
            get
            {
                return tbxPassword.Password;
            }
        }

        public string UserName
        {
            get
            {
                string returnvalue = string.Empty;
                Action lambda = () =>
                {
                    returnvalue= tbxUserName.Text;
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
                return returnvalue;
            }
        }

        public IList<Dianzhu.Model.ReceptionChat> ChatLog
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

        public string ButtonNamePrefix
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

        public string LocalMediaSaveDir
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

        public string CurrentCustomerName
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

        public IList<Dianzhu.Model.DZService> SearchedService
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

        public string ServiceName
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

        public string ServiceBusinessName
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

        public string ServiceDescription
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

        public string ServiceUnitPrice
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

        public string ServiceTime
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

        public string TargetAddress
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

        public string OrderAmount
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

        public string Memo
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

        public Stream SelectedImageStream
        {
            get
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

        public event ViewLogin ViewLogin;
        public event SendMessageHandler SendMessageHandler;
        public event SendImageHandler SendImageHandler;
        public event PlayAudio PlayAudio;
        public event ActiveCustomerHandler ActiveCustomerHandler;
        public event PushExternalService PushExternalService;
        public event PushInternalService PushInternalService;
        public event SearchService SearchService;
        public event SendPayLink SendPayLink;
        public event CreateOrder CreateOrder;
        public event ViewClosed ViewClosed;
        public event BeforeCustomerChanged BeforeCustomerChanged;
    }
}
