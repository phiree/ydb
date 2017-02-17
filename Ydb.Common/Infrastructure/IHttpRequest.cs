using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Infrastructure
{
   public interface IHttpRequest
    {
        string CreateHttpRequest(string url, string type, NameValueCollection paras);
        string CreateHttpRequest(string url);
         string CreateHttpRequest(string url, string type, NameValueCollection paras, Encoding code);
          string CreateHttpRequestPostXml(string url, string requestXml, string certName);

 
        string CreateHttpRequest(string url, string type, string values, string authorizationSecret);
         string CreateHttpRequestPostXml(string url, string parasXml, WebHeaderCollection headers);


    }
}
