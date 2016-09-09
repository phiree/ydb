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
using System.IO;
using System.Threading;

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
        InstantMessage iIM;
        IViewChatSend iViewChatSend;
        IBLLServiceOrder bllServiceOrder;
        IViewOrderHistory iViewOrderHistory;

        IDAL.IDALReceptionStatus dalReceptionStatus;
        IViewSearchResult viewSearchResult;
        IDAL.IDALReceptionStatusArchieve dalReceptionStatusArchieve;
        LocalStorage.LocalChatManager localChatManager;
        LocalStorage.LocalHistoryOrderManager localHistoryOrderManager;
        LocalStorage.LocalUIDataManager localUIDataManager;

        public  PIdentityList(IViewIdentityList iView, IViewChatList iViewChatList,
            InstantMessage iIM,
            IDAL.IDALReceptionChat dalReceptionChat, IViewChatSend iViewChatSend,
            IBLLServiceOrder bllServiceOrder, IViewOrderHistory iViewOrderHistory,
            IDAL.IDALReceptionStatus dalReceptionStatus, IViewSearchResult viewSearchResult,
            IDAL.IDALReceptionStatusArchieve dalReceptionStatusArchieve,
            LocalStorage.LocalChatManager localChatManager,
            LocalStorage.LocalHistoryOrderManager localHistoryOrderManager,
            LocalStorage.LocalUIDataManager localUIDataManager
            )
        {
            this.localChatManager = localChatManager;
            this.localHistoryOrderManager = localHistoryOrderManager;
            this.localUIDataManager = localUIDataManager;
            this.iView = iView;
            this.iIM = iIM;
            this.iViewChatList = iViewChatList;
            this.dalReceptionChat = dalReceptionChat;
            this.iViewChatSend = iViewChatSend;
            this.bllServiceOrder = bllServiceOrder;
            this.iViewOrderHistory = iViewOrderHistory;
            this.dalReceptionStatus = dalReceptionStatus;
            this.viewSearchResult = viewSearchResult;
            this.dalReceptionStatusArchieve = dalReceptionStatusArchieve;
            
            iView.IdentityClick += IView_IdentityClick;
            iView.FinalChatTimerTick += IView_FinalChatTimerTick;
            iViewChatSend.FinalChatTimerSend += IViewChatSend_FinalChatTimerSend;

            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            viewSearchResult.PushServiceTimerSend += ViewSearchResult_PushServiceTimerSend;



            Thread t = new Thread(SysAssign);
            t.Start();
        }

        private void SysAssign()
        {
            NHibernateUnitOfWork.UnitOfWork.Start();

            log.Debug("-------开始 接收离线消息------");
            IList<ReceptionStatus> rsList = dalReceptionStatus.GetRSListByDiandian(GlobalViables.Diandian, 3);
            if (rsList.Count > 0)
            {

                log.Debug("需要接待的离线用户数量:" + rsList.Count);
                foreach (ReceptionStatus rs in rsList)
                {
                    #region 接待记录存档
                    SaveRSA(rs.Customer, rs.CustomerService, rs.Order);
                    #endregion
                    rs.CustomerService = GlobalViables.CurrentCustomerService;
                    log.Debug("保存新分配的接待记录");
                    dalReceptionStatus.Update(rs);
                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

                    //CopyDDToChat(rsList.Select(x => x.Customer).ToList());

                    ReceptionChatReAssign rChatReAss = new ReceptionChatReAssign();
                    rChatReAss.From = GlobalViables.Diandian;
                    rChatReAss.To = rs.Customer;
                    rChatReAss.MessageBody = "客服" + rs.CustomerService.DisplayName + "已上线";
                    rChatReAss.ReAssignedCustomerService = rs.CustomerService;
                    rChatReAss.SavedTime = rChatReAss.SendTime = DateTime.Now;
                    rChatReAss.ServiceOrder = rs.Order;
                    rChatReAss.ChatType = Model.Enums.enum_ChatType.ReAssign;

                    //SendMessage(rChatReAss);//保存更换记录，发送消息并且在界面显示
                    // SaveMessage(rChatReAss, true);
                    iIM.SendMessage(rChatReAss);

                    //ClientState.OrderList.Add(rs.Order);
                    ClientState.customerList.Add(rs.Customer);
                    //view.AddCustomerButtonWithStyle(rs.Order, em_ButtonStyle.Unread);
                    if (rs.Order != null)
                    {
                        if (!localChatManager.LocalCustomerAvatarUrls.ContainsKey(rs.Order.Customer.Id.ToString()))
                        {
                            string avatar = string.Empty;
                            if (rs.Customer.AvatarUrl != null)
                            {
                                avatar = rs.Customer.AvatarUrl;
                            }
                            localChatManager.LocalCustomerAvatarUrls[rs.Order.Customer.Id.ToString()] = avatar;
                        }
                        iView.AddIdentity(rs.Order, localChatManager.LocalCustomerAvatarUrls[rs.Order.Customer.Id.ToString()]);
                    }
                }
            }

            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        /// <summary>
        /// 接待记录存档
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="cs"></param>
        /// <param name="order"></param>
        private void SaveRSA(DZMembership customer, DZMembership cs, ServiceOrder order)
        {
            log.Debug("-------开始 接待记录存档------");
            ReceptionStatusArchieve rsa = new ReceptionStatusArchieve
            {
                Customer = customer,
                CustomerService = cs,
                Order = order,
            };
            dalReceptionStatusArchieve.Add(rsa);
            log.Debug("-------结束 接待记录存档------");
        }

        private void ViewSearchResult_PushServiceTimerSend()
        {
            if (IdentityManager.CurrentIdentity != null)
            {
                iView.IdleTimerStart(IdentityManager.CurrentIdentity.Id); 
            }
        }

        private void IViewChatSend_FinalChatTimerSend()
        {
            if (IdentityManager.CurrentIdentity != null)
            {
                iView.IdleTimerStart(IdentityManager.CurrentIdentity.Id);
            }
        }

        private void IView_FinalChatTimerTick(Guid orderId)
        {
            log.Debug("计时结束，orderId：" + orderId);

            ServiceOrder order = bllServiceOrder.GetOne(orderId);
            string errMsg = string.Empty;
            if (IdentityManager.CurrentIdentity != null)
            {
                if (IdentityManager.CurrentIdentity.Id == order.Id)
                {
                    iViewChatList.ClearUCData();
                    iViewChatList.ShowNoChatMsg();
                    iViewOrderHistory.ClearUCData();
                    iViewOrderHistory.ShowNullListLable();
                }
            }

            //删除分配表中用户和客服的关系
            ReceptionStatus rs = dalReceptionStatus.GetOneByCustomerAndCS(GlobalViables.CurrentCustomerService, order.Customer);
            if (rs != null)
            {
                dalReceptionStatus.Delete(rs);
            }

            //发送客服离线消息给用户
            string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
            string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                                                  <active xmlns = ""http://jabber.org/protocol/chatstates""></active><ext xmlns=""ihelper:notice:cer:offline""></ext></message>",
                                              order.Customer.Id + "@" + server, GlobalViables.CurrentCustomerService.Id, Guid.NewGuid());
            iIM.SendMessage(noticeDraftNew);

            //删除当前订单临时变量
            if (IdentityManager.DeleteIdentity(order))
            {
                localChatManager.Remove(order.Customer.Id.ToString());
                localHistoryOrderManager.Remove(order.Customer.Id.ToString());
                localUIDataManager.Remove(order.Customer.Id.ToString());
                iView.IdentityOrderTemp = null;
                RemoveIdentity(order);
            }
            else
            {
                errMsg = "用户没有对应的订单，收到该通知暂时不处理.";
                log.Error(errMsg);
            }
        }

        BackgroundWorker workerChatImage;
        BackgroundWorker workerCustomerAvatar;
        public void IIM_IMReceivedMessage(ReceptionChat chat)
        {
            string errMsg = string.Empty;
            //判断信息类型
            if (chat.ChatType == enum_ChatType.Media || chat.ChatType == enum_ChatType.Text)
            {
                if (chat.ServiceOrder != null)
                {
                    iView.PlayVoice();

                    //1 更新当前聊天列表
                    //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                    IdentityTypeOfOrder type;
                    IdentityManager.UpdateIdentityList(chat.ServiceOrder, out type);

                    //消息本地化.
                    chat.ReceiveTime = DateTime.Now;
                    if (chat is Model.ReceptionChatMedia)
                    {
                        string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                        string fileName = ((ReceptionChatMedia)chat).MedialUrl.Replace(GlobalViables.MediaGetUrl, "");

                        ((ReceptionChatMedia)chat).MedialUrl = fileName;
                    }
                    //dalReceptionChat.Add(chat);
                    ReceivedMessage(chat, type);

                    workerChatImage = new BackgroundWorker();
                    workerChatImage.DoWork += Worker_DoWork;
                    workerChatImage.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    workerChatImage.RunWorkerAsync(chat);

                    // 用户头像的本地化处理
                    if (chat.From.AvatarUrl != null)
                    {
                        workerCustomerAvatar = new BackgroundWorker();
                        workerCustomerAvatar.DoWork += WorkerCustomerAvatar_DoWork;
                        workerCustomerAvatar.RunWorkerCompleted += WorkerCustomerAvatar_RunWorkerCompleted;
                        workerCustomerAvatar.RunWorkerAsync(chat.From);
                    }
                    
                }
            }
        }
        
        private void WorkerCustomerAvatar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.Debug("用户头像本地加载完成");
        }

        private void WorkerCustomerAvatar_DoWork(object sender, DoWorkEventArgs e)
        {
            DZMembership customer = e.Argument as DZMembership;

            string mediaUrl = customer.AvatarUrl;
            string mediaUrl_32X32 = customer.AvatarUrl + "_32X32";
            string fileName = string.Empty;
            string fileName_32X32 = string.Empty;

            if (!mediaUrl.Contains(GlobalViables.MediaGetUrl))
            {
                fileName = mediaUrl;
                fileName_32X32 = mediaUrl_32X32;
                mediaUrl_32X32 = GlobalViables.MediaGetUrl + mediaUrl_32X32;
            }
            else
            {
                fileName = mediaUrl.Replace(GlobalViables.MediaGetUrl, "");
                fileName_32X32 = mediaUrl_32X32.Replace(GlobalViables.MediaGetUrl, "");
            }

            if (!File.Exists(PHSuit.LocalFileManagement.LocalFilePath + fileName_32X32))
            {
                if (PHSuit.LocalFileManagement.DownLoad(string.Empty, mediaUrl_32X32, fileName_32X32))
                {
                    customer.AvatarUrl = fileName;
                    log.Debug("用户头像本地存储完成");
                }
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ReceptionChat chat = e.Result as ReceptionChat;
            iView.IdleTimerStop(chat.ServiceOrder.Id);

            if (chat is Model.ReceptionChatMedia)
            {
                iViewChatList.RemoveChatImageNormalMask(chat.Id);
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReceptionChat chat = e.Argument as ReceptionChat;

            //聊天中的图片下载到本地
            chat.ReceiveTime = DateTime.Now;
            if (chat is Model.ReceptionChatMedia)
            {
                iViewChatList.ShowChatImageNormalMask(chat.Id);

                string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                string fileName = string.Empty;
                if (!mediaUrl.Contains(GlobalViables.MediaGetUrl))
                {
                    fileName = mediaUrl;
                    mediaUrl = GlobalViables.MediaGetUrl + mediaUrl;
                }
                else
                {
                    fileName = ((ReceptionChatMedia)chat).MedialUrl.Replace(GlobalViables.MediaGetUrl, "");
                }

                ((ReceptionChatMedia)chat).MedialUrl = fileName;

                if (PHSuit.LocalFileManagement.DownLoad(string.Empty, mediaUrl, fileName))
                {
                    ((ReceptionChatMedia)chat).MedialUrl = fileName;
                }
            }

            e.Result = chat;
        }
        
        public void IView_IdentityClick(ServiceOrder serviceOrder)
        {
            try
            {
                IdentityManager.CurrentIdentity = iView.IdentityOrderTemp = serviceOrder;
                
                iViewChatList.ClearUCData();
                iViewChatList.ShowLoadingMsg();

                iViewOrderHistory.ClearUCData();
                iViewOrderHistory.ShowListLoadingMsg();

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
            localChatManager.Add(chat.From.Id.ToString(), chat);
            switch (type)
            {
                case IdentityTypeOfOrder.CurrentCustomer:
                    //提示 用户的订单已经变更
                    iViewChatList.AddOneChat(chat, localChatManager.LocalCustomerAvatarUrls[chat.From.Id.ToString()]);
                    break;
                case IdentityTypeOfOrder.CurrentIdentity:
                    iViewChatList.AddOneChat(chat, localChatManager.LocalCustomerAvatarUrls[chat.From.Id.ToString()]);
                    break;
                case IdentityTypeOfOrder.InList:
                    iView.SetIdentityUnread(chat.ServiceOrder, 1);
                    break;
                case IdentityTypeOfOrder.NewIdentity:
                    AddIdentity(chat.ServiceOrder,localChatManager.LocalCustomerAvatarUrls[chat.From.Id.ToString()]);
                    iView.SetIdentityUnread(chat.ServiceOrder, 1);
                    break;
                default:
                    throw new Exception("无法判断消息属性");

            }
        }

        public void AddIdentity(ServiceOrder order,string customerAvatarUrl)
        {
            iView.AddIdentity(order, customerAvatarUrl);
        }

        public void RemoveIdentity(ServiceOrder order)
        {
            if (iView != null)
            {
                iView.RemoveIdentity(order);
            }
        }
        
    }
}


