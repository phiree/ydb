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
using Dianzhu.Model;
using System.ComponentModel;
using System.IO;
using Dianzhu.CSClient.IView;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatCustomer.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatCustomer : UserControl, IViewChatCustomer
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_ChatCustomer");
        BackgroundWorker worker;
        public UC_ChatCustomer()
        {
            InitializeComponent();
        }

        private void InitData(ReceptionChat chat)
        {
            if (chat is ReceptionChatMedia)
            {
                string mediaType = ((ReceptionChatMedia)chat).MediaType;
                string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                //if (mediaUrl.IndexOf(PHSuit.DownloadSoft.DownloadPath) < 0)
                //{
                //    mediaUrl = PHSuit.DownloadSoft.DownloadPath + mediaUrl;
                //}
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

                        //MediaElement chatImageGif = new MediaElement();
                        //chatImageGif.Name = chat.MessageBody;
                        //chatImageGif.Width = 300;
                        //chatImageGif.MaxHeight = 300;
                        //chatImageGif.LoadedBehavior = MediaState.Play;
                        //chatImageGif.Source = new Uri(mediaUrl);
                        //chatImageGif.MediaEnded += ChatImageGif_MediaEnded;

                        UC_ChatImageNoraml chatImageGif = new UC_ChatImageNoraml(mediaUrl);
                        string chatId = PHSuit.StringHelper.SafeNameForWpfControl(chat.Id.ToString());
                        if ((UC_ChatImageNoraml)this.FindName(chatId) == null)
                        {
                            this.RegisterName(chatId, chatImageGif);
                        }
                        wpnlChat.Children.Add(chatImageGif);
                        break;
                    case "voice":
                        Button btnAudio = new Button();
                        btnAudio.Content = "播放音频---";

                        btnAudio.Tag = chat;
                        btnAudio.Click += BtnAudio_Click;
                        wpnlChat.Children.Add(btnAudio);
                        break;
                    case "video":
                        break;
                    case "url":
                        //LinkLabel ll = new LinkLabel { Text = chat.MessageBody };
                        //ll.Links.Add(0, ll.Text.Length, mediaUrl);
                        //ll.LinkClicked += Ll_LinkClicked;
                        //pnlOneChat.Controls.Add(ll);
                        break;
                    default:
                        log.Warn("该多媒体类型暂时不支持");
                        break;
                }
            }
            else if (chat is ReceptionChatPushService)
            {
                UC_PushService pushService = new UC_PushService();
                pushService.LoadData(((ReceptionChatPushService)chat).PushedServices[0]);
                pushService.FlowDirection = FlowDirection.LeftToRight;
                chat.MessageBody = string.Empty;
                wpnlChat.Children.Add(pushService);
            }
            else
            {
                tbxChat.Visibility = Visibility.Visible;
                tbxChat.Text = chat.MessageBody;
            }
        }

        MediaPlayer player = new MediaPlayer();
        bool isPlay = false;
        string fileName = string.Empty;

        DZMembership currentCS;
        public DZMembership CurrentCS
        {
            set
            {
                currentCS = value;
            }
        }

        ReceptionChat chat;
        public ReceptionChat Chat
        {
            set
            {
                chat = value;

                if (chat.From.UserType == Model.Enums.enum_UserType.customer)
                {
                    wpnlChat.HorizontalAlignment = HorizontalAlignment.Left;
                    tbNameCustomer.Text = chat.From.DisplayName;
                    tbTimeCustomer.Text = chat.SavedTime.ToString();

                    BitmapImage image;
                    try
                    {
                        if (!string.IsNullOrEmpty(chat.From.AvatarUrl))
                        {
                            image = new BitmapImage(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + chat.From.AvatarUrl + "_32X32"));
                        }
                        else
                        {
                            image = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCustomer.png"));
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e.Message);
                        image = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCustomer.png"));
                    }
                    imgAvatarCustomer.Source = image;
                }
                else
                {
                    imgAvatarCS.Source = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png"));
                    wpnlChat.HorizontalAlignment = HorizontalAlignment.Right;
                    tbNameCS.Text = chat.From.DisplayName;
                    tbTimeCS.Text = chat.SavedTime.ToString();
                    if (chat.From.Id == currentCS.Id)
                    {
                        lblChatBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFb3d465"));
                    }
                }

                InitData(chat);
            }
        }

        private void BtnAudio_Click(object sender, EventArgs e)
        {
            ReceptionChatMedia chat = (Model.ReceptionChatMedia)(((Button)sender).Tag);
            string chatFlieName = chat.MedialUrl.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), "");
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
    }
}
