using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Push;
using Dianzhu.IDAL;
using Dianzhu.Model.Enums;
using log4net;
using Dianzhu.Push;
namespace Dianzhu.BLL
{
    public class BLLPush
    {
        ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.BLLPush");
        IDAL.IDALIMUserStatus dalIMStatus;
        IDALDeviceBind dalDeviceBind;
        public BLLPush(IDAL.IDALIMUserStatus dalIMStatus, IDALDeviceBind dalDeviceBind)
        {
            this.dalIMStatus = dalIMStatus;
            this.dalDeviceBind = dalDeviceBind;
        }
        public void Push(PushType pushType, Guid targetUserId, string orderId)
        {
             
            var toStatus = dalIMStatus.FindOne(x => x.UserID == targetUserId);
            try { log.Debug("clientname:" + toStatus.ClientName); } catch (Exception ex) { }
            log.Debug("推送的目标用户:" + targetUserId);

            if (toStatus.Status == enum_UserStatus.unavailable)
            {
                Model.DeviceBind bind=null;
                log.Debug("begin,bind");
                try
                {
                      bind = dalDeviceBind.getDevBindByUserID(targetUserId);
                }
                catch (Exception ex)
                {
                    PHSuit.ExceptionLoger.ExceptionLog(log, ex);
                }
                string deviceName = bind.AppName;
                log.Debug("begin,bind2");
                log.Debug("bind" + deviceName);
                if (deviceName.ToLower().Contains("ios")) { deviceName = "ios"; }
                else if (deviceName.ToLower().Contains("android")) { deviceName = "android"; }
                else
                {
                    log.Debug("未知的设备类型");
                    throw new Exception("未知的设备类型");
                }
                log.Debug(string.Format("prepare for push,pushtype:{0},devicename:{1},orderid:{2}", pushType, deviceName, orderId));
                IPush ipush = Dianzhu.Push.PushFactory.Create(pushType, deviceName, orderId);
                int pushAmount = bind.PushAmount + 1;
                bind.PushAmount = pushAmount;
                dalDeviceBind.Update(bind);
                log.Debug("prepare for push,token:" + bind.AppToken);
                string pushResult = ipush.Push("您有新的消息", bind.AppToken, pushAmount);
                log.Debug("推送结果" + pushResult);
            }
            else
            {
                log.Info("用户在线,不推送");
            }

        }
    }
}
