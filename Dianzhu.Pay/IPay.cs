using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace Dianzhu.Pay
{
    /// <summary>
    /// 支付接口
    /// </summary>
    public interface IPay
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
        //支付平台回调
        string PayCallBack();      
    }

    public class PayAli : IPay

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

        public string PayCallBack()
        {
            throw new NotImplementedException();
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

    public class PayWeChat : IPay
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

        public string PayCallBack()
        {
            throw new NotImplementedException();
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
}
