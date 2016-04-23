using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 第三方支付接口
/// </summary>
namespace Dianzhu.Pay
{
   
    /// <summary>
    /// 接收支付平台回调
    /// </summary>
    public interface IPayCallBack
    {
        /// <summary>
        /// 支付接口回调接口
        /// </summary>
        /// <param name="nvc">回调时的请求参数</param>
        /// <param name="businessOrderId">当初请求的订单ID(支付项ID)</param>
        /// <param name="platformOrderId">平台的订单号</param>
        /// <param name="total_amoun">支付总额</param>
        /// <param name="errMsg">错误消息</param>
        /// <returns>支付是否成功</returns>
        string PayCallBack(object callBackParameters, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMsg);
        //todo: 这两个接口方法要统一成一个.
        // bool PayCallBack(string  callBackParameters, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMsg);
    }    
  

  

  
}
