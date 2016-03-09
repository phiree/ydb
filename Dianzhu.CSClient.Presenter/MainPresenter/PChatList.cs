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
namespace Dianzhu.CSClient.Presenter
{
     /// <summary>
     /// 聊天列表控制
     /// 1)监听im消息
     /// 2)消息展示
     /// 3)监听 icustomer的点击事件.
     /// </summary>
    public  class PChatList
    {
        DALReception dalReception;
        IView.IViewChatList viewChatList;
        IView.IViewIdentityList viewIdentityList;
        InstantMessage iIM;
        public PChatList() { }
        public  PChatList(IView.IViewChatList viewChatList,IView.IViewIdentityList viewCustomerList,DALReception dalReception,InstantMessage iIM)
        {
            this.viewChatList = viewChatList;
            this.dalReception = dalReception;
            //     viewCustomerList.IdentityClick += ViewCustomerList_CustomerClick;
            this.iIM = iIM;
            this.viewIdentityList = viewCustomerList;
            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            viewChatList.SendTextClick += ViewChatList_SendTextClick;
        }

        private void ViewChatList_SendTextClick()
        {
            if (IdentityManager.CurrentIdentity==null)
            { return; }
            string messageText = viewChatList.MessageText;
            if (string.IsNullOrEmpty(messageText) ) return;

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
            viewChatList.MessageText = string.Empty;
            iIM.SendMessage(chat);
            viewChatList.AddOneChat(chat);
        }

        private void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            int rowCount;
            var chatHistory = dalReception
            //.GetListTest();
            .GetReceptionChatList(serviceOrder.Customer, GlobalViables.CurrentCustomerService,
           IdentityManager.CurrentIdentity.Id
            , DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20, enum_ChatTarget.all, out rowCount);

            viewChatList.ChatList = chatHistory;
        }

         
 
        public PChatList(IView.IViewChatList viewChatList, IView.IViewIdentityList viewCustomerList,InstantMessage iIM)
            :this(viewChatList,viewCustomerList,new DALReception(),iIM)
        {
           
        }
        private void ViewCustomerList_CustomerClick(DZMembership customer)
        {

            int rowCount;
            var chatHistory = dalReception
            //.GetListTest();
            .GetReceptionChatList(customer, GlobalViables.CurrentCustomerService,
           IdentityManager.CurrentIdentity.Id
            , DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20, enum_ChatTarget.all, out rowCount);

            viewChatList.ChatList = chatHistory;
        }
        public void SendMessage(ReceptionChat chat)
        {

            

        }
    }

}
