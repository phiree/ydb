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
    public interface IPush
    {
        string Push(string message);
    }
    public class PushIOS:IPush
    {
        string strDeviceToken;
        
        string notificationSound = string.Empty;
        int pushSum;
        string strCertificateFilePath {
            get {
#if DEBUG

                // return   AppDomain.CurrentDomain.BaseDirectory + @"files\aps_development_Mark.p12";
#endif
                return  AppDomain.CurrentDomain.BaseDirectory+ @"files\aps_production_Mark.p12";
            }
        }
        /// <summary>
        /// IOS推送类
        /// </summary>
        /// <param name="strDeviceToken">设备token</param>
        /// <param name="isTestCertificate">是测试证书，否则为正式证书</param>
        /// <param name="pushSum"></param>
        /// <param name="notificationSound"></param>
        public PushIOS(string strDeviceToken , int pushSum, string notificationSound)
        {

            this.strDeviceToken = strDeviceToken;
            this.pushSum = pushSum;
            this.notificationSound = notificationSound;
        }

        public string Push(string strContent)
        {
            bool sandbox = false;

#if DEBUG
           // sandbox=true;
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

            alertNotification.Payload.Sound = "";//为空时就是静音
            alertNotification.Payload.Badge = pushSum;

            //Queue the notification to be sent
            if (service.QueueNotification(alertNotification))
                Console.WriteLine("Notification Queued!");
            else
                Console.WriteLine("Notification Failed to be Queued!");
            service.Close();
            service.Dispose();
            return "OK";
        }
        private   void Service_NotificationFailed(object sender, jd.Notification failed)
        {
            throw new NotImplementedException();
        }

          void service_BadDeviceToken(object sender, jd.BadDeviceTokenException ex)
        {
            Console.WriteLine("Bad Device Token: {0}", ex.Message);
        }

          void service_Disconnected(object sender)
        {
            Console.WriteLine("Disconnected...");
        }

          void service_Connected(object sender)
        {
            Console.WriteLine("Connected...");
        }

          void service_Connecting(object sender)
        {
            Console.WriteLine("Connecting...");
        }

          void service_NotificationTooLong(object sender, jd.NotificationLengthException ex)
        {
            Console.WriteLine(string.Format("Notification Too Long: {0}", ex.Notification.ToString()));
        }

          void service_NotificationSuccess(object sender, jd.Notification notification)
        {
            Console.WriteLine(string.Format("Notification Success: {0}", notification.ToString()));
        }

          void service_NotificationFailed(object sender, jd.Notification notification)
        {
            Console.WriteLine(string.Format("Notification Failed: {0}", notification.ToString()));
        }
 
          void service_Error(object sender, Exception ex)
        {
            Console.WriteLine(string.Format("Error: {0}", ex.Message));
        }
    }

    public class PushDemoClient : IPush
    {
        public string Push(string message)
        {
            return string.Empty;
        }
    }
}
