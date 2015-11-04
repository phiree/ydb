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
            bool isIn = OrderList.Contains(chat.ServiceOrder);
            //订单列表中是否已经有该 订单
           
            if (!isIn)
            {
                OrderList.Add(chat.ServiceOrder);
                view.AddCustomerButtonWithStyle(chat.ServiceOrder, em_ButtonStyle.Unread);
            }
            else
            {
                bool isCurrent =CurrentServiceOrder!=null &&( CurrentServiceOrder.Id == chat.ServiceOrder.Id);
                if (isCurrent)
                {
                    view.LoadOneChat(chat);
                }
                else
                {
                    view.SetCustomerButtonStyle(chat.ServiceOrder, em_ButtonStyle.Unread);

                }
            }      
        }


        void IMPresent(string userFrom, int presentType)
        {
            //string userName = PHSuit.StringHelper.EnsureNormalUserName(userFrom);
            if(userFrom==customerService.Id.ToString()) //如果是自己的状态 则忽略.
            { return; }
            DZMembership userPresent= bllMember.GetUserById(new Guid(userFrom));
            string userName = userPresent.DisplayName;
            bool isInList = customerList.Contains(userPresent);
            switch (presentType)
            {
                case -1:
                    //登录,判断当前用户是否已经在列表中

                    if (isInList)
                    {
                        //改变对应按钮的样式.
                        //view.SetCustomerButtonStyle(userPresent, em_ButtonStyle.Login);
                    }
                    else
                    {
                       // AddCustomer(userName);
                        //view.AddCustomerButtonWithStyle(userPresent, em_ButtonStyle.Login);
                    }

                    break;
                case 4:
                    if (isInList)
                    {
                       // view.SetCustomerButtonStyle(userPresent, em_ButtonStyle.LogOff);
                        
                       DZMembership logoffCustomer= customerList.Single(x => x.UserName ==userName);
                       bllReceptionStatus.CustomerLogOut(logoffCustomer);
                    }
                    else
                    {
                    }

                    break;
                default: break;
            }
        }


        private void SendMessage(ReceptionChat chat)
        {
            SaveMessage(chat, true);
            instantMessage.SendMessage(chat);
            view.LoadOneChat(chat);

        }
        private void InstantMessage_IMIQ()
        {
            
        }
        private bool AddCustomer(string customerName)
        {
            
            string userName = PHSuit.StringHelper.EnsureNormalUserName(customerName);
            bool isInList = customerList.Any(x => x.UserName == userName);
            if (isInList)
            {
                return true;
            }
            DZMembership newCustomer = bllMember.GetUserByName(customerName);
            customerList.Add(newCustomer);
            return false;
        }




    }



}

