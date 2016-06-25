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
        IDAL.IDALReceptionChat dalReceptionChat;
        IViewOrder iViewOrder;
        public PIdentityList(IViewIdentityList iView, IViewChatList iViewChatList, IViewOrder iViewOrder, InstantMessage iIM)
            : this(iView, iViewChatList, iViewOrder, iIM, new DALReceptionChat())
        {

        }

        public  PIdentityList(IViewIdentityList iView, IViewChatList iViewChatList,IViewOrder iViewOrder, InstantMessage iIM, IDAL.IDALReceptionChat dalReceptionChat)
        {
            this.iView = iView;
            this.iViewOrder = iViewOrder;
            this.iViewChatList = iViewChatList;
            this.dalReceptionChat = dalReceptionChat;

            iView.IdentityClick += IView_IdentityClick;
            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
        }

        private void IIM_IMReceivedMessage(ReceptionChat chat)
        {
            string errMsg = string.Empty;
            //判断信息类型
            if (chat.ChatType == enum_ChatType.Media || chat.ChatType == enum_ChatType.Text)
            {
                //1 更新当前聊天列表
                //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                IdentityTypeOfOrder type;
                IdentityManager.UpdateIdentityList(chat.ServiceOrder, out type);
                ReceivedMessage(chat, type);
                //消息本地化.
                chat.ReceiveTime = DateTime.Now;
                if (chat is Model.ReceptionChatMedia)
                {
                    string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                    string fileName = ((ReceptionChatMedia)chat).MedialUrl.Replace(GlobalViables.MediaGetUrl, "");

                    ((ReceptionChatMedia)chat).MedialUrl = fileName;
                }
                dalReceptionChat.Add(chat);

              //  PChatList.chatHistoryAll[chat.From.Id].Add(chat);
            }
            else if (chat.ChatType== enum_ChatType.UserStatus)
            {
                ReceptionChatUserStatus rcus = (ReceptionChatUserStatus)chat;

                if (rcus.Status == Model.Enums.enum_UserStatus.unavailable)
                {
                    IdentityLogOffShowMsg(chat.ServiceOrder);

                    //if (IdentityManager.CurrentIdentity.Id == chat.ServiceOrder.Id)
                    //{
                    //    if (IdentityManager.DeleteIdentity(chat.ServiceOrder))
                    //    {
                    //        //RemoveIdentity(chat.ServiceOrder);
                    //        IdentityLogOffShowMsg(chat.ServiceOrder);
                    //    }
                    //    else
                    //    {
                    //        errMsg = "用户没有对应的订单，收到该通知暂时不处理.";
                    //        log.Error(errMsg);
                    //        throw new NotImplementedException(errMsg);
                    //    }
                    //}
                    //else
                    //{
                    //    //SetSetIdentityLogOff(chat.ServiceOrder);
                    //    if (IdentityManager.DeleteIdentity(chat.ServiceOrder))
                    //    {
                    //        //RemoveIdentity(chat.ServiceOrder);
                    //        IdentityLogOffShowMsg(chat.ServiceOrder);
                    //    }
                    //    else
                    //    {
                    //        errMsg = "用户没有对应的订单，收到该通知暂时不处理.";
                    //        log.Error(errMsg);
                    //        throw new NotImplementedException(errMsg);
                    //    }
                    //}
                }
                else
                {
                    if (IdentityManager.CurrentIdentity == null)
                    {
                        IdentityLogOnShowMsgAndTimer(chat.ServiceOrder, "等待中");
                    }
                    else
                    {
                        if (IdentityManager.CurrentIdentity.Id == chat.ServiceOrder.Id)
                        {
                            IdentityLogOnShowMsg(chat.ServiceOrder, "当前接待中...");
                        }
                        else
                        {
                            IdentityLogOnShowMsgAndTimer(chat.ServiceOrder, "等待中");
                        }
                    }
                    //if (IdentityManager.CurrentIdentity.Id == chat.ServiceOrder.Id)
                    //{
                    //    if (IdentityManager.DeleteIdentity(chat.ServiceOrder))
                    //    {
                    //        IdentityLogOnShowMsg(chat.ServiceOrder,"当前接待中...");
                    //    }
                    //    else
                    //    {
                    //        errMsg = "用户没有对应的订单，收到该通知暂时不处理.";
                    //        log.Error(errMsg);
                    //        throw new NotImplementedException(errMsg);
                    //    }
                    //}
                    //else
                    //{
                    //    SetSetIdentityLogOff(chat.ServiceOrder);
                    //    if (IdentityManager.DeleteIdentity(chat.ServiceOrder))
                    //    {
                    //        IdentityLogOnShowMsg(chat.ServiceOrder,"等待中");
                    //    }
                    //    else
                    //    {
                    //        errMsg = "用户没有对应的订单，收到该通知暂时不处理.";
                    //        log.Error(errMsg);
                    //        throw new NotImplementedException(errMsg);
                    //    }
                    //}
                }
            }
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

        public void RemoveIdentity(ServiceOrder order)
        {
            iView.RemoveIdentity(order);
        }

        protected void IdentityLogOffShowMsg(ServiceOrder order)
        {
            iView.IdentityLogOffShowMsg(order);
        }

        protected void IdentityLogOnShowMsg(ServiceOrder order,string msg)
        {
            iView.IdentityLogOnShowMsg(order,msg);
        }

        protected void IdentityLogOnShowMsgAndTimer(ServiceOrder order,string msg)
        {
            iView.IdentityLogOnShowMsgAndTimer(order, msg);
        }

        public void SetSetIdentityLogOff(ServiceOrder order)
        {
            iView.SetIdentityLogOff(order);
        }

    }
}


