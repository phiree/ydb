using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jd = JdSoft.Apple.Apns.Notifications;
namespace Dianzhu.Push.JPush
{
    /// <summary>
    /// 客户端推送消息
    /// </summary>

    public class JPush : IPush
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Push.JPush");
        string orderid;
        public JPush(string orderid)
        {
            this.orderid = orderid;
            log.Debug("-----------JPush开始--------------");


        }
        public string Push(string message,string target)// string alert, string title,string target,string orderid)
        {
            JPushRequest req = JPushRequest.Create(new string[] { "android" }, new string[] { target }, message,"您有一条新消息", orderid);

            string result=  PHSuit.HttpHelper.CreateHttpRequestPostXml(Config.APIUrl,req.ToJson());
            // Newtonsoft.Json.JsonConvert.DeserializeObject<JPushResponse>(result);
            //简单处理
            log.Debug("推送结果" + result);
            log.Debug("-----------JPush结束--------------");
            return result;
        }
    }



}
