using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;


namespace Dianzhu.RequestRestful
{
    public class RequestRestful: IRequestRestful
    {
        /// <summary>
        /// 请求RestfulApi
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RequestResponse RequestRestfulApi(RequestParams param)
        {
            RequestResponse responseRest = new RequestResponse();
            try
            {
                //RequestParams param = setParams.SetParamByRequestInfo(context, appName, appKey,strHost);
                var client = new RestClient(param.url);
                var request = new RestRequest((Method)int.Parse(param.method));
                //认证时是否加入token
                if (SetCommon.CheckRoute(param.url, ((Method)int.Parse(param.method)).ToString()))
                {
                }
                else
                {
                    if (string.IsNullOrEmpty(param.token))
                    {
                        responseRest.code = false;
                        responseRest.msg = "该接口的请求必须输入token值！";
                        return responseRest;
                    }
                }
                request.AddHeader("token", param.token);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("appname", param.appName);
                request.AddHeader("stamp_times", param.stamp_TIMES);
                request.AddHeader("sign", param.sign);
                if (!string.IsNullOrEmpty(param.content.Trim()))
                {
                    request.AddParameter("application/json", param.content, ParameterType.RequestBody);
                }
                IRestResponse response = client.Execute(request);
                if ((int)response.StatusCode == 200)
                {
                    responseRest.code = true;
                    responseRest.msg = "Restful接口访问成功！";
                }
                else
                {
                    responseRest.code = false;
                    responseRest.msg = "Restful接口访问失败！";
                }
                responseRest.data = response.Content;
            }
            catch (Exception ex)
            {
                responseRest.code = false;
                responseRest.msg = ex.Message;
            }
            return responseRest;
        }

        /// <summary>
        /// 请求用户认证
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public RequestResponse RequestRestfulApiForAuthenticated(string serviceUrl, string loginName, string password)
        {
            //用户认证
            RequestParams rp = new RequestParams();
            rp.method = "1";
            rp.url = serviceUrl.TrimEnd('/') + "/api/v1/authorization";
            rp.content = "{\n\"loginName\":\"" + loginName + "\",\n\"password\":\"" + password + "\"\n}";
            rp = SetCommon.SetParams("ABc907a34381Cdyj8g3ed1F90ac8f82h3b", "2bdKTgh9fjr93gh34f7gnc4w1ZoZJfb9ATKrzCZ1a3A=", rp);
            IRequestRestful req = new Dianzhu.RequestRestful.RequestRestful();
            RequestResponse res = req.RequestRestfulApi(rp);
            if (res.code)
            {
                Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(res.data);
                res.data = jo["token"].ToString();
            }
            return res;
        }

        /// <summary>
        /// 请求RestfulApi获取UserCity
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="userId"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public RequestResponse RequestRestfulApiForUserCity(string serviceUrl,string userId,string userToken)
        {
            //用户认证
            RequestParams rp = new RequestParams();
            rp.method = "0";
            rp.url = serviceUrl.TrimEnd('/') + "/api/v1/Customers/"+ userId;
            rp.content = "";
            rp.token = userToken;
            rp = SetCommon.SetParams("ABc907a34381Cdyj8g3ed1F90ac8f82h3b", "2bdKTgh9fjr93gh34f7gnc4w1ZoZJfb9ATKrzCZ1a3A=", rp);
            IRequestRestful req = new Dianzhu.RequestRestful.RequestRestful();
            RequestResponse res = req.RequestRestfulApi(rp);
            if (res.code)
            {
                Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(res.data);
                res.data = jo["UserCity"].ToString();
            }
            return res;
        }
    }
}
