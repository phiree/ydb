using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class ReqDataOFP001001
    {
        public string jid { get; set; }
        public string status { get; set; }
        public string ipaddress { get; set; }
    }

    public class ReqDataSYS001001
    {
        public string id { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public string body { get; set; }
        public string ext { get; set; }
        public string orderId { get; set; }
        public string msgObj_url { get; set; }
        public string msgObj_type { get; set; }
        public string from_resource { get; set; }
        //public MsgObj msgObj { get; set; }
    }
    public class MsgObj
    {
        public string url { get; set; }
        public string type { get; set; }
    }
}
