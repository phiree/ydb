using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using Dianzhu.DAL;
using log4net;
using System.Windows.Threading;

namespace Dianzhu.CSClient.Presenter
{
     /// <summary>
     /// 聊天列表控制
     /// 1)监听im消息
     /// 2)消息展示
     /// 3)监听 icustomer的点击事件.
     /// </summary>
    public  class PChatSend
    {
        ILog log = LogManager.GetLogger("Dianzhu.CSClient");

        IDAL.IDALReceptionChat dalReceptionChat;
        IView.IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        InstantMessage iIM;
        IViewIdentityList viewIdentityList;
        IViewOrderHistory viewOrderHistory;

        DispatcherTimer timerLable;
        ServiceOrder order;
        
        public PChatSend(IViewChatSend viewChatSend, IView.IViewChatList viewChatList,InstantMessage iIM,IDAL.IDALReceptionChat dalReceptionChat,IViewIdentityList viewIdentityList,IViewOrderHistory viewOrderHistory)
        {
            this.viewChatList = viewChatList;
            this.dalReceptionChat = dalReceptionChat;
            this.viewChatSend = viewChatSend;
            //     viewCustomerList.IdentityClick += ViewCustomerList_CustomerClick;
            this.iIM = iIM;
            this.viewIdentityList = viewIdentityList;
            this.viewOrderHistory = viewOrderHistory;

            this.viewChatSend.SendTextClick += ViewChatSend_SendTextClick;
            this.viewChatSend.SendMediaClick += ViewChatSend_SendMediaClick;

            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
        }

        private void IIM_IMReceivedMessage(ReceptionChat chat)
        {
            if (timerLable != null)
            {
                timerLable.Stop();
            }
        }

        private void ViewChatSend_SendMediaClick(byte[] fileData, string domainType, string mediaType)
        {
            if (IdentityManager.CurrentIdentity == null) return;

            string s = Convert.ToBase64String(fileData);
            string fileName = MediaServer.HttpUploader.Upload(GlobalViables.MediaUploadUrl, s, domainType, mediaType);

            ReceptionChatMedia chat = new ReceptionChatMedia
            {
                ServiceOrder = IdentityManager.CurrentIdentity,
                ChatType = Model.Enums.enum_ChatType.Media,
                From =GlobalViables.CurrentCustomerService,
                To = IdentityManager.CurrentIdentity.Customer,
                MessageBody = viewChatSend.MessageText,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                MedialUrl = GlobalViables.MediaGetUrl + fileName,
                MediaType = mediaType
            };

            iIM.SendMessage(chat);

            //临时存放订单
            order = IdentityManager.CurrentIdentity;

            //重新开始计时
            InitTimer();

            viewChatSend.MessageText = string.Empty;
            chat.MedialUrl = fileName;
            viewChatList.AddOneChat(chat);

            chat.MedialUrl = chat.MedialUrl.Replace(GlobalViables.MediaGetUrl, "");
            dalReceptionChat.Add(chat);

            PChatList.chatHistoryAll[IdentityManager.CurrentIdentity.Customer.Id].Add(chat);
        }

        private void ViewChatSend_SendTextClick()
        {
            try
            {
                if (IdentityManager.CurrentIdentity == null)
                { return; }
                string messageText = viewChatSend.MessageText;
                if (string.IsNullOrEmpty(messageText)) return;

                ReceptionChat chat = new ReceptionChat
                {
                    ChatType = Model.Enums.enum_ChatType.Text,
                    From = GlobalViables.CurrentCustomerService,
                    To = IdentityManager.CurrentIdentity.Customer,
                    MessageBody = messageText,
                    SendTime = DateTime.Now,
                    SavedTime = DateTime.Now,
                    ServiceOrder = IdentityManager.CurrentIdentity
                };
                viewChatSend.MessageText = string.Empty;
                viewChatList.AddOneChat(chat);
                dalReceptionChat.Add(chat);
                iIM.SendMessage(chat);

                //临时存放订单
                order = IdentityManager.CurrentIdentity;

                //重新开始计时
                InitTimer();

                PChatList.chatHistoryAll[IdentityManager.CurrentIdentity.Customer.Id].Add(chat);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return;
            }
        }

        private void InitTimer()
        {
            second = 15;
            //viewChatSend.MessageTimer = second.ToString();            

            if (timerLable != null)
            {
                timerLable.Stop();
            }

            timerLable = new DispatcherTimer();
            timerLable.Interval = new TimeSpan(1, 0, 1);//时间控制
            timerLable.Tick += Timer_Tick;
            timerLable.Start();
        }

        int second = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (second > 0)
            {
                second--;
            }
            else
            {
                second = 15;
                timerLable.Stop();
                
                if (order.Id == IdentityManager.CurrentIdentity.Id)
                {
                    viewChatList.ChatList = null;
                    viewOrderHistory.OrderList = null;
                }

                viewIdentityList.RemoveIdentity(order);
                IdentityManager.DeleteIdentity(order);
            }

            //viewChatSend.MessageTimer = second.ToString();
        }
    }

}
