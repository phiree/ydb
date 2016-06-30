using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        }
    }
}
