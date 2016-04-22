using Com.Alipay;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Dianzhu.Pay.RefundRequest
{
    /// <summary>
    /// 支付宝批量有密退款接口
    /// </summary>
    public class RefundAli : IRefund
    {
        public decimal RefundAmount { get; set; }
        public string PlatformTradeNo { get; set; }
        public string OutTradeNo { get; set; }
        public string OperatorId { get; set; }

        IList<RefundDetail> refundDetail;

        string notify_url;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify_url"></param>
        /// <param name="refundDetail"></param>
        public RefundAli(string notify_url, IList<RefundDetail> refundDetail)
        {

            this.refundDetail = refundDetail;
            this.notify_url = notify_url;

        }
        public NameValueCollection CreateRefundRequest()
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
            //return sHtmlText;
            NameValueCollection collection = new NameValueCollection();
            return collection;
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
            return TradeNo + "^" + Amount + "^" + Reason + "#";

        }
    }
}
