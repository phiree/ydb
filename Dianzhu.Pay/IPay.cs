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
        decimal PayAmount { get; set; }
        string PaySubject { get; set;}
        string PaySubjectPre{ get; set;}
        string PayMemo { get; set; }
        string OrderId { get; set; }
        //创建支付请求
        string CreatePayRequest();
        //保存支付记录

      
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

        public string OrderId
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
        public PayAli( decimal payAmount,string orderId,string paySubject,
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


            sParaTemp.Add("out_trade_no",OrderId);
           
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
}
