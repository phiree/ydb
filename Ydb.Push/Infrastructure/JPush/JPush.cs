using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jd = JdSoft.Apple.Apns.Notifications;
using Ydb.Common;
using Ydb.Common.Infrastructure;

namespace Ydb.Push.JPush
{
    /// <summary>
    /// 客户端推送消息
    /// </summary>

    public class JPush : IPushApi
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Push.JPush");
    
        IHttpRequest httpRequest;
        public JPush(IHttpRequest httpRequest)
        { this.httpRequest = httpRequest; }
        public string Push(PushTargetClient targetClient, PushMessage message, string target,int amount)// string alert, string title,string target,string orderid)
        {
            log.Debug(string.Format("anroid 推送开始:messaage:{0} target:{1} amount:{2}",message,target,amount));
            JPushRequest req = JPushRequest.Create(new string[] { "android" }, new string[] { target }, message.DisplayContent,"您有一条新消息", message.OrderId);
            log.Debug("1");
            System.Net.WebHeaderCollection headers = new System.Net.WebHeaderCollection();
            log.Debug("2");
            headers.Add(System.Net.HttpRequestHeader.Authorization,AuthorizationHeader.GenerateAuthorizationValue());
            log.Debug("3,"+AuthorizationHeader.GenerateAuthorizationValue());
            string strHeaders = string.Empty;
            foreach (var headItem in headers)
            {
                
            }
            log.Debug("3");
            string result = string.Empty;
            try
            {
                log.Debug(string.Format("request detail. url:{0},req.tojson{1},headers:{2}", Config.APIUrl, req.ToJson(), string.Empty));
                  result = httpRequest.CreateHttpRequestPostXml(Config.APIUrl, req.ToJson(), headers);
            }
            catch (Exception ex)
            {
               ExceptionHandler.Throw(ex.ToString(),log);
            }
            log.Debug("4");
            // Newtonsoft.Json.JsonConvert.DeserializeObject<JPushResponse>(result);
            //简单处理
            log.Debug("推送结果" + result);
            log.Debug("-----------JPush结束--------------");
            return result;
        }
    }



}
