using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using Ydb.PayGateway.DomainModel.Pay;

namespace Ydb.PayGateway
{
    #region 支付宝网页
    /// <summary>
    /// 支付宝实现
    /// </summary>
    public class PayAli : IPayRequest

    {
        public decimal PayAmount
        {
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
        public PayAli(decimal payAmount, string paymentId, string paySubject,
            string payment_type, string notify_url, string return_url,
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
        public string CreatePayRequest()
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

            sParaTemp.Add("subject", PaySubjectPre + PaySubject);
            sParaTemp.Add("total_fee", string.Format("{0:N2}", PayAmount));
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("body", PayMemo);
            sParaTemp.Add("it_b_pay", string.Empty);
            sParaTemp.Add("extern_token", string.Empty);
            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }

        public string CreatePayStr(string str)
        {
            return string.Empty;
        }
    }
    
    #endregion
}
