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
    public interface IPushApi
    {
       
        /// <summary>
        /// 推送接口
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="target">目标地址.</param>
        /// <returns>推送结果 </returns>
        string Push( string target,int amount);
    }
    public class PushFactory
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Push.PushFactory");
        public static IPushApi Create(PushType pushType, string type,PushMessage pushMessage)
        {
            string errMsg;
            switch (type.ToLower())
            {
                case "android":
                    if (string.IsNullOrEmpty(pushMessage.OrderId))
                    {
                        errMsg = "安卓推送,请传入orderid";
                        log.Error(errMsg);
                        throw new Exception(errMsg);
                    }
                    return new XMPush.XMPush(pushType,pushMessage);

                case "ios":
                    return new PushIOS(pushType,pushMessage);
               
                default:
                      errMsg = "未知的推送类型";  
                    log.Error(errMsg);
                    throw new Exception(errMsg);
            }
            
        }
    }
    
    
}
