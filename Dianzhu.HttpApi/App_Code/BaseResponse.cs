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
public class BaseResponse
{

    public string protocol_CODE { get; set; }
    public string state_CODE { get; set; }
    public string RespData { get; set; }
    public string err_Msg { get; set; }
    public string stamp_TIMES { get; set; }
    public string serial_NUMBER { get; set; }
    protected BaseRequest request;
    public BaseResponse(BaseRequest request)
    {
        this.request = request;
        this.protocol_CODE = request.protocol_CODE;
        this.serial_NUMBER = request.serial_NUMBER;
        BuildResponse();
        this.stamp_TIMES = GetStampTimes();

    }

    protected virtual void BuildResponse() { }
    protected string GetStampTimes()
    { return (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(); }
}