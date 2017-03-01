using System;
using log4net;
using Ydb.Common;
using jd = JdSoft.Apple.Apns.Notifications;

namespace Ydb.Push
{
    /// <summary>
    ///     客户端推送消息
    /// </summary>
    public class PushIOS : IPushApi
    {
        private readonly bool isDebug;
        private readonly ILog log = LogManager.GetLogger("Ydb.Push.IOSPush");
        private string _strCertificateFilePath;

        public PushIOS(bool isDebug)
        {
            this.isDebug = isDebug;
        }

        public string Push(PushTargetClient pushType, PushMessage message, string target, int amount)
        {
            log.Debug("开始推送消息:" + message.DisplayContent);

            var testDeviceToken = target;
            //Put your PKCS12 .p12 or .pfx filename here.
            // Assumes it is in the same directory as your app
            //开发者测试证书
            var p12File = GetCertificateFilePath(pushType);
            //发布用证书
            // string p12File = "aps_production_identity.p12";
            log.Debug(11);
            //This is the password that you protected your p12File
            //  If you did not use a password, set it as null or an empty string
            var p12FilePassword = "jsyk";
            //Actual Code starts below:
            //--------------------------------
            log.Debug(12);
            jd.NotificationService service = null;
            try
            {
                log.Debug("notificationservice  sandbox:" + isDebug + ",filepath" + p12File);
                service = new jd.NotificationService(isDebug, p12File, p12FilePassword, 1);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Throw(ex.ToString(), log);
                throw;
            }

            service.SendRetries = 5; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds

            log.Debug(1);
            service.Error += service_Error;
            log.Debug(2);
            service.NotificationTooLong += service_NotificationTooLong;
            log.Debug(3);
            service.BadDeviceToken += service_BadDeviceToken;
            log.Debug(4);
            service.NotificationFailed += service_NotificationFailed;
            log.Debug(5);
            service.NotificationSuccess += service_NotificationSuccess;
            log.Debug(6);
            service.Connecting += service_Connecting;
            log.Debug(7);
            service.Connected += service_Connected;
            log.Debug(8);
            service.Disconnected += service_Disconnected;
            log.Debug(9);
            var alertNotification = new jd.Notification(testDeviceToken);
            log.Debug(10);

            //通知内容
            alertNotification.Payload.Alert.Body = message.DisplayContent;

            alertNotification.Payload.Sound = "default"; //为空时就是静音
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

        private string GetCertificateFilePath(PushTargetClient pushTargetClient)
        {
            if (!string.IsNullOrEmpty(_strCertificateFilePath)) return _strCertificateFilePath;
            var fileBasePath = AppDomain.CurrentDomain.BaseDirectory + @"files\";
            var fileName = string.Empty;
            if (pushTargetClient == PushTargetClient.PushToBusiness)
                fileName = isDebug ? "aps_development_Mark_Store.p12" : "aps_production_Mark_Store.p12";
            if (pushTargetClient == PushTargetClient.PushToUser)
                fileName = isDebug ? "aps_development_Mark_Customer.p12" : "aps_production_Mark_Customer.p12";
            _strCertificateFilePath = fileBasePath + fileName;
            return _strCertificateFilePath;
        }

        private void Service_NotificationFailed(object sender, jd.Notification failed)
        {
            log.Error("推送失败.DeviceToken:" + failed.DeviceToken + ";tag" + failed.Tag);
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
            log.Error(string.Format("Notification Too Long: {0}", ex.Notification));
        }

        private void service_NotificationSuccess(object sender, jd.Notification notification)
        {
            log.Debug(string.Format("Notification Success: {0}", notification));
        }

        private void service_NotificationFailed(object sender, jd.Notification notification)
        {
            log.Error(string.Format("Notification Failed: {0}", notification));
        }

        private void service_Error(object sender, Exception ex)
        {
            log.Error(string.Format("Error: {0}", ex.Message));
        }
    }
}