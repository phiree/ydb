using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using Ydb.Common.Infrastructure;
using Ydb.PayGateway.DomainModel.Pay;

namespace Ydb.PayGateway
{
    #region WePay
    
    public class PayCallBackWePay : IPayCallBackSingle
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackWePay");
        public bool DoIgnore(object callbackParameters)
        {
            string json = JsonHelper.Xml2Json((string)callbackParameters, true);

            CallBack callbackParameter = JsonConvert.DeserializeObject<CallBack>(json);
            string trade_status = callbackParameter.result_code.ToUpper();
            if (trade_status == "SUCCESS" )

            {
                log.Debug("继续处理");
                return false;
            }
            else  
            {
                log.Debug("忽略,直接返回");
                return true;
            }
 
        }
        public bool ParseBusinessData(object callBackParameters, out string payedStatus, out string businessOrderId, out string platformOrderId, out decimal total_amount, out string errMsg)
        {
           
            businessOrderId = string.Empty;
            platformOrderId = string.Empty;
            total_amount = 0;
            errMsg = string.Empty;

            string json = JsonHelper.Xml2Json((string)callBackParameters, true);

            CallBack callbackParameter = JsonConvert.DeserializeObject<CallBack>(json);
         
            string result_code = payedStatus=callbackParameter.result_code;
             
                if (result_code.ToUpper()== "SUCCESS")
                {
                    businessOrderId = callbackParameter.out_trade_no;
                    platformOrderId = callbackParameter.transaction_id;
                    total_amount = Convert.ToDecimal(callbackParameter.total_fee);
                    //更新订单 发送订单状态变更通知
                    payedStatus= "TRADE_SUCCESS";

                }
                else if (result_code.ToUpper() == "FAIL")
                {
                    return false;
                }
                else {
                    errMsg = "业务操作失败" + callbackParameter.err_code + ":" + callbackParameter.err_code_des;
                    log.Error(errMsg);
                    payedStatus= "FAIL";
                    return false;
                }

                businessOrderId = callbackParameter.out_trade_no;
                platformOrderId = callbackParameter.transaction_id;
                total_amount = Convert.ToDecimal(callbackParameter.total_fee) / 100;
                errMsg = callbackParameter.err_code + ":" + callbackParameter.err_code_des;
                return true;
                 
          
           
        }

        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="businessOrderId"></param>
        /// <param name="platformOrderId"></param>
        /// <param name="total_amoun"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string PayCallBack(NameValueCollection nvc, out string businessOrderId, out string platformOrderId, out decimal total_amount, out string errMsg)
        {
            throw new NotImplementedException();

        }
        public class CallBack
        {

            public string appid { get; set; }
            public string mch_id { get; set; }
            public string device_info { get; set; }
            public string nonce_str { get; set; }
            public string sign { get; set; }
            public string result_code { get; set; }
            public string result_msg { get; set; }
            public string err_code { get; set; }
            public string err_code_des { get; set; }
            public string openid { get; set; }
            public string is_subscribe { get; set; }
            public string trade_type { get; set; }
            public string bank_type { get; set; }
            public string total_fee { get; set; }
            public string fee_type { get; set; }
            public string cash_fee { get; set; }
            public string cash_fee_type { get; set; }
            public string coupon_fee { get; set; }
            public string transaction_id { get; set; }
            public string out_trade_no { get; set; }
            public string attach { get; set; }
            public string time_end { get; set; }

        }
    }
    #endregion
}
