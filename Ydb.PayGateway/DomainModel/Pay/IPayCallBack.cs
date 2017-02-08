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
    /// 接收支付平台回调
    /// </summary>
    public interface IPayCallBack
    {
        
    }
    /// <summary>
    /// 单一支付请求的回调
    /// </summary>
    public interface IPayCallBackSingle: IPayCallBack
    {
        string PayCallBack(object callBackParameters, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMsg);

    }
    /// <summary>
    /// 批量付款(商家提现申请)的回调处理.
    /// </summary>
    public interface IPayCallBacBatch:IPayCallBack
    {
        string PayCallBackBatch(object callBackParameters, out string success_details, out string fail_details, out string errMsg);
    }
  

  

  
}
