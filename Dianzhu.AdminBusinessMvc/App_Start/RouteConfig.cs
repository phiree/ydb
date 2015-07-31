using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dianzhu.AdminBusinessMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
              name: "thumbnail",
              url: "thumbnail/{imagename}/{width}/{height}",
              defaults: new { controller = "Thumbnail", action = "Index", width = "width", height = "height" }
          );
            routes.MapRoute(
              name: "imagehandler",
              url: "imagehandler/{businessId}/{imageType}",
              defaults: new { controller = "ImageHandler", action = "Upload", businessId = "businessId", imageType = "imageType" }
          );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            
        }
    }
}