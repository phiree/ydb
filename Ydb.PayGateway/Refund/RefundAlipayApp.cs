 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ydb.Common.Infrastructure;

namespace Ydb.PayGateway
{
    public class RefundAlipayApp : IRefundApi
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Order.Infrastructure.RefundAliApp");
        public decimal RefundAmount { get; set; }
        public string PlatformTradeNo { get; set; }

        public string OperatorId { get; set; }

        public string Batch_no { get; set; }

        public string notify_url;


        IHttpRequest httpRequest;
        
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify_url"></param>
        /// <param name="refundDetail"></param>
        public RefundAlipayApp(string notify_url, string refund_no, decimal refundAmount,
            string platformTradeNo, string operatorId, IHttpRequest httpRequest )
        {
            this.notify_url = notify_url;
            this.RefundAmount = refundAmount;
            this.PlatformTradeNo = platformTradeNo;
            this.OperatorId = operatorId;
            this.Batch_no = refund_no;
            this.httpRequest = httpRequest;
           

        }
        public NameValueCollection CreateRefundRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            string detail_data = PlatformTradeNo + "^" + RefundAmount.ToString("0.00") + "^" + "取消订单";

            sParaTemp.Add("service", "refund_fastpay_by_platform_nopwd");
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("batch_no", Batch_no.Replace("-", ""));
            sParaTemp.Add("refund_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sParaTemp.Add("batch_num", "1");
            sParaTemp.Add("detail_data", detail_data);
            string mysign = AlipaySignature.RSASign(sParaTemp,
                                                      HttpRuntime.AppDomainAppPath + "/files/rsa_private_key.pem",
                                                     Config.input_charset);
            sParaTemp.Add("sign", mysign);
            sParaTemp.Add("sign_type", "RSA");

            NameValueCollection collection = new NameValueCollection();
            foreach (var item in sParaTemp)
            {
                collection.Add(item.Key, item.Value);
            }
            return collection;
        }

        public bool GetRefundResponse()
        {
            string refund_no = Batch_no;


            var respDataAliApp = CreateRefundRequest();

            string respDataStrAliApp = string.Empty;
            foreach (string key in respDataAliApp)
            {
                respDataStrAliApp += key + "=" + respDataAliApp[key] + "&";
            }
            respDataStrAliApp = respDataStrAliApp.TrimEnd('&');
            log.Debug("支付宝退款请求参数:" + respDataStrAliApp);

            #region 日志保存即可 ---保存退款请求数据
            //  BLLRefundLog bllRefundLogAliApp = new BLLRefundLog();
            log.Debug("接口请求数据:" + respDataStrAliApp);

            #endregion

            string url_AliApp = "https://mapi.alipay.com/gateway.do";
            string returnstrAliApp = httpRequest.CreateHttpRequest(url_AliApp, "post", respDataAliApp, Encoding.Default);
            log.Debug("支付宝返回数据:" + returnstrAliApp);


            string jsonAliApp = JsonHelper.Xml2Json(returnstrAliApp, true);
            RefundReturnAliApp refundReturnAliApp = JsonConvert.DeserializeObject<RefundReturnAliApp>(jsonAliApp);
            bool refundResult = false;
            if (refundReturnAliApp.is_success.ToUpper() == "T")
            {
                log.Debug("支付宝返回成功");
                refundResult = true;


                log.Debug("更新支付宝退款记录");
                //todo: 请求结束在后 在外部修改退款记录的状态.
                // refundAliApp.RefundStatus = enum_RefundStatus.Success;
                // bllRefund.Update(refundAliApp);
            }
            else
            {
                log.Error("错误提示:" + refundReturnAliApp.error);
                refundResult = false;
            }
            return refundResult;
        }

    }
}
