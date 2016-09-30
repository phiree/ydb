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
using Dianzhu.CSClient.ViewModel;

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
        LocalStorage.LocalChatManager localChatManager;
        VMAdapter.IVMChatAdapter vmChatAdapter;

        ServiceOrder order;
        
        public PChatSend(IViewChatSend viewChatSend, IView.IViewChatList viewChatList,
            InstantMessage iIM,IDAL.IDALReceptionChat dalReceptionChat,IViewIdentityList viewIdentityList,
            IViewOrderHistory viewOrderHistory, LocalStorage.LocalChatManager localChatManager, VMAdapter.IVMChatAdapter vmChatAdapter)
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
            this.localChatManager = localChatManager;
            this.vmChatAdapter = vmChatAdapter;
        }

        private void ViewChatSend_SendMediaClick(byte[] fileData, string domainType, string mediaType)
        {
            if (IdentityManager.CurrentIdentity == null) return;

            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();

                string s = Convert.ToBase64String(fileData);
                string fileName = MediaServer.HttpUploader.Upload(GlobalViables.MediaUploadUrl, s, domainType, mediaType);

                ReceptionChatFactory chatFactory = new ReceptionChatFactory(Guid.NewGuid(), GlobalViables.CurrentCustomerService.Id.ToString(),
                    IdentityManager.CurrentIdentity.Customer.Id.ToString(), viewChatSend.MessageText, IdentityManager.CurrentIdentity.Id.ToString(), enum_XmppResource.YDBan_CustomerService,
                     enum_XmppResource.YDBan_User);
                ReceptionChatMedia chat = (ReceptionChatMedia)chatFactory.CreateChatMedia(GlobalViables.MediaGetUrl + fileName, mediaType);

                iIM.SendMessage(chat);

                if (PHSuit.LocalFileManagement.DownLoad(string.Empty, chat.MedialUrl, fileName))
                {
                    chat.SetMediaUrl(fileName);
                }

                //临时存放订单
                order = IdentityManager.CurrentIdentity;

                viewChatSend.MessageText = string.Empty;
                chat.SetMediaUrl(fileName);
                VMChat vmChat = vmChatAdapter.ChatToVMChat(chat);
                viewChatList.AddOneChat(vmChat);
                localChatManager.Add(chat.ToId, vmChat);
                //viewChatList.AddOneChat(chat,string.Empty);
                chat.SetMediaUrl(chat.MedialUrl.Replace(GlobalViables.MediaGetUrl, ""));
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

            // dalReceptionChat.Add(chat);

            //  PChatList.chatHistoryAll[IdentityManager.CurrentIdentity.Customer.Id].Add(chat);
        }

        private void ViewChatSend_SendTextClick()
        {
            if (IdentityManager.CurrentIdentity == null)
            { return; }

            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();

                string messageText = viewChatSend.MessageText;
                if (string.IsNullOrEmpty(messageText)) return;
                ReceptionChatFactory chatFactory = new ReceptionChatFactory(Guid.NewGuid(), GlobalViables.CurrentCustomerService.Id.ToString(), IdentityManager.CurrentIdentity.Customer.Id.ToString(),
                    messageText, IdentityManager.CurrentIdentity.Id.ToString(), enum_XmppResource.YDBan_CustomerService, enum_XmppResource.YDBan_User);
                ReceptionChat chat = chatFactory.CreateChatText();
                viewChatSend.MessageText = string.Empty;
                VMChat vmChat = vmChatAdapter.ChatToVMChat(chat);
                viewChatList.AddOneChat(vmChat);
                localChatManager.Add(chat.ToId, vmChat);
                //viewChatList.AddOneChat(chat,string.Empty);
                //dalReceptionChat.Add(chat);
                iIM.SendMessage(chat);

 
                //临时存放订单
                order = IdentityManager.CurrentIdentity;

                //PChatList.chatHistoryAll[IdentityManager.CurrentIdentity.Customer.Id].Add(chat); 
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return;
            }
            finally
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
        }
    }

}
