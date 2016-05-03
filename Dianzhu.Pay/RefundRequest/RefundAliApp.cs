using Com.Alipay;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Dianzhu.Pay.RefundRequest
{
    /// <summary>
    /// 支付宝app退款接口
    /// </summary>
    public class RefundAliApp : IRefund
    {
        public decimal RefundAmount { get; set; }
        public string PlatformTradeNo { get; set; }
        public string OutTradeNo { get; set; }
        public string OperatorId { get; set; }
        
        string notify_url;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify_url"></param>
        /// <param name="refundDetail"></param>
        public RefundAliApp(string notify_url, decimal refundAmount, string platformTradeNo, string outTradeNo,string operatorId)
        {
            this.RefundAmount = refundAmount;
            this.PlatformTradeNo = platformTradeNo;
            this.OutTradeNo = outTradeNo;
            this.OperatorId = operatorId;
            
            this.notify_url = notify_url;

        }
        public NameValueCollection CreateRefundRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            
            sParaTemp.Add("app_id", Config.app_id);
            sParaTemp.Add("method", "alipay.trade.refund");
            sParaTemp.Add("charset", Config.input_charset.ToLower());
            sParaTemp.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sParaTemp.Add("version", "1.0");
            sParaTemp.Add("sign_type", "RSA");

            SortedDictionary<string, object> biz_content_dic = new SortedDictionary<string, object>();
            //biz_content_dic.Add("out_trade_no", OutTradeNo.Replace("-", ""));
            biz_content_dic.Add("trade_no", PlatformTradeNo);
            biz_content_dic.Add("refund_amount", RefundAmount.ToString("0.00"));
            biz_content_dic.Add("refund_reason", "正常退款");
            //biz_content_dic.Add("operator_id", OperatorId);

            JavaScriptSerializer json = new JavaScriptSerializer();
            string biz_content_str = json.Serialize(biz_content_dic);

            //string biz_content_str = string.Empty;
            //biz_content_str += "{\"refund_amount\":" + RefundAmount.ToString("0.00") + ",\"trade_no\":\"" + PlatformTradeNo + "\"}";

            sParaTemp.Add("biz_content", biz_content_str);

            string mysign = AlipaySignature.RSASign(sParaTemp, HttpRuntime.AppDomainAppPath + "/files/rsa_private_key.pem", Config.input_charset);
            sParaTemp.Add("sign", System.Web.HttpUtility.UrlEncode(mysign));            

            //string sParaStr = AlipaySignature.GetSignContent(sParaTemp);

            //return sParaStr;
            NameValueCollection collection = new NameValueCollection();
            foreach(var item in sParaTemp)
            {
                collection.Add(item.Key, item.Value);
            }
            return collection;
        }
    }
}
