<%@ WebHandler Language="C#" Class="RequestRestfulApi" %>

using System;
using System.Web;
using RestSharp;
using System.Text;
using System.Security.Cryptography;

public class RequestRestfulApi : IHttpHandler,System.Web.SessionState.IRequiresSessionState{

    public void ProcessRequest(HttpContext context)
    {
        //权限判断
        if (context.Session["UserName"]==null)
        {
            //context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\",\"data\":\""+service.Enabled+"\"}"); 
            //context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\"}");
            //context.Response.Write("unlogin");
            context.Response.Write("{\"result\":\""+false+"\",\"msg\":\"unlogin\"}");
            return;
        }

        //string strUser = context.Session["UserName"].ToString();
        //GET = 0,
        //POST = 1,
        //PUT = 2,
        //DELETE = 3,
        //HEAD = 4,
        //OPTIONS = 5,
        //PATCH = 6,
        //MERGE = 7
        string APPId = "ABc907a34381Cd436eBfed1F90ac8f823b";
        string APIKey = "2bdKTgh9SiNlGnSajt4E6c4w1ZoZJfb9ATKrzCZ1a3A=";
        string strUrl = "http://112.74.198.215:8041/api/v1";
        string strRequestUrl = strUrl+context.Request["url"].ToString();
        if (strRequestUrl == "")
        {  context.Response.Write("{\"result\":\""+false+"\",\"msg\":\"unlogin\"}");
            return;
        }
        string intMethod = context.Request["method"].ToString();
        Method RequestMethod =(Method)int.Parse(intMethod);
        string requestUri = System.Web.HttpUtility.UrlEncode(strRequestUrl);
        string strRequestContent = context.Request["content"].ToString();
        string strRequestToken= context.Request["token"].ToString();

        var client = new RestClient(strRequestUrl);
        var request = new RestRequest(Method.POST);

        request.AddHeader("content-type", "application/json");
        request.AddHeader("appname", APPId);
        DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSpan = DateTime.UtcNow - epochStart;
        string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalMilliseconds).ToString();
        request.AddHeader("stamp_times", requestTimeStamp);
        string requestContentBase64String = "";
        if (strRequestContent != "")
        {
            request.AddParameter("application/json", strRequestContent, ParameterType.RequestBody);

            byte[] byteData = UTF8Encoding.UTF8.GetBytes(strRequestContent);

            MD5 md5 = MD5.Create();
            byte[] requestContentHash = md5.ComputeHash(byteData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < requestContentHash.Length; i++)
            { sb.Append(requestContentHash[i].ToString("x2")); }
            requestContentBase64String = sb.ToString();
        }
        string signatureRawData = String.Format("{0}{1}{2}{3}{4}", APPId, strRequestToken, requestContentBase64String, requestTimeStamp, requestUri);

        byte[] signature22 = Encoding.UTF8.GetBytes(APIKey);
        byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

        using (HMACSHA256 hmac = new HMACSHA256(signature22))
        {
            byte[] signatureBytes = hmac.ComputeHash(signature);
            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < signatureBytes.Length; i++)
            { sb1.Append(signatureBytes[i].ToString("x2")); }
            byte[] baseBuffer = Encoding.UTF8.GetBytes(sb1.ToString());
            string requestSignatureBase64String = Convert.ToBase64String(baseBuffer);
            request.AddHeader("sign", requestSignatureBase64String);
        }

        IRestResponse response = client.Execute(request);
        context.Response.StatusCode=(int)response.StatusCode;
        context.Response.StatusDescription=response.StatusDescription;
        context.Response.Write(response.Content);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}