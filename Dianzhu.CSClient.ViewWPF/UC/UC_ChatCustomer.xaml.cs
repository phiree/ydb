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
using System.ComponentModel;
using System.IO;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatCustomer.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatCustomer : UserControl, IViewChatCustomer
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_ChatCustomer");

        public UC_ChatCustomer()
        {
            InitializeComponent();
        }

        protected void InitData(VMChat vmChat)
        {
            if(vmChat is VMChatText)
            {
                tbxChat.Visibility = Visibility.Visible;
                tbxChat.Text = ((VMChatText)vmChat).MessageBody;
            }
            else if(vmChat is VMChatMedia)
            {
                string mediaType = ((VMChatMedia)vmChat).MediaType;
                string mediaUrl = ((VMChatMedia)vmChat).MedialUrl;
                switch (mediaType)
                {
                    case "image":
                        //MediaElement chatImageGif = new MediaElement();
                        //chatImageGif.Name = chat.MessageBody;
                        //chatImageGif.Width = 300;
                        //chatImageGif.MaxHeight = 300;
                        //chatImageGif.LoadedBehavior = MediaState.Play;
                        //chatImageGif.Source = new Uri(mediaUrl);
                        //chatImageGif.MediaEnded += ChatImageGif_MediaEnded;

                        UC_ChatImageNoraml chatImageGif = new UC_ChatImageNoraml(mediaUrl);
                        string chatId = PHSuit.StringHelper.SafeNameForWpfControl(vmChat.ChatId,GlobalVariable.PRE_CHAT_CUSTOMER);
                        if ((UC_ChatImageNoraml)this.FindName(chatId) == null)
                        {
                            this.RegisterName(chatId, chatImageGif);
                        }
                        wpnlChat.Children.Add(chatImageGif);
                        break;
                    case "voice":
                        Button btnAudio = new Button();
                        btnAudio.Content = "播放音频---";

                        btnAudio.Tag = vmChat;
                        btnAudio.Click += BtnAudio_Click;
                        wpnlChat.Children.Add(btnAudio);
                        break;
                    case "video":
                        break;
                    case "url":
                        break;
                    default:
                        log.Warn("该多媒体类型暂时不支持");
                        break;
                }
            }
            else if(vmChat is VMChatPushServie)
            {
                if (string.IsNullOrEmpty(((VMChatPushServie)vmChat).ServiceName))
                {
                    log.Error("服务名不存在，不显示该推送服务");
                    return;
                }

                UC_PushService pushService = new UC_PushService();
                pushService.LoadData((VMChatPushServie)vmChat);
                pushService.FlowDirection = FlowDirection.LeftToRight;

                wpnlChat.Children.Add(pushService);
            }
        }

        MediaPlayer player = new MediaPlayer();
        bool isPlay = false;
        string fileName = string.Empty;

        Guid currentCSId;
        public Guid CurrentCSId
        {
            set
            {
                currentCSId = value;
            }
        }

        VMChat vmChat;
        public VMChat VMChat
        {
            set
            {
                vmChat = value;

                if (vmChat.IsFromCS)
                {
                    imgAvatarCS.Source = new BitmapImage(new Uri(vmChat.CSAvatar));
                    wpnlChat.HorizontalAlignment = HorizontalAlignment.Right;
                    tbNameCS.Text = vmChat.FromName;
                    tbTimeCS.Text = vmChat.SavedTime.ToString();
                    if (vmChat.FromId == currentCSId.ToString())
                    {
                        lblChatBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(vmChat.ChatBackground));
                    }
                }
                else
                {
                    imgAvatarCustomer.Source = SetCustomerAvatar(vmChat.CustomerAvatar);
                    wpnlChat.HorizontalAlignment = HorizontalAlignment.Left;
                    tbNameCustomer.Text = vmChat.FromName;
                    tbTimeCustomer.Text = vmChat.SavedTime.ToString();
                }

                try
                {
                    InitData(vmChat);
                }
                catch (Exception ee)
                {
                    log.Error(ee);
                }
            }
        }
        
        public BitmapImage SetCustomerAvatar(string imageName)
        {
            BitmapImage image;

            try
            {
                if (!string.IsNullOrEmpty(imageName))
                {
                    //image = new BitmapImage(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + imageName + "_32X32"));

                    string imgName = imageName;
                    string imgPath = PHSuit.LocalFileManagement.LocalFilePath + imageName + "_32X32";

                    string imgUil_32X32 = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + imageName + "_32X32";
                    string imgName_32X32 = imageName + "_32X32";

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
                        image = new BitmapImage(new Uri(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + imageName + "_32X32"));//本地文件读取失败，读取服务器图片
                    }
                }
                else
                {
                    image = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCustomer.png"));
                }
            }
            catch (Exception e)
            {
                log.Error("用户头像读取失败.错误：" + e);
                image = new BitmapImage(new Uri("pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCustomer.png"));
            }

            return image;
        }
        
        private void BtnAudio_Click(object sender, EventArgs e)
        {
            //ReceptionChatMedia chat = (Model.ReceptionChatMedia)(((Button)sender).Tag);
            VMChatMedia vmChatMedia = (VMChatMedia)(((Button)sender).Tag);
            string chatFlieName = vmChatMedia.MedialUrl.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), "");
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
