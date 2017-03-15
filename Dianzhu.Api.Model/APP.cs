using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class AppObj
    {
        public string userId { get; set; }
        public string appName { get; set; }
        public string appUUID { get; set; }
        public string appToken { get; set; }
    }

    public class ReqDataAPP001001
    {
        public AppObj appObj { get; set; }
        public string mark { get; set; }
    }

    public class ReqDataAPP001002
    {
        public string appUUID { get; set; }
    }
}
