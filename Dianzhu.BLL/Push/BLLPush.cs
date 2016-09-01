using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Push;
using Dianzhu.IDAL;
using Dianzhu.Model.Enums;
using log4net;
namespace Dianzhu.BLL
{
  public   class BLLPush
    {
        ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.BLLPush");
        IDAL.IDALIMUserStatus dalIMStatus;
        IDALDeviceBind dalDeviceBind;
        public BLLPush(IDAL.IDALIMUserStatus dalIMStatus,IDALDeviceBind dalDeviceBind)
        {
            this.dalIMStatus = dalIMStatus;
            this.dalDeviceBind = dalDeviceBind;
        }
        public void Push(string strPushType, Guid targetUserId,string orderId)
        {
            PushType pushType;
             
            switch (strPushType.ToLower())
            {
                case "withbusiness": pushType = PushType.UserAndBusiness;break;
                case "withcustomerservice":pushType = PushType.UserAndCustomerService;break;
                default:throw new Exception("该聊天类型不支持推送");
            }
            var toStatus = dalIMStatus.FindOne(x => x.UserID == targetUserId);
            if (toStatus.Status == enum_UserStatus.unavailable)
            {
                Model.DeviceBind bind = dalDeviceBind.getDevBindByUserID(targetUserId);
                string deviceName = bind.AppName;
                if (deviceName.ToLower().Contains("ios")) { deviceName = "ios"; }
                else if (deviceName.ToLower().Contains("android")) { deviceName = "android"; }
                else {
                    throw new Exception("未知的设备类型");
                }

               IPush ipush=  Dianzhu.Push.PushFactory.Create(pushType, deviceName, orderId);
                int pushAmount = bind.PushAmount+1;
                bind.PushAmount = pushAmount;
                dalDeviceBind.Update(bind);
                log.Debug("prepare for push,token:" + bind.AppToken);
               string pushResult=  ipush.Push("您有新的消息", bind.AppToken,pushAmount);
                log.Debug("推送结果" + pushResult);

                }
        }
    }
}
