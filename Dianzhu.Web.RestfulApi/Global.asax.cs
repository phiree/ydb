using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net;
using System.Net.Http;

namespace Dianzhu.Web.RestfulApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 在应用程序启动时运行的代码
            PHSuit.Logging.Config("Dianzhu.Web.RestfulApi");

            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.Result");
            log.Info("Info(request.Url):" + Context.Request.Url);
            log.Info("Info(request.Method):" + Context.Request.HttpMethod);
            log.Info("Info(response.StatusCode):" + Context.Response.StatusCode);
            log.Info("Info(response.StatusCode):" + Context.Response.StatusDescription);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.LastError");
            Exception ex = Server.GetLastError();
            if (ex.InnerException != null)
            {
                log.Error("InnerException(Global.LastError):" + ex.InnerException.Message);
            }
            log.Error("Error(Global.LastError):" + ex.Message);
        }
    }
}
