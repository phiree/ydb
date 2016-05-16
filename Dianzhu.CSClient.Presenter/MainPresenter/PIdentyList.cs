﻿using System;
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
using System.ComponentModel;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 用户列表的控制.
    /// 功能:
    /// 1)根据接收的IM消息,增删用户列表,更改用户状态
    /// 2)点击用户时,修改自身状态,加载聊天列表,加载订单信息
    /// </summary>
    public  class PIdentityList
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PIdentityList");
        IViewIdentityList iView;
        IViewChatList iViewChatList;

        IViewOrder iViewOrder;
        public PIdentityList() { }
        public  PIdentityList(IViewIdentityList iView, IViewChatList iViewChatList,IViewOrder iViewOrder)
        {
            this.iView = iView;
            this.iViewOrder = iViewOrder;
            this.iViewChatList = iViewChatList;

            iView.IdentityClick += IView_IdentityClick;

        }

        public void IView_IdentityClick(ServiceOrder serviceOrder)
        {
            try
            {
                IdentityManager.CurrentIdentity = serviceOrder;
                iView.SetIdentityLoading(serviceOrder);
                
            }
            catch (Exception ex)
            {
                log.Error("IView_IdentityClick Error,skip");
                PHSuit.ExceptionLoger.ExceptionLog(log, ex);
            }
            

        }

      

        



        /// <summary>
        /// 接收聊天消息
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="isCurrentIdentity">是否是当前标识</param>
        /// <param name="isCurrentCustomer"></param>
        public void ReceivedMessage(ReceptionChat chat, IdentityTypeOfOrder type)
        {
            switch (type)
            {
                case IdentityTypeOfOrder.CurrentCustomer:
                    //提示 用户的订单已经变更
                    iViewChatList.AddOneChat(chat);
                    break;
                case IdentityTypeOfOrder.CurrentIdentity:
                    iViewChatList.AddOneChat(chat);
                    break;
                case IdentityTypeOfOrder.InList:
                    iView.SetIdentityUnread(chat.ServiceOrder, 1);
                    break;
                case IdentityTypeOfOrder.NewIdentity:
                    iView.AddIdentity(chat.ServiceOrder);
                    break;
                default:
                    throw new Exception("无法判断消息属性");

            }
         


        }


        public void AddIdentity(ServiceOrder order)
        {
            iView.AddIdentity(order);
            iView.SetIdentityUnread(order, 1);
        }

    }
}


