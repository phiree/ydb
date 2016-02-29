using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
namespace Dianzhu.Pay
{
    /// <summary>
    /// 第三方支付的退款
    /// https://doc.open.alipay.com/doc2/detail.htm?spm=0.0.0.0.TYM6oi&treeId=66&articleId=103600&docType=1
    /// </summary>
    public interface IRefund
    {
       
        string CreateRefundRequest();
        
    }
    /// <summary>
    /// 支付宝批量有密退款接口
    /// </summary>
    public class RefundAli : IRefund
    {

        IList<RefundDetail> refundDetail;
       
        string notify_url;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify_url"></param>
        /// <param name="refundDetail"></param>
        public RefundAli(string notify_url,IList<RefundDetail> refundDetail)
        {
           
            this.refundDetail = refundDetail;
            this.notify_url = notify_url;
            
        }
        public string CreateRefundRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("service", "refund_fastpay_by_platform_pwd");
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("seller_email", Config.seller_email);
            sParaTemp.Add("refund_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sParaTemp.Add("batch_no", DateTime.Now.ToString("yyyyMMdd00yyyyMMddHHmmss"));
            sParaTemp.Add("batch_num", refundDetail.Count.ToString());
            string detail_data = string.Empty;
            foreach (RefundDetail detail in refundDetail)
            {
                detail_data += detail.ToString();
            }
            detail_data = detail_data.TrimEnd('#');
            sParaTemp.Add("detail_data", detail_data);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }
    }
    public class RefundDetail
    {
        public string TradeNo { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public override string ToString()
        {
            //交易退款数据集的格式为：原付款支付宝交易号^退款总金额^退款理由；
            return TradeNo + "^" + Amount + "^" + Reason+"#";

        }
    }
}
