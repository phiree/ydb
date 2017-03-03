using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using Ydb.PayGateway.DomainModel.Pay;

namespace Ydb.PayGateway
{

    public class PayCallBackAliBatch : IPayCallBacBatch
    {
        ICallBackVerify callBackNotify;
        public PayCallBackAliBatch(ICallBackVerify callBackNotify)
        {
            this.callBackNotify = callBackNotify;
        }
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAliBatch");
        string errMsg;
        public bool DoIgnore(object callbackParameters)
        {
            return false; 
            //批量支付只有成功 失败两种状态. 如果成功且打印了succcess,支付宝服务器会停止发送.
            NameValueCollection coll = ParseParameters(callbackParameters.ToString());
            bool isSuccess = coll["success_details"] != null;
            string trade_status = coll["trade_status"].ToUpper();
            log.Debug("返回结果:" + trade_status);
            if (trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED")

            {
                log.Debug("继续处理");
                return false;
            }
            else if (trade_status.ToUpper() == "WAIT_BUYER_PAY")
            {
                log.Debug("忽略,直接返回");
                return true;
            }

            else
            {
                string errMsg = "在指定时间段内未支付.支付结果为:" + trade_status;
                log.Error(errMsg);

                return true;
            }
        }
        public bool ParseBusinessData(object parameters, out string status, out string success_details, out string fail_details, out string errMsg)
        {
            status = string.Empty;
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
                isVerified = callBackNotify.Verify(sPara, notify_id, sign);
            }
            catch (Exception ex)
            {
                log.Error("参数验证结果异常:" + ex.Message.ToString());
            }
            log.Debug("参数验证结果:" + isVerified);
            success_details = string.Empty;
            fail_details = string.Empty;
           
            success_details = coll["success_details"];
            fail_details = coll["fail_details"];
            if (isVerified)
            {
               
                return true;
            }
            else
            {
                return false;
            }
        }
        private NameValueCollection ParseParameters(string parameters)
        {
            NameValueCollection coll = new NameValueCollection();

            string[] parameters_str = parameters.ToString().Split('&');
            string[] item;
            for (int i = 0; i < parameters_str.Count(); i++)
            {
                item = parameters_str[i].Split('=');
                coll.Add(item[0], HttpUtility.UrlDecode(item[1], Encoding.UTF8));
            }
            return coll;
        }


    }
}
