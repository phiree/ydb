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
    #region 支付宝网页
    
    public class PayCallBackAli : IPayCallBackSingle
    {
        ICallBackVerify callBackNotify;
        public PayCallBackAli(ICallBackVerify callBackNotify)
        {
            this.callBackNotify = callBackNotify;
        }
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAli");

        public bool DoIgnore(object callbackParameters)
        {
            NameValueCollection coll = ParseParameters(callbackParameters.ToString());
            string trade_status = coll["trade_status"].ToUpper();
            log.Debug("返回结果:" + trade_status);
            if (trade_status== "TRADE_SUCCESS"|| trade_status == "TRADE_FINISHED")

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
              string  errMsg = "在指定时间段内未支付.支付结果为:" + trade_status;
                log.Error(errMsg);

                return true;
            }
        }
        
        public bool ParseBusinessData(object parameters,out string status,
            out string businessOrderId, out string platformOrderId, out decimal total_amount
            , out string errMsg)
        {
            NameValueCollection coll = ParseParameters(parameters.ToString());
  
            string notify_id = coll["notify_id"];
            string sign = coll["sign"];
            SortedDictionary<string, string> sPara = GetAliRequestGet.GetRequestGet(coll);
            bool isVerified = callBackNotify.Verify(sPara, notify_id, sign);


            log.Debug("参数验证结果:"+isVerified);
            platformOrderId = businessOrderId = errMsg = string.Empty;
            total_amount = 0m;
            platformOrderId = coll["trade_no"];              //支付宝交易号
            businessOrderId = coll["out_trade_no"];
            total_amount = Convert.ToDecimal(coll["total_fee"]);
            status = coll["trade_status"].ToUpper();
            if (isVerified)
            {
                return true;
            }
            else
            {
                errMsg = "回调参数验证失败";
                log.Error(errMsg);
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
    #endregion
}
