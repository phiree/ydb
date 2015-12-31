using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
namespace Dianzhu.DemoClient
{
    
    public class GlobalViables
    {
        public static readonly string ServerName = "ydban.cn";  //home'pc: "20141220-pc";
        public static XmppClientConnection XMPPConnection = null;
        //home pc: http://localhost/DianzhuApi.ashx;
        //server  "http://ydban.cn:8037/DianzhuApi.ashx";
        public static string APIBaseURL = "http://ydban.cn:8037/DianzhuApi.ashx";
        public static string MediaUploadUrl = "http://ydban.cn:8038/UploadFile.ashx";
        //public static string MediaGetUrl = "GetFile.ashx";
        public static string MediaGetUrl = "http://ydban.cn:8038/GetFile.ashx?filename=";
        static GlobalViables()
        {
            XMPPConnection = new XmppClientConnection(ServerName);
            XMPPConnection.Resource = "YDB_DemoClient";
            XMPPConnection.AutoResolveConnectServer = false;
        }
       
    }
}
