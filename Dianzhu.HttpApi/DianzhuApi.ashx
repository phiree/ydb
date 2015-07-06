<%@ WebHandler Language="C#" Class="DianzhuApi" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Web.SessionState;
public class DianzhuApi : IHttpHandler,IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        string jsonStr = new StreamReader(context.Request.InputStream).ReadToEnd();
        BaseRequest request = JsonConvert.DeserializeObject<BaseRequest>(jsonStr);
        BaseResponse response = ResponseFactory.GetApiResponse(request);
        string jsonResponse = JsonConvert.SerializeObject(response);
        context.Response.Write(jsonResponse);
    }
    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }
}