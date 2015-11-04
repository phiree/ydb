using Dianzhu.BLL;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.CSClient.IVew;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
 
namespace Dianzhu.CSClient.Presenter
{
    public partial class MainPresenter
    {
        IVew.IMainFormView view;
        InstantMessage instantMessage;
        DZMembershipProvider bllMember;
        BLLDZService bllService;
        BLLReception bllReception;
        BLLServiceOrder bllOrder;
        BLLReceptionStatus bllReceptionStatus;
        BLLReceptionChat bllReceptionChat = new BLLReceptionChat();
       
        public MainPresenter(IVew.IMainFormView view,
            InstantMessage instantMessage,
            IMessageAdapter.IAdapter messageAdapter
            )
        {
            this.view = view;
            this.instantMessage = instantMessage;
            this.bllMember = new DZMembershipProvider();// bllMember;
            this.bllReception = new BLLReception(); //bllReception;
            this.bllService = new BLLDZService();// bllService;
            this.bllReceptionStatus = new BLLReceptionStatus();// bllReceptionStatus;
           
            this.bllOrder = new BLLServiceOrder();// bllOrder;

            //  IM的委托
            this.instantMessage.IMPresent += new IMPresent(IMPresent);
            this.instantMessage.IMReceivedMessage += new IMReceivedMessage(IMReceivedMessage);
            this.instantMessage.IMClosed += new IMClosed(instantMessage_IMClosed);
            this.instantMessage.IMIQ += InstantMessage_IMIQ;
            this.instantMessage.IMStreamError += InstantMessage_IMStreamError;
            //iview的委托
            this.view.SendMessageHandler += new MessageSent(view_SendMessageHandler);
            this.view.SendMediaHandler += new MediaMessageSent(view_SendMediaHandler);

            this.view.BeforeCustomerChanged += new BeforeCustomerChanged(view_BeforeCustomerChanged);
            this.view.IdentityItemActived += new IVew.IdentityItemActived(ActiveCustomer);
            this.view.IdentityItemActived += new IVew.IdentityItemActived(LoadChatHistory);
            this.view.IdentityItemActived += new IdentityItemActived(LoadCurrentOrder);

            this.view.ButtonNamePrefix = System.Configuration.ConfigurationManager.AppSettings["ButtonNamePrefix"];
            this.view.SearchService += new IVew.SearchService(view_SearchService);
            this.view.SendPayLink += new IVew.SendPayLink(view_SendPayLink);
            this.view.CreateOrder += new CreateOrder(view_CreateOrder);
            this.view.ViewClosed += new ViewClosed(view_ViewClosed);
            this.view.OrderStateChanged += View_OrderStateChanged;

            this.view.PlayAudio += View_PlayAudio;
            this.view.LocalMediaSaveDir = GlobalViables.LocalMediaSaveDir;
            this.view.CreateNewOrder += View_CreateNewOrder;

            this.view.NoticeCustomerService += View_NoticeCustomerService;
            this.view.NoticeOrder += View_NoticeOrder;
            this.view.NoticePromote += View_NoticePromote;
            this.view.NoticeSystem += View_NoticeSystem;

            this.view.ReAssign += View_ReAssign;
        }

        private void View_ReAssign()
        {
            IList<DZMembership> csList = bllReceptionStatus.GetCustomListByCSId(GlobalViables.CurrentCustomerService);
            for(int i=0; i<csList.Count; i++)
            {
                this.view.RecptingCustomList += "/" + csList[i].DisplayName;
            }

            IIMSession imSession = new IMSessionsOpenfire(System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiSessionListUrl"), 
                System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiAuthKey"));
            IList<OnlineUserSession> ouSession = imSession.GetOnlineSessionUser();
            for (int j=0; j< ouSession.Count; j++)
            {
                this.view.RecptingCustomServiceList += "/" + ouSession[j].username;
            }
        }

        private void InstantMessage_IMStreamError()
        {
            this.view.ShowStreamError(string.Empty);
        }

        private void View_NoticeSystem()
        {
            instantMessage.SendMessage(


              string.Format(  @"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}@ydban.cn"" from = ""{1}@ydban.cn"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body> system notice</body>
            <ext xmlns = ""ihelper:notce:system"">
            </ext>
            </message>
                ",CurrentServiceOrder.Customer.Id,
                customerService.Id,
                Guid.NewGuid())
                );
        }

        private void View_NoticePromote()
        {
            instantMessage.SendMessage(
             string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}@ydban.cn"" from = ""{1}@ydban.cn"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body>  promote</body>
            <ext xmlns=""ihelper:notce:promote"">
                <url>http://www.ydban.cn</url>
            </ext>
            </message>
                ", CurrentServiceOrder.Customer.Id,
               customerService.Id,Guid.NewGuid())
               );
        }

        private void View_NoticeOrder()
        {
            instantMessage.SendMessage(
              string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}@ydban.cn"" from = ""{1}@ydban.cn"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body>  订单状态变更通知</body>
            <ext xmlns=""ihelper:notce:order"">
                <orderID>{3}</orderID>
                <orderObj title = ""{5}"" status = ""{4}"" type = """">
                </orderObj>
            </ext>
            </message>
                ", CurrentServiceOrder.Customer.Id,
                customerService.Id, Guid.NewGuid(),
                CurrentServiceOrder.Id,CurrentServiceOrder.ServiceName,CurrentServiceOrder.OrderStatus)
                );
        }

        private void View_NoticeCustomerService()
        {
            instantMessage.SendMessage(
              string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}@ydban.cn"" from = ""{1}@ydban.cn"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body>  客服通知 </body>
             <ext xmlns=""ihelper:notce:cer:change"">
                <orderID>{3}</orderID>
                <cerObj UserID = ""{4}"" alias = ""{5}"" imgUrl = ""{6}"">
                </cerObj>
            </ext>
            </message>
                ", CurrentServiceOrder.Customer.Id,
                customerService.Id,
                Guid.NewGuid(),
                CurrentServiceOrder.Id,
                CurrentServiceOrder.Customer.Id,
                CurrentServiceOrder.Customer.NickName,
                CurrentServiceOrder.Customer.AvatarUrl)
                );
        }

        private void View_OrderStateChanged()
        {
            
            ReceptionChatNotice noticeChat = new ReceptionChatNotice {
                From=customerService,
                To=CurrentServiceOrder.Customer,
                ChatType = Model.Enums.enum_ChatType.Notice };
            noticeChat.ServiceOrder = CurrentServiceOrder;
            noticeChat.UserObj = customerService;
           noticeChat.SendTime= noticeChat.SavedTime = DateTime.Now;
            noticeChat.MessageBody = "订单状态已发生变化";
           
            instantMessage.SendMessage(noticeChat);
        }

        PHSuit.Media media = new PHSuit.Media();
        private void View_PlayAudio(object audioTag, IntPtr handle)
        {
            string mediaUrl = audioTag.ToString();
            string fileName = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);

            string fileLocalPath = GlobalViables.LocalMediaSaveDir + fileName;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(GlobalViables.LocalMediaSaveDir+ fileName);
            //player.Play();
            //  var mediaPlayer = new MediaFoundationReader(mediaUrl);
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            // wplayer.openPlayer(fileLocalPath);
            //wplayer.controls.play();
            //wplayer.URL = "My MP3 file.mp3";
            //wplayer.Controls.Play();

            // player.Play();
           
            media.Play(fileLocalPath,handle);
            //media.Play(fileLocalPath, 
            
        }

        void instantMessage_IMClosed()
        {
           
        }

        void view_ViewClosed()
        {
            IMSessionsOpenfire imSession = new IMSessionsOpenfire(
                System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiSessionListUrl"),
                System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiAuthKey"));
            ReceptionAssigner assigner = new ReceptionAssigner(imSession);
            Dictionary<DZMembership,DZMembership> reassignList = assigner.AssignCSLogoff(customerService);
            //将新分配的客服发送给客户端.
            foreach (KeyValuePair<DZMembership,DZMembership> rs in reassignList)
            {
                ReceptionChat rc = new ReceptionChatReAssign
                {
                    From = customerService,
                    ChatType = Model.Enums.enum_ChatType.ReAssign,
                    ReAssignedCustomerService = rs.Value,
                    To = rs.Key,
                    SendTime = DateTime.Now
                };
                SendMessage(rc);
            }
            this.instantMessage.Close();    
        }
      
        /// <summary>
        /// 保存消息.
        /// 此方法不知道消息方向.
        /// </summary>
        /// <param name="message"></param>
        /// 
        private void SaveMessage(ReceptionChat chat, bool isSend)
        {
            #region 保存聊天消息

            DZMembership fromCustomer = isSend ? chat.To : chat.From;
            string customerName = fromCustomer.UserName;
            string message = chat.MessageBody;
  
          
            DateTime now = DateTime.Now;

            if (isSend)
            {
                chat.SendTime = now;

            }
            else
            {
                chat.ReceiveTime = now;

            }
            if (chat is ReceptionChatMedia)
            {
                if (((ReceptionChatMedia)chat).MediaType != "url")
                {
 
                string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                string localFileName = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);
 
                using (var client = new WebClient())
                {
                    string savedPath = GlobalViables.LocalMediaSaveDir + localFileName;
                    PHSuit.IOHelper.EnsureFileDirectory(savedPath);
                    client.DownloadFile(mediaUrl, savedPath);
  
                    }
                }
            }
           
            bllReceptionChat.Save(chat);
            //re.ChatHistory.Add(chat);
            
           // bllReception.Save(re);
            #endregion
        }

        private void LoadSearchResult(DZMembership customer)
        {
            if (SearchResultForCustomer.ContainsKey(customer.UserName))
            {
                view.SearchedService = SearchResultForCustomer[customer.UserName];
            }
        }
        private void LoadChatHistory()
        {
            LoadChatHistory(CurrentServiceOrder);
        }
        private void LoadChatHistory(ServiceOrder serviceOrder)
        {
            
            int rowCount;
            var chatHistory = bllReception.GetChatListByOrder(
                serviceOrder.Id, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1),0,20,out rowCount);

            view.ChatLog = chatHistory;
        }


 
    }

    

}

