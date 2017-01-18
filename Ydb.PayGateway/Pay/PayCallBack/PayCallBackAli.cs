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
    #region 支付宝网页
    
    public class PayCallBackAli : IPayCallBack
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAli");

        public string PayCallBack(object parameters,
            out string businessOrderId, out string platformOrderId, out decimal total_amount
            , out string errMsg)
        {
            NameValueCollection coll = new NameValueCollection();

            string[] parameters_str = parameters.ToString().Split('&');
            string[] item;
            for (int i = 0; i < parameters_str.Count(); i++)
            {
                item = parameters_str[i].Split('=');
                coll.Add(item[0], HttpUtility.UrlDecode(item[1], Encoding.UTF8));
            }

            //获取订单号
            string total_fee = coll["total_fee"];            //获取总金额
            string subject = coll["subject"];                //商品名称、订单名称
            string body = coll["body"];                      //商品描述、订单备注、描述
            string buyer_email = coll["buyer_email"];        //买家支付宝账号
                                                             //交易状态   

            string notify_id = coll["notify_id"];
            string sign = coll["sign"];

            SortedDictionary<string, string> sPara = GetAliRequestGet.GetRequestGet(coll);
           
            bool isVerified = new Notify().Verify(sPara, notify_id, sign);
            log.Debug("参数验证结果:"+isVerified);
            platformOrderId = businessOrderId = errMsg = string.Empty;

            total_amount = 0m;

            if (isVerified)
            {
                platformOrderId = coll["trade_no"];              //支付宝交易号
                businessOrderId = coll["out_trade_no"];

                total_amount = Convert.ToDecimal(coll["total_fee"]);
                string trade_status = coll["trade_status"];
                log.Debug("交易结果:" + trade_status);
                if (trade_status.ToUpper() == "TRADE_SUCCESS")
                {                   
                    return "TRADE_SUCCESS";
                }
                else if(trade_status.ToUpper() == "WAIT_BUYER_PAY")
                {
                    log.Debug("交易已创建，等待买家付款.支付结果为:" + trade_status);
                    return "WAIT_BUYER_PAY";
                }
                else if(trade_status.ToUpper() == "TRADE_FINISHED")
                {
                    log.Debug("交易成功且结束，不可再做任何操作.支付结果为:" + trade_status);
                    return "TRADE_FINISHED";
                }
                else
                {
                    errMsg = "在指定时间段内未支付.支付结果为:" + trade_status;
                    log.Error(errMsg);

                    return "TRADE_CLOSED";
                }
            }
            else
            {
                errMsg = "支付结果有误.参数验证失败";
                log.Error(errMsg);
                return "ERROR";
            }

        }
       
    }
    #endregion
}
