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
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 用户列表的控制.
    /// 功能:
    /// 1)根据接收的IM消息,增删用户列表,更改用户状态
    /// 2)点击用户时,修改自身状态,加载聊天列表,加载订单信息
    /// </summary>
    public class PCustomerList
    {
        IViewCustomerList iView;
        IViewChatList iViewChatList;
        InstantMessage iIm;
        BLLReception bllReception;
        public PCustomerList(IViewCustomerList iView, InstantMessage iIm, IViewChatList iViewChatList)
        {
            this.iView = iView;
            this.iIm = iIm;
            this.iViewChatList = iViewChatList;
            iIm.IMReceivedMessage += IIm_IMReceivedMessage;
            iView.CustomerClick += IView_CustomerClick;
            customerList = new List<DZMembership>();

            bllReception = new BLLReception();

        }

        private void IView_CustomerClick(DZMembership customer)
        {
            currentCustomer = customer;
            int rowCount;
            var chatHistory = bllReception.GetReceptionChatList(
                customer, null, new Guid(), DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 20, enum_ChatTarget.all, out rowCount);

            iViewChatList.ChatList = chatHistory;
            //iViewChatList.ChatList;
        }

        //负责接收消息
        public  void IIm_IMReceivedMessage(ReceptionChat chat)
        {
            //当前用户为空
            if (currentCustomer == null)
            {
                currentCustomer = chat.From;
                customerList.Add(chat.From);
            }
            //消息来自 当前客户 
            if (currentCustomer == chat.From)
            {
                iViewChatList.AddOneChat(chat);
                return;
            }
            //消息来自 非当前用户
            //在当前用户列表中,则设置该用户为 未读.
            if (customerList.Contains(chat.From))
            {
                iView.SetCustomerUnread(chat.From, 1);
            }
            else
            {
                AddCustomer(chat.From);
            }

        }

        DZMembership currentCustomer;
        public DZMembership CurrentCustomer
        {
            get { return currentCustomer; }
        }
        //当前接待的用户列表 
        IList<DZMembership> customerList;
        public IList<DZMembership> CustomerList
        {
            get { return customerList; }
        }

        public void AddCustomer(DZMembership customer)
        {
            customerList.Add(customer);
            iView.AddCustomer(customer);
        }


    }

}
