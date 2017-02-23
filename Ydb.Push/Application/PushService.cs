using System;
using log4net;
using Ydb.Common.Application;
using Ydb.Push.DomainModel;
using Ydb.Push.DomainModel.IRepository;

namespace Ydb.Push.Application
{
    public class PushService : IPushService
    {
        private readonly ILog log;
        private readonly IPushApiFactory _pushApiFactory;
        private readonly IRepositoryDeviceBind _repoDeviceBind;

        public PushService(IRepositoryDeviceBind repoDeviceBind,
            IPushApiFactory pushApiFactory)
        {
            this._repoDeviceBind = repoDeviceBind;
            this._pushApiFactory = pushApiFactory;
            log = LogManager.GetLogger(GetType().ToString());
        }

        public ActionResult Push(string chatMessage, string chatType, string toUserId, string fromClient,
            string fromUserName,
            string orderId, string orderSerialNo, string orderStatus, string orderStatusStr,
            string businessName, string toClient)
        {
            //判断用户是否在线的接口
            var result = new ActionResult();

            var bind = _repoDeviceBind.getDevBindByUserID(new Guid(toUserId));
            log.Debug("141");
            //用户已注销
            string errMsg;
            if (bind == null)
            {
                errMsg = "没有找到设备对应的Token,可能是用户已注销登录";
                log.Error(errMsg);
                result.ErrMsg = errMsg;
                result.IsSuccess = false;
                return result;
            }

            //判断,  推送内容: 聊天对象, 订单状态
            /*
                <订单更新>xxx订单的状态已变更为xxx,快来看看吧
                <订单完成>xxx订单的状态已变更为xxx,快来看看吧
                <订单完成>推送关于订单变成完成状态的信息，
                例如订单变成了 EndCancel , EndRefound ， EndIntervention等等，app 跳转至已完成列表
            */

            var deviceName = bind.AppName;
            log.Debug("142");
            if (deviceName.ToLower().Contains("ios"))
            {
                deviceName = "ios";
            }
            else if (deviceName.ToLower().Contains("android"))
            {
                deviceName = "android";
            }
            else
            {
                errMsg = "未知的设备类型" + deviceName;
                log.Error(errMsg);
                result.ErrMsg = errMsg;
                result.IsSuccess = false;
                return result;
            }
            log.Debug("15");
            var pushType = PushTargetClient.PushToUser;
            //客服->用户, 用户->商户,商户->用户
            if (toClient == "YDBan_Store") pushType = PushTargetClient.PushToBusiness;
            else if (toClient == "YDBan_User") pushType = PushTargetClient.PushToUser;
            else
                log.Warn("目标客户端有误,忽略");
            log.Debug("16");

            var pushMessage = new PushMessageBuilder().BuildPushMessage(chatMessage, chatType, fromClient, fromUserName,
                orderId, businessName,
                orderSerialNo, orderStatus, orderStatusStr);
            var ipush = _pushApiFactory.Create(pushMessage, pushType, deviceName);
            var pushAmount = bind.PushAmount + 1;
            bind.PushAmount = pushAmount;

            log.Debug("prepare for push,token:" + bind.AppToken);
            var pushResult = ipush.Push(pushType, pushMessage, bind.AppToken, pushAmount);

            return result;
        }

        /// <summary>
        /// 发送系统通知
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="targetUser"></param>
        /// <returns></returns>
    }
}