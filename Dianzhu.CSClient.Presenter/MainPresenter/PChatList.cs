﻿using System;
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

            var chatHistory = dalReceptionChat.GetReceptionChatListByTargetIdAndSize(IdentityManager.CurrentIdentity.Customer.Id, Guid.Empty, Guid.Empty,
                   DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10, viewChatList.ChatList[0], "Y", enum_ChatTarget.all).OrderByDescending(x=>x.SavedTimestamp).ToList();

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

            //var list = viewChatList.ChatList;

            foreach (var item in chatHistory)
            {
                viewChatList.ChatList.Insert(0, item);
                viewChatList.InsertOneChat(item);
            }

            //viewChatList.ChatList = list;
            //viewChatList.ClearUCData();
            viewChatList.ShowMoreLabel();
            //foreach (ReceptionChat chat in list)
            //{
            //    viewChatList.AddOneChat(chat);
            //}

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

        BackgroundWorker worker;
        public void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            //viewChatList.ChatListCustomerName = serviceOrder.Customer.DisplayName;
            //viewChatList.ClearUCData();

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(serviceOrder.Customer.Id);

            log.Debug("开始异步加载聊天记录");
            viewChatList.ChatListCustomerName = serviceOrder.Customer.DisplayName;

            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //worker.RunWorkerAsync(serviceOrder.Customer.Id);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IList<ReceptionChat> chatList=e.Result as List<ReceptionChat>;
            viewChatList.ChatList = chatList;
            if (chatList.Count > 0)
            {
                if (chatList.Count == 10)
                {
                    viewChatList.ShowMoreLabel();
                }
                else
                {
                    viewChatList.ShowNoMoreLabel();
                }

                foreach (ReceptionChat chat in chatList)
                {
                    viewChatList.AddOneChat(chat);
                }
            }
            else
            {
                viewChatList.ShowNoMoreLabel();
            }
            log.Debug("异步加载聊天记录完成");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            Guid customerId = Guid.Parse(e.Argument.ToString());
            int rowCount;
            e.Result = dalReceptionChat.GetReceptionChatList(customerId, Guid.Empty, Guid.Empty,
                   DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 10, enum_ChatTarget.all, out rowCount);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }
    }

}
