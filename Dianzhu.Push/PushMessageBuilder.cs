using System;
 
using Ydb.Common;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Enums;
using Dianzhu.Model.Enums;
namespace Dianzhu.Push
{
    public interface IPushMessageBiulder
    {
        Dianzhu.Push.PushMessage BuildPushMessage(string chatMessage, Type chatType, XmppResource fromResource, string fromUserName, 
            string orderId, string orderBusinessName, string serialNo, enum_OrderStatus orderStatus, string orderStatStr);
    }
    public  class PushMessageBuilder: IPushMessageBiulder
    {
         
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BllPush.BuildMessage");
        
        
        public Dianzhu.Push.PushMessage BuildPushMessage(string chatMessage,Type chatType,XmppResource fromResource,string fromUserName, string orderId,string orderBusinessName, string serialNo,enum_OrderStatus orderStatus,string orderStatStr )
        {
            Dianzhu.Push.PushMessage pushMessage = new Dianzhu.Push.PushMessage {  OrderId= orderId };
         
            //服务推送消息
            if (chatType == typeof(ReceptionChatNoticeOrder))
            {
                 
                pushMessage.OrderSerialNo = serialNo;
                if (orderStatus == enum_OrderStatus.EndWarranty) {   return null; }

                if (orderStatus == enum_OrderStatus.EndCancel ||
                    orderStatus == enum_OrderStatus.EndRefund ||
                    orderStatus == enum_OrderStatus.EndIntervention)
                {
                    pushMessage.DisplayContent = string.Format("<订单完成>{0}订单状态已变为{1},快来看看吧",serialNo, orderStatus);
                }
                else
                {
                    pushMessage.DisplayContent = string.Format("<订单更新>{0}订单状态已变为{1},快来看看吧", serialNo, orderStatus);

                }

            }
            else if (chatType == typeof(ReceptionChatNoticeSys))
            {
                pushMessage.DisplayContent = System.Text.RegularExpressions.Regex.Replace(chatMessage, @"[\<|\>|\[|\]]", string.Empty);
            }
            else if (chatType == typeof(ReceptionChat) || chatType == typeof(ReceptionChatMedia))
            {
 
                switch (fromResource)// member.UserType)
                {
                    case  XmppResource.YDBan_CustomerService:
                        pushMessage.DisplayContent = "[小助理]" + chatMessage;
                        break;
                    case  XmppResource.YDBan_Store:
                      
                        pushMessage.DisplayContent = "[" +orderBusinessName + "]" + chatMessage;
                        break;
                    default:
                        pushMessage.DisplayContent = "[" +fromUserName + "]" + chatMessage;
                        break;
                }

            }
            else
            {
               
                log.Debug("未处理的消息类型:" + chatType);
            }
            return pushMessage;
        }
    }
}
