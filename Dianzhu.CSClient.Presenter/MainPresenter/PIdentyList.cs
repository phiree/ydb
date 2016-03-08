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
    /// 用户列表的控制.
    /// 功能:
    /// 1)根据接收的IM消息,增删用户列表,更改用户状态
    /// 2)点击用户时,修改自身状态,加载聊天列表,加载订单信息
    /// </summary>
    public class PIdentityList
    {
        IViewIdentityList iView;
        IViewChatList iViewChatList;
        
        
       
        public PIdentityList(IViewIdentityList iView,  IViewChatList iViewChatList  )
        {
            this.iView = iView;
            
            this.iViewChatList = iViewChatList;

            iView.IdentityClick += IView_IdentityClick;
            customerList = new List<DZMembership>();
        }

        private void IView_IdentityClick(ServiceOrder serviceOrder)
        {
           
            iView.SetIdentityReaded(serviceOrder);
        }

        

        //负责接收消息
        public  void  ReceivedMessage(ReceptionChat chat)
        {
           
            // //设定当前订单
            ////当前用户为空(第一条消息)
            //if (currentCustomer == null)
            //{
            //    currentCustomer = chat.From;
            //    AddCustomer(chat.From);
            //}
            ////消息来自 当前客户 
            //if (currentCustomer == chat.From)
            //{
            //    iViewChatList.AddOneChat(chat);
            //    return;
            //}
            ////消息来自 非当前用户
            ////在当前用户列表中,则设置该用户为 未读.
            //if (customerList.Contains(chat.From))
            //{
            //    iView.SetCustomerUnread(chat.From, 1);
            //}
            //else
            //{
            //    AddCustomer(chat.From) 
            //}

        }




       
        //当前接待的用户列表 
        IList<DZMembership> customerList;
        public IList<DZMembership> CustomerList
        {
            get { return customerList; }
        }

        public void AddIdentity(ServiceOrder order)
        {
            
           // iView.AddIdentity(customer);
        }


    }

}
