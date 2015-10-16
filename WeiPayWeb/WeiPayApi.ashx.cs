using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiPayWeb
{
    /// <summary>
    /// WeiPayApi 的摘要说明
    /// </summary>
    public class WeiPayApi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public struct MatchOrder  
        {
            public string BagId;           //包ID
            public string UserEmail;    //用户Email
            public string MatchUidOrder;   //当前排队位置
        }
    }
}