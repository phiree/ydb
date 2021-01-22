using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
/// <summary>
///ReqData 的摘要说明
/// </summary>
public class BaseResponse
{

    protected log4net.ILog Log;
    public string protocol_CODE { get; set; }
    public string state_CODE { get; set; }
    //public string RespData { get; set; }
    public string err_Msg { get; set; }
    public string stamp_TIMES { get; set; }
    public string serial_NUMBER { get; set; }
    public object RespData { get; set; }
    protected BaseRequest request;
    public BaseResponse(BaseRequest request)
    {
         Log= log4net.LogManager.GetLogger("Ydb.HttpApi."+this.GetType().ToString());
        this.err_Msg = string.Empty;
        this.request = request;
        this.protocol_CODE = request.protocol_CODE;
        this.serial_NUMBER = request.serial_NUMBER;
        BuildRespData();
        this.stamp_TIMES = GetStampTimes();
     //   Dianzhu.DAL.DianzhuUW.EndSession();

    }
    public virtual string BuildJsonResponse()
    {
        string result= JsonConvert.SerializeObject(this );

        return result;
    }
    /// <summary>
    /// 构建返回对象
    /// </summary>
    protected virtual void BuildRespData() { }
    protected string GetStampTimes()
    { return (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(); }
    
}