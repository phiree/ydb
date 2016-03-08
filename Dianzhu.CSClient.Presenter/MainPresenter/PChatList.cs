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
        IView.IViewChatList viewChatList;
        IView.IViewIdentityList viewCustomerList;
        public PChatList(IView.IViewChatList viewChatList,IView.IViewIdentityList viewCustomerList,DALReception dalReception,InstantMessage iIM)
        {
            this.viewChatList = viewChatList;
            this.dalReception = dalReception;
       //     viewCustomerList.IdentityClick += ViewCustomerList_CustomerClick;
            
            this.viewCustomerList = viewCustomerList;

        }

        private void IIM_IMReceivedMessage(ReceptionChat chat)
        {
            
        }

        public PChatList(IView.IViewChatList viewChatList, IView.IViewIdentityList viewCustomerList,InstantMessage iIM)
            :this(viewChatList,viewCustomerList,new DALReception(),iIM)
        {
           
        }
        private void ViewCustomerList_CustomerClick(DZMembership customer)
        {
            //
           // int rowCount;
           // var chatHistory = dalReception
           // //.GetListTest();
           // .GetReceptionChatList(customer, PGlobal.CurrentCustomerService,
           //PGlobal.CurrentOrder.Id
           // , DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20, enum_ChatTarget.all, out rowCount);

           // viewChatList.ChatList = chatHistory;
        }
    }

}
