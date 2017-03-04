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
/// 支付宝:https://doc.open.alipay.com/docs/doc.htm?spm=a219a.7629140.0.0.dpUNlK&treeId=193&articleId=105301&docType=1
/// </summary>
namespace Ydb.PayGateway.DomainModel.Pay
{
   
    /// <summary>
    /// 接收支付平台回调
    /// </summary>
    public interface IPayCallBack
    {
        /// <summary>
        /// 是否忽略本次回调
        /// </summary>
        /// <param name="callbackParameters"></param>
        /// <returns></returns>
        bool DoIgnore(object callbackParameters);
    }
    /// <summary>
    /// 单一支付请求的回调
    /// </summary>
    public interface IPayCallBackSingle: IPayCallBack
    {
        
        /// <summary>
        /// 从参数中提取数据,为业务处理做准备
        /// </summary>
        /// <param name="callBackParameters"></param>
        /// <param name="status"></param>
        /// <param name="businessOrderId"></param>
        /// <param name="platformOrderId"></param>
        /// <param name="total_amoun"></param>
        /// <param name="errMsg"></param>
        /// <returns>参数验证结果</returns>
        bool ParseBusinessData(object callBackParameters, out string status, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMsg);
      //  string ParseRequest(object callBackParameters, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMs);

    }
    /// <summary>
    /// 批量付款(商家提现申请)的回调处理.
    /// </summary>
    public interface IPayCallBacBatch:IPayCallBack
    {
        bool ParseBusinessData(object callBackParameters, out string status, out string success_details, out string fail_details, out string errMsg);
    }
  

  

  
}
