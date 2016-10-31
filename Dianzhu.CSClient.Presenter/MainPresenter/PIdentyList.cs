using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using Dianzhu.DAL;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.CSClient.Presenter.VMAdapter;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Application.Dto;

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
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PIdentityList");
        IViewIdentityList iView;
        IViewChatList iViewChatList;
        IInstantMessage iIM;
        IViewChatSend iViewChatSend;
        IBLLServiceOrder bllServiceOrder;
        IViewOrderHistory iViewOrderHistory;
        
        IViewSearchResult viewSearchResult;
        IDAL.IDALMembership dalMembership;
        LocalStorage.LocalChatManager localChatManager;
        LocalStorage.LocalHistoryOrderManager localHistoryOrderManager;
        LocalStorage.LocalUIDataManager localUIDataManager;
        IVMChatAdapter vmChatAdapter;
        IVMIdentityAdapter vmIdentityAdapter;
        IReceptionService receptionService;

        public PIdentityList(IViewIdentityList iView, IViewChatList iViewChatList,
            IInstantMessage iIM,
             IDAL.IDALMembership dalMembership,
        IViewChatSend iViewChatSend,
            IBLLServiceOrder bllServiceOrder, IViewOrderHistory iViewOrderHistory,
            IViewSearchResult viewSearchResult,
            LocalStorage.LocalChatManager localChatManager,
            LocalStorage.LocalHistoryOrderManager localHistoryOrderManager,
            LocalStorage.LocalUIDataManager localUIDataManager,
            IVMChatAdapter vmChatAdapter,
            IVMIdentityAdapter vmIdentityAdapter,
            IReceptionService receptionService)
        {
            this.localChatManager = localChatManager;
            this.localHistoryOrderManager = localHistoryOrderManager;
            this.localUIDataManager = localUIDataManager;
            this.iView = iView;
            this.iIM = iIM;
            this.iViewChatList = iViewChatList;
            this.iViewChatSend = iViewChatSend;
            this.bllServiceOrder = bllServiceOrder;
            this.iViewOrderHistory = iViewOrderHistory;
            this.viewSearchResult = viewSearchResult;
            this.dalMembership = dalMembership;
            this.vmChatAdapter = vmChatAdapter;
            this.vmIdentityAdapter = vmIdentityAdapter;
            this.receptionService = receptionService;

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
            try
            {
                log.Debug("-------开始 接收离线用户------");
                IList<ReceptionStatusDto> assignList = receptionService.AssignCSLogin(GlobalViables.CurrentCustomerService.Id.ToString(), 3);

                NHibernateUnitOfWork.UnitOfWork.Start();
                
                if (assignList.Count > 0)
                {
                    log.Debug("需要接待的离线用户数量:" + assignList.Count);
                    for(int i=0;i< assignList.Count;i++)
                    {
                        DZMembership customer = dalMembership.FindById(Guid.Parse(assignList[i].CustomerId));
                        ClientState.customerList.Add(customer);
                        ServiceOrder order = bllServiceOrder.GetOne(Guid.Parse(assignList[i].OrderId));

                        IdentityTypeOfOrder type;
                        IdentityManager.UpdateIdentityList(order, out type);

                        if (order != null)
                        {
                            if (!localChatManager.LocalCustomerAvatarUrls.ContainsKey(assignList[i].CustomerId))
                            {
                                string avatar = string.Empty;
                                if (customer.AvatarUrl != null)
                                {
                                    avatar = customer.AvatarUrl;
                                }
                                localChatManager.LocalCustomerAvatarUrls[assignList[i].CustomerId] = avatar;
                            }
                            VMIdentity vmIdentity = vmIdentityAdapter.OrderToVMIdentity(order, localChatManager.LocalCustomerAvatarUrls[assignList[i].CustomerId]);
                            AddIdentity(vmIdentity);

                            //ReceptionChatFactory chatFactory = new ReceptionChatFactory(Guid.NewGuid(), GlobalViables.Diandian.Id.ToString(), customerId,
                            //"客服" + GlobalViables.CurrentCustomerService.DisplayName + "已上线", order.Id.ToString(), enum_XmppResource.YDBan_DianDian, enum_XmppResource.YDBan_User);
                            //ReceptionChat rChatReAss = chatFactory.CreateReassignList[i](GlobalViables.CurrentCustomerService.Id.ToString(), GlobalViables.CurrentCustomerService.DisplayName, string.Empty);
                            //iIM.SendMessage(rChatReAss);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
            finally
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
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

            receptionService.DeleteReception(order.Customer.Id.ToString());

            //删除当前订单临时变量
            if (IdentityManager.DeleteIdentity(order))
            {
                localChatManager.Remove(order.Customer.Id.ToString());
                localHistoryOrderManager.Remove(order.Customer.Id.ToString());
                localUIDataManager.Remove(order.Customer.Id.ToString());
                localUIDataManager.RemoveSearchObj(order.Customer.Id.ToString());
                iView.IdentityOrderTempId = Guid.Empty;
                RemoveIdentity(order.Id);
            }
            else
            {
                errMsg = "用户没有对应的订单，收到该通知暂时不处理.";
                log.Error(errMsg);
            }
        }

        BackgroundWorker workerChatImage;
        BackgroundWorker workerCustomerAvatar;
        public void IIM_IMReceivedMessage(Ydb.InstantMessage.DomainModel.Chat.ReceptionChatDto chat)
        {
            string errMsg = string.Empty;
            //判断信息类型
            if (chat.ChatType == enum_ChatType.Chat.ToString())
            {
                if (!string.IsNullOrEmpty(chat.SessionId))
                {
                    NHibernateUnitOfWork.UnitOfWork.Start();

                    ServiceOrder order = bllServiceOrder.GetOne(new Guid(chat.SessionId));                   

                    iView.PlayVoice();

                    //1 更新当前聊天列表
                    //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                    IdentityTypeOfOrder type;
                    IdentityManager.UpdateIdentityList(order, out type);
                    //todo:
                    //chat.SetReceiveTime(DateTime.Now);


                    ReceivedMessage(chat, type);

                    workerChatImage = new BackgroundWorker();
                    workerChatImage.DoWork += Worker_DoWork;
                    workerChatImage.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    workerChatImage.RunWorkerAsync(chat);

                    // 用户头像的本地化处理
                    DZMembership from = dalMembership.FindById(new Guid(chat.FromId));
                    if (from.AvatarUrl != null)
                    {
                        workerCustomerAvatar = new BackgroundWorker();
                        workerCustomerAvatar.DoWork += WorkerCustomerAvatar_DoWork;
                        workerCustomerAvatar.RunWorkerCompleted += WorkerCustomerAvatar_RunWorkerCompleted;
                        workerCustomerAvatar.RunWorkerAsync(from);
                    }

                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                    NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
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
            ReceptionChatDto chat = e.Result as ReceptionChatDto;
            if (chat != null)
            {
                if (!string.IsNullOrEmpty(chat.SessionId))
                {
                    iView.IdleTimerStop(new Guid(chat.SessionId));
                }

                if (chat is ReceptionChatMediaDto)
                {
                    iViewChatList.RemoveChatImageNormalMask(chat.Id);
                }
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReceptionChatDto chat = e.Argument as ReceptionChatDto;

            //聊天中的图片下载到本地
            //chat.SetReceiveTime(DateTime.Now);
            if (chat is ReceptionChatMediaDto)
            {
                iViewChatList.ShowChatImageNormalMask(chat.Id);

                string mediaUrl = ((ReceptionChatMediaDto)chat).MedialUrl;
                string fileName = string.Empty;
                if (!mediaUrl.Contains(GlobalViables.MediaGetUrl))
                {
                    fileName = mediaUrl;
                    mediaUrl = GlobalViables.MediaGetUrl + mediaUrl;
                }
                else
                {
                    fileName = ((ReceptionChatMediaDto)chat).MedialUrl.Replace(GlobalViables.MediaGetUrl, "");
                }

                //((ReceptionChatMediaDto)chat).SetMediaUrl(fileName);

                if (PHSuit.LocalFileManagement.DownLoad(string.Empty, mediaUrl, fileName))
                {
                    //((ReceptionChatMediaDto)chat).SetMediaUrl(fileName);
                }
            }

            e.Result = chat;
        }

        public void IView_IdentityClick(VMIdentity vmIdentity)
        {
            try
            {
                iView.IdentityOrderTempId = vmIdentity.OrderId;

                if (IdentityManager.CurrentIdentityList.Keys.Select(x => x.Id).ToList().Contains(vmIdentity.OrderId))
                {
                    IdentityManager.CurrentIdentity = IdentityManager.CurrentIdentityList.Keys.ToList().Find(x => x.Id == vmIdentity.OrderId);
                }
                else
                {
                    throw new Exception("IdentityManager.CurrentIdentity is null");
                }

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
        public void ReceivedMessage(Ydb.InstantMessage.DomainModel.Chat.ReceptionChatDto chat, IdentityTypeOfOrder type)
        {
            VMChat vmChat = vmChatAdapter.ChatToVMChat(chat);
            
            localChatManager.Add(vmChat.FromId, vmChat);
            switch (type)
            {
                case IdentityTypeOfOrder.CurrentCustomer:
                case IdentityTypeOfOrder.CurrentIdentity:                    
                    iViewChatList.AddOneChat(vmChat);
                    break;
                case IdentityTypeOfOrder.InList:
                    iView.SetIdentityUnread(chat.SessionId, 1);
                    break;
                case IdentityTypeOfOrder.NewIdentity:
                    ServiceOrder order = bllServiceOrder.GetOne(new Guid(chat.SessionId));
                    VMIdentity vmIdentity = vmIdentityAdapter.OrderToVMIdentity(order, localChatManager.LocalCustomerAvatarUrls[vmChat.FromId]);
                    AddIdentity(vmIdentity);
                    iView.SetIdentityUnread(chat.SessionId, 1);
                    break;
                default:
                    throw new Exception("无法判断消息属性");

            }
        }

        public void AddIdentity(VMIdentity vmIdentity)
        {
            iView.AddIdentity(vmIdentity);
        }

        public void RemoveIdentity(Guid orderId)
        {
            if (iView != null)
            {
                iView.RemoveIdentity(orderId);
            }
        }

    }
}


