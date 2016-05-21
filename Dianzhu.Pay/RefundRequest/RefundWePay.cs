using Com.Alipay;
using Dianzhu.Pay.WePay;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Dianzhu.Pay.RefundRequest
{
    /// <summary>
    /// 支付宝app退款接口
    /// </summary>
    public class RefundWePay : IRefund
    {
        public decimal TotalAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public string PlatformTradeNo { get; set; }
        public string OutTradeNo { get; set; }
        public string OutRefundNo { get; set; }
        public string OperatorId { get; set; }

        IList<RefundDetail> refundDetail;

        string notify_url;

        string nonce_str = Guid.NewGuid().ToString().Replace("-", "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify_url"></param>
        /// <param name="refundDetail"></param>
        public RefundWePay(string notify_url, decimal refundAmount,string refundNo, string platformTradeNo, string outTradeNo,string operatorId,decimal totalAmount)
        {
            this.TotalAmount = totalAmount;
            this.RefundAmount = refundAmount;
            this.PlatformTradeNo = platformTradeNo;
            this.OutTradeNo = outTradeNo;
            this.OutRefundNo = refundNo;
            this.OperatorId = operatorId;
            
            this.notify_url = notify_url;

        }
        public NameValueCollection CreateRefundRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("appid", ConfigWePay.appid);
            sParaTemp.Add("mch_id", ConfigWePay.mch_id);
            sParaTemp.Add("nonce_str", nonce_str);
            sParaTemp.Add("transaction_id", PlatformTradeNo);
            sParaTemp.Add("out_trade_no", OutTradeNo.Replace("-",""));
            sParaTemp.Add("out_refund_no", OutRefundNo);
            sParaTemp.Add("total_fee", (TotalAmount*100).ToString("0"));
            sParaTemp.Add("refund_fee", (RefundAmount*100).ToString("0"));
            //sParaTemp.Add("op_user_id", OperatorId);
            sParaTemp.Add("op_user_id", ConfigWePay.mch_id);

            string sParaStr = string.Empty;
            string sParStrkey = string.Empty;
            foreach (KeyValuePair<string, string> item in sParaTemp)
            {
                sParaStr += item.Key + "=" + item.Value + "&";
            }
            sParStrkey = sParaStr + "key=9bc3147c13780388c2a58500966ed564";
            MD5 md5Obj = MD5.Create();
            string sParaMd5 = string.Empty;
            byte[] d = md5Obj.ComputeHash(Encoding.GetEncoding("Utf-8").GetBytes(sParStrkey));
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] > 16)
                {
                    sParaMd5 += d[i].ToString("X").ToUpper();
                }
                else
                {
                    sParaMd5 += "0" + d[i].ToString("X").ToUpper();
                }
            }

            sParaTemp.Add("sign", sParaMd5);

            //建立请求
            //string sHtmlText = sParStrkey + "&sign=" + sParaMd5;
            //return sHtmlText;

            NameValueCollection collection = new NameValueCollection();
            foreach (var item in sParaTemp)
            {
                collection.Add(item.Key, item.Value);
            }

            return collection;
        }
    }
}
