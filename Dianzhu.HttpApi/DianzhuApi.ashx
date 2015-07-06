<%@ WebHandler Language="C#" Class="DianzhuApi" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Web.SessionState;
public class DianzhuApi : IHttpHandler,IRequiresSessionState
{

    //-----------------------接口--------------------------
    /// <summary>
    /// 接口
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>


    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        string jsonStr = context.Request["protocol_CODE"];
        jsonStr = new StreamReader(context.Request.InputStream).ReadToEnd();
        
        apiRequest request = JsonConvert.DeserializeObject<apiRequest>(jsonStr);
        apiResponse response = new apiResponse(request);

        string jsonResponse = JsonConvert.SerializeObject(response);

        context.Response.Write(jsonResponse);
        
        
        //JsonObject jo = JsonConvert.DeserializeObject<JsonObject>(jsonStr);
        switch (jsonStr)
        {
            case "VCM001003":
                context.Response.Write(DemoJson[0]);
                break;
            case "USM001002":
                context.Response.Write(UserReg());
                break;
            case "USM001003":
                context.Response.Write(UserEdit());
                break;
            default:
                break;
        }
    }

    private string[] DemoJson = {
                                 
    @"{'protocol_CODE': 'SVM001003', 
        'state_CODE': '009000', 
    ' RespData': 
            { 
            'vcsObj' : 
                    { 
                        'vcsID':  '6F9619FF8B86D011B42D00C04FC964FF',
                        'srvBiz':  '阿里巴巴集团', 
                        'srvBizID': '4F9619FF8B86D011B42D00C04FC964FF',
                        'vcsStartTime': '201506162223', 
                        'vcsEndTime': '000000000000', 
                        'vcsMoney': '500',
                        'vcsStatus': 'Ry','vcsExdes': '自带工具,线下结算'
                    } 
            }, 
    'stamp_TIMES': '1490192929335', 
    'serial_NUMBER': '00147001015869149751' }"
                       
};

    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }

    private void BuildAPIRequest(HttpRequest request)
    { 
    }
    public class JsonObject
    {
        public string Protocol_Code { get; set; }
        public ReqData ReqData { get; set; }
        public string Stamp_Times{get;set;}
        public string Serial_Number { get; set; }
    
    }
    
    public class ReqData
    {
        #region USM用户
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPWord { get; set; }
        public string Uid { get; set; }
        public string Alias { get; set; }
        public string Phone { get; set; }
        public string ImgUrl { get; set; }
        public string Address { get; set; }
        public string AppName { get; set; }
        public string AppToken { get; set; }
        #endregion
    }
    protected string UserVerify(string name,string pwd)
    { return ""; }
    protected string UserReg()
    { return ""; }
    protected string UserEdit()
    { return ""; }
}