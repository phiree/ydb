using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jd = JdSoft.Apple.Apns.Notifications;
namespace Dianzhu.Push
{
    /// <summary>
    /// 客户端推送消息
    /// </summary>
   
    
    public class PushIOS:IPush
    {
        
        string strDeviceToken;
        
        
        
        string strCertificateFilePath {
            get {
                switch (pushType)
                {
                    case PushType.UserAndBusiness:

#if Debug
                        log.Debug("使用商户测试版证书");
                        return AppDomain.CurrentDomain.BaseDirectory + @"files\aps_development_Mark_Store.p12";
#endif
                        log.Debug("使用正式版商户证书");
                        return AppDomain.CurrentDomain.BaseDirectory + @"files\aps_production_Mark_Store.p12";

                    case PushType.UserAndCustomerService:
#if Debug
                        log.Debug("使用测试版证书");
                        return AppDomain.CurrentDomain.BaseDirectory + @"files\aps_development_Mark_CustomerService.p12";
#endif
                        log.Debug("使用正式版证书");
                        return AppDomain.CurrentDomain.BaseDirectory + @"files\aps_production_Mark_CustomerService.p12";
                        
                    default:
                        throw new Exception("未知的推送类型");
                        
                }

            }
        }
        PushType pushType;
        /// <summary>
        /// IOS推送类
        /// </summary>
        /// <param name="strDeviceToken">设备token</param>
        /// <param name="isTestCertificate">是测试证书，否则为正式证书</param>
        /// <param name="pushSum"></param>
        /// <param name="notificationSound"></param>
        public PushIOS(PushType pushType)
        {
            this.pushType = pushType;
            
            
        }
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Push");
        public string Push(string strContent,string strDeviceToken)
        {
            log.Debug("开始推送消息:"+strContent);
            bool sandbox = false;

#if DEBUG
             sandbox=true;
#endif
            //Put your device token in here
            //ipod
            // string testDeviceToken = "80b9d1003efc019c70c33ccf943247941bf3f27b05760a71ab7e65bb6be3e071";
            //iphone
            string testDeviceToken = strDeviceToken;
            //Put your PKCS12 .p12 or .pfx filename here.
            // Assumes it is in the same directory as your app
            //开发者测试证书
            string p12File = strCertificateFilePath;
            //发布用证书
            // string p12File = "aps_production_identity.p12";

            //This is the password that you protected your p12File 
            //  If you did not use a password, set it as null or an empty string
            string p12FilePassword = "jsyk";
            //Actual Code starts below:
            //--------------------------------

            
            jd.NotificationService service = new jd.NotificationService(sandbox, strCertificateFilePath, p12FilePassword, 1);

            service.SendRetries = 5; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds

            service.Error += new jd.NotificationService.OnError(service_Error);
            service.NotificationTooLong += new jd.NotificationService.OnNotificationTooLong(service_NotificationTooLong);

            service.BadDeviceToken += new jd.NotificationService.OnBadDeviceToken(service_BadDeviceToken);
            service.NotificationFailed += new jd.NotificationService.OnNotificationFailed(service_NotificationFailed);
            service.NotificationSuccess += new jd.NotificationService.OnNotificationSuccess(service_NotificationSuccess);
            service.Connecting += new jd.NotificationService.OnConnecting(service_Connecting);
            service.Connected += new jd.NotificationService.OnConnected(service_Connected);
            service.Disconnected += new jd.NotificationService.OnDisconnected(service_Disconnected);
            jd.Notification alertNotification = new jd.Notification(testDeviceToken);

            //通知内容
            alertNotification.Payload.Alert.Body = strContent;

            alertNotification.Payload.Sound = "default";//为空时就是静音
            alertNotification.Payload.Badge = 1;

            //Queue the notification to be sent
            if (service.QueueNotification(alertNotification))
               log.Debug("Notification Queued!");
            else
                log.Debug("Notification Failed to be Queued!");
            service.Close();
            service.Dispose();
            return "OK";
        }
        private   void Service_NotificationFailed(object sender, jd.Notification failed)
        {
            log.Error("推送失败.DeviceToken:" + failed.DeviceToken+";tag"+ failed.Tag.ToString());
             
        }

          void service_BadDeviceToken(object sender, jd.BadDeviceTokenException ex)
        {
            log.Error(string.Format("Bad Device Token: {0}", ex.Message));
        }

          void service_Disconnected(object sender)
        {
           log.Debug("Disconnected...");
        }

          void service_Connected(object sender)
        {
            log.Debug("Connected...");
        }

          void service_Connecting(object sender)
        {
            log.Debug("Connecting...");
        }

          void service_NotificationTooLong(object sender, jd.NotificationLengthException ex)
        {

            log.Error(string.Format( "Notification Too Long: {0}", ex.Notification.ToString()));
        }

          void service_NotificationSuccess(object sender, jd.Notification notification)
        {
            log.Debug(string.Format("Notification Success: {0}", notification.ToString()));
        }

          void service_NotificationFailed(object sender, jd.Notification notification)
        {
          log.Error(string.Format("Notification Failed: {0}", notification.ToString()));
        }
 
          void service_Error(object sender, Exception ex)
        {
          log.Error(string.Format("Error: {0}", ex.Message));
        }
    }

   
}
