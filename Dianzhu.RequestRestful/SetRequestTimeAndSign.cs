using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dianzhu.RequestRestful
{
    public class SetRequestTimeAndSign : ISetRequestParams
    {
        /// <summary>
        /// 根据请求信息设置请求参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appName"></param>
        /// <param name="appKey"></param>
        /// <param name="strHost"></param>
        /// <returns></returns>
        public RequestParams SetParamByRequestInfo(HttpContext context, string appName, string appKey, string strHost)
        {
            RequestParams rp = new RequestParams();
            if (string.IsNullOrEmpty(appName.Trim()))
            {
                throw new Exception("appName不能为空!");
            }
            if (string.IsNullOrEmpty(appKey.Trim()))
            {
                throw new Exception("appKey不能为空!");
            }
            try
            {
                string strData = "";
                using (var reader = new System.IO.StreamReader(context.Request.InputStream))
                {
                    String xmlData = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(xmlData))
                    {
                        throw new Exception("没有请求参数!");
                    }
                    strData = xmlData;
                }
                Dictionary<string, string> allowedApps = new Dictionary<string, string>();
                Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(strData);
                string apiurl = jo["apiurl"].ToString();
                if (apiurl == "")
                {
                    throw new Exception("请求的接口路径(apiurl)不能为空!");
                }
                string strRequestUrl = strHost + apiurl;
                string requestUri = System.Web.HttpUtility.UrlEncode(strRequestUrl.ToLower());
                string strRequestContent = jo["content"].ToString();//context.Request.Form["content"] ?? "";
                string strRequestToken = jo["token"].ToString();//context.Request.Form["token"] ?? "";
                string requestTimeStamp = SetCommon.SetTimeStamp();
                string requestContentBase64String = SetCommon.SetContentMD5(strRequestContent);
                string requestSignatureBase64String = SetCommon.SetSign(appName, appKey, strRequestToken, requestContentBase64String, requestTimeStamp, requestUri);
                
                rp.stamp_TIMES = requestTimeStamp;
                rp.sign = requestSignatureBase64String;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rp;
        }
    }
}
