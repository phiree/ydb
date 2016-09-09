<%@ WebHandler Language="C#" Class="RequestRestfulApi" %>

using System;
using System.Web;
using System.Collections.Generic;
using RestSharp;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

using System.Web.Security;
using Dianzhu.RequestRestful;

public class RequestRestfulApi : IHttpHandler,System.Web.SessionState.IRequiresSessionState{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "application/json";

            //权限判断
            if (!AjaxAuth.authAjaxUser(context)){
                context.Response.StatusCode = 400;
                context.Response.Clear();
                context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"unlogin\"}");
                return;
            }

            string appName = "ABc907a34381Cd436eBfed1F90ac8f823b";
            string appKey = "2bdKTgh9SiNlGnSajt4E6c4w1ZoZJfb9ATKrzCZ1a3A=";

            ISetRequestParams srp = new SetRequestParams();
            RequestParams param = srp.SetParamByRequestInfo(context, appName, appKey, "");

            IRequestRestful req = new RequestRestful();
            RequestResponse res = req.RequestRestfulApi(param);

            //GET = 0,
            //POST = 1,
            //PUT = 2,
            //DELETE = 3,
            //HEAD = 4,
            //OPTIONS = 5,
            //PATCH = 6,
            //MERGE = 7
            //    string strData = "";
            //    using (var reader = new System.IO.StreamReader(context.Request.InputStream))
            //    {
            //        String xmlData = reader.ReadToEnd();
            //        if (string.IsNullOrEmpty(xmlData))
            //        {
            //            context.Response.StatusCode = 400;
            //            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"没有请求参数!\"}");
            //            return;
            //        }
            //        strData = xmlData;
            //    }
            //    Dictionary<string, string> allowedApps = new Dictionary<string, string>();
            //    Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(strData);
            //    string APPId = "ABc907a34381Cd436eBfed1F90ac8f823b";
            //    string APIKey = "2bdKTgh9SiNlGnSajt4E6c4w1ZoZJfb9ATKrzCZ1a3A=";
            //    //string APPId = "UI3f4185e97b3E4a4496594eA3b904d60d";
            //    //string APIKey = "NoJBn3npJIvre2fC2SQL5aQGNB/3l73XXSqNZYdY6HU=";
            //    //string strUrl = "http://112.74.198.215:8041/api/v1;
            //    //strUrl = "http://localhost:52553/api/v1/";
            //    //string strUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority; 

            //    string apiurl = jo["apiurl"].ToString();//context.Request.Form["apiurl"]??"";
            //    if (apiurl == "")
            //    {
            //        context.Response.StatusCode = 400;
            //        context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求的接口路径(apiurl)不能为空!\"}");
            //        return;
            //    }
            //    string strRequestUrl = apiurl;
            //    string intMethod = jo["method"].ToString();//context.Request.Form["method"] ?? "";
            //    if (intMethod == "")
            //    {
            //        context.Response.StatusCode = 400;
            //        context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求方式(method)不能为空!\"}");
            //        return;
            //    }
            //    if (intMethod != "0" && intMethod != "1" && intMethod != "2" && intMethod != "3" && intMethod != "6")
            //    {
            //        context.Response.StatusCode = 400;
            //        context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求方式(method)错误!\"}");
            //        return;
            //    }

            //    Method RequestMethod = (Method)int.Parse(intMethod);
            //    string requestUri = System.Web.HttpUtility.UrlEncode(strRequestUrl);
            //    string strRequestContent = jo["content"].ToString();//context.Request.Form["content"] ?? "";
            //    string strRequestToken = jo["token"].ToString();//context.Request.Form["token"] ?? "";

            //    var client = new RestClient(strRequestUrl);
            //    var request = new RestRequest(RequestMethod);

            //    if (intMethod == "1" && (apiurl.ToLower() == "authorization" || apiurl.ToLower() == "customers" || apiurl.ToLower() == "merchants" || apiurl.ToLower() == "customer3rds"))
            //    {
            //    }
            //    else
            //    {
            //        if (strRequestToken == "")
            //        {
            //            context.Response.StatusCode = 400;
            //            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求的token值不能为空！\"}");
            //            return;
            //        }
            //        request.AddHeader("token", strRequestToken);
            //    }


            //    request.AddHeader("content-type", "application/json");
            //    request.AddHeader("appname", APPId);
            //    DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            //    TimeSpan timeSpan = DateTime.UtcNow - epochStart;
            //    string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalMilliseconds).ToString();
            //    request.AddHeader("stamp_times", requestTimeStamp);
            //    string requestContentBase64String = "";
            //    if (strRequestContent != "")
            //    {
            //        request.AddParameter("application/json", strRequestContent, ParameterType.RequestBody);

            //        byte[] byteData = UTF8Encoding.UTF8.GetBytes(strRequestContent);

            //        MD5 md5 = MD5.Create();
            //        byte[] requestContentHash = md5.ComputeHash(byteData);
            //        StringBuilder sb = new StringBuilder();
            //        for (int i = 0; i < requestContentHash.Length; i++)
            //        { sb.Append(requestContentHash[i].ToString("x2")); }
            //        requestContentBase64String = sb.ToString();
            //    }
            //    string signatureRawData = String.Format("{0}{1}{2}{3}{4}", APPId, strRequestToken, requestContentBase64String, requestTimeStamp, requestUri);

            //    byte[] signature22 = Encoding.UTF8.GetBytes(APIKey);
            //    byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

            //    using (HMACSHA256 hmac = new HMACSHA256(signature22))
            //    {
            //        byte[] signatureBytes = hmac.ComputeHash(signature);
            //        StringBuilder sb1 = new StringBuilder();
            //        for (int i = 0; i < signatureBytes.Length; i++)
            //        { sb1.Append(signatureBytes[i].ToString("x2")); }
            //        byte[] baseBuffer = Encoding.UTF8.GetBytes(sb1.ToString());
            //        string requestSignatureBase64String = Convert.ToBase64String(baseBuffer);
            //        request.AddHeader("sign", requestSignatureBase64String);
            //    }

            //    IRestResponse response = client.Execute(request);
            //    //context.Response.StatusCode=(int)response.StatusCode;
            //    //context.Response.StatusDescription=response.StatusDescription;
            //    //context.Response.Write(response.Content);
            //if ((int)response.StatusCode == 200)
            if (res.code)
            {
                context.Response.StatusCode = 200;
                context.Response.Write("{\"result\":\"" + true + "\",\"msg\":\"Restful接口访问成功！\",\"data\":" + res.data + "}");
            }
            else
            {
                //string str = response.Content.ToString() == "" ? "{}" : response.Content;
                context.Response.StatusCode = 400;
                context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"Restful接口访问失败！\",\"data\":" + res.data + "}");
                //" + response.Content.ToString()==""?"{}":response.Content + "
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400;
            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求异常!" + ex.Message + "\"}");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}