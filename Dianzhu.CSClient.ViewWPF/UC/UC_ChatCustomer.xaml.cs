﻿using System;
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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatCustomer.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatCustomer : UserControl
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_ChatCustomer");

        public UC_ChatCustomer(ReceptionChat chat,DZMembership currentCS)
        {
            InitializeComponent();

            if(chat.From.UserType== Model.Enums.enum_UserType.customer)
            {
                imgAvatarCustomer.Source = InitAvatar(chat.From.AvatarUrl);
                wpnlChat.HorizontalAlignment = HorizontalAlignment.Left;
                tbNameCustomer.Text = chat.From.DisplayName;
                tbTimeCustomer.Text = chat.SavedTime.ToString();
            }
            else
            {
                imgAvatarCS.Source = InitAvatar(chat.From.AvatarUrl);
                wpnlChat.HorizontalAlignment = HorizontalAlignment.Right;
                tbNameCS.Text = chat.From.DisplayName;
                tbTimeCS.Text = chat.SavedTime.ToString();
                if (chat.From.Id== currentCS.Id)
                {
                    lblChatBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFb3d465"));
                }
            }
            
            InitData(chat);
        }

        private ImageSource InitAvatar(string avatar)
        {
            string uriAvatar = avatar;

            if (uriAvatar != null)
            {
                uriAvatar = uriAvatar.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), "");
                uriAvatar = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + uriAvatar;
            }
            else
            {
                uriAvatar = "pack://siteoforigin:,,,/Resources/logourl.png";
            }

            return new BitmapImage(new Uri(uriAvatar, UriKind.Absolute));
        }

        private void InitData(ReceptionChat chat)
        {
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

                        //MediaElement chatImageGif = new MediaElement();
                        //chatImageGif.Name = chat.MessageBody;
                        //chatImageGif.Width = 300;
                        //chatImageGif.MaxHeight = 300;
                        //chatImageGif.LoadedBehavior = MediaState.Play;
                        //chatImageGif.Source = new Uri(mediaUrl);
                        //chatImageGif.MediaEnded += ChatImageGif_MediaEnded;

                        UC_ChatImageNoraml chatImageGif = new UC_ChatImageNoraml(new Uri(mediaUrl));
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
                tbChat.Padding = new Thickness(5);
                tbChat.Text = chat.MessageBody;
            }
        }

        MediaPlayer player = new MediaPlayer();
        bool isPlay = false;
        string fileName = string.Empty;
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