using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;

/// <summary>
/// 第三方支付接口
/// </summary>
namespace Dianzhu.Pay
{
    /// <summary>
    /// 创建支付链接
    /// </summary>
    public interface IPayRequest
    {
        /// <summary>
        /// 支付金额
        /// </summary>
        decimal PayAmount { get; set; }
        /// <summary>
        /// 支付主题
        /// </summary>
        string PaySubject { get; set;}
        /// <summary>
        /// 支付主题前缀
        /// </summary>
        string PaySubjectPre{ get; set;}
        /// <summary>
        /// 支付备注
        /// </summary>
        string PayMemo { get; set; }
        /// <summary>
        /// 支付项ID
        /// </summary>
        string PaymentId { get; set; }
        //创建支付请求
        string CreatePayRequest();
        //支付平台回调 参数验证
             
    }
    /// <summary>
    /// 接收支付平台回调
    /// </summary>
    public interface IPayCallBack
    {
        bool PayCallBack(NameValueCollection nvc, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMsg);
    }
    /// <summary>
    /// 支付宝实现
    /// </summary>
    public class PayAli : IPayRequest

    {  
        public decimal PayAmount{
            get; set;
        }

        public string PaySubjectPre
        {
            get; set;
             
            
        }

        public string PayMemo
        {
            get; set;
        }

        public string PaymentId
        {
            get; set;
        }

        public string PaySubject
        {
            get; set;
        }

        string payment_type, notify_url, return_url,
              show_url
           ;
        public PayAli( decimal payAmount,string paymentId, string paySubject,
            string payment_type,string notify_url,string return_url,
            string show_url
            )
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.payment_type = payment_type;
            this.notify_url = notify_url;
            this.return_url = return_url;
            this.show_url = show_url;
            this.PaymentId = paymentId;

        }

        log4net.ILog log = log4net.LogManager.GetLogger("Diuanzhu.Pay.PayAli");
        public string CreatePayRequest( )
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("seller_id", Config.seller_id);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
            sParaTemp.Add("payment_type", Config.paytype);
            sParaTemp.Add("notify_url", notify_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/notify_url.aspx");
            sParaTemp.Add("return_url", return_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/return_url.aspx");
 
            sParaTemp.Add("out_trade_no", PaymentId);
           
            sParaTemp.Add("subject",PaySubjectPre+ PaySubject);
            sParaTemp.Add("total_fee", string.Format("{0:N2}", PayAmount));
            sParaTemp.Add("show_url",  show_url);
            sParaTemp.Add("body", PayMemo);
            sParaTemp.Add("it_b_pay", string.Empty);
            sParaTemp.Add("extern_token", string.Empty);
            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }

      
    }
    public class PayCallBackAli : IPayCallBack
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAli");
        public bool PayCallBack(NameValueCollection coll,
            out string businessOrderId,out string platformOrderId,out decimal total_amount
            , out string errMsg)
        {
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
            platformOrderId = businessOrderId =errMsg=  string.Empty;
            
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

    public class PayWeChat : IPayRequest
    {
        public decimal PayAmount { get; set; }

        public string PaySubjectPre { get; set; }

        public string PayMemo { get; set; }

        public string PaymentId { get; set; }

        public string PaySubject { get; set; }

        string  notify_url;

        public PayWeChat(decimal payAmount, string paymentId, string paySubject,
            string notify_url,string memo)
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.PayMemo = memo;
            this.notify_url = notify_url;
            this.PaymentId = paymentId;
        }


        public string CreatePayRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("appid", "wxd928d1f351b77449");
            sParaTemp.Add("mch_id", "1304996701");
            sParaTemp.Add("nonce_str",Guid.NewGuid().ToString().Replace("-",""));
            sParaTemp.Add("body", PaySubjectPre + PaySubject);
            sParaTemp.Add("out_trade_no", PaymentId.Replace("-",""));
            sParaTemp.Add("total_fee", string.Format("{0:0}", PayAmount * 100));
            sParaTemp.Add("spbill_create_ip", "192.168.1.173");
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("trade_type", "APP");

            //key=9bc3147c13780388c2a58500966ed564

            string sParaStr = string.Empty;
            string sParStrkey = string.Empty;
            foreach(KeyValuePair<string,string > item in sParaTemp)
            {
                sParaStr += item.Key + "=" + item.Value + "&";
            }
            sParStrkey = sParaStr + "key=9bc3147c13780388c2a58500966ed564";
            MD5 md5Obj = MD5.Create();
            string sParaMd5 = string.Empty;
            byte[] d = md5Obj.ComputeHash(Encoding.GetEncoding("Utf-8").GetBytes(sParStrkey));
            for (int i = 0; i < d.Length; i++)
            {
                sParaMd5 += d[i].ToString("X").ToUpper();
            }            

            //sParaTemp.Add("sign", sParaMd5);

            //建立请求
            string sHtmlText = sParStrkey + "&sign=" + sParaMd5;
            return sHtmlText;
        }

        public bool PayCallBack(NameValueCollection nvc)
        {
            throw new NotImplementedException();
        }

    
    }

    public class PayAlipayApp : IPayRequest
    {
        public decimal PayAmount { get; set; }
        public string PaySubjectPre { get; set; }
        public string PayMemo { get; set; }
        public string PaymentId { get; set; }
        public string PaySubject { get; set; }

        string notify_url;

        public PayAlipayApp(decimal payAmount, string paymentId, string paySubject, string notify_url, string memo)
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.PayMemo = memo;
            this.notify_url = notify_url;
            this.PaymentId = paymentId;
        }


        public string CreatePayRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", "mobile.securitypay.pay");
            sParaTemp.Add("partner", "2088021632422534");
            sParaTemp.Add("_input_charset", "utf-8");
            sParaTemp.Add("sign_type", "RSA");
            sParaTemp.Add("sign", @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDdyOxmayNrwOr821IwUIkLxw2BVVTDDqRD/PRNCnJx/UCCYIVRL7rxXdKMZrSu24m96JNjIYbiUmwEslYnbLMWY3oZr3CGttjiGq10Y2S/tz8FBAvY59ZlxNRF+CMpbii34hHFKkikdC+ave0TN0oqJl3jNYiNN4xA7wqF1bTT4QIDAQAB");

            sParaTemp.Add("app_id", "2016012201112719");
            sParaTemp.Add("seller_id", "jsyk_company@126.com");
            sParaTemp.Add("subject", PaySubjectPre + PaySubject);
            sParaTemp.Add("body", PayMemo);
            sParaTemp.Add("out_trade_no", PaymentId.Replace("-", ""));
            sParaTemp.Add("total_fee", string.Format("{0:N2}", PayAmount));
            sParaTemp.Add("notify_url", notify_url);            

            string sParaStr = string.Empty;
            string sParStrkey = string.Empty;
            foreach (KeyValuePair<string, string> item in sParaTemp)
            {
                sParaStr += item.Key + "=" + item.Value + "&";
            }

            sParaStr = sParaStr.TrimEnd('&');

            return sParaStr;
        }

        public bool PayCallBack(NameValueCollection nvc)
        {
            throw new NotImplementedException();
        }

       
    }
}
