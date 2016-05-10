using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace Dianzhu.Pay
{
    public class RefundCallBackAli : IRefundCallBack
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.RefundCallBackAli");

        public string RefundCallBack(object parameters, out string businessOrderId, out string platformOrderId, out decimal total_amount, out string errMsg)
        {
            NameValueCollection coll = new NameValueCollection();

            string[] parameters_str = parameters.ToString().Split('&');
            string[] item;
            for (int i = 0; i < parameters_str.Count(); i++)
            {
                item = parameters_str[i].Split('=');
                coll.Add(item[0], HttpUtility.UrlDecode(item[1], Encoding.UTF8));
            }

            //获取参数
            string sign = coll["sign"];                         //签名
            string result_details = coll["result_details"];     //处理结果描述
            string notify_time = coll["notify_time"];           //通知发送时间
            string sign_type = coll["sign_type"];               //签名方式
            string notify_type = coll["notify_type"];           //通知类型
            string notify_id = coll["notify_id"];               //通知校验id
            string batch_no = coll["batch_no"];                 //原请求退款批次号
            string success_num = coll["success_num"];           //退交易成功的笔数            

            SortedDictionary<string, string> sPara = GetRequestGet(coll);
           
            bool isVerified = new Notify().Verify(sPara, notify_id, sign);
            log.Debug("参数验证结果:"+isVerified);
            platformOrderId = businessOrderId = errMsg = string.Empty;

            total_amount = 0m;

            if (isVerified)
            {
                platformOrderId = batch_no;

                string[] details_list = result_details.Split('^');

                total_amount = Convert.ToDecimal(details_list[1]);

                return "SUCCESS";
            }
            else
            {
                errMsg = "支付结果有误.参数验证失败";
                log.Error(errMsg);
                return "ERROR";
            }

        }

        private SortedDictionary<string, string> GetRequestGet(NameValueCollection coll)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            //Load Form variables into NameValueCollection variable.
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], coll[requestItem[i]]);
            }

            return sArray;
        }
    }
}
