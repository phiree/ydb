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
    #region WePay
    public class PayWeChat : IPayRequest
    {
        public decimal PayAmount { get; set; }

        public string PaySubjectPre { get; set; }

        public string PayMemo { get; set; }

        public string PaymentId { get; set; }

        public string PaySubject { get; set; }

        string notify_url;

        string nonce_str = Guid.NewGuid().ToString().Replace("-", "");

        public PayWeChat(decimal payAmount, string paymentId, string paySubject,
            string notify_url, string memo)
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.PayMemo = memo;
            this.notify_url = notify_url;
            this.PaymentId = paymentId;
        }

        /// <summary>
        /// 获取prepayID
        /// </summary>
        /// <returns></returns>
        public string CreatePayRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("appid", "wxd928d1f351b77449");
            sParaTemp.Add("mch_id", "1304996701");
            sParaTemp.Add("nonce_str", nonce_str);
            sParaTemp.Add("body", PaySubjectPre + PaySubject);
            sParaTemp.Add("out_trade_no", PaymentId.Replace("-", ""));
            sParaTemp.Add("total_fee", string.Format("{0:0}", PayAmount * 100));
            sParaTemp.Add("spbill_create_ip", "192.168.1.173");
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("trade_type", "APP");

            //key=9bc3147c13780388c2a58500966ed564

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

            //sParaTemp.Add("sign", sParaMd5);

            //建立请求
            string sHtmlText = sParStrkey + "&sign=" + sParaMd5;
            return sHtmlText;
        }

        /// <summary>
        /// 根据prepayId 构造支付连接,供前端app调用
        /// </summary>
        /// <param name="prepayid"></param>
        /// <returns></returns>
        public string CreatePayStr(string prepayid)
        {
            int times = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalSeconds;
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("appid", Config.appid_WePay);
            sParaTemp.Add("partnerid", Config.partnerid_WePay);
            sParaTemp.Add("prepayid", prepayid);
            sParaTemp.Add("package", @"Sign=WXPay");
            sParaTemp.Add("noncestr", nonce_str);
            sParaTemp.Add("timestamp", times.ToString());

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

            string sHtmlText = sParStrkey + "&sign=" + sParaMd5;
            return sHtmlText;
        }

    }
   
    #endregion
}
