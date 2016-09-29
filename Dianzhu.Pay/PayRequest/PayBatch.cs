using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;

namespace Dianzhu.Pay.PayRequest
{
    /// <summary>
    /// 支付宝批量支付到支付宝账户
    /// </summary>
    public class PayBatch : IPayRequest
    {
        public decimal PayAmount { get; set; }
        public string PaySubjectPre { get; set; }
        public string PayMemo { get; set; }
        public string PaymentId { get; set; }
        public string PaySubject { get; set; }
        string notify_url;

        public PayBatch(decimal payAmount, string paymentId, string paySubject, string notify_url, string memo)
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.PayMemo = memo;
            this.notify_url = notify_url;
            this.PaymentId = paymentId;
        }


        log4net.ILog log = log4net.LogManager.GetLogger("Diuanzhu.Pay.PayAli");
        public string CreatePayRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("seller_id", Config.seller_id);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("service", "batch_trans_notify");
            sParaTemp.Add("payment_type", Config.paytype);
            sParaTemp.Add("notify_url", notify_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/notify_url.aspx");
            //sParaTemp.Add("return_url", return_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/return_url.aspx");

            
            sParaTemp.Add("email", Config.seller_email);
            sParaTemp.Add("account_name", Config.seller_name);
            sParaTemp.Add("pay_date", DateTime.Now.ToString("yyyyMMdd"));
            sParaTemp.Add("batch_no", PaymentId);
            sParaTemp.Add("batch_fee", string.Format("{0:N2}", PayAmount));
            sParaTemp.Add("batch_num", PaySubjectPre);
            sParaTemp.Add("detail_data", PaySubject);
           
            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }

        public string CreatePayStr(string str)
        {
            return string.Empty;
        }
    }
}
