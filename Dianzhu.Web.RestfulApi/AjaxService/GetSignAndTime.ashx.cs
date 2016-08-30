using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Dianzhu.Web.RestfulApi.AjaxService
{
    /// <summary>
    /// GetSignAndTime 的摘要说明
    /// </summary>
    public class GetSignAndTime : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/json";
                string strData = "";
                using (var reader = new System.IO.StreamReader(context.Request.InputStream))
                {
                    String xmlData = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(xmlData))
                    {
                        context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"没有请求参数!\"}");
                        return;
                    }
                    strData = xmlData;
                }
                Dictionary<string, string> allowedApps = new Dictionary<string, string>();
                Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(strData);
                string APPId = "UI3f4185e97b3E4a4496594eA3b904d60d";
                string APIKey = "NoJBn3npJIvre2fC2SQL5aQGNB/3l73XXSqNZYdY6HU=";
                string strUrl = "http://112.74.198.215:8041";
                strUrl = "http://localhost:52553";
                string apiurl = jo["apiurl"].ToString();//context.Request.Form["apiurl"]??"";
                if (apiurl == "")
                {
                    context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求的接口路径(apiurl)不能为空!\"}");
                    return;
                }
                string strRequestUrl = strUrl + apiurl;
                string intMethod = jo["method"].ToString();//context.Request.Form["method"] ?? "";
                if (intMethod == "")
                {
                    context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求方式(method)不能为空!\"}");
                    return;
                }
                if (intMethod != "0" && intMethod != "1" && intMethod != "2" && intMethod != "3" && intMethod != "6")
                {
                    context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求方式(method)错误!\"}");
                    return;
                }
                string requestUri = System.Web.HttpUtility.UrlEncode(strRequestUrl);
                string strRequestContent = jo["content"].ToString();//context.Request.Form["content"] ?? "";
                string strRequestToken = jo["token"].ToString();//context.Request.Form["token"] ?? "";
                DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = DateTime.UtcNow - epochStart;
                string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalMilliseconds).ToString();
                string requestContentBase64String = "";
                if (strRequestContent != "")
                {
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
                }
                context.Response.Write("{\"result\":\"" + true + "\",\"msg\":\"Restful接口访问成功！\",\"data\":" + "response.Content" + "}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"请求异常!" + ex.Message + "\"}");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}