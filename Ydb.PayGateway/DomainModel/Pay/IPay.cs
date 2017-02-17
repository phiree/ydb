using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 第三方支付接口
/// </summary>
namespace Ydb.PayGateway.DomainModel.Pay
{
    /// <summary>
    /// 创建支付链接
    /// </summary>
    public interface IPayRequest
    {
        /// <summary>
        /// 支付金额
        /// </summary>
        decimal PayAmount { get; set; }
        /// <summary>
        /// 支付主题
        /// </summary>
        string PaySubject { get; set;}
        /// <summary>
        /// 支付主题前缀
        /// </summary>
        string PaySubjectPre{ get; set;}
        /// <summary>
        /// 支付备注
        /// </summary>
        string PayMemo { get; set; }
        /// <summary>
        /// 支付项ID
        /// </summary>
        string PaymentId { get; set; }
        //创建支付请求
        string CreatePayRequest();
     
        //创建支付请求字符串
        string CreatePayStr(string str);
    }
 
  

  

  
}
