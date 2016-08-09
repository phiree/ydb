using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;


namespace Dianzhu.Web.RestfulApi
{
    public partial class Startup
    {
        /// <summary>
        /// 配置路由
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //ConfigureOAuth(app);
            WebApiConfig.Register(config);
            Bootstrap.Boot();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);//to wire up ASP.NET Web API to our Owin server pipeline.
            
        }

        /// <summary>
        /// 配置OAuth授权服务器
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            //OAuthAuthorizationServerOptions OauthServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/token"),//token路径设置
            //    AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(7200),//token过期时间设置
            //    Provider = new AuthorizationServerProvider(),
            //    RefreshTokenProvider = new RefreshTokenProvider()
            //};
            //app.UseOAuthAuthorizationServer(OauthServerOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
            
        }
    }
}