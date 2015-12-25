using Dianzhu.BLL;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.CSClient.IVew;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Collections;
using Dianzhu.Model.Enums;

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
        BLLReceptionChat bllReceptionChat;
       
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
            this.bllReceptionChat = new BLLReceptionChat();// bllReceptionStatus;

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
            this.view.SelectService += View_SelectService;
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
            this.view.SaveReAssign += View_SaveReAssign;
            this.view.CurrentCustomerService = GlobalViables.CurrentCustomerService.UserName;

            this.view.MessageSentAndNew += View_MessageSentAndNew;

            //this.SysAssign();
            
        }

        /// <summary>
        /// 发送消息后，生成新订单
        /// </summary>
        private void View_MessageSentAndNew()
        {
            if (CurrentServiceOrder == null)
            { return; }

            if (string.IsNullOrEmpty(view.CopyResult.Trim())) return;

            ReceptionChat chat = new ReceptionChat
            {
                ChatType = Model.Enums.enum_ChatType.Text,
                From = customerService,
                To = CurrentServiceOrder.Customer,
                MessageBody = view.CopyResult,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                ServiceOrder = CurrentServiceOrder,

            };

            SendMessage(chat);
            view.CopyResult = string.Empty;

            //更新搜索订单状态：改为已完成
            CurrentServiceOrder.OrderStatus = enum_OrderStatus.Finished;
            bllOrder.SaveOrUpdate(CurrentServiceOrder);

            //新建订单
            ServiceOrder orderNew = ServiceOrder.Create(
                         enum_ServiceScopeType.OSIM
                       , string.Empty //serviceName
                       , string.Empty//serviceBusinessName
                       , string.Empty//serviceDescription
                       , 0//serviceUnitPrice
                       , string.Empty//serviceUrl
                       , CurrentServiceOrder.Customer //member
                       , string.Empty
                       , 0
                       , 0);
            orderNew.CustomerService = customerService;
            bllOrder.SaveOrUpdate(orderNew);

            //通知前端订单已更改
            CurrentServiceOrder = orderNew;
            NoticeDraftNew();

            CleanOrderData();
        }

        /// <summary>
        /// 当前选择的服务
        /// </summary>
        private void View_SelectService()
        {
            Guid id = new Guid(view.CurrentServiceId);
            view.CurrentService = bllService.GetOne(id);
        }

        /// <summary>
        /// 系统分配用户给在线客服
        /// </summary>
        private void SysAssign()
        {
            ReceptionStatus rs = bllReceptionStatus.GetRSByDiandian(GlobalViables.Diandian);
            if (rs != null)
            {
                rs.CustomerService = GlobalViables.CurrentCustomerService;
                bllReceptionStatus.SaveByRS(rs);

                BLLReceptionChatDD bllReceptionChatDD = new BLLReceptionChatDD();
                //查询聊天记录表中是否有该订单的聊天，如果没有，从点点记录的聊天记录表中复制一份
                IList<ReceptionChatDD> chatDDList = bllReceptionChatDD.GetChatDDListByOrder(rs.Order);
                if (chatDDList.Count > 0)
                {
                    ReceptionChat copychat;
                    for (int i = 0; i < chatDDList.Count; i++)
                    {
                        //ReceptionChat chat = ReceptionChat.Create(chatType);
                        copychat = ReceptionChat.Create(chatDDList[i].ChatType);
                        copychat.Id = chatDDList[i].Id;
                        copychat.MessageBody = chatDDList[i].MessageBody;
                        copychat.ReceiveTime = chatDDList[i].ReceiveTime;
                        copychat.SendTime = chatDDList[i].SendTime;
                        copychat.To = rs.Customer;
                        copychat.From = chatDDList[i].From;
                        copychat.Reception = chatDDList[i].Reception;
                        copychat.SavedTime = chatDDList[i].SavedTime;
                        copychat.ChatType = chatDDList[i].ChatType;
                        copychat.ServiceOrder = chatDDList[i].ServiceOrder;
                        copychat.Version = chatDDList[i].Version;
                        if(chatDDList[i].ChatType== enum_ChatType.Media)
                        {
                            ((ReceptionChatMedia)copychat).MedialUrl = chatDDList[i].MedialUrl;
                            ((ReceptionChatMedia)copychat).MediaType = chatDDList[i].MediaType;
                        }

                        bllReceptionChat.Save(copychat);

                        chatDDList[i].IsCopy = true;
                        bllReceptionChatDD.Save(chatDDList[i]);
                    }

                    ////复制用户与点点的聊天记录
                    //IList<ReceptionChatDD> chatList = bllReceptionChatDD.GetChatListByOrder(rs.Order);
                    //if (chatList.Count > 0)
                    //{
                    //    ReceptionChat copychat;
                    //    foreach (ReceptionChatDD chatDD in chatList)
                    //    {
                    //        if (chatDD.To != rs.Customer)
                    //        {
                    //            copychat = new ReceptionChat();
                    //            copychat.MessageBody = chatDD.MessageBody;
                    //            copychat.ReceiveTime = chatDD.ReceiveTime;
                    //            copychat.SendTime = chatDD.SendTime;
                    //            copychat.To = rs.Customer;
                    //            copychat.From = chatDD.From;
                    //            copychat.Reception = chatDD.Reception;
                    //            copychat.SavedTime = chatDD.SavedTime;
                    //            copychat.ChatType = chatDD.ChatType;
                    //            copychat.ServiceOrder = chatDD.ServiceOrder;
                    //            copychat.Version = chatDD.Version;

                    //            bllReceptionChat.Save(copychat);
                    //        }
                    //    }
                    //}
                }                

                ReceptionChatReAssign rChatReAss = new ReceptionChatReAssign();
                rChatReAss.From = GlobalViables.Diandian;
                rChatReAss.To = rs.Customer;
                rChatReAss.MessageBody = "客服" + rs.CustomerService.DisplayName + "已上线";
                rChatReAss.ReAssignedCustomerService = rs.CustomerService;
                rChatReAss.SavedTime = rChatReAss.SendTime = DateTime.Now;
                rChatReAss.ServiceOrder = rs.Order;
                rChatReAss.ChatType = Model.Enums.enum_ChatType.ReAssign;

                //SendMessage(rChatReAss);//保存更换记录，发送消息并且在界面显示
                SaveMessage(rChatReAss, true);
                instantMessage.SendMessage(rChatReAss);

                OrderList.Add(rs.Order);
                view.AddCustomerButtonWithStyle(rs.Order, em_ButtonStyle.Unread);
            }
        }

        private void View_SaveReAssign()
        {
            IDictionary<DZMembership, string> reCSL = this.view.RecptingCustomServiceList;
            foreach(KeyValuePair<DZMembership,string> p in reCSL)
            {
                string[] clist = p.Value.Split(',');
                ReceptionChatReAssign rChatReAss;
                for (int i=0; i<clist.Length; i++)
                {
                    DZMembership dm = bllMember.GetUserById(new Guid(clist[i]));
                    ReceptionStatus o = bllReceptionStatus.GetOrder(dm, GlobalViables.CurrentCustomerService);
                    bllReceptionStatus.SaveReAssign(dm, p.Key, o.Order);
                    bllReceptionStatus.DeleteAssign(dm, GlobalViables.CurrentCustomerService);//删除已有分配

                    rChatReAss = new ReceptionChatReAssign();
                    rChatReAss.From = GlobalViables.CurrentCustomerService;
                    rChatReAss.To = dm;
                    rChatReAss.MessageBody = "您的客服已更换为" + p.Key.DisplayName;
                    rChatReAss.ReAssignedCustomerService = p.Key;
                    rChatReAss.SavedTime = rChatReAss.SendTime=DateTime.Now;
                    rChatReAss.ServiceOrder = CurrentServiceOrder;
                    rChatReAss.ChatType = Model.Enums.enum_ChatType.ReAssign;
                                        
                    SendMessage(rChatReAss);//保存更换记录，发送消息并且在界面显示

                    //删除现在OrderList中的客户
                    for (int j=0;j< OrderList.Count; j++)
                    {
                        if(dm== OrderList[j].Customer)
                        {
                            view.RemoveOrderBtn(OrderList[j].Id.ToString());
                            OrderList.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }

            this.view.ShowMsg("保存成功");

        }

        private void View_ReAssign()
        {
            IList<DZMembership> csList = bllReceptionStatus.GetCustomListByCSId(GlobalViables.CurrentCustomerService);
            this.view.RecptingCustomList = csList;

            var ouSession = getOnlineSessionUser();
            IDictionary<DZMembership, string> reDicCS = new Dictionary<DZMembership, string>();
            for (int j=0; j< ouSession.Count; j++)
            {
                if(ouSession[j].ressource.ToLower() == "ydb_cstool" && ouSession[j].username != GlobalViables.CurrentCustomerService.Id.ToString())
                {
                    reDicCS.Add(bllMember.GetUserById(new Guid(ouSession[j].username)), string.Empty);
                }
            }
            this.view.RecptingCustomServiceList = reDicCS;
        }

        private IList<OnlineUserSession> getOnlineSessionUser()
        {
            IIMSession imSession = new IMSessionsOpenfire(System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiSessionListUrl"),
                System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiAuthKey"));
            IList<OnlineUserSession> ouSession = imSession.GetOnlineSessionUser();

            return ouSession;
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
            <ext xmlns = ""ihelper:notice:system"">
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
            <ext xmlns=""ihelper:notice:promote"">
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
            <ext xmlns=""ihelper:notice:order"">
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
             <ext xmlns=""ihelper:notice:cer:change"">
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

        private void NoticeDraftNew()
        {
            instantMessage.SendMessage(
              string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}@ydban.cn"" from = ""{1}@ydban.cn"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <ext xmlns=""ihelper:notice:draft:new"">
                <orderID>{3}</orderID>
            </ext>
            </message>
                ", CurrentServiceOrder.Customer.Id,customerService.Id, Guid.NewGuid(), CurrentServiceOrder.Id));
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
                ServiceOrder order = bllReceptionStatus.GetOrder(rs.Key, rs.Value).Order;
                ReceptionChat rc = new ReceptionChatReAssign
                {
                    From = customerService,
                    ChatType = Model.Enums.enum_ChatType.ReAssign,
                    ReAssignedCustomerService = rs.Value,
                    To = rs.Key,
                    ServiceOrder= order,
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
            if (chat.To.UserType == enum_UserType.customerservice.ToString() || chat.From.UserType == enum_UserType.customerservice.ToString())
            {
                chat.ChatTarget = enum_ChatTarget.cer;
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
                serviceOrder.Id, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1),0,20,enum_ChatTarget.all, out rowCount);

            view.ChatLog = chatHistory;
        }


 
    }

    

}

