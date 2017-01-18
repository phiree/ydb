using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Collections.Specialized;
 

namespace Ydb.PayGateway
{
    /// <summary>
    /// 第三方支付的退款
    /// https://doc.open.alipay.com/doc2/detail.htm?spm=0.0.0.0.TYM6oi&treeId=66&articleId=103600&docType=1
    /// </summary>
    public interface IRefundApi
    {
        /// <summary>
        /// 退款金额
        /// </summary>
        decimal RefundAmount { get; set; }
        /// <summary>
        /// 平台交易号
        /// </summary>
        string PlatformTradeNo { get; set; }
      
        /// </summary>
         /// <summary>
        /// 操作员帐号, 默认为商户号
        /// </summary>
        string OperatorId { get; set; }
 

        /// <summary>
        /// 请求退款接口
        /// </summary>
        /// <returns></returns>
        NameValueCollection CreateRefundRequest();
        /// <summary>
        /// 调用支付接口的退款接口.
        /// </summary>
        /// <returns></returns>
        bool GetRefundResponse();


    }


}
