using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
namespace Dianzhu.DemoClient
{
    
    public class GlobalViables
    {
        public static readonly string ServerName = System.Configuration.ConfigurationManager.AppSettings["server"];  //home'pc: "20141220-pc";
        public static XmppClientConnection XMPPConnection = null;
        public static string APIBaseURL = System.Configuration.ConfigurationManager.AppSettings["APIBaseURL"];
        public static string MediaUploadUrl = System.Configuration.ConfigurationManager.AppSettings["MediaUploadUrl"];
        public static string MediaGetUrl = System.Configuration.ConfigurationManager.AppSettings["MediaGetUrl"];
        static GlobalViables()
        {
            XMPPConnection = new XmppClientConnection(ServerName);
            XMPPConnection.Resource = "YDB_DemoClient";
            XMPPConnection.AutoResolveConnectServer = false;
        }
       
    }
}
