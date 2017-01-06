using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Infrastructure
{
   public interface IHttpRequest
    {
        string CreateHttpRequest(string url, string type, NameValueCollection paras);
        string CreateHttpRequest(string url, string type, string values, string authorizationSecret);
    }
}
