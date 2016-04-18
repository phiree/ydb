using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.Model;

namespace Dianzhu.CSClient.Presenter
{
    
    /// <summary>
    /// 处理和通讯相关的事情
    /// </summary>
    public class InstantMessageHandler
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Present.InstantMessageHandler");
        InstantMessage iIM;
        PIdentityList pIdentityList;
        DAL.DALReceptionChat dalReceptionChat;
        PNotice pNotice;
        public InstantMessageHandler(InstantMessage iIM, 
            PIdentityList pIdentityList,
            DAL.DALReceptionChat dalReceptionChat,
            PNotice pNotice)
        {
            this.iIM = iIM;
            this.iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            this.pIdentityList = pIdentityList;
            this.dalReceptionChat = dalReceptionChat;
            this.pNotice = pNotice;
        }
        public InstantMessageHandler(InstantMessage iIM,PIdentityList pIdentityList,PNotice pNotice)
            :this(iIM,pIdentityList,new DAL.DALReceptionChat(),pNotice)
        {
             

        }

        /// <summary>
        /// 接收消息
        /// 该逻辑泄漏了.
        /// </summary>
        /// <param name="chat"></param>
        private void IIM_IMReceivedMessage(Model.ReceptionChat chat)
        {
            string errMsg = string.Empty;
            string debugMsg = string.Empty;
            //判断信息类型
            switch (chat.ChatType)
            {
                case Model.Enums.enum_ChatType.BeginPay:
                    debugMsg = "用户开始支付";
                    pNotice.ShowNotice(debugMsg);
                    log.Debug(debugMsg);
                    return;
                case Model.Enums.enum_ChatType.Notice:
                    debugMsg = "通知:" + chat.MessageBody;
                    pNotice.ShowNotice(debugMsg);
                    log.Debug(debugMsg);
                    return;
                case Model.Enums.enum_ChatType.ConfirmedService:
                    debugMsg = "用户已确认服务";
                    pNotice.ShowNotice(debugMsg);
                    log.Debug(debugMsg);
                    return;
                case Model.Enums.enum_ChatType.Media:
                case Model.Enums.enum_ChatType.Text:
                    //1 更新当前聊天列表
                    //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                    IdentityTypeOfOrder type;
                    IdentityManager.UpdateIdentityList(chat.ServiceOrder, out type);
                    pIdentityList.ReceivedMessage(chat, type);
                    //消息本地化.
                    chat.ReceiveTime = DateTime.Now;
                    if (chat is Model.ReceptionChatMedia)
                    {
                        ((Model.ReceptionChatMedia)chat).MedialUrl = ((Model.ReceptionChatMedia)chat).MedialUrl.Replace(GlobalViables.MediaGetUrl, "");
                    }
                    dalReceptionChat.Save(chat);
                    break;
                case Model.Enums.enum_ChatType.Order:
                    debugMsg = "订单通知" + chat.ServiceOrder.GetSummaryString();
                    pNotice.ShowNotice(debugMsg);
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
                    ReceptionChatUserStatus rcus = (ReceptionChatUserStatus)chat;
                    pNotice.ShowNotice("用户" + rcus.User.DisplayName +
                        (rcus.Status == Model.Enums.enum_UserStatus.available ? "已上线" : "已下线")
                        );
                    break;
                default:
                    errMsg = "尚未实现这种聊天类型:" + chat.ChatType;
                    log.Error(errMsg);
                    throw new NotImplementedException(errMsg);
                    
            }
 
        }
      
        public void SendMessage(Dianzhu.Model.ReceptionChat chat)
        {
            iIM.SendMessage(chat);
        }
       
    }
}
