using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace Dianzhu.Test.DianzhuRestfulApi
{
    public abstract class ApiTestBase
    {
        public abstract string GetBaseAddress();

        protected TResult InvokeGetRequest<TResult>(string api)
        {
            using (var invoker = CreateMessageInvoker())
            {
                using (var cts = new CancellationTokenSource())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, GetBaseAddress() + api);
                    using (HttpResponseMessage response = invoker.SendAsync(request, cts.Token).Result)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<TResult>(result);
                    }
                }
            }
        }

        protected TResult InvokePostRequest<TResult, TArguemnt>(string api, TArguemnt arg)
        {
            var invoker = CreateMessageInvoker();
            using (var cts = new CancellationTokenSource())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, GetBaseAddress() + api);
                request.Content = new ObjectContent<TArguemnt>(arg, new JsonMediaTypeFormatter());
                using (HttpResponseMessage response = invoker.SendAsync(request, cts.Token).Result)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TResult>(result);
                }
            }
        }

        private HttpMessageInvoker CreateMessageInvoker()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            var server = new HttpServer(config);
            var messageInvoker = new HttpMessageInvoker(server);
            return messageInvoker;
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }// action = "DefaultAction",
            );

        }
    }
}
