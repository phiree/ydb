<%@ WebHandler Language="C#" Class="DianzhuApi" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class DianzhuApi : IHttpHandler
{

    //-----------------------接口--------------------------
    /// <summary>
    /// 接口
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>


    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string jsonStr = context.Request.Params["jsonStr"].ToString();
        JsonObject jo = JsonConvert.DeserializeObject<JsonObject>(jsonStr);
        switch(jo.Protocol_Code)
        {
            case "USM001001":
                context.Response.Write(UserVerify(jo.ReqData.UserPhone, jo.ReqData.UserPWord));
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