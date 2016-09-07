using Dianzhu.BLL;
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
        IViewMainForm viewMainForm;
        IViewFormShowMessage viewFormShowMessage;

        InstantMessage iIM;
        IViewIdentityList iViewIdentityList;
        IBLLMembershipLoginLog bllLoginLog;

        public PMain(IViewMainForm viewMainForm, InstantMessage iIM, IViewIdentityList iViewIdentityList, IBLLMembershipLoginLog bllLoginLog,
            IDAL.IDALReceptionStatus dalReceptionStatus, IDAL.IDALReceptionStatusArchieve dalReceptionStatusArchieve,
             IDAL.IDALReceptionChatDD dalReceptionChatDD, IDAL.IDALReceptionChat dalReceptionChat,
              IDAL.IDALIMUserStatus dalIMUserStatus, IViewFormShowMessage viewFormShowMessage)
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
            this.viewFormShowMessage = viewFormShowMessage;
            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            iIM.IMStreamError += IIM_IMStreamError;
            iIM.IMClosed += IIM_IMClosed;
        }

        private void IIM_IMClosed()
        {
            log.Debug("openfire已下线");
            this.viewFormShowMessage.Message = "网络出现异常，请重新登录";
            this.viewFormShowMessage.ShowDialog();
            //this.ShowMessage("网络出现异常，请重新登录");
            this.viewMainForm.CloseApplication();
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
                    viewMainForm.FlashTaskBar();
                    break;

                case Model.Enums.enum_ChatType.PushedService:
                    errMsg = "错误.客服工具不可能收到 PushedService类型的message.";
                    log.Error(errMsg);
                    break;

                case Model.Enums.enum_ChatType.ReAssign:
                    errMsg = "错误.客服工具不可能收到 ReAssign类型的message.";
                    log.Error(errMsg);
                    break;

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
                    return;

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
