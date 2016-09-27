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
using Dianzhu.CSClient.ViewModel;

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
        Dianzhu.CSClient.LocalStorage.LocalChatManager chatManager;
        VMAdapter.IVMChatAdapter vmChatAdapter;


        public PChatList(IViewChatList viewChatList,IViewChatSend viewChatSend, IViewIdentityList viewCustomerList,IDAL.IDALReceptionChat dalReceptionChat, InstantMessage iIM,
            Dianzhu.CSClient.LocalStorage.LocalChatManager chatManager, VMAdapter.IVMChatAdapter vmChatAdapter)
        {
            this.chatManager = chatManager;
            this.viewChatList = viewChatList;
            this.viewChatSend = viewChatSend;
            this.dalReceptionChat = dalReceptionChat;
            this.iIM = iIM;
            this.viewIdentityList = viewCustomerList;
            this.vmChatAdapter = vmChatAdapter;

            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            viewChatList.CurrentCustomerServiceId = GlobalViables.CurrentCustomerService.Id;
            viewChatList.BtnMoreChat += ViewChatList_BtnMoreChat;

            
        }

        private void ViewChatList_BtnMoreChat()
        {
            var chatHistory = dalReceptionChat.GetReceptionChatListByTargetIdAndSize(IdentityManager.CurrentIdentity.Customer.Id, Guid.Empty, Guid.Empty,
                   DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10, viewChatList.ChatList[0].SavedTimestamp, "Y", enum_ChatTarget.cer).OrderByDescending(x=>x.SavedTimestamp).ToList();

            if (chatHistory.Count() > 0)
            {
                if (chatHistory.Count == 10)
                {
                    viewChatList.ShowMoreLabel();
                }
                else
                {
                    viewChatList.ShowNoMoreLabel();
                }

                for (int i = 0; i < chatHistory.Count; i++)
                {
                    string customerId = string.Empty;
                    if(chatHistory[i].FromResource== enum_XmppResource.YDBan_User)
                    {
                        customerId = chatHistory[i].FromId;
                    }
                    else if(chatHistory[i].ToResource==  enum_XmppResource.YDBan_User)
                    {
                        customerId = chatHistory[i].ToId;
                    }
                    VMChat vmChat = vmChatAdapter.ChatToVMChat(chatHistory[i], chatManager.LocalCustomerAvatarUrls[customerId]);
                    viewChatList.InsertOneChat(vmChat);
                    viewChatList.ChatList.Insert(0, vmChat);
                    //viewChatList.InsertOneChat(item, chatManager.LocalCustomerAvatarUrls[customerId]);
                }
            }
            else
            {
                viewChatList.ShowNoMoreLabel();
            }
        }

      

        BackgroundWorker worker;
        public void ViewIdentityList_IdentityClick(VMIdentity vmIdentity)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(vmIdentity);

            log.Debug("开始异步加载聊天记录");
            viewChatList.ChatListCustomerName = vmIdentity.CustomerName;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IList<ReceptionChat> chatList=e.Result as List<ReceptionChat>;
            
            if (chatList.Count > 0)
            {
                NHibernateUnitOfWork.UnitOfWork.Start();

                if (chatList.Count >= 10)
                {
                    viewChatList.ShowMoreLabel();
                }
                else
                {
                    viewChatList.ShowNoMoreLabel();
                }

                VMChat vmChat;
                for (int i = 0; i < chatList.Count; i++)
                {
                    try
                    {
                        if (chatList[i].FromResource == enum_XmppResource.YDBan_User)
                        {
                            vmChat = vmChatAdapter.ChatToVMChat(chatList[i], chatManager.LocalCustomerAvatarUrls[chatList[i].FromId]);
                        }
                        else
                        {
                            vmChat = vmChatAdapter.ChatToVMChat(chatList[i], string.Empty);
                        }
                    }
                    catch (Exception eee)
                    {
                        log.Error(eee.Message);
                        return;
                    }
                    viewChatList.AddOneChat(vmChat);
                    viewChatList.ChatList.Add(vmChat);
                }

                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
            else
            {
                viewChatList.ShowNoMoreLabel();
            }

            log.Debug("异步加载聊天记录完成");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                VMIdentity vmIdentity = e.Argument as VMIdentity;

                e.Result = chatManager.InitChatList(vmIdentity.CustomerId, Guid.Empty, Guid.Empty);
                //e.Result = dalReceptionChat.GetReceptionChatList(customerId, Guid.Empty, Guid.Empty,
                //       DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 10, enum_ChatTarget.cer, out rowCount);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
            catch (Exception ee)
            {
                e.Result = new List<ReceptionChat>();
                PHSuit.ExceptionLoger.ExceptionLog(log, ee);
            }
        }
    }

}
