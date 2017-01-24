using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Ydb.Push.DomainModel
{
    public class PushMessageBuilder
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Push.DomainModel.PushMessageBuilder");


        public  PushMessage BuildPushMessage(string chatMessage, string  chatType, string fromResource, string fromUserName, string orderId, string orderBusinessName, 
            string serialNo, string orderStatus, string orderStatStr)
        {
           PushMessage pushMessage = new PushMessage { OrderId = orderId };

            //服务推送消息
            if (chatType =="ReceptionChatNoticeOrder")
            {

                pushMessage.OrderSerialNo = serialNo;
                if (orderStatus == "EndWarranty") { return null; }

                if (orderStatus == "EndCancel" ||
                    orderStatus == "EndRefund" ||
                    orderStatus == "EndIntervention")
                {
                    pushMessage.DisplayContent = string.Format("<订单完成>{0}订单状态已变为{1},快来看看吧", serialNo, orderStatStr);
                }
                else
                {
                    pushMessage.DisplayContent = string.Format("<订单更新>{0}订单状态已变为{1},快来看看吧", serialNo, orderStatStr);

                }

            }
            else if (chatType =="ReceptionChatNoticeSys")
            {
                pushMessage.DisplayContent = System.Text.RegularExpressions.Regex.Replace(chatMessage, @"[\<|\>|\[|\]]", string.Empty);
            }
            else if (chatType == "ReceptionChat" || chatType == "ReceptionChatMedia"
                || chatType == "ReceptionChatPushService")
            {

                switch (fromResource)// member.UserType)
                {
                    case "YDBan_CustomerService":
                        pushMessage.DisplayContent = "[小助理]" + chatMessage;
                        break;
                    case "YDBan_Store":

                        pushMessage.DisplayContent = "[" + orderBusinessName + "]" + chatMessage;
                        break;
                    default:
                        pushMessage.DisplayContent = "[" + fromUserName + "]" + chatMessage;
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
