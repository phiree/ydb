using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace Ydb.PayGateway
{
    public class PayCallBackAliBatch : IPayCallBacBatch
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAliBatch");
        string errMsg;

        public string PayCallBackBatch(object parameters, out string success_details, out string fail_details, out string errMsg)
        {
            errMsg = string.Empty;
            log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
            NameValueCollection coll = new NameValueCollection();

            string[] parameters_str = parameters.ToString().Split('&');
            string[] item;
            for (int i = 0; i < parameters_str.Count(); i++)
            {
                item = parameters_str[i].Split('=');
                coll.Add(item[0], HttpUtility.UrlDecode(item[1], Encoding.UTF8));
            }
            SortedDictionary<string, string> sPara = GetAliRequestGet.GetRequestGet(coll);

            string notify_id = coll["notify_id"];
            string sign = coll["sign"];
            bool isVerified = false;
            try
            {
                isVerified = new Notify().Verify(sPara, notify_id, sign);
            }
            catch (Exception ex)
            {
                log.Error("参数验证结果异常:" + ex.Message.ToString());
            }
            log.Debug("参数验证结果:" + isVerified);
            success_details = string.Empty;
            fail_details = string.Empty;
            if (isVerified)
            {
                string trade_status = coll["trade_status"].ToUpper();
                success_details = coll["success_details"];
                fail_details = coll["fail_details"];
                return "";
            }
            else
            {
                return "";
            }
        }
        

    }
}
