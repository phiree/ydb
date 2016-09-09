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
                rp.url = strHost + apiurl;
                rp.content = jo["content"].ToString().Replace("\r", string.Empty);//context.Request.Form["content"] ?? "";
                rp.token = jo["token"].ToString();//context.Request.Form["token"] ?? "";
                rp = SetCommon.SetParams(appName, appKey, rp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rp;
        }
    }
}
