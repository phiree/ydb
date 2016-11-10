using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Push;
using Dianzhu.IDAL;
using Dianzhu.Model.Enums;
using log4net;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Dianzhu.BLL
{

    /// <summary>
    /// 负责消息推送.
    /// </summary>

    public class BLLPush
    {
        ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.BLLPush");
       
        IDALDeviceBind dalDeviceBind;

        IBLLServiceOrder bllServiceOrder;
        IReceptionSession iimSession;
        IDAL.IDALMembership dalMembership;
        public BLLPush( IDALDeviceBind dalDeviceBind, BLL.IBLLServiceOrder bllServiceOrder, IReceptionSession iimSession, IDAL.IDALMembership dalMembership)

        {
          
            this.dalDeviceBind = dalDeviceBind;
            this.bllServiceOrder = bllServiceOrder;
            this.iimSession = iimSession;
            this.dalMembership = dalMembership;
        }



        public void Push(ReceptionChat chat, Guid targetUserId, string orderId)
        {
            //判断用户是否在线的接口
            log.Debug("1");
            var isOnline = iimSession.IsUserOnline(targetUserId.ToString());// dalIMStatus.FindOne(x => x.UserID.ToString() == chat.ToId);
            if (isOnline)
            {
                log.Debug("用户在线,不推送");
                return;
            }
            chat.SetUnread();
            Model.DeviceBind bind = dalDeviceBind.getDevBindByUserID(targetUserId);
            log.Debug("141");
            //用户已注销
            if (bind == null)
            {
                log.Debug("没有找到设备对应的Token,可能是用户已注销登录");
                return;
            }
           
            log.Debug("12");
            string pushMessage = string.Empty;
            PushType pushType;
            string errorLog;
            //判断,  推送内容: 聊天对象, 订单状态
            /*
                <订单更新>xxx订单的状态已变更为xxx,快来看看吧
                <订单完成>xxx订单的状态已变更为xxx,快来看看吧
                <订单完成>推送关于订单变成完成状态的信息，
                例如订单变成了 EndCancel , EndRefound ， EndIntervention等等，app 跳转至已完成列表 
            */
            log.Debug("13");
            if (chat.GetType() == typeof(ReceptionChatNoticeOrder))
            {
                Model.ServiceOrder serviceOrder = bllServiceOrder.GetOne(new Guid(chat.SessionId));
                if (serviceOrder.OrderStatus == enum_OrderStatus.EndWarranty) { return; }
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
                Model.DZMembership member = dalMembership.FindById(new Guid(chat.FromId));
                switch (member.UserType)
                {
                    case enum_UserType.customerservice:
                        pushMessage = "[小助理]" + chat.MessageBody;
                        break;
                    case enum_UserType.business:
                        Model.ServiceOrder serviceOrder = bllServiceOrder.GetOne(new Guid(chat.SessionId));

                        pushMessage = "[" + serviceOrder.ServiceBusinessName + "]" + chat.MessageBody;
                        break;
                    default:
                        pushMessage = "[" + member.UserName + "]" + chat.MessageBody;
                        break;
                }

            }
            else
            {
                log.Debug("未处理的消息类型:" + chat.GetType());
            }
            log.Debug("14");
            
            string deviceName = bind.AppName;
            log.Debug("142");
            if (deviceName.ToLower().Contains("ios")) { deviceName = "ios"; }
            else if (deviceName.ToLower().Contains("android")) { deviceName = "android"; }
            else
            {
                errorLog = "未知的设备类型" + deviceName;
                log.Error(errorLog);
                throw new Exception(errorLog);
            }
            log.Debug("15");
            pushType = PushType.PushToUser;
            //客服->用户, 用户->商户,商户->用户
            if (chat.ToResource == XmppResource.YDBan_Store) { pushType = PushType.PushToBusiness; }
            else if (chat.ToResource == XmppResource.YDBan_User) { pushType = PushType.PushToUser; }
            else
            {
                log.Warn("不需要推送的目标" + chat.Id);
                return;
            }
            log.Debug("16");
            IPush ipush = Dianzhu.Push.PushFactory.Create(pushType, deviceName, orderId);
            int pushAmount = bind.PushAmount + 1;
            bind.PushAmount = pushAmount;
            dalDeviceBind.Update(bind);
            log.Debug("17");
            log.Debug("prepare for push,token:" + bind.AppToken);
            string pushResult = ipush.Push(pushMessage, bind.AppToken, pushAmount);
            log.Debug("推送结果" + pushResult);


        }

    }
}
