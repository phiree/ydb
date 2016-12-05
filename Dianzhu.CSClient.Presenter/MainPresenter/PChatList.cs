using System;
using System.Collections.Generic;
using System.Linq;
using Dianzhu.CSClient.IView;
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
    /// </summary>
    public class PChatList
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PChatList");

        IViewChatList viewChatList;
        IInstantMessage iIM;
        VMAdapter.IVMChatAdapter vmChatAdapter;
        IChatService chatService;

        string identity;//对应的用户标志，当前为用户id
        string customerName;//用户名

        public PChatList(IViewChatList viewChatList,
            IInstantMessage iIM,
            VMAdapter.IVMChatAdapter vmChatAdapter,
            IChatService chatService,
            string identity,string customerName)
        {
            this.viewChatList = viewChatList;
            this.iIM = iIM;
            this.vmChatAdapter = vmChatAdapter;
            this.chatService = chatService;

            this.identity = identity;
            this.customerName = customerName;

            //iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            viewChatList.CurrentCustomerServiceId = GlobalViables.CurrentCustomerService.Id;
            viewChatList.BtnMoreChat += ViewChatList_BtnMoreChat;
            viewChatList.ChatListCustomerName = customerName;

            LoadChats();
        }

        private void LoadChats()
        {
         //   viewChatList.ChatListCustomerName = customerName;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(identity);

            log.Debug("开始异步加载聊天记录");            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IList<VMChat> vmChatList = e.Result as List<VMChat>;

            if (vmChatList.Count > 0)
            {
                viewChatList.ShowMoreLabel(vmChatList[0].ChatId);

                for (int i = 0; i < vmChatList.Count; i++)
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
                
                string identity = e.Argument.ToString();

                IList<VMChat> vmList = new List<VMChat>();

                IList<ReceptionChatDto> dtoChatList = chatService.GetReceptionChatListByCustomerId(identity, 10);

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
                    }
                }

                e.Result = vmList;
            }
            catch (Exception ee)
            {
                e.Result = new List<ReceptionChatDto>();
                log.Error(log, ee);
            }
            
        }

        private void IIM_IMReceivedMessage(ReceptionChatDto dto)
        {
            if (dto.FromId == identity)
            {
                VMChat vmChat = vmChatAdapter.ChatToVMChat(dto);
                viewChatList.AddOneChat(vmChat);
            }
        }

        public void ReceivedMessage(ReceptionChatDto dto)
        {
            VMChat vmChat = vmChatAdapter.ChatToVMChat(dto);
            viewChatList.AddOneChat(vmChat);
        }

        private void ViewChatList_BtnMoreChat(string targetChatId)
        {
            //var chatHistory = dalReceptionChat.GetReceptionChatListByTargetIdAndSize(IdentityManager.CurrentIdentity.Customer.Id, Guid.Empty, Guid.Empty,
            //       DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10, viewChatList.ChatList[0].SavedTimestamp, "Y", enum_ChatTarget.cer).OrderByDescending(x=>x.SavedTimestamp).ToList();
            
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();//查询服务需开启

                if (string.IsNullOrEmpty( identity))
                {
                    log.Error("Identity为null");
                    return;
                }

                string customerId = identity;

                var chatHistory = chatService.GetReceptionChatListByTargetId(customerId, 10, targetChatId, "Y");

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
                        
                    }

                    viewChatList.ShowMoreLabel(chatHistory[chatHistory.Count - 1].Id.ToString());
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
    }

}
