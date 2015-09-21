using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
namespace Dianzhu.DemoClient
{
    
    public class GlobalViables
    {
        public static readonly string ServerName = "192.168.1.140";  //home'pc: "20141220-pc";
        public static XmppClientConnection XMPPConnection = null;
        public static string APIBaseURL = "http://192.168.1.140:805/DianzhuApi.ashx";//home pc: http://localhost/DianzhuApi.ashx;

        static GlobalViables()
        {
            XMPPConnection = new XmppClientConnection(ServerName);
            XMPPConnection.AutoResolveConnectServer = false;
        }
       
    }
}
