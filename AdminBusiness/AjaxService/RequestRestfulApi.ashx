<%@ WebHandler Language="C#" Class="RequestRestfulApi" %>

using System;
using System.Web;
using RestSharp;

public class RequestRestfulApi : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
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
        int intMethod = int.Parse(context.Request["method"].ToString());
        Method RequestMethod =(Method)intMethod;
        string strRequestUrl = context.Request["url"].ToString();
        string strRequestContent = context.Request["content"].ToString();

        var client = new RestClient("http://112.74.198.215:8041/api/v1");
        var request = new RestRequest(Method.POST);

        request.AddHeader("content-type", "application/json");
        request.AddHeader("appname", APPId);
        DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSpan = DateTime.UtcNow - epochStart;
        string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalMilliseconds).ToString();
        request.AddHeader("stamp_times", requestTimeStamp);
        request.AddParameter("undefined", "{\n\"username\":\"532991450@qq.com\",\n\"password\":\"123456\"\n}", ParameterType.RequestBody);
        //string strParam = JsonConvert.SerializeObject(request.Parameters);
        ////strParam = "{\n\"loginName\":\"issumao@126.com\",\n\"password\":\"123456\"\n}";
        //strParam = "{\n\"loginName\":\"licdream@126.com\",\n\"password\":\"123456\"\n}";//issumao@126.com
        ////	str	"{\n\"username\":\"532991450@qq.com\",\n\"password\":\"123456\"\n}"	string
        //strParam = "{\n\"loginName\":\"eeee\",\n\"password\":\"eeee\"\n}";
        //byte[] byteData = UTF8Encoding.UTF8.GetBytes(strParam);

        ////byte[] content = await request.Content.ReadAsByteArrayAsync();
        //MD5 md5 = MD5.Create();
        ////Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
        //byte[] requestContentHash = md5.ComputeHash(byteData);
        ////string requestContentBase64String = Convert.ToBase64String(requestContentHash);
        //StringBuilder sb = new StringBuilder();
        //for (int i = 0; i < requestContentHash.Length; i++)
        //{ sb.Append(requestContentHash[i].ToString("x2")); }
        //string  requestContentBase64String = sb.ToString();

        //string signatureRawData = String.Format("{0}{1}{2}{3}{4}", APPId, "", requestContentBase64String, requestTimeStamp, requestUri);

        ////var secretKeyByteArray = Convert.FromBase64String(APIKey);
        //byte[] signature22 = Encoding.UTF8.GetBytes(APIKey);
        //byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

        //using (HMACSHA256 hmac = new HMACSHA256(signature22))
        //{
        //    byte[] signatureBytes = hmac.ComputeHash(signature);
        //    StringBuilder sb1 = new StringBuilder();
        //    for (int i = 0; i < signatureBytes.Length; i++)
        //    { sb1.Append(signatureBytes[i].ToString("x2")); }
        //    byte[] baseBuffer = Encoding.UTF8.GetBytes(sb1.ToString());
        //    string requestSignatureBase64String = Convert.ToBase64String(baseBuffer);
        //    request.AddHeader("sign", requestSignatureBase64String);
        //}




        // Set the content length in the request headers  
        //request.ContentLength = byteData.Length;
        //// Write data  
        //using (Stream postStream = request.GetRequestStream())
        //{
        //    postStream.Write(byteData, 0, byteData.Length);
        //}

        //if (request.Parameters != null)
        //{

        //    request.ContentLength = byteData.Length;

        //    // Write data  
        //    using (Stream postStream = request.GetRequestStream())
        //    {
        //        postStream.Write(byteData, 0, byteData.Length);
        //    }
        //    byte[] content = await request.Parameters.ReadAsByteArrayAsync();
        //    MD5 md5 = MD5.Create();
        //    //Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
        //    byte[] requestContentHash = md5.ComputeHash(content);
        //    requestContentBase64String = Convert.ToBase64String(requestContentHash);
        //}


        IRestResponse response = client.Execute(request);
        //return response;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}