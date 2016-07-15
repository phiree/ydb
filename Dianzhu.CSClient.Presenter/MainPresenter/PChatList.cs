using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.Model.Enums;
using System.ComponentModel;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 聊天列表控制
    /// 1)监听im消息
    /// 2)消息展示
    /// 3)监听 icustomer的点击事件.
    /// </summary>
    public class PChatList
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PChatList");
        IDAL.IDALReceptionChat dalReceptionChat;
        IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        IViewIdentityList viewIdentityList;
        InstantMessage iIM;
        public static Dictionary<Guid, IList<ReceptionChat>> chatHistoryAll;        
        
        public PChatList(IViewChatList viewChatList,IViewChatSend viewChatSend, IViewIdentityList viewCustomerList,IDAL.IDALReceptionChat dalReceptionChat, InstantMessage iIM)
        {
            this.viewChatList = viewChatList;
            this.viewChatSend = viewChatSend;
            this.dalReceptionChat = dalReceptionChat;
            this.iIM = iIM;
            this.viewIdentityList = viewCustomerList;

            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            viewChatList.CurrentCustomerService = GlobalViables.CurrentCustomerService;
            viewChatList.BtnMoreChat += ViewChatList_BtnMoreChat;

            iIM.IMReceivedMessage += IIM_IMReceivedMessage;

           // chatHistoryAll = new Dictionary<Guid, IList<ReceptionChat>>();
        }

        private void ViewChatList_BtnMoreChat()
        {
            //ReceptionChat chatOldest = dalReceptionChat.FindById(chatOldestId);
            //if (chatOldest == null)
            //{
            //    return;
            //}

            var chatHistory = dalReceptionChat.GetReceptionChatListByTargetIdAndSize(IdentityManager.CurrentIdentity.Customer, null, Guid.Empty,
                   DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10, viewChatList.ChatList[0], "Y", enum_ChatTarget.all).OrderByDescending(x=>x.SavedTime).ToList();

            if (chatHistory.Count() == 0)
            {
                viewChatList.ShowNoMoreLabel();
                return;
            }

            //viewChatList.ChatOldestId = chatHistory[0].Id;

            //foreach(ReceptionChat chat in chatHistory.OrderByDescending(x=>x.SavedTime))
            //{
            //    chatHistoryAll[IdentityManager.CurrentIdentity.Customer.Id].Insert(0, chat);
            //}

            var list = viewChatList.ChatList;

            foreach (var item in chatHistory)
            {
                list.Insert(0, item);
            }

            viewChatList.ChatList = list;

            //viewChatList.ChatList = chatHistoryAll[IdentityManager.CurrentIdentity.Customer.Id];
        }

        private void IIM_IMReceivedMessage(ReceptionChat chat)
        {
            //判断信息类型
            if (chat.ChatType == enum_ChatType.Media || chat.ChatType == enum_ChatType.Text)
            {
                //if (chatHistoryAll.Count > 0)
                //{
                //    chatHistoryAll[chat.From.Id].Add(chat);
                //}                
            }
        }

        public void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            try
            {
                viewChatList.ChatListCustomerName = serviceOrder.Customer.DisplayName;

                //if (chatHistoryAll.ContainsKey(serviceOrder.Customer.Id))
                //{
                //    viewChatList.ChatList = chatHistoryAll[serviceOrder.Customer.Id];
                //    return;
                //}

                int rowCount;
                var chatHistory = dalReceptionChat
                       //.GetListTest();
                       .GetReceptionChatList(serviceOrder.Customer, null, Guid.Empty,
                       DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20, enum_ChatTarget.all, out rowCount);
                //viewChatList.ChatList.Clear();
                viewChatList.ChatList = chatHistory;

                //if (chatHistory != null)
                //{
                //    viewChatList.ChatOldestId = chatHistory[0].Id;
                //}                

                //  chatHistoryAll[serviceOrder.Customer.Id] = chatHistory;
            }
            catch (Exception ex)
            {
                log.Error("加载聊天信息失败");
                PHSuit.ExceptionLoger.ExceptionLog(log, ex);

            }
        }
    }

}
