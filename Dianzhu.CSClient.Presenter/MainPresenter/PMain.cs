﻿using Dianzhu.BLL;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter
{
    public class PMain
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PMain");

        IDAL.IDALReceptionStatus dalReceptionStatus;
        IDAL.IDALReceptionChat dalReceptionChat;
        IDAL.IDALReceptionChatDD dalReceptionChatDD;
        IDAL.IDALReceptionStatusArchieve dalReceptionStatusArchieve;
        IDAL.IDALIMUserStatus dalIMUserStatus;
        IView.IViewMainForm viewMainForm;


        InstantMessage iIM;
        IViewIdentityList iViewIdentityList;
        IBLLMembershipLoginLog bllLoginLog;

        public PMain(IView.IViewMainForm viewMainForm, InstantMessage iIM, IViewIdentityList iViewIdentityList, IBLLMembershipLoginLog bllLoginLog,
            IDAL.IDALReceptionStatus dalReceptionStatus, IDAL.IDALReceptionStatusArchieve dalReceptionStatusArchieve,
             IDAL.IDALReceptionChatDD dalReceptionChatDD, IDAL.IDALReceptionChat dalReceptionChat,
              IDAL.IDALIMUserStatus dalIMUserStatus)
        {
            this.viewMainForm = viewMainForm;
            this.viewMainForm.FormTitle = GlobalViables.CurrentCustomerService.DisplayName;
            this.iIM = iIM;
            this.iViewIdentityList = iViewIdentityList;
            this.dalReceptionStatus = dalReceptionStatus;
            this.dalReceptionChat = dalReceptionChat;
            this.dalReceptionChatDD = dalReceptionChatDD;
            this.dalReceptionStatusArchieve = dalReceptionStatusArchieve;
            this.dalIMUserStatus = dalIMUserStatus;
            this.bllLoginLog = bllLoginLog;
            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            iIM.IMStreamError += IIM_IMStreamError;
            //NHibernateUnitOfWork.UnitOfWork.Start();
            //NHibernateUnitOfWork.With.Transaction(() => SysAssign(3));
            //SysAssign(3);
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.CurrentSession.Dispose();
            //NHibernateUnitOfWork.UnitOfWork.Current.Dispose();

            //另开线程加载点点的数据
            BackgroundWorker workerSendMedia = new BackgroundWorker();
            workerSendMedia.DoWork += WorkerSendMedia_DoWork;
            workerSendMedia.RunWorkerCompleted += WorkerSendMedia_RunWorkerCompleted;
            workerSendMedia.RunWorkerAsync();
        }

        private void WorkerSendMedia_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                log.Debug("操作被取消");
            }
            else
            {
                log.Debug("点点的数据异步拉取完成");
            }            
        }

        private void WorkerSendMedia_DoWork(object sender, DoWorkEventArgs e)
        {
            log.Debug("开始异步拉取点点的数据");
            //NHibernateUnitOfWork.With.Transaction(() => SysAssign(3));
            NHibernateUnitOfWork.UnitOfWork.Start();
            SysAssign(3);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        /// <summary>
        /// 系统按数量分配用户给在线客服
        /// </summary>
        /// <param name="num"></param>
        private void SysAssign(int num)
        {

            log.Debug("-------开始 接收离线消息------");
            IList<ReceptionStatus> rsList = dalReceptionStatus.GetRSListByDiandian(GlobalViables.Diandian, num);
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

                    CopyDDToChat(rsList.Select(x => x.Customer).ToList());

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
                        iViewIdentityList.AddIdentity(rs.Order);
                    }
                }

            }
            else
            {
                try
                {
                    IList<ServiceOrder> orderCList = dalReceptionChatDD.GetCustomListDistinctFrom(num);
                    if (orderCList.Count > 0)
                    {
                        IList<DZMembership> logoffCList = new List<DZMembership>();
                        foreach (ServiceOrder order in orderCList)
                        {
                            ReceptionStatus rs;
                            if (!logoffCList.Contains(order.Customer))
                            {
                                logoffCList.Add(order.Customer);

                                rs = new ReceptionStatus();
                                rs.Customer = order.Customer;
                                rs.CustomerService = GlobalViables.CurrentCustomerService;
                                rs.Order = order;
                                dalReceptionStatus.Add(rs);
                            }

                            //按订单显示按钮
                            //ClientState.OrderList.Add(order);
                            ClientState.customerList.Add(order.Customer);
                            //view.AddCustomerButtonWithStyle(order, em_ButtonStyle.Unread);
                            iViewIdentityList.AddIdentity(order);
                        }
                        CopyDDToChat(logoffCList);
                    }
                }
                catch (Exception)
                {

                }
            }

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

        /// <summary>
        /// 把点点的记录复制到聊天记录
        /// </summary>
        /// <param name="cList"></param>
        private void CopyDDToChat(IList<DZMembership> cList)
        {
            //查询点点聊天记录表中该用户的聊天记录
            IList<ReceptionChatDD> chatDDList = dalReceptionChatDD.GetChatDDListByOrder(cList);

            ReceptionChat copychat;
            DZMembership cs = GlobalViables.CurrentCustomerService;
            IMUserStatus userStatus = dalIMUserStatus.GetIMUSByUserId(GlobalViables.CurrentCustomerService.Id);
            foreach (ReceptionChatDD chatDD in chatDDList)
            {
                copychat = ReceptionChat.Create(chatDD.ChatType);
                //  copychat.Id = chatDD.Id;
                copychat.MessageBody = chatDD.MessageBody;
                copychat.ReceiveTime = chatDD.ReceiveTime;
                copychat.SendTime = chatDD.SendTime;
                copychat.To = cs;
                copychat.From = chatDD.From;

                copychat.SavedTime = chatDD.SavedTime;
                copychat.ChatType = chatDD.ChatType;
                copychat.FromResource = chatDD.FromResource;
                copychat.ToResource = (enum_XmppResource)Enum.Parse(typeof(enum_XmppResource), userStatus.ClientName);
                copychat.ServiceOrder = chatDD.ServiceOrder;
                copychat.Version = chatDD.Version;
                if (chatDD.ChatType == enum_ChatType.Media)
                {
                    ((ReceptionChatMedia)copychat).MedialUrl = chatDD.MedialUrl;
                    ((ReceptionChatMedia)copychat).MediaType = chatDD.MediaType;
                }
                chatDD.IsCopy = true;
                dalReceptionChat.Add(copychat);


                dalReceptionChatDD.Add(chatDD);
            }
        }

        private void IIM_IMStreamError()
        {
            ShowMessage("错误.同一用户在其他客户端登录,您已被迫下线");
            CloseApplication();
        }

        private void IIM_IMReceivedMessage(Model.ReceptionChat chat)
        {
            string errMsg = string.Empty;
            //判断信息类型
            switch (chat.ChatType)
            {
                //下列状态在其他地方已处理，此处直接跳过
                case Model.Enums.enum_ChatType.Text:
                case Model.Enums.enum_ChatType.Media:
                case Model.Enums.enum_ChatType.BeginPay:
                case Model.Enums.enum_ChatType.Notice:
                case Model.Enums.enum_ChatType.ConfirmedService:
                case Model.Enums.enum_ChatType.Order:
                    break;

                case Model.Enums.enum_ChatType.PushedService:
                    errMsg = "错误.客服工具不可能收到 PushedService类型的message.";
                    log.Error(errMsg);
                    throw new NotImplementedException(errMsg);

                case Model.Enums.enum_ChatType.ReAssign:
                    errMsg = "错误.客服工具不可能收到 ReAssign类型的message.";
                    log.Error(errMsg);
                    throw new NotImplementedException(errMsg);

                case Model.Enums.enum_ChatType.UserStatus:
                    //ReceptionChatUserStatus rcus = (ReceptionChatUserStatus)chat;

                    //if (rcus.Status == Model.Enums.enum_UserStatus.unavailable)
                    //{
                    //    if (IdentityManager.CurrentIdentity == null || IdentityManager.CurrentIdentity == chat.ServiceOrder)
                    //    {
                    //        ShowMessage("该用户已下线");
                    //    }
                    //}

                    break;

                default:
                    errMsg = "尚未实现这种聊天类型:" + chat.ChatType;
                    log.Error(errMsg);
                    throw new NotImplementedException(errMsg);

            }
        }

        public bool? ShowDialog()
        {
            return viewMainForm.ShowDialog();
        }
        public void ShowMessage(string message)
        {
            viewMainForm.ShowMessage(message);
        }
        public void CloseApplication()
        {
             bllLoginLog.MemberLogoff(IdentityManager.CurrentIdentity.CustomerService, string.Empty);
            viewMainForm.CloseApplication();
        }
    }
}
