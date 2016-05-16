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

        DALReceptionChat dalReceptionChat;
        IView.IViewChatList viewChatList;
        IViewChatSend viewChatSend;
        InstantMessage iIM;
        public PChatSend(IViewChatSend viewChatSend, IView.IViewChatList viewChatList, InstantMessage iIM):this(viewChatSend,viewChatList,iIM,new DALReceptionChat())
        { }
        public PChatSend(IViewChatSend viewChatSend, IView.IViewChatList viewChatList,InstantMessage iIM, DALReceptionChat dalReceptionChat)
        {
            this.viewChatList = viewChatList;
            this.dalReceptionChat = dalReceptionChat;
            this.viewChatSend = viewChatSend;
            //     viewCustomerList.IdentityClick += ViewCustomerList_CustomerClick;
            this.iIM = iIM;
            this.viewChatSend.SendTextClick += ViewChatSend_SendTextClick;
            this.viewChatSend.SendMediaClick += ViewChatSend_SendMediaClick;
               
        }

        private void ViewChatSend_SendMediaClick(byte[] fileData, string domainType, string mediaType)
        {
            if (IdentityManager.CurrentIdentity == null) return;

            string s = Convert.ToBase64String(fileData);
            string fileName = MediaServer.HttpUploader.Upload(GlobalViables.MediaUploadUrl, s, domainType, mediaType);

            ReceptionChatMedia chat = new ReceptionChatMedia
            {
                ServiceOrder = IdentityManager.CurrentIdentity,
                ChatType = Model.Enums.enum_ChatType.Media,
                From =GlobalViables.CurrentCustomerService,
                To = IdentityManager.CurrentIdentity.Customer,
                MessageBody = viewChatSend.MessageText,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                MedialUrl = GlobalViables.MediaGetUrl + fileName,
                MediaType = mediaType
            };

            iIM.SendMessage(chat);

            viewChatSend.MessageText = string.Empty;
            chat.MedialUrl = fileName;
            viewChatList.AddOneChat(chat);

            chat.MedialUrl = chat.MedialUrl.Replace(GlobalViables.MediaGetUrl, "");
            dalReceptionChat.Save(chat);
        }

        private void ViewChatSend_SendTextClick()
        {

            try
            {
                if (IdentityManager.CurrentIdentity == null)
                { return; }
                string messageText = viewChatSend.MessageText;
                if (string.IsNullOrEmpty(messageText)) return;

                ReceptionChat chat = new ReceptionChat
                {
                    ChatType = Model.Enums.enum_ChatType.Text,
                    From = GlobalViables.CurrentCustomerService,
                    To = IdentityManager.CurrentIdentity.Customer,
                    MessageBody = messageText,
                    SendTime = DateTime.Now,
                    SavedTime = DateTime.Now,
                    ServiceOrder = IdentityManager.CurrentIdentity
                };
                viewChatSend.MessageText = string.Empty;
                viewChatList.AddOneChat(chat);
                dalReceptionChat.Save(chat);
                iIM.SendMessage(chat);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return;
            }
        }


 
    }

}
