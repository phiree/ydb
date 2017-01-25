using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Ydb.Order.DomainModel;

namespace Dianzhu.Api.Model
{
    public class ReqDataPY001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string payID { get; set; }
        public string target { get; set; }
    }

    public class RespData_WeChatUserObj
    {
        public string return_code { get; set; }//返回状态码
        public string return_msg { get; set; }//返回信息

        public string appid { get; set; }//公众账号ID,企业号corpid即为此appId
        public string mch_id { get; set; }//商户号
        public string device_info { get; set; }//设备号    
        public string nonce_str { get; set; }//随机字符串
        public string sign { get; set; }//签名    
        public string result_code { get; set; }//业务结果
        public string err_code { get; set; }//错误代码
        public string err_code_des { get; set; }//错误代码描述

        public string trade_type { get; set; }//交易类型
        public string prepay_id { get; set; }//预支付交易会话标识
        public string code_url { get; set; }//二维码链接 
    }

    public class RespDataPY001007
    {
        public string orderString { get; set; }
    }

    public class RespDataPY_payObj
    {
        public string payID { get; set; }
        public string amount { get; set; }
        public string orderID { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string updateTime { get; set; }
        public RespDataPY_payObj Adapt(Payment pay)
        {
            this.payID = pay.Id.ToString();
            this.amount = string.Format("{0:N2}", pay.Amount);
            this.orderID = pay.Order.Id.ToString();
            this.status = pay.Status.ToString();
            this.type = pay.PayTarget.ToString();
            this.updateTime = pay.LastUpdateTime.ToString("yyyyMMddHHmmss");

            return this;
        }
    }

    public class ReqDataPY001008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
    }

    public class RespDataPY001008
    {
        public RespDataPY_payObj payObj { get; set; }
        public RespDataPY001008 Adapt(Payment pay)
        {
            this.payObj = new RespDataPY_payObj().Adapt(pay);
            return this;
        }
    }
}
