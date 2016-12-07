using System;
using Dianzhu.CSClient.IView;
using log4net;
using Dianzhu.CSClient.ViewModel;
using Ydb.InstantMessage.Application;
using Dianzhu.CSClient.LocalStorage;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 发送im消息，目前包括文本、图片
    /// </summary>
    public  class PChatSend
    {
        ILog log = LogManager.GetLogger("Dianzhu.CSClient");

        IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        IInstantMessage iIM;
        IViewTabContentTimer viewTabContentTimer;

        string identity;
        public IViewChatSend ViewChatSend
        {
            get
            {
                return viewChatSend;
            }
        }
        
        public PChatSend(
            IViewChatSend viewChatSend, 
            IViewChatList viewChatList,
            IInstantMessage iIM,
            IViewTabContentTimer viewTabContentTimer,
            string identity)
        {
            this.viewChatList = viewChatList;
            this.viewChatSend = viewChatSend;
            this.iIM = iIM;
            this.viewTabContentTimer = viewTabContentTimer;
            this.identity = identity;
            
            this.viewChatSend.SendDidichuxing += ViewChatSend_SendDidichuxing;
            this.viewChatSend.SendTextClick += ViewChatSend_SendTextClick;
            this.viewChatSend.SendMediaClick += ViewChatSend_SendMediaClick;
        }

        private void ViewChatSend_SendDidichuxing()
        {
            iIM.SendDidichuxing("1", "1", "海口", "国贸路", "2", "2", "海口", "龙昆南路", "13012345678",
                Guid.NewGuid(), identity, "YDBan_User", IdentityManager.CurrentOrderId);
        }

        private void ViewChatSend_SendMediaClick(byte[] fileData, string domainType, string mediaType)
        {
            if (string.IsNullOrEmpty( identity))
                return;

            try
            {
                string s = Convert.ToBase64String(fileData);
                string fileName = MediaServer.HttpUploader.Upload(GlobalViables.MediaUploadUrl, s, domainType, mediaType);
                Guid messageId = Guid.NewGuid();
                iIM.SendMessageMedia(messageId, GlobalViables.MediaGetUrl + fileName,mediaType, identity, IdentityManager.CurrentOrderId, "YDBan_User");

                viewTabContentTimer.StartTimer();

                bool downSuccess = PHSuit.LocalFileManagement.DownLoad(string.Empty, GlobalViables.MediaGetUrl + fileName, fileName);
                
                VMChatMedia vmChat = new VMChatMedia(
                    mediaType,
                    fileName,
                    messageId.ToString(), 
                    GlobalViables.CurrentCustomerService.Id.ToString(), 
                    GlobalViables.CurrentCustomerService.DisplayName,
                    identity,
                    DateTime.Now,
                    (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds,
                    "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png",
                    string.Empty,
                    "#b3d465",
                    true);
                viewChatList.AddOneChat(vmChat);
            }
            catch (Exception ee)
            {
                log.Error(ee.ToString());
            }
        }

        public void ViewChatSend_SendTextClick()
        {
            if (string.IsNullOrEmpty( identity))
            { return; }

            try
            {
                string messageText = viewChatSend.MessageText;
                if (string.IsNullOrEmpty(messageText)) return;

                Guid messageId = Guid.NewGuid();
                iIM.SendMessageText(
                    messageId,
                    messageText,
                    identity,
                    "YDBan_User",
                     IdentityManager.CurrentOrderId);

                viewChatSend.MessageText = string.Empty;//发送后清空文本框
                viewTabContentTimer.StartTimer();

                VMChatText vmChatText = new VMChatText(
                    messageText,
                    messageId.ToString(),
                    GlobalViables.CurrentCustomerService.Id.ToString(),
                    GlobalViables.CurrentCustomerService.DisplayName,
                    identity,
                    DateTime.Now,
                    (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds,
                    "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png",
                    string.Empty,
                    "#b3d465",
                    true);
                viewChatList.AddOneChat(vmChatText);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return;
            }
        }
    }

}
