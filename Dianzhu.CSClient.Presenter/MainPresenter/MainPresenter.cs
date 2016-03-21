using Dianzhu.BLL;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.CSClient.IView;
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
        IView.IMainFormView view;
        InstantMessage instantMessage;
        DZMembershipProvider bllMember;
        BLLDZService bllService;
        BLLReception bllReception;
        BLLServiceOrder bllOrder;
        BLLReceptionStatus bllReceptionStatus;
        BLLReceptionChat bllReceptionChat;
        BLLDeviceBind bllDeviceBind;
        BLLReceptionChatDD bllReceptionChatDD;
        BLLIMUserStatus bllIMUserStatus;
        BLLReceptionStatusArchieve bllReceptionStatusArchieve;
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis;
        string server;
        int rsaCustomerAmount;
        IView.IViewIdentityList ViewCustomerList { get; set; }

        public MainPresenter(IView.IMainFormView view,
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
            this.bllReceptionChat = new BLLReceptionChat();// bllReceptionChat;
            this.bllDeviceBind = new BLLDeviceBind();//bllDeviceBind

            this.bllOrder = new BLLServiceOrder();// bllOrder;
            this.bllReceptionChatDD = new BLLReceptionChatDD();
            this.bllIMUserStatus = new BLLIMUserStatus();
            this.bllReceptionStatusArchieve = new BLLReceptionStatusArchieve();
            this.bllServiceOrderStateChangeHis = new BLLServiceOrderStateChangeHis();

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
            this.view.IdentityItemActived += new IView.IdentityItemActived(ActiveCustomer);
            this.view.IdentityItemActived += new IView.IdentityItemActived(LoadChatHistory);
            //this.view.IdentityItemActived += new IdentityItemActived(LoadCurrentOrder);

            this.view.ButtonNamePrefix =GlobalViables.ButtonNamePrefix;
            this.view.SearchService += new IView.SearchService(view_SearchService);
            this.view.SelectService += View_SelectService;
            this.view.SendPayLink += new IView.SendPayLink(view_SendPayLink);
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

            this.SysAssign(3);
            server = Dianzhu.Config.Config.GetAppSetting("ImServer");
            this.view.ReceptionCustomerList = bllReceptionStatusArchieve.GetCustomerListByCS(ClientState.customerService, 1, 10, out rsaCustomerAmount);

            #region 组件化
          //  this.ViewCustomerList.IdentityClick += ViewCustomerList_CustomerClick;
            #endregion
        }

        private void ViewCustomerList_CustomerClick(DZMembership customer)
        {
            System.Diagnostics.Debug.Assert(false, "点击");
        }

        /// <summary>
        /// 发送消息后，生成新订单
        /// </summary>
        private void View_MessageSentAndNew()
        {
            try
            {
                if (ClientState.CurrentServiceOrder == null)
                { return; }

                if (string.IsNullOrEmpty(view.MessageTextBox.Trim())) return;

                ReceptionChat chat = new ReceptionChat
                {
                    ChatType = Model.Enums.enum_ChatType.Text,
                    From = ClientState.customerService,
                    To = ClientState.CurrentServiceOrder.Customer,
                    MessageBody = view.MessageTextBox,
                    SendTime = DateTime.Now,
                    SavedTime = DateTime.Now,
                    ServiceOrder = ClientState.CurrentServiceOrder,

                };

                SendMessage(chat);
                view.MessageTextBox = string.Empty;

                ClientState.CurrentServiceOrder.OrderStatus = enum_OrderStatus.Search;
                ClientState.CurrentServiceOrder.OrderFinished = DateTime.Now;
                bllOrder.SaveOrUpdate(ClientState.CurrentServiceOrder);

                //新建订单
                ServiceOrder orderNew = ServiceOrderFactory.CreateDraft(ClientState.customerService, ClientState.CurrentCustomer);

                orderNew.Customer = ClientState.CurrentServiceOrder.Customer;
                orderNew.CustomerService = ClientState.customerService;
                bllOrder.SaveOrUpdate(orderNew);

                //通知前端订单已更改
                ClientState.CurrentServiceOrder = orderNew;
                NoticeDraftNew();

                CleanOrderData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 当前选择的服务
        /// </summary>
        private void View_SelectService(DZService selectedService)
        {
            
            
            //将服务添加到当前订单中.
            ClientState.CurrentServiceOrder.AddDetailFromIntelService(selectedService, 1, string.Empty, DateTime.Now);
            view.CurrentOrder = ClientState.CurrentServiceOrder;
        }

        /// <summary>
        /// 接待记录存档
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="cs"></param>
        /// <param name="order"></param>
        private void SaveRSA(DZMembership customer,DZMembership cs,ServiceOrder order)
        {
            ReceptionStatusArchieve rsa = new ReceptionStatusArchieve
            {
                Customer = customer,
                CustomerService = cs,
                Order = order,
            };
            bllReceptionStatusArchieve.SaveOrUpdate(rsa);
        }

        /// <summary>
        /// 系统按数量分配用户给在线客服
        /// </summary>
        /// <param name="num"></param>
        private void SysAssign(int num)
        {
            IList<ReceptionStatus> rsList = bllReceptionStatus.GetRSListByDiandian(GlobalViables.Diandian,num);
            if (rsList.Count > 0)
            {
                foreach (ReceptionStatus rs in rsList)
                {
                    #region 接待记录存档
                    SaveRSA(rs.Customer, rs.CustomerService, rs.Order);
                    #endregion
                    rs.CustomerService = GlobalViables.CurrentCustomerService;                    
                    bllReceptionStatus.SaveByRS(rs);

                    CopyDDToChat(rsList.Select(x => x.Customer).ToList());

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
                    ClientState.CurrentServiceOrder = rs.Order;
                    View_NoticeCustomerService();
                    //instantMessage.SendMessage(rChatReAss);

                    //ClientState.OrderList.Add(rs.Order);
                    ClientState.customerList.Add(rs.Customer);
                    //view.AddCustomerButtonWithStyle(rs.Order, em_ButtonStyle.Unread);
                    view.AddCustomerButtonWithStyle(rs.Customer, em_ButtonStyle.Unread);
                }
            }
            else
            {
                IList<ServiceOrder> orderCList = bllReceptionChatDD.GetCustomListDistinctFrom(num);
                if (orderCList.Count > 0)
                {
                    IList<DZMembership> logoffCList = new List<DZMembership>();
                    foreach (ServiceOrder order in orderCList)
                    {
                        if (!logoffCList.Contains(order.Customer))
                        {
                            logoffCList.Add(order.Customer);
                        }

                        //按订单显示按钮
                        //ClientState.OrderList.Add(order);
                        ClientState.customerList.Add(order.Customer);
                        //view.AddCustomerButtonWithStyle(order, em_ButtonStyle.Unread);
                        view.AddCustomerButtonWithStyle(order.Customer, em_ButtonStyle.Unread);
                    }
                    CopyDDToChat(logoffCList);
                }
            }
        }

        /// <summary>
        /// 把点点的记录复制到聊天记录
        /// </summary>
        /// <param name="cList"></param>
        private void CopyDDToChat(IList<DZMembership> cList)
        {
            //查询点点聊天记录表中该用户的聊天记录
            IList<ReceptionChatDD> chatDDList = bllReceptionChatDD.GetChatDDListByOrder(cList);

            ReceptionChat copychat;
            foreach (ReceptionChatDD chatDD in chatDDList)
            {
                copychat = ReceptionChat.Create(chatDD.ChatType);
                copychat.Id = chatDD.Id;
                copychat.MessageBody = chatDD.MessageBody;
                copychat.ReceiveTime = chatDD.ReceiveTime;
                copychat.SendTime = chatDD.SendTime;
                copychat.To = GlobalViables.CurrentCustomerService;
                copychat.From = chatDD.From;
                copychat.Reception = chatDD.Reception;
                copychat.SavedTime = chatDD.SavedTime;
                copychat.ChatType = chatDD.ChatType;
                copychat.ServiceOrder = chatDD.ServiceOrder;
                copychat.Version = chatDD.Version;
                if (chatDD.ChatType == enum_ChatType.Media)
                {
                    ((ReceptionChatMedia)copychat).MedialUrl = chatDD.MedialUrl;
                    ((ReceptionChatMedia)copychat).MediaType = chatDD.MediaType;
                }

                bllReceptionChat.Save(copychat);

                chatDD.IsCopy = true;
                bllReceptionChatDD.Save(chatDD);
            }
        }

        private void View_SaveReAssign()
        {
            IDictionary<DZMembership, string> reCSL = this.view.RecptingCustomServiceList;
            foreach(KeyValuePair<DZMembership,string> p in reCSL)
            {
                string[] clist = p.Value.Split(',');
                ReceptionChatReAssign rChatReAss;
                for (int i = 0; i < clist.Length; i++)
                {
                    DZMembership dm = bllMember.GetUserById(new Guid(clist[i]));
                    ReceptionStatus o = bllReceptionStatus.GetOrder(dm, GlobalViables.CurrentCustomerService);
                    bllReceptionStatus.SaveReAssign(dm, p.Key, o.Order);
                    #region 接待记录存档
                    SaveRSA(dm, GlobalViables.CurrentCustomerService, o.Order);
                    #endregion
                    bllReceptionStatus.DeleteAssign(dm, GlobalViables.CurrentCustomerService);//删除已有分配

                    rChatReAss = new ReceptionChatReAssign();
                    rChatReAss.From = GlobalViables.CurrentCustomerService;
                    rChatReAss.To = dm;
                    rChatReAss.MessageBody = "您的客服已更换为" + p.Key.DisplayName;
                    rChatReAss.ReAssignedCustomerService = p.Key;
                    rChatReAss.SavedTime = rChatReAss.SendTime=DateTime.Now;
                    rChatReAss.ServiceOrder = ClientState.CurrentServiceOrder;
                    rChatReAss.ChatType = Model.Enums.enum_ChatType.ReAssign;
                                        
                    SendMessage(rChatReAss);//保存更换记录，发送消息并且在界面显示

                    //删除现在OrderList中的客户
                    //for (int j=0;j< ClientState.OrderList.Count; j++)
                    //{
                    //    if(dm== ClientState.OrderList[j].Customer)
                    //    {
                    //        view.RemoveOrderBtn(ClientState.OrderList[j].Id.ToString());
                    //        ClientState.OrderList.RemoveAt(j);
                    //        j--;
                    //    }
                    //}
                    if (ClientState.customerList.Contains(dm))
                    {
                        view.RemoveCustomBtnAndClear(dm.Id.ToString());
                        ClientState.customerList.Remove(dm);
                    }
                }
            }

            this.view.ShowMsg("保存成功");

        }

        private void View_ReAssign()
        {
            IList<DZMembership> csList = bllReceptionStatus.GetCustomListByCS(GlobalViables.CurrentCustomerService);
            this.view.RecptingCustomList = csList;

            var ouSession = getOnlineSessionUser();
            IDictionary<DZMembership, string> reDicCS = new Dictionary<DZMembership, string>();
            for (int j=0; j< ouSession.Count; j++)
            {
                if(ouSession[j].ressource.ToLower() == enum_XmppResource.YDBan_Win_CustomerService.ToString() && ouSession[j].username != GlobalViables.CurrentCustomerService.Id.ToString())
                {
                    reDicCS.Add(bllMember.GetUserById(new Guid(ouSession[j].username)), string.Empty);
                }
            }
            this.view.RecptingCustomServiceList = reDicCS;
        }

        private IList<OnlineUserSession> getOnlineSessionUser()
        {
            IIMSession imSession = new IMSessionsDB();
            IList<OnlineUserSession> ouSession = imSession.GetOnlineSessionUser(enum_XmppResource.YDBan_Win_CustomerService.ToString());

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
    to = ""{0}"" from = ""{1}"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body> system notice</body>
            <ext xmlns = ""ihelper:notice:system"">
            </ext>
            </message>
                ", ClientState.CurrentServiceOrder.Customer.Id+"@"+ server,
                ClientState.customerService.Id + "@" + server,
                Guid.NewGuid())
                );
        }

        private void View_NoticePromote()
        {
            instantMessage.SendMessage(
             string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}"" from = ""{1}"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body>  promote</body>
            <ext xmlns=""ihelper:notice:promote"">
                <url>http://www.ydban.cn</url>
            </ext>
            </message>
                ", ClientState.CurrentServiceOrder.Customer.Id + "@" + server,
               ClientState.customerService.Id + "@" + server, Guid.NewGuid())
               );
        }

        private void View_NoticeOrder()
        {
            instantMessage.SendMessage(
              string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}"" from = ""{1}"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body>  订单状态变更通知</body>
            <ext xmlns=""ihelper:notice:order"">
                <orderID>{3}</orderID>
                <orderObj title = ""{4}"" status = ""{5}"" type = """">
                </orderObj>
            </ext>
            </message>
                ", ClientState.CurrentServiceOrder.Customer.Id + "@" + server,
                ClientState.customerService.Id + "@" + server, Guid.NewGuid(),
               ClientState.CurrentServiceOrder.Id, 
               ClientState.CurrentServiceOrder.Title,
               ClientState.CurrentServiceOrder.OrderStatus)
                );
        }

        private void View_NoticeCustomerService()
        {
            instantMessage.SendMessage(
              string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}"" from = ""{1}"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <body>  客服通知 </body>
             <ext xmlns=""ihelper:notice:cer:change"">
                <orderID>{3}</orderID>
                <cerObj UserID = ""{4}"" alias = ""{5}"" imgUrl = ""{6}"">
                </cerObj>
            </ext>
            </message>
                ", ClientState.CurrentServiceOrder.Customer.Id + "@" + server,
               ClientState.customerService.Id + "@" + server,
                Guid.NewGuid(),
                ClientState.CurrentServiceOrder.Id,
               ClientState.CurrentServiceOrder.Customer.Id,
               ClientState.CurrentServiceOrder.Customer.NickName,
                ClientState.CurrentServiceOrder.Customer.AvatarUrl)
                );
        }

        private void NoticeDraftNew()
        {
            instantMessage.SendMessage(
              string.Format(@"
             <message xmlns = ""jabber:client"" type = ""headline"" 
        id = ""{2}""
    to = ""{0}"" from = ""{1}"">
            <active xmlns = ""http://jabber.org/protocol/chatstates""></active>
            <ext xmlns=""ihelper:notice:draft:new"">
                <orderID>{3}</orderID>
            </ext>
            </message>
                ", ClientState.CurrentServiceOrder.Customer.Id+"@"+ server, ClientState.customerService.Id,Guid.NewGuid()+"@"+ server, ClientState.CurrentServiceOrder.Id));
        }

        private void View_OrderStateChanged()
        {
            
            ReceptionChatNotice noticeChat = new ReceptionChatNotice {
                From= ClientState.customerService,
                To= ClientState.CurrentServiceOrder.Customer,
                ChatType = Model.Enums.enum_ChatType.Notice };
            noticeChat.ServiceOrder = ClientState.CurrentServiceOrder;
            noticeChat.UserObj = ClientState.customerService;
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
            #region 当前接待记录存档 
            IList<ReceptionStatus> rsList = bllReceptionStatus.GetRsListByCS(ClientState.customerService);
            foreach(ReceptionStatus rs in rsList)
            {
                SaveRSA(rs.Customer, rs.CustomerService, rs.Order);
            }
            #endregion
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

            if(chat.ChatType== enum_ChatType.Notice)
            {
                return;
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
            if (ClientState.SearchResultForCustomer.ContainsKey(customer.UserName))
            {
                view.SearchedService = ClientState.SearchResultForCustomer[customer.UserName];
            }
        }
        //private void LoadChatHistory()
        //{
        //    LoadChatHistory(ClientState.CurrentServiceOrder);
        //}
        //private void LoadChatHistory(ServiceOrder serviceOrder)
        //{
            
        //    int rowCount;
        //    var chatHistory = bllReception.GetChatListByOrder(
        //        serviceOrder.Id, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1),0,20,enum_ChatTarget.all, out rowCount);

        //    view.ChatLog = chatHistory;
        //}

        private void LoadChatHistory(DZMembership dm)
        {
            int rowCount;
            var chatHistory = bllReception.GetReceptionChatList(
                dm,null,new Guid(), DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20, enum_ChatTarget.all, out rowCount);

            view.ChatLog = chatHistory;
        }



    }

    

}

