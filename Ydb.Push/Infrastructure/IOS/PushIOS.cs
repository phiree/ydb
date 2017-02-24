using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jd = JdSoft.Apple.Apns.Notifications;
using Ydb.Common;

namespace Ydb.Push
{
    /// <summary>
    /// 客户端推送消息
    /// </summary>

    public class PushIOS : IPushApi
    {
        private string _strCertificateFilePath;

        private string GetCertificateFilePath(PushTargetClient pushTargetClient)
        {
            if (!string.IsNullOrEmpty(_strCertificateFilePath)) return _strCertificateFilePath;
            string fileBasePath = AppDomain.CurrentDomain.BaseDirectory + @"files\";
            string fileName = pushTargetClient == PushTargetClient.PushToBusiness ? "aps_production_Mark_Store.p12" : "aps_production_Mark_Customer.p12";
            _strCertificateFilePath = fileBasePath + fileName;
            return _strCertificateFilePath;
        }

        private log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Push");

        public string Push(PushTargetClient pushType, PushMessage message, string target, int amount)
        {
            log.Debug("开始推送消息:" + message.DisplayContent);
            bool sandbox = false;
            string testDeviceToken = target;
            //Put your PKCS12 .p12 or .pfx filename here.
            // Assumes it is in the same directory as your app
            //开发者测试证书
            string p12File = GetCertificateFilePath(pushType);
            //发布用证书
            // string p12File = "aps_production_identity.p12";
            log.Debug(11);
            //This is the password that you protected your p12File
            //  If you did not use a password, set it as null or an empty string
            string p12FilePassword = "jsyk";
            //Actual Code starts below:
            //--------------------------------
            log.Debug(12);
            jd.NotificationService service = null;
            try
            {
                log.Debug("notificationservice  sandbox:" + sandbox + ",filepath" + p12File);
                service = new jd.NotificationService(sandbox, p12File, p12FilePassword, 1);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Throw(ex.ToString(), log);
                throw;
            }

            service.SendRetries = 5; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds

            log.Debug(1);
            service.Error += new jd.NotificationService.OnError(service_Error);
            log.Debug(2);
            service.NotificationTooLong += new jd.NotificationService.OnNotificationTooLong(service_NotificationTooLong);
            log.Debug(3);
            service.BadDeviceToken += new jd.NotificationService.OnBadDeviceToken(service_BadDeviceToken);
            log.Debug(4);
            service.NotificationFailed += new jd.NotificationService.OnNotificationFailed(service_NotificationFailed);
            log.Debug(5);
            service.NotificationSuccess += new jd.NotificationService.OnNotificationSuccess(service_NotificationSuccess);
            log.Debug(6);
            service.Connecting += new jd.NotificationService.OnConnecting(service_Connecting);
            log.Debug(7);
            service.Connected += new jd.NotificationService.OnConnected(service_Connected);
            log.Debug(8);
            service.Disconnected += new jd.NotificationService.OnDisconnected(service_Disconnected);
            log.Debug(9);
            jd.Notification alertNotification = new jd.Notification(testDeviceToken);
            log.Debug(10);

            //通知内容
            alertNotification.Payload.Alert.Body = message.DisplayContent;

            alertNotification.Payload.Sound = "default";//为空时就是静音
            alertNotification.Payload.Badge = amount;

            //Queue the notification to be sent
            if (service.QueueNotification(alertNotification))
                log.Debug("Notification Queued!");
            else
                log.Debug("Notification Failed to be Queued!");
            service.Close();
            service.Dispose();
            return "OK";
        }

        private void Service_NotificationFailed(object sender, jd.Notification failed)
        {
            log.Error("推送失败.DeviceToken:" + failed.DeviceToken + ";tag" + failed.Tag.ToString());
        }

        private void service_BadDeviceToken(object sender, jd.BadDeviceTokenException ex)
        {
            log.Error(string.Format("Bad Device Token: {0}", ex.Message));
        }

        private void service_Disconnected(object sender)
        {
            log.Debug("Disconnected...");
        }

        private void service_Connected(object sender)
        {
            log.Debug("Connected...");
        }

        private void service_Connecting(object sender)
        {
            log.Debug("Connecting...");
        }

        private void service_NotificationTooLong(object sender, jd.NotificationLengthException ex)
        {
            log.Error(string.Format("Notification Too Long: {0}", ex.Notification.ToString()));
        }

        private void service_NotificationSuccess(object sender, jd.Notification notification)
        {
            log.Debug(string.Format("Notification Success: {0}", notification.ToString()));
        }

        private void service_NotificationFailed(object sender, jd.Notification notification)
        {
            log.Error(string.Format("Notification Failed: {0}", notification.ToString()));
        }

        private void service_Error(object sender, Exception ex)
        {
            log.Error(string.Format("Error: {0}", ex.Message));
        }
    }
}