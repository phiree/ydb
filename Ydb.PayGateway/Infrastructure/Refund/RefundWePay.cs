 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Ydb.Common;
using Ydb.Common.Infrastructure;
 
namespace Ydb.PayGateway
{
    /// <summary>
    /// 微信app退款接口
    /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_4
    /// </summary>
    public class RefundWePay : IRefundApi
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Order.Infrastructure.RefundWePay");
        public decimal TotalAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public string PlatformTradeNo { get; set; }
         public string OutRefundNo { get; set; }
        public string OperatorId { get; set; }

     
        string nonce_str = Guid.NewGuid().ToString().Replace("-", "");

        IHttpRequest httpRequest;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify_url"></param>
        /// <param name="refundDetail"></param>
        public RefundWePay( string refundNo, decimal refundAmount, 
            string platformTradeNo,decimal totalAmount, string operatorId, IHttpRequest httpRequest)
        {
            this.TotalAmount = totalAmount;
            this.RefundAmount = refundAmount;
            this.PlatformTradeNo = platformTradeNo;
         
            this.OutRefundNo = refundNo;
            this.OperatorId = operatorId;
            this.httpRequest = httpRequest;
          

        }
        public NameValueCollection CreateRefundRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("appid", ConfigWePay.appid);
            sParaTemp.Add("mch_id", ConfigWePay.mch_id);
            sParaTemp.Add("nonce_str", nonce_str);
            sParaTemp.Add("transaction_id", PlatformTradeNo);
            sParaTemp.Add("out_refund_no", OutRefundNo.Replace("-",""));
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

        public bool GetRefundResponse()
        {

            var respDataWeChat = CreateRefundRequest();

            string respDataXmlWechat = "<xml>";

            string sign = string.Empty;
            foreach (string key in respDataWeChat)
            {
                if (key != "sign")
                {
                    respDataXmlWechat += "<" + key + ">" + respDataWeChat[key] + "</" + key + ">";
                }
                else
                {
                    sign = respDataWeChat[key];
                }
            }
            respDataXmlWechat += "<sign>" + sign + "</sign>";
            respDataXmlWechat = respDataXmlWechat + "</xml>";
            log.Debug("请求微信api数据:" + respDataXmlWechat);

            
          
            string returnstrWeChat = httpRequest.CreateHttpRequestPostXml(ConfigWePay.refund_url, respDataXmlWechat, "北京集思优科网络科技有限公司");
            log.Debug("微信返回数据:" + returnstrWeChat);

            string jsonWeChat = JsonHelper.Xml2Json(returnstrWeChat, true);
            RefundReturnWeChat refundReturnWeChat = JsonConvert.DeserializeObject<RefundReturnWeChat>(jsonWeChat);

          

            bool refundSuccess = false;
            if (refundReturnWeChat.return_code.ToUpper() == "SUCCESS")
            {
                if (refundReturnWeChat.result_code.ToUpper() == "SUCCESS")
                {
                    log.Debug("微信返回退款成功");
                    refundSuccess = true;


                    log.Debug("更新微信退款记录");
                    //todo: 在外部更新.
                    //refundWeChat.RefundStatus = enum_RefundStatus.Success;
                    //bllRefund.Update(refundWeChat);
                }
                else
                {
                    log.Error("err_code:" + refundReturnWeChat.err_code + "err_code_des:" + refundReturnWeChat.err_code_des);
                    refundSuccess = false;
                }
            }
            else
            {
                log.Error(refundReturnWeChat.return_msg);
                refundSuccess = false;
            }
            return refundSuccess;
        }
    }
}
