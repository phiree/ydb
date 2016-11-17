using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Dianzhu.Model;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.BLL.Push
{
    public interface IPushMessageBiulder
    {
        string BuildPushMessage(ReceptionChat chat, out bool needPush);
    }
   public  class PushMessageBuilder: IPushMessageBiulder
    {
        IBLLServiceOrder bllServiceOrder;
        IDZMembershipService memberService;
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BllPush.BuildMessage");
        public PushMessageBuilder(IBLLServiceOrder bllServiceOrder,
         IDZMembershipService memberService)
        {
            this.bllServiceOrder = bllServiceOrder;
            this.memberService = memberService;
        }
        private ServiceOrder serviceOrder;
        private ServiceOrder GetCurrentOrder(string chatSessionId) {
            
                if (serviceOrder == null)
                {
                serviceOrder = bllServiceOrder.GetOne(new Guid(chatSessionId));
                }
            return serviceOrder;
            
        }
        public string BuildPushMessage( ReceptionChat chat,out bool needPush)
        {
            needPush = true;
            string pushMessage = string.Empty;
            //服务推送消息
            if (chat.GetType() == typeof(ReceptionChatNoticeOrder))
            {
                serviceOrder = GetCurrentOrder(chat.SessionId);
                if (serviceOrder.OrderStatus == enum_OrderStatus.EndWarranty) { needPush = false; return pushMessage; }

                if (serviceOrder.OrderStatus == enum_OrderStatus.EndCancel ||
                    serviceOrder.OrderStatus == enum_OrderStatus.EndRefund ||
                    serviceOrder.OrderStatus == enum_OrderStatus.EndIntervention)
                {
                    pushMessage = string.Format("<订单完成>{0}订单状态已变为{1},快来看看吧", serviceOrder.SerialNo, serviceOrder.OrderStatusStr);
                }
                else
                {
                    pushMessage = string.Format("<订单更新>{0}订单状态已变为{1},快来看看吧", serviceOrder.SerialNo, serviceOrder.OrderStatusStr);

                }

            }
            else if (chat.GetType() == typeof(ReceptionChatNoticeSys))
            {
                pushMessage = System.Text.RegularExpressions.Regex.Replace(chat.MessageBody, @"[\<|\>|\[|\]]", string.Empty);
            }
            else if (chat.GetType() == typeof(ReceptionChat) || chat.GetType() == typeof(ReceptionChatMedia))
            {

              MemberDto member = memberService.GetUserById( chat.FromId);
                switch (chat.FromResource)// member.UserType)
                {
                    case  XmppResource.YDBan_CustomerService:
                        pushMessage = "[小助理]" + chat.MessageBody;
                        break;
                    case  XmppResource.YDBan_Store:
                        serviceOrder = GetCurrentOrder(chat.SessionId);
                        pushMessage = "[" + serviceOrder.ServiceBusinessName + "]" + chat.MessageBody;
                        break;
                    default:
                        pushMessage = "[" + member.UserName + "]" + chat.MessageBody;
                        break;
                }

            }
            else
            {
                needPush = false;
                log.Debug("未处理的消息类型:" + chat.GetType());
            }
            return pushMessage;
        }
    }
}
