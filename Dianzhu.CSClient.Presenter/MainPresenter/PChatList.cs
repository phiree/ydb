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
        Dianzhu.CSClient.LocalStorage.LocalChatManager chatManager;
        
        public PChatList(IViewChatList viewChatList,IViewChatSend viewChatSend, IViewIdentityList viewCustomerList,IDAL.IDALReceptionChat dalReceptionChat, InstantMessage iIM,
            Dianzhu.CSClient.LocalStorage.LocalChatManager chatManager)
        {
            this.chatManager = chatManager;
            this.viewChatList = viewChatList;
            this.viewChatSend = viewChatSend;
            this.dalReceptionChat = dalReceptionChat;
            this.iIM = iIM;
            this.viewIdentityList = viewCustomerList;

            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            viewChatList.CurrentCustomerService = GlobalViables.CurrentCustomerService;
            viewChatList.BtnMoreChat += ViewChatList_BtnMoreChat;

            
        }

        private void ViewChatList_BtnMoreChat()
        {
            var chatHistory = dalReceptionChat.GetReceptionChatListByTargetIdAndSize(IdentityManager.CurrentIdentity.Customer.Id, Guid.Empty, Guid.Empty,
                   DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10, viewChatList.ChatList[0], "Y", enum_ChatTarget.cer).OrderByDescending(x=>x.SavedTimestamp).ToList();

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

                foreach (var item in chatHistory)
                {
                    viewChatList.ChatList.Insert(0, item);
                    string customerId = string.Empty;
                    if(item.FromResource== enum_XmppResource.YDBan_User)
                    {
                        customerId = item.FromId;
                    }
                    else if(item.ToResource==  enum_XmppResource.YDBan_User)
                    {
                        customerId = item.ToId;
                    }
                    viewChatList.InsertOneChat(item, chatManager.LocalCustomerAvatarUrls[customerId]);
                }
            }
            else
            {
                viewChatList.ShowNoMoreLabel();
            }
        }

      

        BackgroundWorker worker;
        public void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(serviceOrder);

            log.Debug("开始异步加载聊天记录");
            viewChatList.ChatListCustomerName = serviceOrder.Customer.DisplayName;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IList<ReceptionChat> chatList=e.Result as List<ReceptionChat>;
            viewChatList.ChatList = chatList;
            if (chatList.Count > 0)
            {
                if (chatList.Count >= 10)
                {
                    viewChatList.ShowMoreLabel();
                }
                else
                {
                    viewChatList.ShowNoMoreLabel();
                }

                foreach (ReceptionChat chat in chatList)
                {
                    if(chat.FromResource== enum_XmppResource.YDBan_User)
                    {
                        viewChatList.AddOneChat(chat, chatManager.LocalCustomerAvatarUrls[chat.FromId]);
                    }
                    else
                    {
                        viewChatList.AddOneChat(chat, string.Empty);
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
                NHibernateUnitOfWork.UnitOfWork.Start();
                ServiceOrder order = (ServiceOrder)e.Argument;

                e.Result = chatManager.InitChatList(order.Customer.Id, Guid.Empty, Guid.Empty);
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
