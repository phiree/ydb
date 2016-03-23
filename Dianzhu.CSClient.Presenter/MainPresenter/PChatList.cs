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
    public class PChatList
    {
        DALReception dalReception;
        IViewChatList viewChatList;
        IViewIdentityList viewIdentityList;
        InstantMessage iIM;
        public PChatList() { }
        public PChatList(IView.IViewChatList viewChatList, IViewIdentityList viewCustomerList, InstantMessage iIM)
            : this(viewChatList, viewCustomerList, new DALReception(), iIM)
        {

        }
        public PChatList(IViewChatList viewChatList, IViewIdentityList viewCustomerList, DALReception dalReception, InstantMessage iIM)
        {
            this.viewChatList = viewChatList;
            this.dalReception = dalReception;
            //     viewCustomerList.IdentityClick += ViewCustomerList_CustomerClick;
            this.iIM = iIM;
            //   this.iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            this.viewIdentityList = viewCustomerList;
            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;
            viewChatList.CurrentCustomerService = GlobalViables.CurrentCustomerService;
            viewChatList.AudioPlay += ViewChatList_AudioPlay;

        }

        PHSuit.Media media = new PHSuit.Media();
        private void ViewChatList_AudioPlay(object audioTag, IntPtr handle)
        {
            string mediaUrl = audioTag.ToString();
            string fileName = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);

            string fileLocalPath = GlobalViables.LocalMediaSaveDir + fileName;

            media.Play(mediaUrl, handle);
        }

        private void ViewIdentityList_IdentityClick(ServiceOrder serviceOrder)
        {
            int rowCount;
            var chatHistory = dalReception
            //.GetListTest();
            .GetReceptionChatList(serviceOrder.Customer, GlobalViables.CurrentCustomerService,
           IdentityManager.CurrentIdentity.Id
            , DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20, enum_ChatTarget.all, out rowCount);
            viewChatList.ChatList.Clear();
            viewChatList.ChatList = chatHistory;
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
