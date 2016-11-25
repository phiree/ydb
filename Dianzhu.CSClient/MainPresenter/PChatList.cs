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

        string identity;//对应的用户标志，当前为用户id
        public IViewChatList ViewChatList
        {
            get
            {
                return viewChatList;
            }
        }

        public PChatList(IViewChatList viewChatList,IViewChatSend viewChatSend, IViewIdentityList viewCustomerList, IInstantMessage iIM,
            Dianzhu.CSClient.LocalStorage.LocalChatManager chatManager, VMAdapter.IVMChatAdapter vmChatAdapter,IChatService chatService,string identity)
        {
            this.chatManager = chatManager;
            this.viewChatList = viewChatList;
            this.viewChatSend = viewChatSend;
            this.iIM = iIM;
            this.viewIdentityList = viewCustomerList;
            this.vmChatAdapter = vmChatAdapter;
            this.chatService = chatService;

            this.identity = identity;

            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            viewChatList.CurrentCustomerServiceId = GlobalViables.CurrentCustomerService.Id;
            viewChatList.BtnMoreChat += ViewChatList_BtnMoreChat;
        }

        private void IIM_IMReceivedMessage(ReceptionChatDto dto)
        {
            if (dto.FromId == identity)
            {
                VMChat vmChat = vmChatAdapter.ChatToVMChat(dto);
                viewChatList.AddOneChat(vmChat);
            }
        }

        private void ViewChatList_BtnMoreChat(string targetChatId)
        {
            //var chatHistory = dalReceptionChat.GetReceptionChatListByTargetIdAndSize(IdentityManager.CurrentIdentity.Customer.Id, Guid.Empty, Guid.Empty,
            //       DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10, viewChatList.ChatList[0].SavedTimestamp, "Y", enum_ChatTarget.cer).OrderByDescending(x=>x.SavedTimestamp).ToList();
            
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();//查询服务需开启

                if (string.IsNullOrEmpty( IdentityManager.CurrentCustomerId))
                {
                    log.Error("IdentityManager.CurrentIdentity为null");
                    return;
                }

                string customerId = IdentityManager.CurrentCustomerId;

                if (chatManager.LocalChats.Keys.Contains(customerId))
                {
                    var existList = chatManager.LocalChats[customerId].ToList();
                    var targetSavedTimestamp = existList.Where(x => x.ChatId == targetChatId).ToList()[0].SavedTimestamp;
                    var list = existList.Where(x => x.SavedTimestamp < targetSavedTimestamp).Take(10).OrderByDescending(x=>x.SavedTimestamp).ToList();
                    if (list.Count > 0)
                    {
                        viewChatList.ShowMoreLabel(list[list.Count - 1].ChatId);

                        for (int i = 0; i < list.Count; i++)
                        {
                            viewChatList.InsertOneChat(list[i]);
                        }
                    }
                    else
                    {
                        var chatHistory = chatService.GetReceptionChatListByTargetId(
                                            customerId,
                                            10,
                                            targetChatId,
                                            "Y"
                                            );

                        if (chatHistory.Count() > 0)
                        {
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

                                chatManager.InsertTop(customerId, vmChat);
                            }

                            viewChatList.ShowMoreLabel(chatHistory[chatHistory.Count - 1].Id.ToString());
                        }
                        else
                        {
                            viewChatList.ShowNoMoreLabel();
                        }
                    }
                }
                else
                {
                    log.Error("该用户没有缓存数据");
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
            if (string.IsNullOrEmpty( IdentityManager.CurrentCustomerId))
            { return; }

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
                viewChatList.ShowMoreLabel(vmChatList[0].ChatId);
                //if (vmChatList.Count >= 10)
                //{
                //    viewChatList.ShowMoreLabel();
                //}
                //else
                //{
                //    viewChatList.ShowNoMoreLabel();
                //}
                
                for(int i = 0; i < vmChatList.Count; i++)
                {
                    try
                    {
                        viewChatList.AddOneChat(vmChatList[i]);
                    }
                    catch (Exception ee)
                    {
                        log.Error("ChatId:" + vmChatList[i].ChatId);
                        log.Error(ee);
                    }
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
                NHibernateUnitOfWork.UnitOfWork.Start();//查询服务需开启
                VMIdentity vmIdentity = e.Argument as VMIdentity;
                
                IList<VMChat> vmList = new List<VMChat>();

                if (chatManager.LocalChats.Keys.Contains(vmIdentity.CustomerId))
                {
                    int count = chatManager.LocalChats[vmIdentity.CustomerId].Count;
                    if( count < 10)
                    {
                        IList<ReceptionChatDto> dtoChatList = chatService.GetReceptionChatListByTargetId(
                            vmIdentity.CustomerId, 
                            10 - count, 
                            chatManager.LocalChats[vmIdentity.CustomerId][0].ChatId,
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
                                
                                chatManager.InsertTop(vmIdentity.CustomerId, vmChat);
                            }
                        }

                        vmList = chatManager.LocalChats[vmIdentity.CustomerId];
                    }
                    else
                    {
                        vmList = chatManager.LocalChats[vmIdentity.CustomerId].OrderByDescending(x => x.SavedTimestamp).Take(10).OrderBy(x => x.SavedTimestamp).ToList();
                    }
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
                            chatManager.Add(vmIdentity.CustomerId, vmChat);
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
