using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.Model.Enums;
using System.ComponentModel;
using Dianzhu.CSClient.ViewModel;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;

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
        IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        IViewIdentityList viewIdentityList;
        IInstantMessage iIM;
        Dianzhu.CSClient.LocalStorage.LocalChatManager chatManager;
        VMAdapter.IVMChatAdapter vmChatAdapter;
        IChatService chatService;


        public PChatList(IViewChatList viewChatList,IViewChatSend viewChatSend, IViewIdentityList viewCustomerList, IInstantMessage iIM,
            Dianzhu.CSClient.LocalStorage.LocalChatManager chatManager, VMAdapter.IVMChatAdapter vmChatAdapter,IChatService chatService)
        {
            this.chatManager = chatManager;
            this.viewChatList = viewChatList;
            this.viewChatSend = viewChatSend;
            this.iIM = iIM;
            this.viewIdentityList = viewCustomerList;
            this.vmChatAdapter = vmChatAdapter;
            this.chatService = chatService;

            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            viewChatList.CurrentCustomerServiceId = GlobalViables.CurrentCustomerService.Id;
            viewChatList.BtnMoreChat += ViewChatList_BtnMoreChat;
        }

        private void ViewChatList_BtnMoreChat()
        {
            //var chatHistory = dalReceptionChat.GetReceptionChatListByTargetIdAndSize(IdentityManager.CurrentIdentity.Customer.Id, Guid.Empty, Guid.Empty,
            //       DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10, viewChatList.ChatList[0].SavedTimestamp, "Y", enum_ChatTarget.cer).OrderByDescending(x=>x.SavedTimestamp).ToList();
            
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();

                if (IdentityManager.CurrentIdentity == null)
                {
                    log.Error("IdentityManager.CurrentIdentity为null");
                    return;
                }

                string customerId = IdentityManager.CurrentIdentity.CustomerId;

                var chatHistory = chatService.GetReceptionChatListByTargetId(
                    new Guid( customerId),
                    10,
                    Guid.Parse(chatManager.LocalChats[customerId][0].ChatId),
                    "Y"
                    );

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
                        VMChat vmChat;
                        try
                        {
                            vmChat = vmChatAdapter.ChatToVMChat(chatHistory[i]);
                        }
                        catch (Exception ee)
                        {
                            log.Error(ee);
                            continue;
                        }

                        viewChatList.InsertOneChat(vmChat);
                        //viewChatList.ChatList.Insert(0, vmChat);
                        chatManager.InsertTop(customerId, vmChat);
                    }
                }
                else
                {
                    viewChatList.ShowNoMoreLabel();
                }
            }
            catch (Exception ee)
            {
                log.Error(ee.ToString());
            }
            finally
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
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
            IList<VMChat> vmChatList = e.Result as List<VMChat>;
            
            if (vmChatList.Count > 0)
            {
                if (vmChatList.Count >= 10)
                {
                    viewChatList.ShowMoreLabel();
                }
                else
                {
                    viewChatList.ShowNoMoreLabel();
                }
                
                foreach(var vmChat in vmChatList)
                {
                    viewChatList.AddOneChat(vmChat);
                    //viewChatList.ChatList.Add(vmChat);
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
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                VMIdentity vmIdentity = e.Argument as VMIdentity;
                
                IList<VMChat> vmList = new List<VMChat>();

                if (chatManager.LocalChats.Keys.Contains(vmIdentity.CustomerId.ToString()))
                {
                    if(chatManager.LocalChats[vmIdentity.CustomerId.ToString()].Count < 10)
                    {
                        IList<ReceptionChatDto> dtoChatList = chatService.GetReceptionChatListByTargetId(
                            vmIdentity.CustomerId, 
                            10, 
                            Guid.Parse(chatManager.LocalChats[vmIdentity.CustomerId.ToString()][0].ChatId),
                            "Y");

                        if (dtoChatList.Count > 0)
                        {
                            for (int i = 0; i < dtoChatList.Count; i++)
                            {
                                VMChat vmChat;
                                try
                                {
                                    vmChat = vmChatAdapter.ChatToVMChat(dtoChatList[i]);
                                }
                                catch (Exception ee)
                                {
                                    log.Error(ee);
                                    continue;
                                }
                                
                                chatManager.InsertTop(vmIdentity.CustomerId.ToString(), vmChat);
                            }
                        }
                    }

                    vmList = chatManager.LocalChats[vmIdentity.CustomerId.ToString()];
                }
                else
                {
                    //IList<ReceptionChat> chatList = dalReceptionChat.GetReceptionChatList(vmIdentity.CustomerId, Guid.Empty, Guid.Empty, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 10, enum_ChatTarget.cer, out rowCount);

                    IList<ReceptionChatDto> dtoChatList = chatService.GetReceptionChatListByCustomerId(vmIdentity.CustomerId, 10);

                    if (dtoChatList.Count > 0)
                    {
                        VMChat vmChat;
                        for (int i = 0; i < dtoChatList.Count; i++)
                        {
                            try
                            {
                                vmChat = vmChatAdapter.ChatToVMChat(dtoChatList[i]);
                            }
                            catch (Exception ee)
                            {
                                log.Error(ee);
                                continue;
                            }

                            vmList.Add(vmChat);
                            chatManager.Add(vmIdentity.CustomerId.ToString(), vmChat);
                        }
                    }
                }

                e.Result = vmList;
            }
            catch (Exception ee)
            {
                e.Result = new List<ReceptionChatDto>();
                log.Error(log, ee);
            }
            finally
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
        }
    }

}
