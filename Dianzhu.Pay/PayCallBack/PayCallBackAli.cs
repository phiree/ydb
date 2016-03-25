using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

namespace Dianzhu.Pay
{
    #region 支付宝网页
    
    public class PayCallBackAli : IPayCallBack
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAli");



        public bool PayCallBack(object parameters,
            out string businessOrderId, out string platformOrderId, out decimal total_amount
            , out string errMsg)
        {
            NameValueCollection coll = (NameValueCollection)parameters;
            //获取订单号
            string total_fee = coll["total_fee"];            //获取总金额
            string subject = coll["subject"];                //商品名称、订单名称
            string body = coll["body"];                      //商品描述、订单备注、描述
            string buyer_email = coll["buyer_email"];        //买家支付宝账号
                                                             //交易状态   

            string notify_id = coll["notify_id"];
            string sign = coll["sign"];

            SortedDictionary<string, string> sPara = GetRequestGet(coll);

            bool isVerified = new Notify().Verify(sPara, notify_id, sign);
            platformOrderId = businessOrderId = errMsg = string.Empty;

            total_amount = 0m;

            if (isVerified)
            {
                platformOrderId = coll["trade_no"];              //支付宝交易号
                businessOrderId = coll["out_trade_no"];

                total_amount = Convert.ToDecimal(coll["total_fee"]);
                string trade_status = coll["trade_status"];
                if (trade_status.ToUpper() == "TRADE_FINISHED" || trade_status.ToUpper() == "TRADE_SUCCESS")
                {
                    return true;
                }

                else
                {
                    errMsg = "支付不成功.支付结果为:" + trade_status;
                    log.Error(errMsg);

                    return false;
                }
            }
            else
            {
                errMsg = "支付结果有误.参数验证失败";
                log.Error(errMsg);
                return false;
            }

        }

        private SortedDictionary<string, string> GetRequestGet(NameValueCollection coll)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            //Load Form variables into NameValueCollection variable.
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], coll[requestItem[i]]);
            }

            return sArray;
        }
    }
    #endregion
}
