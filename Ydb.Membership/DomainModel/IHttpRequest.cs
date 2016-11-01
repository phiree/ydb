using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel
{
   public interface IHttpRequest
    {
        string CreateHttpRequest(string url, string type, NameValueCollection paras);
    }
}
