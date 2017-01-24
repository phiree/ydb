using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ydb.Push.XMPush
{
    public class XMResponse
    {
        /*
        {"result":"error","trace_id":"Xcm04b25474860887543eZ","code":21301,"description":"认证失败" ,"reason":"Invalid application secret.",} 
        {"result":"ok","trace_id":"Xcm34b86474860873282Xz","    code":0,"   "description":"成功","data":{"id":"acm34b86474860873288It"}, info":"Received push messages for 1 ALIAS"}
        */
        public string result { get; set; }
        public string trace_id { get; set; }
        public int code { get; set; }
        public string description { get; set; }
    }
    public class XMResponseSuc
    {
        public XMResponseSuc_Data data { get; set; }
        public string info { get; set; }
    }
    public class XMResponseSuc_Data
    {
        public string id { get; set; }
    }

    public class XMResponseFail
    {
        public string reason { get; set; }
    }

}
