﻿using Com.Alipay;
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
        public bool GetRefundResponse(string refundNo,string callbackUrl,decimal amount,string platformTradeNo,string paymentId)
        {
            string refund_no = refundNo;// DateTime.Now.ToString("yyyyMMdd") + refundAliApp.Id.ToString().Substring(0, 10);

            IRefund iRefundAliApp = new RefundAliApp(
                callbackUrl,// Dianzhu.Config.Config.GetAppSetting("PaySite") + "RefundCallBack/Alipay/notify_url.aspx",
                refund_no, 
                amount,// refundAliApp.RefundAmount,
                platformTradeNo,//refundAliApp.PlatformTradeNo,
                paymentId,// refundAliApp.Payment.Id.ToString(),
                string.Empty);
            var respDataAliApp = iRefundAliApp.CreateRefundRequest();

            string respDataStrAliApp = string.Empty;
            foreach (string key in respDataAliApp)
            {
                respDataStrAliApp += key + "=" + respDataAliApp[key] + "&";
            }
            respDataStrAliApp = respDataStrAliApp.TrimEnd('&');
            log.Debug("支付宝退款请求参数:" + respDataStrAliApp);

            #region 保存退款请求数据
            BLLRefundLog bllRefundLogAliApp = new BLLRefundLog();
            RefundLog refundLogAliApp = new RefundLog(respDataStrAliApp, refundAliApp.Id, refundAliApp.RefundAmount, enum_PaylogType.ResultNotifyFromAli, enum_PayType.Online);
            bllRefundLogAliApp.Save(refundLogAliApp);
            #endregion

            string url_AliApp = "https://mapi.alipay.com/gateway.do";
            string returnstrAliApp = HttpHelper.CreateHttpRequest(url_AliApp, "post", respDataAliApp, Encoding.Default);
            log.Debug("支付宝返回数据:" + returnstrAliApp);

            #region 保存退款返回数据，这里是同步数据，异步数据的在notify中处理
            refundLogAliApp = new RefundLog(returnstrAliApp, refundAliApp.Id, refundAliApp.RefundAmount, enum_PaylogType.ResultReturnFromAli, enum_PayType.Online);
            bllRefundLogAliApp.Save(refundLogAliApp);
            #endregion

            string jsonAliApp = JsonHelper.Xml2Json(returnstrAliApp, true);
            RefundReturnAliApp refundReturnAliApp = JsonConvert.DeserializeObject<RefundReturnAliApp>(jsonAliApp);

            if (refundReturnAliApp.is_success.ToUpper() == "T")
            {
                log.Debug("支付宝返回成功");
                isRefund = true;


                log.Debug("更新支付宝退款记录");
                refundAliApp.RefundStatus = enum_RefundStatus.Success;
                bllRefund.Update(refundAliApp);
            }
            else
            {
                log.Error("错误提示:" + refundReturnAliApp.error);
                isRefund = false;
            }
            return isRefund;
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
