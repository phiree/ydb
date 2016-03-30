using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace PHSuit
{
    public class HttpHelper
    {
        public static string CreateHttpRequest(string url, string type, NameValueCollection paras)
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

        public static string CreateHttpRequestPostXml(string url, string parasXml)
        {
            var responseString = string.Empty;
            using (var wb = new WebClient())
            {
                wb.Encoding = Encoding.UTF8;

                responseString = wb.UploadString(url, "POST", parasXml);
            }

            return responseString;

        }
        
    }
}
