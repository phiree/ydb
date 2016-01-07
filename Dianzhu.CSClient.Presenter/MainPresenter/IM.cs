using System;
using System.Linq;
using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
using System.Diagnostics;
namespace Dianzhu.CSClient.Presenter
{
    public partial class MainPresenter
    {

        /// <summary>
        /// 加载该客户的订单列表 和 当前正在处理的订单


 

        /// <summary>
        /// 接收消息后的处理
        /// 1)判断该用户是否已在聊天会话中
        /// 2)如果在 则取出该会话, 没有 则创建会话
        /// 3)获取 该聊天记录中的 订单ID.并加载
        /// </summary>
        /// <param name="chat"></param>
        public void IMReceivedMessage(ReceptionChat chat)
        {
           
            SaveMessage(chat, false);
            if (chat.ChatType== Model.Enums.enum_ChatType.Notice
                &&chat.ServiceOrder == null)
            {
                view.ShowNotice(chat.MessageBody);
                return;
            }
            Debug.Assert(chat.ServiceOrder != null,"Order shouldnot be null");
            //判断订单列表是否已经存在
            bool isIn = ClientState.OrderList.Contains(chat.ServiceOrder);
            //订单列表中是否已经有该 订单
           
            if (!isIn)
            {
                ClientState.OrderList.Add(chat.ServiceOrder);
                view.AddCustomerButtonWithStyle(chat.ServiceOrder, em_ButtonStyle.Unread);
            }
            else
            {
                bool isCurrent = ClientState.CurrentServiceOrder !=null &&(ClientState.CurrentServiceOrder.Id == chat.ServiceOrder.Id);
                if (isCurrent)
                {
                    if (chat.ChatType == Model.Enums.enum_ChatType.UserStatus)
                    {
                        ClientState.OrderList.Remove(chat.ServiceOrder);
                        view.ShowMsg("用户" + ((ReceptionChatUserStatus)chat).User.DisplayName + "已下线！");
                        return;
                    }
                    view.LoadOneChat(chat);
                }
                else
                {
                    if (chat.ChatType == Model.Enums.enum_ChatType.UserStatus)
                    {
                        ClientState.OrderList.Remove(chat.ServiceOrder);
                        view.SetCustomerButtonStyle(chat.ServiceOrder, em_ButtonStyle.LogOff);
                        return;
                    }
                    view.SetCustomerButtonStyle(chat.ServiceOrder, em_ButtonStyle.Unread);

                }
            }      
        }


        void IMPresent(string userFrom, int presentType)
        {
            //string userName = PHSuit.StringHelper.EnsureNormalUserName(userFrom);
            if(userFrom== ClientState.customerService.Id.ToString()) //如果是自己的状态 则忽略.
            { return; }
            DZMembership userPresent= bllMember.GetUserById(new Guid(userFrom));
            string userName = userPresent.DisplayName;
            bool isInList = ClientState.customerList.Contains(userPresent);
            bool isInOnlineList=!(null== ClientState.customerOnlineList.SingleOrDefault(x => x.Id.ToString() == userFrom));
            switch (presentType)
            {
                case -1://available
                        //用户上线. 更新在线列表
                    if (!isInOnlineList)
                    {
                        ClientState.customerOnlineList.Add(userPresent);
                    }
                     

                    break;
                case 4://unavailable
                    if (isInOnlineList)
                    {
                        ClientState.customerOnlineList.Remove(userPresent);
                    }
                    if (isInList)
                    {
                       // view.SetCustomerButtonStyle(userPresent, em_ButtonStyle.LogOff);
                        
                       DZMembership logoffCustomer= ClientState.customerList.Single(x => x.UserName ==userName);
                       bllReceptionStatus.CustomerLogOut(logoffCustomer);
                    }
                    

                    break;
                default: break;
            }
        }


        public void SendMessage(ReceptionChat chat)
        {
            
            SaveMessage(chat, true);
            SendMessageWithPush(instantMessage, chat);
            //instantMessage.SendMessage(chat);
            view.LoadOneChat(chat);

        }
        private void InstantMessage_IMIQ()
        {
            
        }
        private bool AddCustomer(string customerName)
        {
            
            string userName = PHSuit.StringHelper.EnsureNormalUserName(customerName);
            bool isInList = ClientState.customerList.Any(x => x.UserName == userName);
            if (isInList)
            {
                return true;
            }
            DZMembership newCustomer = bllMember.GetUserByName(customerName);
            ClientState.customerList.Add(newCustomer);
            return false;
        }




    }



}

