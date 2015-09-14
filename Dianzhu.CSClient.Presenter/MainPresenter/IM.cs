using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
using Dianzhu.CSClient.IInstantMessage;
namespace Dianzhu.CSClient.Presenter
{
    public partial class MainPresenter
    {

        /// <summary>
        /// 加载该客户的订单列表 和 当前正在处理的订单



        /// <summary>
        /// 界面的搜索事件

        /// <summary>
        /// 1)判断该用户是否已在聊天会话中
        /// 2)如果在 则取出该会话, 没有 则创建会话
        /// </summary>
        /// <param name="chat"></param>
        public void IMReceivedMessage(ReceptionChat chat)
        {

            //判断客户列表中是否有该用户
            bool isIn = AddCustomer(chat.From.UserName);
            if (customer != chat.From)
            {
                if (isIn)
                {
                    view.SetCustomerButtonStyle(chat.From.UserName, em_ButtonStyle.Unread);
                }
                else
                {
                    view.AddCustomerButtonWithStyle(chat.From.UserName, em_ButtonStyle.Unread);
                }
            }
            SaveMessage(chat, false);
            //当前激活客户是否等于发送者.
            if (customer != null && chat.From == customer)
            {
                view.LoadOneChat(chat);
            }
        }


        void IMPresent(string userFrom, int presentType)
        {
            string userName = PHSuit.StringHelper.EnsureNormalUserName(userFrom);
            bool isInList = customerList.Any(x => x.UserName == userName);
            switch (presentType)
            {
                case -1:
                    //登录,判断当前用户是否已经在列表中

                    if (isInList)
                    {
                        //改变对应按钮的样式.
                        view.SetCustomerButtonStyle(userName, em_ButtonStyle.Login);
                    }
                    else
                    {
                        AddCustomer(userName);
                        view.AddCustomerButtonWithStyle(userName, em_ButtonStyle.Login);
                    }

                    break;
                case 4:
                    if (isInList)
                    {
                        view.SetCustomerButtonStyle(userName, em_ButtonStyle.LogOff);
                        bllReceptionStatus.CustomerLogOut(customer);
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

