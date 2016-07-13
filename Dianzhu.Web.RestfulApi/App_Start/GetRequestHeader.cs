using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.ApplicationService;
using System.Net.Http;
using System.Configuration;

//System.Net.Http  System.Web.Http;

namespace Dianzhu.Web.RestfulApi
{
    public class GetRequestHeader
    {
        public static common_Trait_Headers GetTraitHeaders()
        {
            common_Trait_Headers headers = new common_Trait_Headers();
            HttpRequest req = HttpContext.Current.Request;
            headers.appName = req.Headers.GetValues("appName").FirstOrDefault();
            headers.token = req.Headers.GetValues("token").FirstOrDefault();
            //headers.sign = req.Headers.GetValues("sign").FirstOrDefault();
            headers.stamp_TIMES = req.Headers.GetValues("stamp_TIMES").FirstOrDefault();
            MySectionCollection mysection = (MySectionCollection)ConfigurationManager.GetSection("MySectionCollection");
            //string apiKey = mysection.KeyValues[headers.appName].Value;
            headers.apiKey = mysection.KeyValues[headers.appName].Value;
            return headers;
        }
    }
}