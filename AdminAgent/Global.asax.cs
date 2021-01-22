using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AdminAgent
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.  
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.  
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 500:
                        // Server error.  
                        routeData.Values.Add("action", "HttpError500");
                        break;
                    // Here you can handle Views to other error codes.  
                    // I choose a General error template    
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
            }
            // Pass exception details to the target error View.  
            routeData.Values.Add("error", exception.Message);
            // Clear the error on server.  
            Server.ClearError();
            // Call target Controller and pass the routeData.  
            IController errorController = new Controllers.ErrorController();
            errorController.Execute(new RequestContext(
           new HttpContextWrapper(Context), routeData));
        }
    }
}
