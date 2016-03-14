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
        IList<ReceptionChat> chatList=new List<ReceptionChat>();
        public IList<ReceptionChat> ChatList
        {
            get
            {
                return chatList;
            }

            set
            {
                pnlChatList.Children.Clear();
                foreach (ReceptionChat chat in value)
                {
                    AddOneChat(chat);

                }
            }
        }
        
        public void AddOneChat(ReceptionChat chat)
        {
            Action lamda = () =>
            {
               bool isSender = chat.From.UserName ==currentCustomerService.UserName;
                Label lblTime = new Label();
               
                lblTime.Foreground = new SolidColorBrush(Colors.Black);
                Label lblFrom = new Label();
                Label lblMessage = new Label();
                WrapPanel pnlOneChat = new WrapPanel();

                pnlOneChat.Children.Add(lblFrom);
                pnlOneChat.Children.Add(lblTime);
                pnlOneChat.Children.Add(lblMessage);
                if (isSender) { pnlOneChat.FlowDirection = FlowDirection.RightToLeft; }
                else { pnlOneChat.FlowDirection = FlowDirection.LeftToRight; }
                      lblTime.Content = chat.SavedTime.ToShortTimeString() + " ";

                lblFrom.Content = chat.From.UserName;

                if (chat.MessageBody == null)
                {
                    return;
                }
                LoadBody(chat.MessageBody, pnlOneChat);
 

                //显示多媒体信息.

                if (chat is ReceptionChatMedia)
                {

                    string mediaType = ((ReceptionChatMedia)chat).MediaType;
                    string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                    switch (mediaType)
                    {
                        case "image":

                            Image chatImage = new Image();
                            BitmapImage chatImageBitmap = new BitmapImage();
                            chatImageBitmap.BeginInit();
                           // string filename = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);
                           // string localFile = LocalMediaSaveDir + filename;
                            chatImageBitmap.UriSource = new Uri(mediaUrl);
                            chatImageBitmap.EndInit();
                            chatImage.Source = chatImageBitmap;
                            pnlOneChat.Children.Add(chatImage);
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
                
                //svChatList.ScrollToBottom();
                //svChatList.UpdateLayout();
                //svChatList.ScrollToVerticalOffset(svChatList.ScrollableHeight);
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

        DZMembership currentCustomerService;

        public DZMembership CurrentCustomerService
        {
            get
            {
                return currentCustomerService;
            }

            set
            {
                currentCustomerService = value;
            }
        }

       

        
    }
}
