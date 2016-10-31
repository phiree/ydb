using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.CSClient.IView;
using Dianzhu.Model.Enums;
using Dianzhu.DAL;
using log4net;
using System.Windows.Threading;
using Dianzhu.CSClient.ViewModel;
using Ydb.InstantMessage.Application;

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
        
        IView.IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        Ydb.InstantMessage.Application.IInstantMessage iIM;
        IViewIdentityList viewIdentityList;
        IViewOrderHistory viewOrderHistory;
        LocalStorage.LocalChatManager localChatManager;
        VMAdapter.IVMChatAdapter vmChatAdapter;

        ServiceOrder order;
        
        public PChatSend(IViewChatSend viewChatSend, IView.IViewChatList viewChatList,
            Ydb.InstantMessage.Application.IInstantMessage iIM,IViewIdentityList viewIdentityList,
            IViewOrderHistory viewOrderHistory, LocalStorage.LocalChatManager localChatManager, VMAdapter.IVMChatAdapter vmChatAdapter)
        {
            this.viewChatList = viewChatList;
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
                Guid messageId = Guid.NewGuid();
                iIM.SendMessageMedia(messageId, GlobalViables.MediaGetUrl + fileName,mediaType, IdentityManager.CurrentIdentity.CustomerId.Id.ToString(), IdentityManager.CurrentIdentity.Id.ToString(), "YDBan_User");

                if (PHSuit.LocalFileManagement.DownLoad(string.Empty, GlobalViables.MediaGetUrl + fileName, fileName))
                {
                    //chat.SetMediaUrl(fileName);
                }

                //临时存放订单
                order = IdentityManager.CurrentIdentity;

                viewChatSend.MessageText = string.Empty;
                //chat.SetMediaUrl(fileName);
                VMChatMedia vmChat = new VMChatMedia(
                    mediaType,
                    fileName,
                    messageId.ToString(), 
                    GlobalViables.CurrentCustomerService.Id.ToString(), 
                    GlobalViables.CurrentCustomerService.DisplayName,
                    IdentityManager.CurrentIdentity.CustomerId.Id.ToString(),
                    DateTime.Now,
                    (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds,
                    "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png",
                    string.Empty,
                    "#b3d465",
                    true);
                viewChatList.AddOneChat(vmChat);
                localChatManager.Add(vmChat.ToId, vmChat);
                //viewChatList.AddOneChat(chat,string.Empty);
                //chat.SetMediaUrl(chat.MedialUrl.Replace(GlobalViables.MediaGetUrl, ""));
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

                //ReceptionChatFactory chatFactory = new ReceptionChatFactory(Guid.NewGuid(), GlobalViables.CurrentCustomerService.Id.ToString(), IdentityManager.CurrentIdentity.Customer.Id.ToString(),
                //    messageText, IdentityManager.CurrentIdentity.Id.ToString(), enum_XmppResource.YDBan_CustomerService, enum_XmppResource.YDBan_User);
                //ReceptionChat chat = chatFactory.CreateChatText();

                Guid messageId = Guid.NewGuid();
                iIM.SendMessageText(
                    messageId,
                    messageText,
                    IdentityManager.CurrentIdentity.CustomerId.Id.ToString(),
                    "YDBan_User",
                     IdentityManager.CurrentIdentity.Id.ToString());

                viewChatSend.MessageText = string.Empty;//发送后清空文本框
                //VMChat vmChat = vmChatAdapter.ChatToVMChat(chat);
                VMChatText vmChatText = new VMChatText(
                    messageText,
                    messageId.ToString(),
                    GlobalViables.CurrentCustomerService.Id.ToString(),
                    GlobalViables.CurrentCustomerService.DisplayName,
                    IdentityManager.CurrentIdentity.CustomerId.Id.ToString(),
                    DateTime.Now,
                    (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds,
                    "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png",
                    string.Empty,
                    "#b3d465",
                    true);
                viewChatList.AddOneChat(vmChatText);
                localChatManager.Add(vmChatText.ToId, vmChatText);
 
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
