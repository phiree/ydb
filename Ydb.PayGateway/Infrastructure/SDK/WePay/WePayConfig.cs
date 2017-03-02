using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace Ydb.PayGateway
{
    public class ConfigWePay
    {
        #region 字段
 
        public readonly static string appid = "wx11ee76c3c2104b41";
        public readonly static string mch_id = "1437565802";//商户号
        public readonly static string key = "lZ16HklmBTXaRnf1PC9afrhvz4rxdJvA";
 
        public readonly static string refund_url = "https://api.mch.weixin.qq.com/secapi/pay/refund";

        #endregion

    }
}