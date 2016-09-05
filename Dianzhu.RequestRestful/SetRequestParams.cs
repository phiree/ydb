using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dianzhu.RequestRestful
{
    public class SetRequestParams: ISetRequestParams
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
                //GET = 0,
                //POST = 1,
                //PUT = 2,
                //DELETE = 3,
                //HEAD = 4,
                //OPTIONS = 5,
                //PATCH = 6,
                //MERGE = 7
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
                string intMethod = jo["method"].ToString();//context.Request.Form["method"] ?? "";
                if (intMethod == "")
                {
                    throw new Exception("请求方式(method)不能为空!");
                }
                if (intMethod != "0" && intMethod != "1" && intMethod != "2" && intMethod != "3" && intMethod != "6")
                {
                    throw new Exception("请求方式(method)错误!");
                    //    switch (intMethod.ToUpper())
                    //    {
                    //        case "GET":
                    //            intMethod = "0";
                    //            break;
                    //        case "POST":
                    //            intMethod = "1";
                    //            break;
                    //        case "PUT":
                    //            intMethod = "2";
                    //            break;
                    //        case "DELETE":
                    //            intMethod = "3";
                    //            break;
                    //        case "PATCH":
                    //            intMethod = "6";
                    //            break;
                    //        default:
                    //            throw new Exception("请求方式(method)错误!");
                    //    }
                }
                rp.url = strHost + apiurl;
                rp.method = intMethod;
                rp.content = jo["content"].ToString();//context.Request.Form["content"] ?? "";
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
