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
                if (param.method == "1" && (param.url.Contains("api/v1/authorization") || param.url.Contains("api/v1/customers") || param.url.Contains("api/v1/merchants") || param.url.Contains("api/v1/customer3rds")))
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
    }
}
