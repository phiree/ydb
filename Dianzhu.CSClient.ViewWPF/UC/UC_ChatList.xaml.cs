using System;
using System.Collections.Generic;
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
using Dianzhu.CSClient.IView;
using Dianzhu.Model;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// ChatList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatList : UserControl,IView.IViewChatList
    {
        public UC_ChatList()
        {
            InitializeComponent();
        }

        public IList<ReceptionChat> ChatList
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                foreach (ReceptionChat chat in value)
                {
                    Label lbl = new Label { Content=chat.MessageBody };

                }
            }
        }
        public void AddOneChat(ReceptionChat chat)
        {
            Action lamda = () =>
            {
                bool isSender = false;// chat.From.UserName == currentCustomerService;
                Label lblTime = new Label();
                //_AutoSize(lblTime);
                lblTime.Foreground = new SolidColorBrush(Colors.Black);
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

                // WindowNotification();
                pnlChatList.Children.Add(pnlOneChat);
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

        public string MessageText
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

        public event SendTextClick SendTextClick;

        
    }
}
