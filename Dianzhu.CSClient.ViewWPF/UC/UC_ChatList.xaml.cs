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
using System.Windows.Interop;
using System.Windows.Controls.Primitives;
using System.IO;

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

            ((StackPanel)svChatList.FindName("StackPanel")).SizeChanged += UC_ChatList_SizeChanged;
        }

        private void UC_ChatList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewer sc = ((ScrollViewer)svChatList.FindName("ScrollViewer"));
            if (sc.ScrollableHeight == sc.VerticalOffset)
            {
                sc.ScrollToEnd();
            }
        }

        IList<ReceptionChat> chatList = new List<ReceptionChat>();
        public IList<ReceptionChat> ChatList
        {
            get
            {
                return chatList;
            }

            set
            {
                Action lambda = () =>
                {
                    chatList = value;
                    ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();

                    if (chatList == null)
                    {
                        chatList = new List<ReceptionChat>();
                        return;
                    }
                    
                    //pnlChatList.Children.Clear();
                    
                    if (chatList.Count > 0)
                    {
                        Button btn = new Button();
                        btn.Content = "查看更多";
                        btn.Click += Btn_Click;

                        ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(btn);//加载更多按钮

                        foreach (ReceptionChat chat in chatList)
                        {
                            AddOneChat(chat);
                        }
                    }
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
            }
        }

        public void ShowNoMoreLabel()
        {
            Label lbl = new Label();
            lbl.Content = "——没有更多消息了——";
            lbl.HorizontalAlignment = HorizontalAlignment.Center;

            ((StackPanel)svChatList.FindName("StackPanel")).Children.RemoveAt(0);
            ((StackPanel)svChatList.FindName("StackPanel")).Children.Insert(0, lbl);
        }


        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            BtnMoreChat();
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
 

                //显示多媒体信息.
                if (chat is ReceptionChatMedia)
                {

                    string mediaType = ((ReceptionChatMedia)chat).MediaType;
                    string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                    if (mediaUrl.IndexOf(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl")) < 0)
                    {
                        mediaUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + mediaUrl;
                    }
                    switch (mediaType)
                    {
                        case "image":

                            //Image chatImage = new Image();
                            //BitmapImage chatImageBitmap = new BitmapImage();
                            //chatImageBitmap.BeginInit();
                            ////string filename = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);
                            ////string localFile = LocalMediaSaveDir + filename;
                            //chatImageBitmap.UriSource = new Uri(mediaUrl);
                            //chatImageBitmap.EndInit();
                            //chatImage.Source = chatImageBitmap;
                            //pnlOneChat.Children.Add(chatImage);

                            MediaElement chatImageGif = new MediaElement();
                            chatImageGif.Name = chat.MessageBody;
                            chatImageGif.Width = 300;
                            chatImageGif.MaxHeight = 300;
                            chatImageGif.LoadedBehavior = MediaState.Play;
                            chatImageGif.Source = new Uri(mediaUrl);
                            chatImageGif.MediaEnded += ChatImageGif_MediaEnded;
                            pnlOneChat.Children.Add(chatImageGif);

                            break;
                        case "voice":
                             Button btnAudio = new Button();
                            btnAudio.Content = "播放音频---";
                            //btnAudio.Tag = mediaUrl;
                            //string fileName = ((Model.ReceptionChatMedia)chat).MedialUrl.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), "");
                            //btnAudio.Name = ((Model.ReceptionChatMedia)chat).MedialUrl;

                            //string targetFileName = Environment.CurrentDirectory + "message/media/" + fileName + ".mp3";

                            btnAudio.Tag = chat;
                            btnAudio.Click += BtnAudio_Click;
                            pnlOneChat.Children.Add(btnAudio);
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
                else if(chat is ReceptionChatPushService)
                {
                    UC_PushService pushService = new UC_PushService();
                    pushService.LoadData(((ReceptionChatPushService)chat).PushedServices[0]);
                    pushService.FlowDirection = FlowDirection.LeftToRight;
                    chat.MessageBody = string.Empty;
                    pnlOneChat.Children.Add(pushService);
                }


                if (chat.MessageBody == null)
                {
                    return;
                }
                LoadBody(chat.MessageBody, pnlOneChat);


                //bye bye. you are abandoned. 2015-9-2

                //对当前窗体已存在控件的操作

                // WindowNotification();
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(pnlOneChat);
                //pnlChatList.Children.Add(pnlOneChat);

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

        private void ChatImageGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }

        MediaPlayer player = new MediaPlayer();
        bool isPlay = false;
        string fileName = string.Empty;
        private void BtnAudio_Click(object sender, EventArgs e)
        {
            //if(AudioPlay!=null)
            //{
            //    Window w = Window.GetWindow(this);
            //    IntPtr windowHandle = new WindowInteropHelper(w).Handle;
            //    AudioPlay(((Button)sender).Tag, windowHandle);
            //}

            ReceptionChatMedia chat = (Model.ReceptionChatMedia)(((Button)sender).Tag);
            string chatFlieName= chat.MedialUrl.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), "");
            chatFlieName += ".mp3";

            if (string.IsNullOrEmpty(fileName) || fileName != chatFlieName)
            {
                fileName = chatFlieName;
                isPlay = true;
            }

            player.Open(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + fileName));
            player.MediaEnded += Player_MediaEnded;

            if (isPlay)
            {
                player.Play();
                isPlay = false;
            }
            else
            {
                player.Pause();
                isPlay = true;
            }
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            fileName = string.Empty;
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

        public event AudioPlay AudioPlay;
        public event BtnMoreChat BtnMoreChat;

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
