using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Ydb.Common.Infrastructure;
using System.Web;
using System.Net;

namespace Ydb.Infrastructure
{  
    public class HttpRequestImpl : IHttpRequest
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.HttpHelper");
        public   string CreateHttpRequest(string url, string type, NameValueCollection paras)
        {
            var responseString = string.Empty;
            using (var wb = new WebClient())
            {
                wb.Encoding = Encoding.UTF8;
                switch (type.ToLower())
                {
                    case "get":
                        responseString = wb.DownloadString(url);
                        break;
                    case "post":
                        byte[] response = wb.UploadValues(url, "POST", paras);
                        responseString = Encoding.UTF8.GetString(response);
                        break;
                    default:
                        throw new Exception("Unsupported method");
                }
            }
            return responseString;
        }

    }
}
