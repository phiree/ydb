using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
/// <summary>
///ReqData 的摘要说明
/// </summary>
public class BaseRequest
{
    //详情见接口文档
    public string protocol_CODE { get; set; }//接口编码
    public string stamp_TIMES { get; set; }//请求时间戳
    public string serial_NUMBER { get; set; }//流水号
    public string appToken { get; set; }
    public string appName { get; set; }
    public string Ver { get; set; }
    public JObject ReqData { get; set; }//JObject：json对象
}