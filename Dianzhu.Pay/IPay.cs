using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
namespace Dianzhu.Pay
{
    /// <summary>
    /// 支付接口
    /// </summary>
    public interface IPay
    {
        ServiceOrder Order { get; set; }
        //创建支付请求
        string CreatePayRequest(enum_PayTarget payTarget);
        //保存支付记录
      
    }

    public class PayAli : IPay

    {  
        public ServiceOrder Order{
            get; set;
        }
        string payment_type, notify_url, return_url,
              show_url
           ;
        public PayAli( ServiceOrder order,
            string payment_type,string notify_url,string return_url,
            string show_url
            )
        {
            this.Order = order;
            this.payment_type = payment_type;
            this.notify_url = notify_url;
            this.return_url = return_url;
            this.show_url = show_url;
           

        }
        

        public string CreatePayRequest(enum_PayTarget payTarget)
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("seller_id", Config.seller_id);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
            sParaTemp.Add("payment_type", Config.paytype);
            sParaTemp.Add("notify_url", notify_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/notify_url.aspx");
            sParaTemp.Add("return_url", return_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/return_url.aspx");


            sParaTemp.Add("out_trade_no", Order.Id.ToString());
            string subject_pre = string.Empty;
           
            decimal total_fee=0;
            switch (payTarget)
            {
                case enum_PayTarget.Deposit:

                    subject_pre = "[订金]";
                    total_fee = Order.DepositAmount;
                    break;
                case enum_PayTarget.FinalPayment:
                    subject_pre = "[尾款]";
                    total_fee = Order.NegotiateAmount - total_fee;
                    break;
            }
            if (Order.DepositAmount == 0)
            {
                subject_pre = string.Empty;
            }
            sParaTemp.Add("subject",subject_pre+ Order.ServiceName);
            sParaTemp.Add("total_fee", string.Format("{0:N2}", total_fee));
            sParaTemp.Add("show_url",  show_url);
            sParaTemp.Add("body", Order.Memo);
            sParaTemp.Add("it_b_pay", string.Empty);
            sParaTemp.Add("extern_token", string.Empty);
            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }

    }
}
