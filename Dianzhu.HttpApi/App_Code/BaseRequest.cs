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
    public string protocol_CODE { get; set; }
    public string stamp_TIMES { get; set; }
    public string serial_NUMBER { get; set; }
    public string appToken { get; set; }
    public string appName { get; set; }
    public string Ver { get; set; }
    public JObject ReqData { get; set; }
}