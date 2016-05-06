using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter
{
   public  class PNotice
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PNotice");

        IView.IViewNotice viewNotice;
        public PNotice(IView.IViewNotice viewNotice, InstantMessage iIM)
        {
            this.viewNotice = viewNotice;

            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
        }

        private void IIM_IMReceivedMessage(Model.ReceptionChat chat)
        {
            string debugMsg = string.Empty;
            //判断信息类型
            if(chat.ChatType== Model.Enums.enum_ChatType.BeginPay)
            {
                debugMsg = "用户开始支付";
                ShowNotice(debugMsg);
                log.Debug(debugMsg);
            }
            else if (chat.ChatType == Model.Enums.enum_ChatType.Notice)
            {
                debugMsg = "通知:" + chat.MessageBody;
                ShowNotice(debugMsg);
                log.Debug(debugMsg);
            }
            else if (chat.ChatType == Model.Enums.enum_ChatType.ConfirmedService)
            {
                debugMsg = "用户已确认服务";
                ShowNotice(debugMsg);
                log.Debug(debugMsg);
            }
            else if (chat.ChatType == Model.Enums.enum_ChatType.Order)
            {
                debugMsg = "订单通知" + chat.ServiceOrder.GetSummaryString();
                ShowNotice(debugMsg);
            }
            else if (chat.ChatType == Model.Enums.enum_ChatType.UserStatus)
            {
                ReceptionChatUserStatus rcus = (ReceptionChatUserStatus)chat;
                ShowNotice("用户" + rcus.User.DisplayName + (rcus.Status == Model.Enums.enum_UserStatus.available ? "已上线" : "已下线"));
            }
        }

        public void ShowNotice(string noticeBody)
        {
            viewNotice.NoticeBody = noticeBody;
        }
    }
}
