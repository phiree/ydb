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
                pushService.LoadData(((ReceptionChatPushService)chat).ServiceInfos[0]);
                pushService.FlowDirection = FlowDirection.LeftToRight;
                 
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

                //不是客服发出的消息
                if (!chat.IsfromCustomerService)
                {
                    wpnlChat.HorizontalAlignment = HorizontalAlignment.Left;
                    //todo chatrefactor
                    tbNameCustomer.Text = chat.FromId;
                    tbTimeCustomer.Text = chat.SavedTime.ToString();
                }
                else
                {
                    imgAvatarCS.Source = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png"));
                    wpnlChat.HorizontalAlignment = HorizontalAlignment.Right;
                    tbNameCS.Text = chat.FromId;
                    tbTimeCS.Text = chat.SavedTime.ToString();
                    if (chat.FromId == currentCS.Id.ToString())
                    {
                        lblChatBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFb3d465"));
                    }
                }

                InitData(chat);
            }
        }

        public string CustomerAvatar
        {
            set
            {
                if(chat.FromResource== Model.Enums.enum_XmppResource.YDBan_User)
                {
                    BitmapImage image;
                    try
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            //image = new BitmapImage(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + value + "_32X32"));

                            string imgName = value;
                            string imgPath = PHSuit.LocalFileManagement.LocalFilePath + value + "_32X32";

                            string imgUil_32X32 = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + value + "_32X32";
                            string imgName_32X32 = value + "_32X32";

                            bool isExist = false;

                            if (!File.Exists(imgPath))
                            {
                                if (PHSuit.LocalFileManagement.DownLoad(string.Empty, imgUil_32X32, imgName_32X32))
                                {
                                    isExist = true;
                                }
                                else
                                {
                                    log.Error("图片下载失败，地址：" + imgUil_32X32);
                                }
                            }
                            else
                            {
                                isExist = true;
                            }

                            if (isExist)
                            {
                                using (BinaryReader loader = new BinaryReader(File.Open(imgPath, FileMode.Open)))
                                {
                                    FileInfo fd = new FileInfo(imgPath);
                                    int Length = (int)fd.Length;
                                    byte[] buf = new byte[Length];
                                    buf = loader.ReadBytes((int)fd.Length);
                                    loader.Dispose();
                                    loader.Close();


                                    //开始加载图像  
                                    BitmapImage bim = new BitmapImage();
                                    bim.BeginInit();
                                    bim.StreamSource = new MemoryStream(buf);
                                    bim.EndInit();
                                    image = bim;
                                    //image1.Source = bim;
                                    GC.Collect(); //强制回收资源  
                                }
                            }
                            else
                            {
                                image = new BitmapImage(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + value + "_32X32"));//本地文件读取失败，读取服务器图片
                            }                            
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
