using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Infrastructure;

namespace Ydb.Push.DomainModel
{
    public class PushApiFactory : IPushApiFactory
    {
        private IHttpRequest httpRequest;

        public PushApiFactory(IHttpRequest httpRequest)
        {
            this.httpRequest = httpRequest;
        }

        private static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Push.PushFactory");

        public IPushApi Create(PushMessage message, PushTargetClient pushType, string type, bool isDebug)
        {
            string errMsg;
            switch (type.ToLower())
            {
                case "android":
                    if (string.IsNullOrEmpty(message.OrderId))
                    {
                        errMsg = "安卓推送,请传入orderid";
                        log.Error(errMsg);
                        throw new Exception(errMsg);
                    }
                    return new XMPush.XMPush(httpRequest);

                case "ios":
                    return new PushIOS(isDebug);

                default:
                    errMsg = "未知的推送类型";
                    log.Error(errMsg);
                    throw new Exception(errMsg);
            }
        }
    }
}